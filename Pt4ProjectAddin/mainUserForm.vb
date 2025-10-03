Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports Inventor

Public Class mainUserForm
    'later used for looping and creating AM profiles
    Private ProfileFactory As New Dictionary(Of String, Func(Of AMProfile))

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
        'extracting inventor and part (NO ASSEMBLIES!) information for analysis
        ' I have also realized that calling with no parts will crash the code... might need an error handler class
        'I am also now upset that the code after megining with the main branch now got deleted :(
        'Dim inventorApp As Inventor.Application = Marshal.GetActiveObject("Inventor.Application")
        'Dim partDoc As PartDocument = CType(inventorApp.ActiveDocument, PartDocument)
        'Dim CMAnalyser As New ConventionalMachiningUtility(inventorApp, partDoc)
        'Dim CMResults = CMAnalyser.CheckAllFeatures()

        'writingnotes on how this code works
        'checks the tick boxes of machines, and the material text (replace with dictionary and list looping using booleans)
        'declaring variables for the AM technology's respective categorical value
        'using the checkstate to create boolean variables (think booleans are 1 or 0 anyways, will research)
        'If else statements then further applying material and technology combinations
        'converting the inputted text into the categorical numerical value
        'suddenly calls complexity function to calculate the numerical value
        'calculates baseline score
        'calculates every score for each combination
        'aggregate score (we could replace with a loop)
        'calculates final score
        'then start making comments with traffic light

        'preliminary checks forcing user to input all necessary information before computing
        If Not ErrorHandler.ValidateSelections(Me) Then
            Return ' Stop if validation fails
        End If

        'attempting my code - essentially a way of getting ready for if-else statements using a loop.
        Dim techMap As New Dictionary(Of String, CheckBox) From {
            {"PBF", CheckBoxPowederBedFusion},
            {"MEX", CheckBoxMaterialExtrusion},
            {"VPP", CheckBoxVatPhotopolymerisation},
            {"DED", CheckBoxDirectedEnergyDeposition},
            {"IMFP", CheckBoxIMFP}, 'why did i put this here??? need to test removing it won't break code
            {"MJT", CheckBoxMaterialJetting},
            {"BJT", CheckBoxBinderJetting}
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
        MsgBox("Best option: " & bestProfile.Technology & " (" & bestProfile.Material & ") with score: " & bestScore)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("The Post Processing Calculator is still in the works - stay tuned!")
    End Sub


End Class