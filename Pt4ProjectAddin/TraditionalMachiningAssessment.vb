Imports System.Collections.Generic
Imports System.Linq
Imports Inventor

Public Class TraditionalMachiningAssessmentImports
    Private ReadOnly _inventorApp As Inventor.Application
    Private ReadOnly _partDoc As PartDocument

    ''' <summary>
    ''' Constructor - pass in Inventor application and the STEP file document
    ''' </summary>
    Public Sub New(inventorApp As Inventor.Application, partDoc As PartDocument)
        _inventorApp = inventorApp
        _partDoc = partDoc
    End Sub

    ''' <summary>
    ''' Returns all boolean checks as a simple result object
    ''' </summary>
    Public Function CheckAllFeatures() As ConventionalChecks
        Dim result As New ConventionalChecks()

        Try
            Dim partDef As PartComponentDefinition = _partDoc.ComponentDefinition

            result.HasRotationalSymmetry = CheckRotationalSymmetry(partDef)
            result.HasSimpleSurfaces = CheckSimpleSurfaces(partDef)
            result.HasExternalAccessibility = CheckExternalAccessibility(partDef)
            result.HasSimpleGeometry = CheckSimpleGeometry(partDef)
            result.HasMinimalMaterialRemoval = CheckMaterialRemoval(partDef)
            result.HasNoUndercuts = CheckNoUndercuts(partDef)

            result.IsValid = True

        Catch ex As Exception
            result.ErrorMessage = ex.Message
            result.IsValid = False
        End Try

        Return result
    End Function

    ''' <summary>
    ''' TRUE if part appears to have rotational symmetry (good for lathe)
    ''' </summary>
    Public Function CheckRotationalSymmetry(partDef As PartComponentDefinition) As Boolean
        Try
            ' Check bounding box proportions
            Dim bbox As Box = partDef.RangeBox
            Dim lengthX As Double = bbox.MaxPoint.X - bbox.MinPoint.X
            Dim lengthY As Double = bbox.MaxPoint.Y - bbox.MinPoint.Y
            Dim lengthZ As Double = bbox.MaxPoint.Z - bbox.MinPoint.Z

            Dim dimensions() As Double = {lengthX, lengthY, lengthZ}
            Array.Sort(dimensions)

            ' One dimension much larger = potentially cylindrical
            Dim isElongated As Boolean = dimensions(2) > 2 * dimensions(1)

            ' Count cylindrical surfaces
            Dim cylindricalSurfaces As Integer = 0
            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    If face.SurfaceType = SurfaceTypeEnum.kCylinderSurface Then
                        cylindricalSurfaces += 1
                    End If
                Next
            Next

            ' Good rotational symmetry if elongated AND has cylindrical surfaces
            Return isElongated And cylindricalSurfaces >= 2

        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' TRUE if part has mostly simple surfaces (planes, cylinders)
    ''' </summary>
    Public Function CheckSimpleSurfaces(partDef As PartComponentDefinition) As Boolean
        Try
            Dim totalFaces As Integer = 0
            Dim simpleFaces As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    totalFaces += 1

                    ' Count simple surface types
                    If face.SurfaceType = SurfaceTypeEnum.kPlaneSurface Or
                       face.SurfaceType = SurfaceTypeEnum.kCylinderSurface Then
                        simpleFaces += 1
                    End If
                Next
            Next

            ' TRUE if at least 70% of surfaces are simple
            'not sure if this is a fair assumption
            If totalFaces > 0 Then
                Return (simpleFaces / totalFaces) >= 0.7
            Else
                Return False
            End If

        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' TRUE if part geometry is externally accessible (no deep internal cavities)
    ''' </summary>
    Public Function CheckExternalAccessibility(partDef As PartComponentDefinition) As Boolean
        Try
            Dim tg As TransientGeometry = partDef.Document.Application.TransientGeometry

            ' Candidate tool directions
            Dim toolDirs As New List(Of Vector)
            Dim dirs As Double(,) = {{1, 0, 0}, {-1, 0, 0}, {0, 1, 0}, {0, -1, 0}, {0, 0, 1}, {0, 0, -1}}
            For i = 0 To dirs.GetLength(0) - 1
                Dim v = tg.CreateVector(dirs(i, 0), dirs(i, 1), dirs(i, 2))
                v.Normalize()
                toolDirs.Add(v)
            Next

            Dim totalFaces As Integer = 0
            Dim inaccessibleFaces As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    totalFaces += 1
                    Try
                        ' Get a point on the face
                        Dim pt As Point = face.PointOnFace

                        ' Convert to array
                        Dim ptArr As Double() = {pt.X, pt.Y, pt.Z}

                        ' Get the normal at that point
                        Dim normalArr(2) As Double
                        face.Evaluator.GetNormalAtPoint(ptArr, normalArr)

                        ' Convert back to vector
                        Dim faceNormal As Vector = tg.CreateVector(normalArr(0), normalArr(1), normalArr(2))
                        faceNormal.Normalize()

                        ' Check if within 30° of any tool direction
                        Dim accessible As Boolean = toolDirs.Any(Function(d) _
                        Math.Acos(Math.Min(1, Math.Max(-1, Math.Abs(faceNormal.DotProduct(d))))) < (30 * Math.PI / 180))

                        If Not accessible Then
                            inaccessibleFaces += 1
                        End If
                    Catch
                        inaccessibleFaces += 1
                    End Try
                Next
            Next

            If totalFaces > 0 Then
                Return (inaccessibleFaces / totalFaces) < 0.1
            Else
                Return True
            End If

        Catch
            Return False
        End Try
    End Function




    ''' <summary>
    ''' TRUE if part has simple geometric features (straight lines, circular arcs)
    ''' </summary>
    Public Function CheckSimpleGeometry(partDef As PartComponentDefinition) As Boolean
        Try
            Dim totalEdges As Integer = 0
            Dim simpleEdges As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    For Each edge As Edge In face.Edges
                        totalEdges += 1

                        ' Count simple edge types
                        If edge.GeometryType = CurveTypeEnum.kLineSegmentCurve Or
                           edge.GeometryType = CurveTypeEnum.kCircularArcCurve Then
                            simpleEdges += 1
                        End If
                    Next
                Next
            Next

            ' TRUE if at least 80% of edges are simple AND total edge count reasonable
            If totalEdges > 0 And totalEdges < 150 Then
                Return (simpleEdges / totalEdges) >= 0.8
            Else
                Return False
            End If

        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' TRUE if part requires minimal material removal (efficient machining)
    ''' </summary>
    Public Function CheckMaterialRemoval(partDef As PartComponentDefinition) As Boolean
        Try
            ' Get bounding box volume
            Dim bbox As Box = partDef.RangeBox
            Dim boundingVolume As Double = (bbox.MaxPoint.X - bbox.MinPoint.X) *
                                       (bbox.MaxPoint.Y - bbox.MinPoint.Y) *
                                       (bbox.MaxPoint.Z - bbox.MinPoint.Z)

            ' Get actual solid volume (always in cm³ from MassProperties)
            Dim solidVolume As Double = partDef.MassProperties.Volume

            ' Compute ratio of removed material
            Dim removalRatio As Double = 1 - (solidVolume / boundingVolume)

            ' TRUE if less than 30% of the bounding box is removed
            Return removalRatio < 0.3

        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' TRUE if part has no significant undercuts (tool can reach all surfaces)
    ''' </summary>
    Public Function CheckNoUndercuts(partDef As PartComponentDefinition) As Boolean
        Try
            Dim totalFaces As Integer = 0
            Dim problematicFaces As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    totalFaces += 1

                    Try
                        ' Check face normal direction
                        Dim faceNormal As UnitVector = face.NormalAtParam(0.5, 0.5)

                        ' Faces pointing mostly horizontal might be undercuts
                        If Math.Abs(faceNormal.Z) < 0.1 Then
                            problematicFaces += 1
                        End If

                    Catch
                        ' If we can't get normal, assume it's problematic
                        problematicFaces += 1
                    End Try
                Next
            Next

            ' TRUE if less than 15% of faces are potentially problematic
            If totalFaces > 0 Then
                Return (problematicFaces / totalFaces) < 0.15
            Else
                Return True
            End If

        Catch
            Return False
        End Try
    End Function

End Class

''' <summary>
''' Simple container for all the boolean check results
''' </summary>
Public Class ConventionalChecks
    Public Property HasRotationalSymmetry As Boolean = False
    Public Property HasSimpleSurfaces As Boolean = False
    Public Property HasExternalAccessibility As Boolean = False
    Public Property HasSimpleGeometry As Boolean = False
    Public Property HasMinimalMaterialRemoval As Boolean = False
    Public Property HasNoUndercuts As Boolean = False
    Public Property IsValid As Boolean = False
    Public Property ErrorMessage As String = ""
End Class