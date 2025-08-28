Imports Inventor

'Helper class used for any geometrical related calculations and analysis required. 
'Used for Overhang, Part complexity, etc in an automated manner (untested)
'TryCast usually means we only read the active part document the user is currently viewing.
Public Class GeometricalHelper
    Private _app As Inventor.Application

    'constructor essentially passing the Inventor application object to the class
    'that's how we can access the properties of the part
    Public Sub New(app As Inventor.Application)
        _app = app
    End Sub

    'basic function to obtain the volume of the part (in cm)
    Public Function GetVolume() As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.") 'plan to replace with Error handler 
        Dim compDef = doc.ComponentDefinition
        Return compDef.MassProperties.Volume
    End Function

    'basic function to obtain the bounding box dimensions of the part (in cm)
    Public Function GetBoundingBox() As (Double, Double, Double)
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.")
        Dim rangeBox = doc.ComponentDefinition.RangeBox
        Dim length = Math.Abs(rangeBox.MaxPoint.X - rangeBox.MinPoint.X)
        Dim width = Math.Abs(rangeBox.MaxPoint.Y - rangeBox.MinPoint.Y)
        Dim height = Math.Abs(rangeBox.MaxPoint.Z - rangeBox.MinPoint.Z)
        Return (length, width, height)
    End Function

    'function to obtain the bounding box volume of the part (in cm^3)
    'Public Function GetBoundingBoxVolume() As Double
    '    Dim (length, width, height) = GetBoundingBox()
    '    Return length * width * height
    'End Function

    'function to calculate the surface area of the part (in cm^2)
    Public Function GetSurfaceArea() As Double
        Dim doc As PartDocument = TryCast(_app.ActiveDocument, PartDocument)
        If doc Is Nothing Then Throw New InvalidOperationException("No part document is active.")
        Dim compDef = doc.ComponentDefinition
        Return compDef.MassProperties.SurfaceArea
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

    'function to calculate part complexity (based on surface area to volume ratio)
    Public Function CalculatePartComplexity() As Double
        Dim volume As Double = GetVolume()
        Dim surfaceArea As Double = GetSurfaceArea()
        If volume = 0 Then Return 0
        Return surfaceArea / volume ' Higher ratio indicates higher complexity
    End Function
End Class