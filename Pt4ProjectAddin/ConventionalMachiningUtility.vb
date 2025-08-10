Imports Inventor
Imports Microsoft.VisualBasic

' A helper class to detect conventional manufacturing features that may be useful in assessing a low score for
' a given part to be evaluated for additive manufacturing suitability. Ideally, a boolean is likely to be used for each 
' feature to be used for scoring as well as a comment to the user that explains why conventional machining may be more 
' viable for the evaluated part.

'brainstorming some features to check:
' roational symmetry - good for lathes and turning machines
' external features - surfaces good for tool access by a 3-axis cnc?
' internal features - cavities and voids possibly bad for a 3-axis cnc?
' some sort of function looking for weird splines, unsuitable for lathing?
' maybe create bounding box and check how much material removed and see if worth it?

Public Class ConventionalMachiningUtility
    'input the part, function then goes through all checks
    Private _inventorApp As Inventor.Application
    Private _partDoc As PartDocument

    Public Sub New(inventorApp As Inventor.Application, partDoc As PartDocument)
        _inventorApp = inventorApp
        _partDoc = partDoc
    End Sub

    Public Function CheckAllFeatures() As ConventionalChecks
        Dim result As New ConventionalChecks()

        Try
            Dim partDef As PartComponentDefinition = _partDoc.ComponentDefinition

            result.RotationalSymmetryPresent = CheckRotationalSymmetry(partDef)
            result.IsValid = True

        Catch ex As Exception
            result.ErrorMessage = ex.Message
            result.IsValid = False
        End Try
        Return result
    End Function
    'Function detecting rotational symmetry
    'very basic function, does not check for complexity, only cylindrical shapes.
    'flaws include not considering splines (like a chess piece)
    Public Function CheckRotationalSymmetry(partDef As PartComponentDefinition) As Boolean
        Try
            'creating a bounding box of the part
            Dim Bbox As Box = partDef.RangeBox
            Dim lengthX As Double = Bbox.MaxPoint.X - Bbox.MinPoint.X
            Dim lengthY As Double = Bbox.MaxPoint.Y - Bbox.MinPoint.Y
            Dim lengthZ As Double = Bbox.MaxPoint.Z - Bbox.MinPoint.Z

            'sorts the dimensions from smallest to biggest
            Dim dimensions() As Double = {lengthX, lengthY, lengthZ}
            Array.Sort(dimensions)

            ' making the assumption that a reasonable cylinder would have a 1:2 ratio radius to height
            ' huge flaw for short height and large diameter (like a rectangular beam)
            Dim isElongated As Boolean = dimensions(2) > 2 * dimensions(1)

            ' Count cylindrical surfaces - needs a sufficient amount to be lathe worthy
            Dim cylindricalSurfaces As Integer = 0
            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    If face.SurfaceType = SurfaceTypeEnum.kCylinderSurface Then
                        cylindricalSurfaces += 1
                    End If
                Next
            Next

            ' only returns true when part is cylindrical and has sufficient cylindrical features
            Return isElongated And cylindricalSurfaces >= 2


        Catch
            Return False
        End Try
    End Function

End Class


'creating a separate class containing the booleans of each check that has been called
' further improvement will contain a method to calculate and assign score depending on number/type of checks?
Public Class ConventionalChecks
    Public Property RotationalSymmetryPresent As Boolean = False
    Public Property ExternalAccessibilityPresent As Boolean = False
    Public Property MinimalMaterialRemovalPresent As Boolean = False
    Public Property UndercutsPresent As Boolean = False
    Public Property IsValid As Boolean = False  'flag to check something going wrong - debugging purpose
    Public Property ErrorMessage As String = ""

End Class
