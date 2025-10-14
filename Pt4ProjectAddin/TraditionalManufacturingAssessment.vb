'------------------------------------------------------------------------------
' <summary>
'	Class assess the part for traditional machining suitability based on geometric features (milling, turning, 3-axis CNC, etc).
'   Additional aspect to consider Overmoulding, but not implemented yet.
'   Significant drawbacks are the abritrary thresholds, which may not be universally applicable.
' </summary>
' <author>Jasper Koid</author>
' <created>26-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports System.Linq
Imports Inventor

Public Class TraditionalManufacturingAssessment
    Private ReadOnly _inventorApp As Inventor.Application
    Private ReadOnly _partDoc As PartDocument


    ' Constructor - pass in Inventor application and the STEP file document.
    Public Sub New(inventorApp As Inventor.Application, partDoc As PartDocument)
        _inventorApp = inventorApp
        _partDoc = partDoc
    End Sub

    ' Returns all boolean checks as a simple result object.
    Public Function CheckAllFeatures() As TraditionalChecks
        Dim result As New TraditionalChecks()

        Try
            Dim partDef As PartComponentDefinition = _partDoc.ComponentDefinition

            result.HasRotationalSymmetry = CheckRotationalSymmetry(partDef)
            result.HasSimpleSurfaces = CheckSimpleSurfaces(partDef)
            result.HasExternalAccessibility = CheckExternalAccessibility(partDef)
            result.HasSimpleGeometry = CheckSimpleGeometry(partDef)
            result.HasMinimalMaterialRemoval = CheckMaterialRemoval(partDef)
            result.HasNoUndercuts = CheckNoUndercuts(partDef)
            result.HasMinimumWallThickness = CheckMinimumThickness(partDef)
            result.HasUniformWallThickness = CheckUniformThickness(partDef)
            result.HasDraftAngles = CheckDraftAngles(partDef)
            result.HasAcceptableAspectRatios = CheckAspectRatios(partDef)
            result.HasNoSharpCorners = CheckSharpCorners(partDef)

            result.IsValid = True

        Catch ex As Exception
            result.ErrorMessage = ex.Message
            result.IsValid = False
        End Try

        Return result
    End Function

    ' Function checks for rotational symmetry, suited for turning operations.
    Public Function CheckRotationalSymmetry(partDef As PartComponentDefinition) As Boolean
        Try
            ' Check bounding box proportions.
            Dim bbox As Box = partDef.RangeBox
            Dim lengthX As Double = bbox.MaxPoint.X - bbox.MinPoint.X
            Dim lengthY As Double = bbox.MaxPoint.Y - bbox.MinPoint.Y
            Dim lengthZ As Double = bbox.MaxPoint.Z - bbox.MinPoint.Z

            Dim dimensions() As Double = {lengthX, lengthY, lengthZ}
            Array.Sort(dimensions)

            ' One dimension much larger = potentially cylindrical.
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

            ' Good rotational symmetry if elongated with cylindrical surfaces.
            Return isElongated And cylindricalSurfaces >= 2

        Catch
            Return False
        End Try
    End Function

    ' Function checks for simple surfaces (planes, cylinders).
    Public Function CheckSimpleSurfaces(partDef As PartComponentDefinition) As Boolean
        Try
            Dim totalFaces As Integer = 0
            Dim simpleFaces As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    totalFaces += 1

                    ' Count simple surface types.
                    If face.SurfaceType = SurfaceTypeEnum.kPlaneSurface Or
                       face.SurfaceType = SurfaceTypeEnum.kCylinderSurface Then
                        simpleFaces += 1
                    End If
                Next
            Next

            ' threshold set at 70% simple surfaces.
            If totalFaces > 0 Then
                Return (simpleFaces / totalFaces) >= 0.7
            Else
                Return False
            End If

        Catch
            Return False
        End Try
    End Function

    ' Function checks if most surfaces are externally accessible (no deep cavities).
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
                        Dim pt As Point = face.PointOnFace
                        Dim ptArr As Double() = {pt.X, pt.Y, pt.Z}
                        Dim normalArr(2) As Double
                        face.Evaluator.GetNormalAtPoint(ptArr, normalArr)
                        Dim faceNormal As Vector = tg.CreateVector(normalArr(0), normalArr(1), normalArr(2))
                        faceNormal.Normalize()

                        ' Check if within 30° of any tool direction.
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

            ' Threshold of at least 80% accessible faces.
            If totalFaces > 0 Then
                Return (inaccessibleFaces / totalFaces) < 0.2
            Else
                Return True
            End If

        Catch
            Return False
        End Try
    End Function

    ' Function checks for simple geometry (few complex edges).
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

    ' Function checks for minimal material removal (not a heavily machined part).
    Public Function CheckMaterialRemoval(partDef As PartComponentDefinition) As Boolean
        Try
            ' Get bounding box volume.
            Dim bbox As Box = partDef.RangeBox
            Dim boundingVolume As Double = (bbox.MaxPoint.X - bbox.MinPoint.X) *
                                       (bbox.MaxPoint.Y - bbox.MinPoint.Y) *
                                       (bbox.MaxPoint.Z - bbox.MinPoint.Z)

            ' Get actual solid volume (always in cm³ from MassProperties).
            Dim solidVolume As Double = partDef.MassProperties.Volume

            ' Compute ratio of removed material
            Dim removalRatio As Double = 1 - (solidVolume / boundingVolume)

            ' TRUE if less than 50% of the bounding box is removed.
            Return removalRatio < 0.5

        Catch
            Return False
        End Try
    End Function

    ' Function checks for undercuts (faces pointing sideways).
    Public Function CheckNoUndercuts(partDef As PartComponentDefinition) As Boolean
        Try
            Dim totalFaces As Integer = 0
            Dim problematicFaces As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                For Each face As Face In body.Faces
                    totalFaces += 1

                    Try
                        ' Check face normal direction.
                        Dim faceNormal As UnitVector = face.NormalAtParam(0.5, 0.5)

                        ' Faces pointing mostly horizontal might be undercuts.
                        If Math.Abs(faceNormal.Z) < 0.1 Then
                            problematicFaces += 1
                        End If

                    Catch
                        ' If we can't get normal, assume it's problematic.
                        problematicFaces += 1
                    End Try
                Next
            Next

            ' TRUE if less than 15% of faces are potentially problematic.
            If totalFaces > 0 Then
                Return (problematicFaces / totalFaces) < 0.15
            Else
                Return True
            End If

        Catch
            Return False
        End Try
    End Function

    ' Function checks if the part has a minimum thickness reasonable for machining, 2mm is used for now.
    Public Function CheckMinimumThickness(partDef As PartComponentDefinition,
                                      Optional minThickness As Double = 0.2) As Boolean
        Try
            Dim tg As TransientGeometry = partDef.Document.Application.TransientGeometry
            Dim minDist As Double = Double.MaxValue

            For Each body As SurfaceBody In partDef.SurfaceBodies
                If Not body.IsSolid Then Continue For

                ' Loop through planar faces and find opposite parallel ones.
                For Each f1 As Face In body.Faces
                    If f1.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then Continue For
                    Dim n1 As Vector = f1.Geometry.Normal
                    n1.Normalize()

                    For Each f2 As Face In body.Faces
                        If f2 Is f1 Then Continue For
                        If f2.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then Continue For

                        Dim n2 As Vector = f2.Geometry.Normal
                        n2.Normalize()

                        ' Check if normals are opposite (parallel planes).
                        If Math.Abs(n1.DotProduct(n2) + 1) < 0.01 Then
                            ' Distance between planes = |(p2 - p1)·n1|.
                            Dim p1 As Point = f1.PointOnFace
                            Dim p2 As Point = f2.PointOnFace
                            Dim v As Vector = tg.CreateVector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z)
                            Dim dist As Double = Math.Abs(v.DotProduct(n1))
                            If dist < minDist Then minDist = dist
                        End If
                    Next
                Next
            Next

            If minDist = Double.MaxValue Then
                Return False ' No parallel planes found.
            Else
                Return minDist >= minThickness
            End If

        Catch
            Return False
        End Try
    End Function

    ' Function checks if the part has uniform thickness. Allows 0.3 deviation from the spread of min and max thickness found.
    Public Function CheckUniformThickness(partDef As PartComponentDefinition,
                                          Optional toleranceRatio As Double = 0.3) As Boolean
        Try
            Dim tg As TransientGeometry = partDef.Document.Application.TransientGeometry
            Dim minDist As Double = Double.MaxValue
            Dim maxDist As Double = 0.0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                If Not body.IsSolid Then Continue For

                For Each f1 As Face In body.Faces
                    If f1.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then Continue For
                    Dim n1 As Vector = f1.Geometry.Normal
                    n1.Normalize()

                    For Each f2 As Face In body.Faces
                        If f2 Is f1 Then Continue For
                        If f2.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then Continue For

                        Dim n2 As Vector = f2.Geometry.Normal
                        n2.Normalize()

                        If Math.Abs(n1.DotProduct(n2) + 1) < 0.01 Then
                            Dim p1 As Point = f1.PointOnFace
                            Dim p2 As Point = f2.PointOnFace
                            Dim v As Vector = tg.CreateVector(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z)
                            Dim dist As Double = Math.Abs(v.DotProduct(n1))
                            If dist < minDist Then minDist = dist
                            If dist > maxDist Then maxDist = dist
                        End If
                    Next
                Next
            Next

            If minDist = Double.MaxValue OrElse maxDist = 0 Then
                Return False ' No valid thickness found.
            Else
                ' Uniform if variation is within tolerance.
                Dim variation As Double = (maxDist - minDist) / maxDist
                Return variation <= toleranceRatio
            End If
        Catch
            Return False
        End Try
    End Function

    ' Temporary function to check draft angles are within 3 degrees for most vertical faces. Should technically use face selector.
    Public Function CheckDraftAngles(partDef As PartComponentDefinition,
                                     Optional minDraftAngleDeg As Double = 3.0) As Boolean
        Try
            Dim tg As TransientGeometry = partDef.Document.Application.TransientGeometry

            ' Assume Z axis is the right direction.
            Dim pullDir As UnitVector = tg.CreateUnitVector(0, 0, 1)

            Dim totalVerticalFaces As Integer = 0
            Dim facesWithDraft As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                If Not body.IsSolid Then Continue For

                For Each face As Face In body.Faces
                    Try
                        ' Only consider planar faces.
                        If face.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then Continue For

                        ' Get normal at a representative point.
                        Dim pt As Point = face.PointOnFace
                        Dim ptArr As Double() = {pt.X, pt.Y, pt.Z}
                        Dim normalArr(2) As Double
                        face.Evaluator.GetNormalAtPoint(ptArr, normalArr)

                        Dim faceNormal As Vector = tg.CreateVector(normalArr(0), normalArr(1), normalArr(2))
                        faceNormal.Normalize()

                        ' Angle between face normal and pull direction.
                        Dim dot As Double = Math.Abs(faceNormal.DotProduct(pullDir))
                        dot = Math.Min(1.0, Math.Max(-1.0, dot)) ' clamp
                        Dim angleFromVertical As Double = Math.Acos(dot) * 180.0 / Math.PI

                        ' Faces close to vertical (within 5–85°) need draft.
                        If angleFromVertical > 5 AndAlso angleFromVertical < 85 Then
                            totalVerticalFaces += 1

                            ' Count as having draft if angle exceeds minimum draft angle.
                            If angleFromVertical >= minDraftAngleDeg Then
                                facesWithDraft += 1
                            End If
                        End If

                    Catch
                        ' Skip problematic faces.
                        Continue For
                    End Try
                Next
            Next

            ' If no vertical faces, draft not applicable.
            If totalVerticalFaces = 0 Then
                Return True
            End If

            ' Pass if at least 70% of vertical faces have adequate draft.
            Return (facesWithDraft / totalVerticalFaces) >= 0.7

        Catch
            ' Fail‑safe.
            Return True
        End Try
    End Function

    ' Function checks aspect ratios of the part using their bounding box.
    Public Function CheckAspectRatios(partDef As PartComponentDefinition,
                                  Optional maxAspectRatio As Double = 4.0) As Boolean
        Try
            Dim bbox As Box = partDef.RangeBox
            Dim dims As New List(Of Double) From {
            bbox.MaxPoint.X - bbox.MinPoint.X,
            bbox.MaxPoint.Y - bbox.MinPoint.Y,
            bbox.MaxPoint.Z - bbox.MinPoint.Z
        }
            dims.Sort() ' Sorts by increasing size.

            ' Largest dimension / smallest dimension.
            Dim aspectRatio As Double = dims(2) / dims(0)

            ' Allow up to twice the nominal max ratio for overall part geometry.
            Return aspectRatio <= (maxAspectRatio * 2)

        Catch
            ' True if error occurs.
            Return True
        End Try
    End Function


    ' Function checks for sharp corners by evaluating edge fillet radii. Technically also good for DfAM.
    Public Function CheckSharpCorners(partDef As PartComponentDefinition,
                                  Optional minFilletRadius As Double = 0.1,
                                  Optional maxSharpFraction As Double = 0.3) As Boolean
        Try
            Dim totalEdges As Integer = 0
            Dim sharpEdges As Integer = 0

            For Each body As SurfaceBody In partDef.SurfaceBodies
                If Not body.IsSolid Then Continue For

                For Each edge As Edge In body.Edges
                    Try
                        ' Only consider edges that are not smooth (sharp transitions).
                        If Not edge.Smooth Then
                            totalEdges += 1

                            If edge.GeometryType = CurveTypeEnum.kCircularArcCurve Then
                                ' Arc edges: check radius
                                Dim arc As Arc3d = TryCast(edge.Geometry, Arc3d)
                                If arc IsNot Nothing AndAlso arc.Radius < minFilletRadius Then
                                    sharpEdges += 1
                                End If
                            Else
                                ' Non-arc edges are inherently sharp.
                                sharpEdges += 1
                            End If
                        End If
                    Catch
                        ' Skip problematic edges.
                        Continue For
                    End Try
                Next
            Next

            ' Pass if no edges, or if sharp fraction is below threshold.
            If totalEdges = 0 Then
                Return True
            Else
                Return (sharpEdges / totalEdges) < maxSharpFraction
            End If

        Catch
            Return True
        End Try
    End Function


End Class

''' <summary>
'''     Simple class to hold the results of traditional manufacturing checks.
''' </summary>
Public Class TraditionalChecks
    Public Property HasRotationalSymmetry As Boolean = False
    Public Property HasSimpleSurfaces As Boolean = False
    Public Property HasExternalAccessibility As Boolean = False
    Public Property HasSimpleGeometry As Boolean = False
    Public Property HasMinimalMaterialRemoval As Boolean = False
    Public Property HasNoUndercuts As Boolean = False
    Public Property HasMinimumWallThickness As Boolean = False
    Public Property HasUniformWallThickness As Boolean = False
    Public Property HasDraftAngles As Boolean = False
    Public Property HasAcceptableAspectRatios As Boolean = False
    Public Property HasNoSharpCorners As Boolean = False
    Public Property IsValid As Boolean = False
    Public Property ErrorMessage As String = ""
End Class

'TODO
' fix up this class
' panel glossary terms 
' panel recommendations to be appended
' fixup am profiles numbers and weights
