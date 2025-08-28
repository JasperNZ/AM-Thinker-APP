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


#Region "function to get part complexity"
    Public Function GetPartComplexity() As Double
        ' Get the active document.
        Dim invDoc As Document
        invDoc = g_inventorApplication.ActiveDocument
        ' Get the design tracking property set.
        Dim invDesignInfo As PropertySet
        invDesignInfo = invDoc.PropertySets.Item("Design Tracking Properties")
        ' Get the volume property.
        Dim invPartNumberProperty As [Property]
        invPartNumberProperty = invDesignInfo.Item("Volume")
        'Get the surface area property.
        Dim invPartAreaproperty As [Property]
        invPartAreaproperty = invDesignInfo.Item("SurfaceArea")
        'create the complexity ratio
        Dim complex As Double
        complex = CDbl(invPartAreaproperty.Value / invPartNumberProperty.Value)
        'return the complexity ratio
        Return complex
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

#End Region

#Region "get user to select face to touch build plate"
    Public Function GetBaseFace() As Object
        Dim invDoc As Document
        invDoc = g_inventorApplication.ActiveDocument
        Dim BaseFace As Object
        BaseFace = invDoc.CommandManager.Pick(SelectionFilterEnum.kAllPlanarEntities, "Pick the face for base of print")
        Return BaseFace
    End Function

#End Region
End Class