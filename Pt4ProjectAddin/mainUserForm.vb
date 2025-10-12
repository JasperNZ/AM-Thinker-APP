'------------------------------------------------------------------------------
' <summary>
'     The initial User Form the user interacts with, inputting data and settings. 
'     All calculations and information set is finalised once the Compute Button is pressed, hence treated as the "main" file.
' </summary>
' <author>Jasper Koid</author>
' <created>24-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Inventor

''' <summary>
''' Main user form for the Add-In, paired with a .Designer.vb file.
''' </summary>
Public Class MainUserForm
    Private ProfileFactory As New Dictionary(Of String, Func(Of AMProfile))
    Private _highlightSet As HighlightSet

    ' Constructor for the MainUserForm class.
    ' creates the form itself and initialises the ProfileFactory dictionary.
    Public Sub New()
        InitializeComponent()
        InitialiseProfileFactory()
    End Sub

    ''' <summary>
    ''' Initialises the ProfileFactory dictionary with AMProfile instances for each material and technology combination.
    ''' </summary>
    Public Sub InitialiseProfileFactory()
        ' Metal profiles.
        ProfileFactory.Add("Metal|MEX", Function() New MetalMEX())
        ProfileFactory.Add("Metal|MJT", Function() New MetalMJT())
        ProfileFactory.Add("Metal|BJT", Function() New MetalBJT())
        ProfileFactory.Add("Metal|VPP", Function() New MetalVPP())
        ProfileFactory.Add("Metal|PBF", Function() New MetalPBF())
        ProfileFactory.Add("Metal|DED", Function() New MetalDED())

        ' Plastic profiles.
        ProfileFactory.Add("Plastic|MEX", Function() New PlasticMEX())
        ProfileFactory.Add("Plastic|MJT", Function() New PlasticMJT())
        ProfileFactory.Add("Plastic|BJT", Function() New PlasticBJT())
        ProfileFactory.Add("Plastic|VPP", Function() New PlasticVPP())
        ProfileFactory.Add("Plastic|PBF", Function() New PlasticPBF())
        ProfileFactory.Add("Plastic|DED", Function() New PlasticDED())

        ' Ceramic profiles.
        ProfileFactory.Add("Ceramic|MEX", Function() New CeramicMEX())
        ProfileFactory.Add("Ceramic|MJT", Function() New CeramicMJT())
        ProfileFactory.Add("Ceramic|BJT", Function() New CeramicBJT())
        ProfileFactory.Add("Ceramic|VPP", Function() New CeramicVPP())
        ProfileFactory.Add("Ceramic|PBF", Function() New CeramicPBF())
        ProfileFactory.Add("Ceramic|DED", Function() New CeramicDED())
    End Sub

    ' Function computes the various parameters of the object and applies the AM analysis given the users input, once the Compute Button is pressed. 
    Private Sub ButtonCompute_Click(sender As Object, e As EventArgs) Handles ButtonCompute.Click
        ' Preliminary checks forcing user to input all necessary information before computing.
        If Not ErrorHandler.ValidateSelections(Me) Then
            Return ' Stop if validation fails
        End If

        ' Generates a mapping of technology codes to their corresponding checkboxes.
        Dim techMap As New Dictionary(Of String, CheckBox) From {
            {"MEX", CheckBoxMaterialExtrusion},
            {"MJT", CheckBoxMaterialJetting},
            {"BJT", CheckBoxBinderJetting},
            {"VPP", CheckBoxVatPhotopolymerisation},
            {"PBF", CheckBoxPowederBedFusion},
            {"DED", CheckBoxDirectedEnergyDeposition}
        }

        ' Generate AM profiles based on user input and using ProfileFactory.
        Dim selectedProfiles As New List(Of AMProfile)
        Dim selectedMaterial As String = ComboBoxMaterial.Text
        For Each tech In techMap.Keys
            Dim key As String = selectedMaterial & "|" & tech
            If techMap(tech).Checked AndAlso ProfileFactory.ContainsKey(key) Then
                selectedProfiles.Add(ProfileFactory(key).Invoke())
            End If
        Next

        ' Loop through all selected AM profiles and adjust weights based on application purpose.
        For Each profile In selectedProfiles
            profile.AdjustWeightsForPurpose(ComboBoxIntendedApplication.Text)
        Next

        ' Geometrical analysis section.
        Dim geoHelper As New GeometricalHelper(g_inventorApplication)
        Dim doc = TryCast(g_inventorApplication.ActiveDocument, PartDocument)
        Dim selSet = doc.SelectSet

        ' Face selection validation (if more checks, should make geometry error handler class for this).
        If doc.SelectSet.Count = 0 Then
            MessageBox.Show("Please select a single face in Inventor and try again.", "No Face Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If selSet.Count <> 1 OrElse TypeOf selSet.Item(1) IsNot Face Then
            MessageBox.Show("Please select exactly one planar face as the reference surface before measuring overhang.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim referenceFace As Face = CType(selSet.Item(1), Face)
        If referenceFace.SurfaceType <> SurfaceTypeEnum.kPlaneSurface Then
            MessageBox.Show("Please select a flat planar face — curved surfaces cannot be used as the build reference.", "Non-Planar Surface Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Passing Geometrical data to GeometrySummary class for easy access when passing to Summary Form.
        Dim overhangFaces As List(Of Face) = Nothing
        Dim geoSummary As New GeometrySummary With {
            .Volume = geoHelper.GetVolume(),
            .SurfaceArea = geoHelper.GetSurfaceArea(),
            .BoundingBoxVolume = geoHelper.GetBoundingBoxVolume(),
            .ComplexityRatio = geoHelper.CalculatePartComplexity(),
            .OverhangArea = geoHelper.CalculateOverhangArea(referenceFace, 45.0, overhangFaces) ' 45 degrees as standard, but consider future implementation of allowing user to input angle.
            }
        geoSummary.OverhangRatio = geoHelper.CalculateOverhangRatio(geoSummary.SurfaceArea, geoSummary.OverhangArea)

        ' Highlight overhang faces if the checkbox is checked.
        If CheckBoxHighlight.Checked Then HighlightFaces(overhangFaces)

        ' Conventional machining checks section, then passed to class suited for easy access in Summary Form.
        Dim machiningAssessment As New TraditionalManufacturingAssessment(g_inventorApplication, doc)
        Dim convChecks As TraditionalChecks = machiningAssessment.CheckAllFeatures()

        ' Assembling all user inputs.
        Dim categoricalInputs As New Dictionary(Of String, String) From {
            {"Precision", ComboBoxPrecisionOfPart.Text},
            {"LeadTime", ComboBoxLeadTime.Text},
            {"PostProcessing", ComboBoxPostProcessingEffort.Text},
            {"Volume", ComboBoxVolumeOfProduction.Text}
        }
        Dim numericInputs As New Dictionary(Of String, Double) From {
            {"Complexity", geoSummary.ComplexityRatio},
            {"Overhang", geoSummary.OverhangRatio}
        }
        Dim hasImpossibleFeatures As Boolean = CheckBoxNonMachinableFeaturesPresent.Checked


        ' Aggregates inputs into helper class for easy passing to CalculateScore function.
        Dim criteria As New ManufacturingCriteria(categoricalInputs, numericInputs, hasImpossibleFeatures)
        Dim scoredProfiles As New List(Of ScoredProfile)

        ' Calculate scores for each selected profile.
        For Each profile In selectedProfiles
            Dim score = profile.CalculateScore(criteria)
            scoredProfiles.Add(New ScoredProfile With {
                .Technology = profile.Technology,
                .Material = profile.Material,
                .Score = score
            })
        Next

        ' Show custom results form.
        Dim resultsForm As New SummaryForm(scoredProfiles, geoSummary, convChecks)
        resultsForm.ShowDialog()
    End Sub

    ' TODO: Post Processing Calculator still in the works - placeholder button for now.
    Private Sub ButtonPostProcess_Click(sender As Object, e As EventArgs) Handles ButtonPostProcess.Click
        MsgBox("The Post Processing Calculator is still in the works - stay tuned!")
    End Sub

    ' Function clears highlights when the MainUserForm is closed.
    Private Sub MainUserForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ClearHighlights()
    End Sub

    ' Function toggles visibility of bottom tool tip based on state of CheckboxOverhangHighlight.
    Private Sub CheckBoxOverhangHighlight_CheckedChanged(sender As Object, e As EventArgs) _
    Handles CheckBoxHighlight.CheckedChanged
        LabelInstructions.Visible = Not CheckBoxHighlight.Checked
    End Sub

    ' Function removes any existing highlights in the Inventor document.
    Private Sub ClearHighlights()
        Try
            If _highlightSet IsNot Nothing Then
                _highlightSet.Clear()
                _highlightSet = Nothing
            End If
        Catch
        End Try
    End Sub

    ' Function highlights the given faces in the Inventor document.
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

End Class