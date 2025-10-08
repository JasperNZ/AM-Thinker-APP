Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Inventor

Public Class mainUserForm
    'later used for looping and creating AM profiles
    Private ProfileFactory As New Dictionary(Of String, Func(Of AMProfile))
    Private _highlightSet As HighlightSet

    Private Sub ClearHighlights()
        Try
            If _highlightSet IsNot Nothing Then
                _highlightSet.Clear()
                _highlightSet = Nothing
            End If
        Catch
        End Try
    End Sub

    Private Sub HighlightFaces(faces As List(Of Face))
        If faces Is Nothing OrElse faces.Count = 0 Then Exit Sub

        ClearHighlights() ' remove old ones first

        _highlightSet = g_inventorApplication.ActiveDocument.HighlightSets.Add()
        _highlightSet.Color = g_inventorApplication.TransientObjects.CreateColor(255, 0, 0) ' red

        For Each f As Face In faces
            Try
                _highlightSet.AddItem(f)
            Catch
            End Try
        Next
    End Sub

    Public Sub New()
        InitializeComponent()
        InitializeProfileFactory()
    End Sub

    Public Sub InitializeProfileFactory()
        ' Metal
        ProfileFactory.Add("Metal|MEX", Function() New MetalMEX())
        ProfileFactory.Add("Metal|MJT", Function() New MetalMJT())
        ProfileFactory.Add("Metal|BJT", Function() New MetalBJT())
        ProfileFactory.Add("Metal|VPP", Function() New MetalVPP())
        ProfileFactory.Add("Metal|PBF", Function() New MetalPBF())
        ProfileFactory.Add("Metal|DED", Function() New MetalDED())

        ' Plastic
        ProfileFactory.Add("Plastic|MEX", Function() New PlasticMEX())
        ProfileFactory.Add("Plastic|MJT", Function() New PlasticMJT())
        ProfileFactory.Add("Plastic|BJT", Function() New PlasticBJT())
        ProfileFactory.Add("Plastic|VPP", Function() New PlasticVPP())
        ProfileFactory.Add("Plastic|PBF", Function() New PlasticPBF())
        ProfileFactory.Add("Plastic|DED", Function() New PlasticDED())

        ' Ceramic
        ProfileFactory.Add("Ceramic|MEX", Function() New CeramicMEX())
        ProfileFactory.Add("Ceramic|MJT", Function() New CeramicMJT())
        ProfileFactory.Add("Ceramic|BJT", Function() New CeramicBJT())
        ProfileFactory.Add("Ceramic|VPP", Function() New CeramicVPP())
        ProfileFactory.Add("Ceramic|PBF", Function() New CeramicPBF())
        ProfileFactory.Add("Ceramic|DED", Function() New CeramicDED())
    End Sub

    'When Compute Button is pressed
    'Function computes the various parameters of the object and applies the AM analysis given the users input, once the Compute Button is pressed. 
    Private Sub ButtonCompute_Click(sender As Object, e As EventArgs) Handles ButtonCompute.Click
        'preliminary checks forcing user to input all necessary information before computing
        If Not ErrorHandler.ValidateSelections(Me) Then
            Return ' Stop if validation fails
        End If

        'attempting my code - essentially a way of getting ready for if-else statements using a loop.
        Dim techMap As New Dictionary(Of String, CheckBox) From {
            {"MEX", CheckBoxMaterialExtrusion},
            {"MJT", CheckBoxMaterialJetting},
            {"BJT", CheckBoxBinderJetting},
            {"VPP", CheckBoxVatPhotopolymerisation},
            {"PBF", CheckBoxPowederBedFusion},
            {"DED", CheckBoxDirectedEnergyDeposition}
        }

        ' Build selected profiles list
        Dim selectedProfiles As New List(Of AMProfile)
        Dim selectedMaterial As String = ComboBoxMaterial.Text

        For Each tech In techMap.Keys
            Dim key As String = selectedMaterial & "|" & tech
            If techMap(tech).Checked AndAlso ProfileFactory.ContainsKey(key) Then
                selectedProfiles.Add(ProfileFactory(key).Invoke())
            End If
        Next

        ' Loop through all selected AM profiles
        For Each profile In selectedProfiles
            profile.AdjustWeightsForPurpose(ComboBoxIntendedUseOfPart.Text)
        Next

        'getting the geometrical helper
        Dim GeoChecker As New GeometricalHelper(g_inventorApplication)

        'testing out overhang face selection
        Dim doc = TryCast(g_inventorApplication.ActiveDocument, PartDocument)
        If doc.SelectSet.Count = 0 Then
            MessageBox.Show("Please select a single face in Inventor and try again.")
            Return
        End If

        Dim selSet = doc.SelectSet
        If selSet.Count <> 1 OrElse Not TypeOf selSet.Item(1) Is Face Then
            MessageBox.Show("Please select exactly one planar face as the reference surface before measuring overhang.")
            Exit Sub
        End If


        Dim referenceFace As Face = CType(selSet.Item(1), Face)

        If referenceFace.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then
            MessageBox.Show("Please select a flat planar face — curved surfaces cannot be used as the build reference.")
            Exit Sub
        End If
        Dim overhangFaces As List(Of Face) = Nothing
        Dim overhangArea = GeoChecker.CalculateOverhangArea(referenceFace, 45.0, overhangFaces)
        'Dim overhangArea = GeoChecker.CalculateOverhangAreaSimple(45.0, overhangFaces)
        HighlightFaces(overhangFaces)
        MessageBox.Show($"Estimated overhang area (deg > 45): {overhangArea:F3} (cm²)")

        'testing summary class
        Dim geo As New GeometrySummary()
        geo.Volume = GeoChecker.GetVolume()
        geo.SurfaceArea = GeoChecker.GetSurfaceArea()
        geo.BoundingBoxVolume = GeoChecker.GetBoundingBoxVolume()
        geo.ComplexityRatio = GeoChecker.CalculatePartComplexity()
        geo.OverhangArea = overhangArea



        'preparing to pass in user inputs to each classes' CalculateScore function
        Dim categoricalInputs As New Dictionary(Of String, String) From {
            {"Precision", ComboBoxPrecisionOfPart.Text},
            {"LeadTime", ComboBoxLeadTime.Text},
            {"PostProcessing", ComboBoxPostProcessingEffort.Text},
            {"Volume", ComboBoxVolumeOfProduction.Text}
        }
        Dim numericInputs As New Dictionary(Of String, Double) From {
            {"Complexity", GeoChecker.CalculatePartComplexity()},     ' i think this is the function??
            {"Overhang", 0}   'idk we can have a function called GetOverhangComplexity()
        }
        Dim hasImpossibleFeatures As Boolean = CheckBoxIMFP.Checked


        'loop aggregates scores, keeping biggest value and tracking the profile. 
        'I don't know how to handle multiple profiles with the same score... (maybe a list of tied profiles and present them all? if >1 loop length?)
        Dim criteria As New ManufacturingCriteria(categoricalInputs, numericInputs, hasImpossibleFeatures)
        Dim bestScore As Double = Double.MinValue
        Dim bestProfile As AMProfile = Nothing

        For Each profile In selectedProfiles
            Dim score = profile.CalculateScore(criteria)
            If score > bestScore Then
                bestScore = score
                bestProfile = profile
            End If
        Next

        'for testing purposes - needs finality and cases for multiple solutions suggested TODO
        'MsgBox("Best option: " & bestProfile.Technology & " (" & bestProfile.Material & ") with score: " & bestScore)
        Dim scoredProfiles As New List(Of ScoredProfile)

        For Each profile In selectedProfiles
            Dim score = profile.CalculateScore(criteria)
            scoredProfiles.Add(New ScoredProfile With {
                .Technology = profile.Technology,
                .Material = profile.Material,
                .Score = score
            })
        Next

        ' Show custom results form
        Dim resultsForm As New SummaryForm(scoredProfiles, geo)
        resultsForm.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("The Post Processing Calculator is still in the works - stay tuned!")
    End Sub

    Private Sub MainUserForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ClearHighlights()
    End Sub
End Class