Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports Inventor

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
    Public Function CalculateOverhangArea(referenceFace As Face,
                                      overhangAngleDeg As Double,
                                      ByRef overhangFaces As List(Of Face)) As Double
        If referenceFace Is Nothing Then Throw New ArgumentNullException(NameOf(referenceFace))
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No active PartDocument.")
        Dim compDef = doc.ComponentDefinition

        ' Ensure list exists
        overhangFaces = New List(Of Face)()

        ' Ensure planar reference
        If referenceFace.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then
            Throw New InvalidOperationException("Reference face must be planar.")
        End If

        ' Determine build direction
        Dim buildDir As Inventor.Vector
        Try
            Dim refPoint = referenceFace.PointOnFace
            Dim pointArr() As Double = {refPoint.X, refPoint.Y, refPoint.Z}
            Dim guessParams() As Double = {0, 0}
            Dim maxDeviations() As Double = {0}
            Dim paramsOut() As Double = {0, 0}
            Dim solnNatures() As SolutionNatureEnum = {}
            referenceFace.Evaluator.GetParamAtPoint(pointArr, guessParams, maxDeviations, paramsOut, solnNatures)
            Dim normalArr() As Double = {0, 0, 0}
            referenceFace.Evaluator.GetNormal(paramsOut, normalArr)
            buildDir = _app.TransientGeometry.CreateVector(normalArr(0), normalArr(1), normalArr(2))
        Catch
            buildDir = _app.TransientGeometry.CreateVector(0, 0, 1)
        End Try

        buildDir.Normalize()

        Dim totalOverhangArea As Double = 0.0
        Dim thresholdRad As Double = overhangAngleDeg * Math.PI / 180.0

        ' Loop through faces
        For Each sBody As SurfaceBody In compDef.SurfaceBodies
            If Not sBody.IsSolid Then Continue For
            For Each f As Face In sBody.Faces
                Try
                    'If f.Internal OrElse Not f.SurfaceBody.IsSolid Then Continue For ' skip internal cavities

                    Dim facePoint = f.PointOnFace
                    Dim ptArr() As Double = {facePoint.X, facePoint.Y, facePoint.Z}
                    Dim guessParams() As Double = {0, 0}
                    Dim maxDeviations() As Double = {0}
                    Dim paramsOut() As Double = {0, 0}
                    Dim solnNatures() As SolutionNatureEnum = {}

                    f.Evaluator.GetParamAtPoint(ptArr, guessParams, maxDeviations, paramsOut, solnNatures)

                    Dim normalArr() As Double = {0, 0, 0}
                    f.Evaluator.GetNormal(paramsOut, normalArr)
                    Dim faceNormal = _app.TransientGeometry.CreateVector(normalArr(0), normalArr(1), normalArr(2))
                    faceNormal.Normalize()

                    Dim dot = faceNormal.DotProduct(buildDir)
                    ' Flip the normal if it’s pointing the wrong way (inward)
                    If dot < 0 Then
                        faceNormal.ScaleBy(-1)
                        dot = faceNormal.DotProduct(buildDir)
                    End If
                    dot = Math.Max(-1.0, Math.Min(1.0, dot))
                    Dim angle = Math.Acos(dot)
                    ' Only count faces facing downward (angle > 90° - threshold)
                    If angle > (Math.PI / 2 + thresholdRad) Then
                        Dim faceArea As Double = 0
                        Try
                            faceArea = f.Evaluator.Area
                        Catch
                            Dim rb = f.RangeBox
                            faceArea = Math.Abs((rb.MaxPoint.X - rb.MinPoint.X) * (rb.MaxPoint.Y - rb.MinPoint.Y))
                        End Try
                        totalOverhangArea += faceArea
                        overhangFaces.Add(f) ' record face for highlight
                    End If

                Catch
                End Try
            Next
        Next

        Return totalOverhangArea
    End Function

    'Public Function GetSurface() As Double
    '    Dim oPartDoc As PartDocument
    '    oPartDoc = g_inventorApplication.ActiveDocument

    '    Dim oPartDef As PartComponentDefinition
    '    oPartDef = oPartDoc.ComponentDefinition

    '    Dim oSurfaceBody As SurfaceBody
    '    Dim oFace As Face
    '    Dim faceCount As Long
    '    faceCount = 0
    '    Dim eval As SurfaceEvaluator
    '    Dim Baseeval As SurfaceEvaluator
    '    Baseeval = BaseFace.Evaluator
    '    Dim basecenter(1) As Double
    '    basecenter(0) = (Baseeval.ParamRangeRect.MinPoint.X + Baseeval.ParamRangeRect.MaxPoint.X) / 2
    '    basecenter(1) = (Baseeval.ParamRangeRect.MinPoint.Y + Baseeval.ParamRangeRect.MaxPoint.Y) / 2
    '    Dim Normal(2) As Double
    '    Call Baseeval.GetNormal(basecenter, Normal)
    '    Dim Basevector As UnitVector
    '    Basevector = g_inventorApplication.TransientGeometry.CreateUnitVector(Normal(0), Normal(1), Normal(2))
    '    Dim TotalArea As Double = 0
    '    Dim area As Double
    '    For Each oSurfaceBody In oPartDef.SurfaceBodies
    '        For Each oFace In oSurfaceBody.Faces
    '            eval = oFace.Evaluator
    '            Dim center(1) As Double
    '            center(0) = (eval.ParamRangeRect.MinPoint.X + eval.ParamRangeRect.MaxPoint.X) / 2
    '            center(1) = (eval.ParamRangeRect.MinPoint.Y + eval.ParamRangeRect.MaxPoint.Y) / 2
    '            Dim NormalTest(2) As Double
    '            Call eval.GetNormal(center, NormalTest)
    '            Dim Testvector As UnitVector
    '            Testvector = g_inventorApplication.TransientGeometry.CreateUnitVector(NormalTest(0), NormalTest(1), NormalTest(2))
    '            Dim angle As Double
    '            angle = Basevector.AngleTo(Testvector)
    '            If angle < 0.785 Then
    '                area = eval.Area
    '                TotalArea = TotalArea + area
    '            End If
    '            area = 0
    '        Next
    '    Next
    '    Dim basearea As Double
    '    basearea = Baseeval.Area
    '    TotalArea = TotalArea - basearea
    '    Return TotalArea

    'End Function

    'function to calculate part complexity (based on surface area to volume ratio)
    Public Function CalculatePartComplexity() As Double
        Dim volume As Double = GetVolume()
        Dim surfaceArea As Double = GetSurfaceArea()
        If volume = 0 Then Return 0 ' edge case to somehow avoid crashing by dividing by 0
        Return (surfaceArea / volume) ' Higher ratio indicates higher complexity
    End Function
End Class

Public Class GeometrySummary
    Public Property Volume As Double
    Public Property SurfaceArea As Double
    Public Property BoundingBoxVolume As Double
    Public Property ComplexityRatio As Double
    Public Property OverhangArea As Double

End Class