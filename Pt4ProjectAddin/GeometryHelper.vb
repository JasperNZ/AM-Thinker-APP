'------------------------------------------------------------------------------
' <summary>
'	Helper class used for any geometrical related calculations and analysis required. 
'   Used for Overhang ratio, Part complexity, etc in an automated manner.
' </summary>
' <author>Jasper Koid</author>
' <created>26-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports Inventor
Imports System.Text


Public Class GeometricalHelper
    Private ReadOnly _app As Inventor.Application

    ' Constructor to initialise Inventor application instance.
    Public Sub New(app As Inventor.Application)
        _app = app
    End Sub

    ' Function to obtain the volume of the part (in cm3).
    Public Function GetVolume() As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.") 'plan to replace with Error handler 
        Dim compDef = doc.ComponentDefinition
        Return compDef.MassProperties.Volume
    End Function

    ' Function to calculate the surface area of the part (in cm^2).
    Public Function GetSurfaceArea() As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.")
        Dim compDef = doc.ComponentDefinition
        Return compDef.MassProperties.Area
    End Function

    ' Function to obtain the bounding box dimensions of the part (in cm, cm, cm).
    Public Function GetBoundingBox() As Tuple(Of Double, Double, Double)
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No active PartDocument.")
        Dim compDef = doc.ComponentDefinition
        Dim box = compDef.RangeBox
        Dim length = Math.Abs(box.MaxPoint.X - box.MinPoint.X)
        Dim width = Math.Abs(box.MaxPoint.Y - box.MinPoint.Y)
        Dim height = Math.Abs(box.MaxPoint.Z - box.MinPoint.Z)
        Return New Tuple(Of Double, Double, Double)(length, width, height)
    End Function

    ' Function to obtain the bounding box volume of the part (in cm^3).
    Public Function GetBoundingBoxVolume() As Double
        Dim box = GetBoundingBox()
        Return box.Item1 * box.Item2 * box.Item3
    End Function

    ''' <summary>
    '''     Archived functions
    '''     TODO - maybe aspect ratio might be considered?
    '''     TODO - spies ratio by extending bounding box with other primitive shapes?
    '''     TODO - could be used to prevent excessive computation time, but that interferes with user freedom...
    ''' </summary>
    ''' <returns></returns>
    Public Function GetNumberOfFaces() As Integer
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Return 0
        Dim compDef = doc.ComponentDefinition
        Dim total As Integer = 0
        For Each sb As SurfaceBody In compDef.SurfaceBodies
            total += sb.Faces.Count
        Next
        Return total
    End Function

    Public Function GetNumberOfEdges() As Integer
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Return 0
        Dim compDef = doc.ComponentDefinition
        Dim total As Integer = 0
        For Each sb As SurfaceBody In compDef.SurfaceBodies
            total += sb.Edges.Count
        Next
        Return total
    End Function

    Public Function GetNumberOfSolidBodies() As Integer
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Return 0
        Return doc.ComponentDefinition.SurfaceBodies.Count
    End Function

    ''' / <summary>
    '''     Main function to calculate overhang area based on a reference face and overhang angle. 
    '''     Significant issues arise from arbitrary thresholds, lack of coplanar surfaces detected well, as well as complex curves correctly detecting segments of the face.
    ''' </summary>
    ''' <param name="referenceFace">The planar face used to determine build direction.</param>
    ''' <param name="overhangAngleDeg">The overhang angle threshold in degrees, measured from the horizontal.</param>
    ''' <param name="overhangFaces">Output list of faces identified as overhangs.</param>
    ''' <param name="enableDebug">Enable detailed debug output.</param>
    ''' <returns>Total overhang area in cm2.</returns>
    Public Function CalculateOverhangArea(referenceFace As Face,
                                          overhangAngleDeg As Double,
                                          ByRef overhangFaces As List(Of Face),
                                          Optional enableDebug As Boolean = False) As Double
        ' Always check for validity, but redundant with main form validation.
        If referenceFace Is Nothing Then
            Throw New ArgumentNullException(NameOf(referenceFace))
        End If

        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then
            Throw New InvalidOperationException("No active PartDocument.")
        End If

        Dim compDef = doc.ComponentDefinition
        overhangFaces = New List(Of Face)()

        ' Validate reference face is planar
        If referenceFace.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then
            Throw New InvalidOperationException("Reference face must be planar.")
        End If

        Dim plane As Plane = TryCast(referenceFace.Geometry, Plane)
        If plane Is Nothing Then
            Throw New InvalidOperationException("Failed to get plane geometry.")
        End If

        ' First get's the face normal, check flips it back if needed and normalizes it.
        Dim rawNormal As Vector = plane.Normal.AsVector()
        If referenceFace.IsParamReversed Then
            rawNormal.ScaleBy(-1)
        End If
        rawNormal.Normalize()

        ' Get part centroid and reference face center.
        Dim centroid As Point = compDef.MassProperties.CenterOfMass
        Dim refFaceCenter As Point = referenceFace.PointOnFace

        ' Vector pointing from reference face to centroid.
        Dim faceToCentroid = _app.TransientGeometry.CreateVector(
            centroid.X - refFaceCenter.X,
            centroid.Y - refFaceCenter.Y,
            centroid.Z - refFaceCenter.Z
        )

        ' Checks if the normalis pointing towards the centroid, an indicator of build direction.
        ' If rawNormal points away from centroid, flip it.
        Dim buildDir As Vector
        If rawNormal.DotProduct(faceToCentroid) < 0 Then
            ' Normal points away from part - flip it.
            buildDir = _app.TransientGeometry.CreateVector(-rawNormal.X, -rawNormal.Y, -rawNormal.Z)
        Else
            ' Normal points toward part - use as is.
            buildDir = rawNormal
        End If
        buildDir.Normalize()

        Dim totalOverhangArea As Double = 0.0
        Dim thresholdRad As Double = overhangAngleDeg * Math.PI / 180.0 ' Converting input angle to radians.

        ' Debugger toggled by optional input parameter as true or false.
        Dim debugInfo As New StringBuilder()
        If enableDebug Then
            debugInfo.AppendLine("=======================================")
            debugInfo.AppendLine("     OVERHANG ANALYSIS - DEBUG")
            debugInfo.AppendLine("=======================================")
            debugInfo.AppendLine()
            debugInfo.AppendLine("REFERENCE FACE:")
            debugInfo.AppendLine($"  Center: ({refFaceCenter.X:F2}, {refFaceCenter.Y:F2}, {refFaceCenter.Z:F2})")
            debugInfo.AppendLine($"  Raw Normal: ({rawNormal.X:F3}, {rawNormal.Y:F3}, {rawNormal.Z:F3})")
            debugInfo.AppendLine($"  Build Dir (UP): ({buildDir.X:F3}, {buildDir.Y:F3}, {buildDir.Z:F3})")
            debugInfo.AppendLine()
            debugInfo.AppendLine("PART INFO:")
            debugInfo.AppendLine($"  Centroid: ({centroid.X:F2}, {centroid.Y:F2}, {centroid.Z:F2})")
            debugInfo.AppendLine($"  Threshold: {overhangAngleDeg}° from horizontal")
            debugInfo.AppendLine()
            debugInfo.AppendLine("INTERPRETATION:")
            debugInfo.AppendLine($"  • Downward faces ≤{overhangAngleDeg}° from horizontal = OVERHANG")
            debugInfo.AppendLine($"  • Horizontal down (0°) = Maximum overhang")
            debugInfo.AppendLine($"  • Vertical (90°) = Self-supporting")
            debugInfo.AppendLine()
            debugInfo.AppendLine("=======================================")
            debugInfo.AppendLine()
        End If

        Dim faceIndex As Integer = 0
        Dim internalCount As Integer = 0
        Dim upwardCount As Integer = 0
        Dim tooSteepCount As Integer = 0
        Dim overhangCount As Integer = 0

        ' Process all solid bodies.
        For Each sBody As SurfaceBody In compDef.SurfaceBodies
            If Not sBody.IsSolid Then Continue For

            For Each f As Face In sBody.Faces
                faceIndex += 1

                ' Skip the reference face itself.
                If f Is referenceFace Then
                    If enableDebug Then
                        debugInfo.AppendLine($"Face {faceIndex}: REFERENCE FACE (skipped)")
                        debugInfo.AppendLine()
                    End If
                    Continue For
                End If

                Try
                    ' Get face normal at a point on the face.
                    Dim facePoint As Point = f.PointOnFace
                    Dim faceNormal As Vector = GetFaceNormalAtPoint(f, facePoint)

                    If faceNormal Is Nothing Then
                        If enableDebug Then
                            debugInfo.AppendLine($"Face {faceIndex}: Cannot get normal (skipped)")
                            debugInfo.AppendLine()
                        End If
                        Continue For
                    End If
                    faceNormal.Normalize()

                    ' Computes vector from face to centroid.
                    Dim vectorToCentroid = _app.TransientGeometry.CreateVector(
                        centroid.X - facePoint.X,
                        centroid.Y - facePoint.Y,
                        centroid.Z - facePoint.Z
                    )
                    vectorToCentroid.Normalize()
                    Dim dotToCenter = faceNormal.DotProduct(vectorToCentroid)

                    ' Computes distance from face to centroid.
                    Dim distFromCenter = Math.Sqrt(
                        Math.Pow(facePoint.X - centroid.X, 2) +
                        Math.Pow(facePoint.Y - centroid.Y, 2) +
                        Math.Pow(facePoint.Z - centroid.Z, 2)
                    )

                    ' Use more lenient threshold: 0.3 instead of 0.1
                    ' OR if face is far from centroid, likely external.
                    Dim isExternal = (dotToCenter <= 0.3) OrElse (distFromCenter > 1.0)

                    If Not isExternal Then
                        internalCount += 1
                        If enableDebug Then
                            debugInfo.AppendLine($"Face {faceIndex}: INTERNAL")
                            debugInfo.AppendLine($"  dotToCenter={dotToCenter:F3}, dist={distFromCenter:F2}")
                            debugInfo.AppendLine()
                        End If
                        Continue For
                    End If

                    ' Calculate dot product with build direction.
                    Dim dot = faceNormal.DotProduct(buildDir)
                    dot = Math.Max(-1.0, Math.Min(1.0, dot))

                    ' Overhang Logic:
                    ' dot > 0: Face points same direction as build (upward) - NOT overhang
                    ' dot < 0: Face points opposite to build (downward) - potential overhang
                    ' dot = 0: Face is perpendicular (vertical)

                    Dim isDownwardFacing = dot < -0.01 ' Small threshold to handle numerical errors

                    If Not isDownwardFacing Then
                        upwardCount += 1
                        If enableDebug Then
                            debugInfo.AppendLine($"Face {faceIndex}: UPWARD/VERTICAL (dot={dot:F3})")
                            debugInfo.AppendLine()
                        End If
                        Continue For
                    End If

                    ' For downward faces, calculate angle from horizontal.
                    ' We want: arccos(-dot) which gives angle from horizontal plane.
                    ' dot = -1 (straight down) → arccos(1) = 0° from horizontal.
                    ' dot = -0.707 (45° down) → arccos(0.707) = 45° from horizontal.
                    ' dot = 0 (vertical) → arccos(0) = 90° from horizontal.

                    Dim angleFromHorizontalRad = Math.Acos(-dot)
                    Dim angleFromHorizontalDeg = angleFromHorizontalRad * 180.0 / Math.PI

                    ' Overhang if angle ≤ threshold.
                    Dim needsSupport = angleFromHorizontalDeg <= overhangAngleDeg

                    If enableDebug Then
                        debugInfo.AppendLine($"Face {faceIndex}: DOWNWARD CANDIDATE")
                        debugInfo.AppendLine($"  Position: ({facePoint.X:F1}, {facePoint.Y:F1}, {facePoint.Z:F1})")
                        debugInfo.AppendLine($"  Normal: ({faceNormal.X:F3}, {faceNormal.Y:F3}, {faceNormal.Z:F3})")
                        debugInfo.AppendLine($"  Dot with build: {dot:F3}")
                        debugInfo.AppendLine($"  Angle from horizontal: {angleFromHorizontalDeg:F1}°")
                        debugInfo.AppendLine($"  Threshold: {overhangAngleDeg}°")
                        debugInfo.AppendLine($"  Needs support: {needsSupport}")
                    End If

                    If needsSupport Then
                        Dim faceArea As Double = GetFaceArea(f)
                        totalOverhangArea += faceArea
                        overhangFaces.Add(f)
                        overhangCount += 1

                        If enableDebug Then
                            debugInfo.AppendLine($" OVERHANG FOUND! Area: {faceArea:F2} cm²")
                        End If
                    Else
                        tooSteepCount += 1
                        If enableDebug Then
                            debugInfo.AppendLine($"self-supporting")
                        End If
                    End If

                    If enableDebug Then
                        debugInfo.AppendLine()
                    End If

                Catch ex As Exception
                    If enableDebug Then
                        debugInfo.AppendLine($"Face {faceIndex}: ERROR - {ex.Message}")
                        debugInfo.AppendLine()
                    End If
                    Continue For
                End Try
            Next
        Next

        If enableDebug Then
            debugInfo.AppendLine("=======================================")
            debugInfo.AppendLine("              SUMMARY")
            debugInfo.AppendLine("=======================================")
            debugInfo.AppendLine($"Total faces analyzed: {faceIndex}")
            debugInfo.AppendLine($"  • Internal faces: {internalCount}")
            debugInfo.AppendLine($"  • Upward/vertical faces: {upwardCount}")
            debugInfo.AppendLine($"  • Downward too steep: {tooSteepCount}")
            debugInfo.AppendLine($"  • OVERHANGS DETECTED: {overhangCount}")
            debugInfo.AppendLine()
            debugInfo.AppendLine($"Total overhang area: {totalOverhangArea:F2} cm²")
            debugInfo.AppendLine("=======================================")

            ' Show debug output
            Dim result = System.Windows.Forms.MessageBox.Show(
                debugInfo.ToString(),
                "Overhang Analysis - Detailed Results",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information)
        End If

        Return totalOverhangArea
    End Function

    ' Archived functions, tried to mitigate coplanar detection and face sampling for obtuse curves with minimal overhang.
    ' Testing function to check if two faces are coplanar within a tolerance (good for stl). It didn't work... 
    ' Overtly sensitive and difficulty in assessing for parallel faces with an offset. Also fails to detect at least 1 coplanar surface.
    Private Function CheckFacesCoplanar(f1 As Face, f2 As Face, tol As Double) As Boolean
        Dim n1 = GetFaceNormalAtPoint(f1, f1.PointOnFace)
        Dim n2 = GetFaceNormalAtPoint(f2, f2.PointOnFace)
        n1.Normalize() : n2.Normalize()
        If Math.Abs(n1.DotProduct(n2) - 1.0) > tol Then Return False

        Dim p1 = f1.PointOnFace
        Dim p2 = f2.PointOnFace
        Dim planeDiff = Math.Abs(n1.X * (p2.X - p1.X) + n1.Y * (p2.Y - p1.Y) + n1.Z * (p2.Z - p1.Z))
        Return planeDiff < tol
    End Function

    ' Get multiple sample points on a face (simple grid sampling).
    Private Function GetSamplePointsOnFace(f As Face, divisionsU As Integer, divisionsV As Integer) As List(Of Point)
        Dim points As New List(Of Point)

        ' Get UV parameter bounds
        Dim paramRange As Double() = {}
        f.Evaluator.GetParamExtents(paramRange)

        Dim uMin As Double = paramRange(0)
        Dim uMax As Double = paramRange(1)
        Dim vMin As Double = paramRange(2)
        Dim vMax As Double = paramRange(3)

        For i As Integer = 0 To divisionsU
            For j As Integer = 0 To divisionsV
                Dim u = uMin + (uMax - uMin) * i / divisionsU
                Dim v = vMin + (vMax - vMin) * j / divisionsV

                ' Inventor expects parameter array and returns coordinate array
                Dim param(1) As Double
                param(0) = u
                param(1) = v

                Dim coord(2) As Double
                f.Evaluator.GetPointAtParam(param, coord)

                Dim p As Point = f.Evaluator.Parent.SurfaceBody.Parent.ComponentDefinition.Application.TransientGeometry.CreatePoint(coord(0), coord(1), coord(2))
                points.Add(p)
            Next
        Next

        Return points
    End Function

    ' Helper function to get face normal vector at a specific point.
    Public Function GetFaceNormalAtPoint(f As Face, pt As Point) As Vector
        Try
            Dim ptArr() As Double = {pt.X, pt.Y, pt.Z}
            Dim guessParams() As Double = {0, 0}
            Dim maxDeviations() As Double = {0.001}
            Dim paramsOut() As Double = {0, 0}
            Dim solnNatures() As SolutionNatureEnum = {}

            f.Evaluator.GetParamAtPoint(ptArr, guessParams, maxDeviations,
                                        paramsOut, solnNatures)

            Dim normalArr() As Double = {0, 0, 0}
            f.Evaluator.GetNormal(paramsOut, normalArr)

            Return _app.TransientGeometry.CreateVector(
                normalArr(0), normalArr(1), normalArr(2)
            )
        Catch
            Return Nothing
        End Try
    End Function

    'Helper function to safely get face area.
    Public Function GetFaceArea(f As Face) As Double
        Try
            Return f.Evaluator.Area
        Catch
            Try
                Dim rb = f.RangeBox
                Return Math.Abs((rb.MaxPoint.X - rb.MinPoint.X) *
                               (rb.MaxPoint.Y - rb.MinPoint.Y))
            Catch
                Return 0
            End Try
        End Try
    End Function

    'Function to calculate part complexity (based on surface area to volume ratio).
    Public Function CalculatePartComplexity() As Double
        Dim volume As Double = GetVolume()
        Dim surfaceArea As Double = GetSurfaceArea()
        If volume = 0 Then Return 0 ' Edge case to somehow avoid crashing by dividing by 0.
        Return (surfaceArea / volume) ' Higher ratio indicates higher complexity
    End Function

    ' Function to calculate volume ratio (part volume to bounding box volume).
    Public Function CalculateVolumeRatio() As Double
        Dim volume As Double = GetVolume()
        Dim bbvolume As Double = GetBoundingBoxVolume()
        If volume = 0 Then Return 0
        Return (volume / bbvolume)
    End Function

    ' Function to calculate overhang ratio (overhang area to total surface area).
    Public Function CalculateOverhangRatio(totalSurfaceArea As Double, totalOverhangArea As Double) As Double
        If totalSurfaceArea <= 0 Then Return 0
        Return (totalOverhangArea / totalSurfaceArea)
    End Function
End Class

'------------------------------------------------------------------------------
' <summary>
'	Class to hold a summary of geometrical properties for easy access and transfer.
' </summary>
' <author>Jasper Koid</author>
' <created>26-AUG-2025</created>
'------------------------------------------------------------------------------
Public Class GeometrySummary
    Public Property Volume As Double
    Public Property SurfaceArea As Double
    Public Property BoundingBoxVolume As Double
    Public Property ComplexityRatio As Double
    Public Property OverhangArea As Double
    Public Property VolumeRatio As Double
    Public Property OverhangRatio As Double
End Class