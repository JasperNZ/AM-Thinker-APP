Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports Inventor
Imports System.Text

'Helper class used for any geometrical related calculations and analysis required. 
'Used for Overhang, Part complexity, etc in an automated manner (untested)
'TryCast usually means we only read the active part document the user is currently viewing.
Public Class GeometricalHelper
    Private ReadOnly _app As Inventor.Application

    'constructor essentially passing the Inventor application object to the class
    'that's how we can access the properties of the part
    Public Sub New(app As Inventor.Application)
        _app = app
    End Sub

    'basic function to obtain the volume of the part (in cm3)
    'needs testing with other units like inches to ensure consistency
    Public Function GetVolume() As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.") 'plan to replace with Error handler 
        Dim compDef = doc.ComponentDefinition
        Return compDef.MassProperties.Volume
    End Function

    'function to calculate the surface area of the part (in cm^2)
    ' it's off by a scale of 100??? scaling up to match iproperties
    Public Function GetSurfaceArea() As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.")
        Dim compDef = doc.ComponentDefinition
        Return compDef.MassProperties.Area
    End Function

    'basic function to obtain the bounding box dimensions of the part (in cm)
    'tuples are a thing in vb,net???
    ' ensures immutable parameters, avoiding accidental overwrite.
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

    'function to obtain the bounding box volume of the part (in cm^3)
    Public Function GetBoundingBoxVolume() As Double
        Dim box = GetBoundingBox()
        Return box.Item1 * box.Item2 * box.Item3
    End Function

    'Archived functions
    'maybe aspect ratio might be considered?
    'spies ratio by extending bounding box with other primitive shapes
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

    'function to calculate the overhang area of the part (in cm^2)
    'Public Function CalculateOverhangArea(overhangAngle As Double) As Double
    '    Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
    '    If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.")
    '    Dim compDef = doc.ComponentDefinition
    '    Dim totalOverhangArea As Double = 0.0
    '    ' Iterate through all faces in the part
    '    For Each face As Face In compDef.SurfaceBodies(1).Faces
    '        ' Get the normal vector of the face
    '        Dim normal As Vector = face.Evaluator.GetNormalAtPoint(face.PointOnFace)
    '        ' Calculate the angle between the face normal and the build direction (assumed to be Z-axis)
    '        Dim buildDirection As New Vector(0, 0, 1)
    '        Dim angle As Double = Math.Acos(normal.DotProduct(buildDirection) / (normal.Length * buildDirection.Length)) * (180.0 / Math.PI)
    '        ' If the angle exceeds the overhang threshold, add the face area to the total overhang area
    '        If angle > overhangAngle Then
    '            totalOverhangArea += face.Evaluator.Area
    '        End If
    '    Next
    '    Return totalOverhangArea
    'End Function

    'function considers internal overhangs, not sure how to mitigate this.

    ''' <summary>
    ''' TRULY CORRECTED: Calculates overhang area with proper angle interpretation.
    ''' The key insight: We need angle from HORIZONTAL, not vertical!
    ''' </summary>
    Public Function CalculateOverhangArea(referenceFace As Face,
                                          overhangAngleDeg As Double,
                                          ByRef overhangFaces As List(Of Face),
                                          Optional enableDebug As Boolean = False) As Double
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

        ' === CRITICAL FIX 1: Determine build direction properly ===
        ' The build direction should point AWAY from the build plate INTO the part
        ' We need to figure out which way is "up" based on the part geometry

        Dim rawNormal As Vector = plane.Normal.AsVector()
        If referenceFace.IsParamReversed Then
            rawNormal.ScaleBy(-1)
        End If
        rawNormal.Normalize()

        ' Get part centroid and reference face center
        Dim centroid As Point = compDef.MassProperties.CenterOfMass
        Dim refFaceCenter As Point = referenceFace.PointOnFace

        ' Vector from reference face to centroid
        Dim faceToCentroid = _app.TransientGeometry.CreateVector(
            centroid.X - refFaceCenter.X,
            centroid.Y - refFaceCenter.Y,
            centroid.Z - refFaceCenter.Z
        )

        ' Build direction should point toward the part (toward centroid)
        ' If rawNormal points away from centroid, flip it
        Dim buildDir As Vector
        If rawNormal.DotProduct(faceToCentroid) < 0 Then
            ' Normal points away from part - flip it
            buildDir = _app.TransientGeometry.CreateVector(-rawNormal.X, -rawNormal.Y, -rawNormal.Z)
        Else
            ' Normal points toward part - use as is
            buildDir = rawNormal
        End If
        buildDir.Normalize()

        Dim totalOverhangArea As Double = 0.0
        Dim thresholdRad As Double = overhangAngleDeg * Math.PI / 180.0

        ' Debug output
        Dim debugInfo As New StringBuilder()
        If enableDebug Then
            debugInfo.AppendLine("═══════════════════════════════════════")
            debugInfo.AppendLine("     OVERHANG ANALYSIS - DEEP DEBUG")
            debugInfo.AppendLine("═══════════════════════════════════════")
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
            debugInfo.AppendLine("───────────────────────────────────────")
            debugInfo.AppendLine()
        End If

        Dim faceIndex As Integer = 0
        Dim internalCount As Integer = 0
        Dim upwardCount As Integer = 0
        Dim tooSteepCount As Integer = 0
        Dim overhangCount As Integer = 0

        ' === CRITICAL FIX 2: Less aggressive internal face filtering ===
        ' Process all solid bodies
        For Each sBody As SurfaceBody In compDef.SurfaceBodies
            If Not sBody.IsSolid Then Continue For

            For Each f As Face In sBody.Faces
                faceIndex += 1

                ' Skip the reference face itself
                If f Is referenceFace Then
                    If enableDebug Then
                        debugInfo.AppendLine($"Face {faceIndex}: REFERENCE FACE (skipped)")
                        debugInfo.AppendLine()
                    End If
                    Continue For
                End If

                Try
                    ' Get face normal at a point on the face
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

                    ' === IMPROVED internal face detection ===
                    ' Method 1: Centroid-based (less aggressive threshold)
                    Dim vectorToCentroid = _app.TransientGeometry.CreateVector(
                        centroid.X - facePoint.X,
                        centroid.Y - facePoint.Y,
                        centroid.Z - facePoint.Z
                    )
                    vectorToCentroid.Normalize()
                    Dim dotToCenter = faceNormal.DotProduct(vectorToCentroid)

                    ' Method 2: Distance from centroid (faces far from center are likely external)
                    Dim distFromCenter = Math.Sqrt(
                        Math.Pow(facePoint.X - centroid.X, 2) +
                        Math.Pow(facePoint.Y - centroid.Y, 2) +
                        Math.Pow(facePoint.Z - centroid.Z, 2)
                    )

                    ' Use more lenient threshold: 0.3 instead of 0.1
                    ' OR if face is far from centroid, likely external
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

                    ' Calculate dot product with build direction
                    Dim dot = faceNormal.DotProduct(buildDir)
                    dot = Math.Max(-1.0, Math.Min(1.0, dot))

                    ' === CORE OVERHANG LOGIC ===
                    ' dot > 0: Face points same direction as build (upward) - NOT overhang
                    ' dot < 0: Face points opposite to build (downward) - potential overhang
                    ' dot ≈ 0: Face is perpendicular (vertical)

                    Dim isDownwardFacing = dot < -0.01 ' Small threshold to handle numerical errors

                    If Not isDownwardFacing Then
                        upwardCount += 1
                        If enableDebug Then
                            debugInfo.AppendLine($"Face {faceIndex}: UPWARD/VERTICAL (dot={dot:F3})")
                            debugInfo.AppendLine()
                        End If
                        Continue For
                    End If

                    ' For downward faces, calculate angle from horizontal
                    ' We want: arccos(-dot) which gives angle from horizontal plane
                    ' dot = -1 (straight down) → arccos(1) = 0° from horizontal
                    ' dot = -0.707 (45° down) → arccos(0.707) = 45° from horizontal  
                    ' dot = 0 (vertical) → arccos(0) = 90° from horizontal

                    Dim angleFromHorizontalRad = Math.Acos(-dot)
                    Dim angleFromHorizontalDeg = angleFromHorizontalRad * 180.0 / Math.PI

                    ' Overhang if angle ≤ threshold
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
                            debugInfo.AppendLine($"  ✓ OVERHANG! Area: {faceArea:F2} cm²")
                        End If
                    Else
                        tooSteepCount += 1
                        If enableDebug Then
                            debugInfo.AppendLine($"  ✗ Too steep (self-supporting)")
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
            debugInfo.AppendLine("═══════════════════════════════════════")
            debugInfo.AppendLine("              SUMMARY")
            debugInfo.AppendLine("═══════════════════════════════════════")
            debugInfo.AppendLine($"Total faces analyzed: {faceIndex}")
            debugInfo.AppendLine($"  • Internal faces: {internalCount}")
            debugInfo.AppendLine($"  • Upward/vertical faces: {upwardCount}")
            debugInfo.AppendLine($"  • Downward too steep: {tooSteepCount}")
            debugInfo.AppendLine($"  • OVERHANGS DETECTED: {overhangCount}")
            debugInfo.AppendLine()
            debugInfo.AppendLine($"Total overhang area: {totalOverhangArea:F2} cm²")
            debugInfo.AppendLine("═══════════════════════════════════════")

            ' Show debug output
            Dim result = System.Windows.Forms.MessageBox.Show(
                debugInfo.ToString(),
                "Overhang Analysis - Detailed Results",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information)
        End If

        Return totalOverhangArea
    End Function

    ''' <summary>
    ''' Alternative: Simple Z-axis vertical build assumption
    ''' Use this if the reference face method isn't working
    ''' </summary>
    Public Function CalculateOverhangAreaZUp(overhangAngleDeg As Double,
                                             ByRef overhangFaces As List(Of Face),
                                             Optional enableDebug As Boolean = False) As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then
            Throw New InvalidOperationException("No active PartDocument.")
        End If

        Dim compDef = doc.ComponentDefinition
        overhangFaces = New List(Of Face)()

        ' Simple: Z-axis is UP
        Dim buildDir = _app.TransientGeometry.CreateVector(0, 0, 1)
        Dim centroid As Point = compDef.MassProperties.CenterOfMass

        Dim totalOverhangArea As Double = 0.0
        Dim thresholdRad As Double = overhangAngleDeg * Math.PI / 180.0

        For Each sBody As SurfaceBody In compDef.SurfaceBodies
            If Not sBody.IsSolid Then Continue For

            For Each f As Face In sBody.Faces
                Try
                    Dim facePoint As Point = f.PointOnFace
                    Dim faceNormal As Vector = GetFaceNormalAtPoint(f, facePoint)

                    If faceNormal Is Nothing Then Continue For
                    faceNormal.Normalize()

                    ' Simple internal filter
                    Dim vectorToCentroid = _app.TransientGeometry.CreateVector(
                        centroid.X - facePoint.X,
                        centroid.Y - facePoint.Y,
                        centroid.Z - facePoint.Z
                    )
                    vectorToCentroid.Normalize()

                    If faceNormal.DotProduct(vectorToCentroid) > 0.3 Then
                        Continue For
                    End If

                    ' Overhang check
                    Dim dot = faceNormal.DotProduct(buildDir)
                    dot = Math.Max(-1.0, Math.Min(1.0, dot))

                    If dot < -0.01 Then ' Downward facing
                        Dim angleFromHorizontal = Math.Acos(-dot) * 180.0 / Math.PI

                        If angleFromHorizontal <= overhangAngleDeg Then
                            Dim faceArea As Double = GetFaceArea(f)
                            totalOverhangArea += faceArea
                            overhangFaces.Add(f)
                        End If
                    End If

                Catch
                    Continue For
                End Try
            Next
        Next

        Return totalOverhangArea
    End Function

    ''' <summary>
    ''' Helper function to get face normal vector at a specific point
    ''' </summary>
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

    ''' <summary>
    ''' Helper function to safely get face area
    ''' </summary>
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
    'function to calculate part complexity (based on surface area to volume ratio)
    Public Function CalculatePartComplexity() As Double
        Dim volume As Double = GetVolume()
        Dim surfaceArea As Double = GetSurfaceArea()
        If volume = 0 Then Return 0 ' edge case to somehow avoid crashing by dividing by 0
        Return (surfaceArea / volume) ' Higher ratio indicates higher complexity
    End Function

    Public Function CalculateVolumeRatio() As Double
        Dim volume As Double = GetVolume()
        Dim bbvolume As Double = GetBoundingBoxVolume()
        If volume = 0 Then Return 0
        Return (volume / bbvolume)
    End Function

    Public Function CalculateOverhangPercentage(totalSurfaceArea As Double, totalOverhangArea As Double) As Double
        If totalSurfaceArea <= 0 Then Return 0
        Return (totalOverhangArea / totalSurfaceArea) * 100.0
    End Function
End Class

Public Class GeometrySummary
    Public Property Volume As Double
    Public Property SurfaceArea As Double
    Public Property BoundingBoxVolume As Double
    Public Property ComplexityRatio As Double
    Public Property OverhangArea As Double
    Public Property VolumeRatio As Double
    Public Property OverhangPercentage As Double

End Class