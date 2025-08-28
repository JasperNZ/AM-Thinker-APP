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
            {"IMFP", CheckBoxIMFP},
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

        Dim GeometryHelper As New GeometricalHelper(Marshal.GetActiveObject("Inventor.Application"))

        'preparing to pass in user inputs to each classes' CalculateScore function
        Dim categoricalInputs As New Dictionary(Of String, String) From {
            {"Precision", ComboBoxPrecisionOfPart.Text},
            {"LeadTime", ComboBoxLeadTimeSignificance.Text},
            {"PostProcessing", ComboBoxPostProcessingEffort.Text},
            {"Volume", ComboBoxVolumeOfProduction.Text}
        }
        Dim numericInputs As New Dictionary(Of String, Double) From {
            {"Complexity", GeometryHelper.GetPartComplexity()},     ' i think this is the function??
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

        'for testing purposes
        MsgBox("Best option: " & bestProfile.Technology & " (" & bestProfile.Material & ") with score: " & bestScore)
    End Sub




    '#Region "dims for user input"
    '        'Dims from Machine Selection
    '        Dim MEXAVA As Boolean = CheckBoxMaterialExtrusion.CheckState
    '        Dim MJTAVA As Boolean = CheckBoxMaterialJetting.CheckState
    '        Dim BJTAVA As Boolean = CheckBoxBinderJetting.CheckState
    '        Dim VPPAVA As Boolean = CheckBoxVatPhotopolymerisation.CheckState
    '        Dim PBFAVA As Boolean = CheckBoxPowederBedFusion.CheckState
    '        Dim DEDAVA As Boolean = CheckBoxDirectedEnergyDeposition.CheckState
    '        Dim IMFP As Boolean = CheckBoxIMFP.CheckState
    '        'dims from attribute input
    '        Dim Mat As String = ComboBoxMaterial.Text
    '        Dim UserPOP As String = ComboBoxPrecisionOfPart.Text
    '        Dim UserLTS As String = ComboBoxLeadTimeSignificance.Text
    '        Dim UserPPE As String = ComboBoxPostProcessingEffort.Text
    '        Dim UserQOP As String = ComboBoxVolumeOfProduction.Text
    '        Dim UserIPP As String = ComboBoxIntendedUseOfPart.Text
    '#End Region
    '#Region "Weightings"
    '        Dim POP As Double
    '        Dim PPE As Double
    '        Dim OVH As Double
    '        Dim IUP As Double
    '        Dim COP As Double
    '        Dim IMF As Double
    '        Dim QOP As Double
    '        Dim LTS As Double
    '        Dim COM As Double
    '        Dim IMP As Double
    '#End Region
    '#Region "Material Extrusion Weighting"
    '        Dim MEXPOP As Double
    '        Dim MEXPPE As Double
    '        Dim MEXOVH As Double
    '        Dim MEXIUP As Double
    '#End Region
    '#Region "Material Jetting Weighting"
    '        Dim MJTPOP As Double
    '        Dim MJTPPE As Double
    '        Dim MJTOVH As Double
    '        Dim MJTIUP As Double
    '#End Region
    '#Region "Binder Jetting Weighting"
    '        Dim BJTPOP As Double
    '        Dim BJTPPE As Double
    '        Dim BJTOVH As Double
    '        Dim BJTIUP As Double
    '#End Region
    '#Region "Vat Photopolymerisation Weighting"
    '        Dim VPPPOP As Double
    '        Dim VPPPPE As Double
    '        Dim VPPOVH As Double
    '        Dim VPPIUP As Double
    '#End Region
    '#Region "Powder Bed Fusion Weighting"
    '        Dim PBFPOP As Double
    '        Dim PBFPPE As Double
    '        Dim PBFOVH As Double
    '        Dim PBFIUP As Double
    '#End Region
    '#Region "Directed Enery Deposition Weighting"
    '        Dim DEDPOP As Double
    '        Dim DEDPPE As Double
    '        Dim DEDOVH As Double
    '        Dim DEDIUP As Double
    '#End Region
    '#Region "Avaiable Technology Detection"
    '        Dim ISMEXAVA As Integer
    '        If MEXAVA = True Then
    '            ISMEXAVA = 1
    '        Else
    '            ISMEXAVA = -1000
    '        End If
    '        Dim ISMJTAVA As Integer
    '        If MJTAVA = True Then
    '            ISMJTAVA = 1
    '        Else
    '            ISMJTAVA = -1000
    '        End If
    '        Dim ISBJTAVA As Integer
    '        If BJTAVA = True Then
    '            ISBJTAVA = 1
    '        Else
    '            ISBJTAVA = -1000
    '        End If
    '        Dim ISVPPAVA As Integer
    '        If VPPAVA = True Then
    '            ISVPPAVA = 1
    '        Else
    '            ISVPPAVA = -1000
    '        End If
    '        Dim ISPBFAVA As Integer
    '        If PBFAVA = True Then
    '            ISPBFAVA = 1
    '        Else
    '            ISPBFAVA = -1000
    '        End If
    '        Dim ISDEDAVA As Integer
    '        If DEDAVA = True Then
    '            ISDEDAVA = 1
    '        Else
    '            ISDEDAVA = -1000
    '        End If
    '#End Region
    '#Region "Usable Technology Detection"
    '        Dim MATMEX As Integer
    '        Dim MATVPP As Integer
    '        Dim MATPBF As Integer
    '        Dim MATDED As Integer
    '        Dim MATMJT As Integer
    '        Dim MATBJT As Integer
    '        If Mat = "Plastic" Then
    '            MATMEX = 1
    '            MATVPP = 1
    '            MATPBF = 1
    '            MATDED = -1000
    '            MATMJT = 1
    '            MATBJT = 1
    '        ElseIf Mat = "Metal" Then
    '            MATMEX = -1000
    '            MATVPP = -1000
    '            MATPBF = 1
    '            MATDED = 1
    '            MATMJT = -1000
    '            MATBJT = 1
    '        Else
    '            MATMEX = -1000
    '            MATVPP = -1000
    '            MATPBF = -1000
    '            MATDED = -1000
    '            MATMJT = -1000
    '            MATBJT = 1
    '        End If
    '#End Region
    '#Region "User input to numerical value"
    '        Dim NumPOP As Integer
    '        If UserPOP = "High" Then
    '            NumPOP = 1
    '        ElseIf UserPOP = "Medium" Then
    '            NumPOP = 3
    '        Else
    '            NumPOP = 5
    '        End If
    '        Dim NumPPE As Integer
    '        If UserPPE = "High" Then
    '            NumPPE = 1
    '        ElseIf UserPPE = "Medium" Then
    '            NumPPE = 3
    '        Else
    '            NumPPE = 5
    '        End If
    '        Dim NumLTS As Integer
    '        If UserLTS = "High" Then
    '            NumLTS = 1
    '        ElseIf UserLTS = "Medium" Then
    '            NumLTS = 3
    '        Else
    '            NumLTS = 5
    '        End If
    '        Dim NumQOP As Integer
    '        If UserQOP = "High Volume Production" Then
    '            NumQOP = 1
    '        ElseIf UserQOP = "Low Volume Production" Then
    '            NumQOP = 3
    '        Else
    '            NumQOP = 5
    '        End If
    '        Dim PartUse As Integer
    '        If UserIPP = "Mass Production" Then
    '            PartUse = 1
    '        ElseIf UserIPP = "Prototype" Then
    '            PartUse = 3
    '        Else
    '            PartUse = 5
    '        End If
    '#End Region
    '#Region "Calculate complexity"
    'Dim NumCOM As Double
    '        NumCOM = GetPartComplexity()
    '#End Region
    '#Region "Impossible Machining Features Numerical Value"
    '        Dim NumIMFP As Integer
    '        If IMFP = "True" Then
    '            NumIMFP = 5
    '        Else
    '            NumIMFP = 1
    '        End If
    '#End Region
    '#Region "Baseline Score Calculation"
    '        Dim BLS As Double
    '        BLS = NumCOM * COM + NumIMFP * IMP + NumQOP * QOP + NumLTS * LTS
    '#End Region
    '#Region "Machine Score"
    '        Dim MEXSCORE As Double
    '        MEXSCORE = MEXPOP * POP + MEXPPE * PPE + MEXOVH * OVH + MEXIUP * IUP + ISMEXAVA + MATMEX
    '        Dim MJTSCORE As Double
    '        MJTSCORE = MJTPOP * POP + MJTPPE * PPE + MJTOVH * OVH + MJTIUP * IUP + ISMJTAVA + MATMJT
    '        Dim BJTSCORE As Double
    '        BJTSCORE = BJTPOP * POP + BJTPPE * PPE + BJTOVH * OVH + BJTIUP * IUP + ISBJTAVA + MATBJT
    '        Dim VPPSCORE As Double
    '        VPPSCORE = VPPPOP * POP + VPPPPE * PPE + VPPOVH * OVH + VPPIUP * IUP + ISVPPAVA + MATVPP
    '        Dim PBFSCORE As Double
    '        PBFSCORE = PBFPOP * POP + PBFPPE * PPE + PBFOVH * OVH + PBFIUP * IUP + ISPBFAVA + MATPBF
    '        Dim DEDSCORE As Double
    '        DEDSCORE = DEDPOP * POP + DEDPPE * PPE + DEDOVH * OVH + DEDIUP * IUP + ISDEDAVA + MATDED
    '#End Region
    '#Region "Selecting Best Machine Score"
    '        Dim MachineScore As Double
    '        MachineScore = MEXSCORE
    '        If MJTSCORE > MachineScore Then
    '            MachineScore = MJTSCORE
    '        ElseIf BJTSCORE > MachineScore Then
    '            MachineScore = BJTSCORE
    '        ElseIf VPPSCORE > MachineScore Then
    '            MachineScore = VPPSCORE
    '        ElseIf PBFSCORE > MachineScore Then
    '            MachineScore = PBFSCORE
    '        ElseIf DEDSCORE > MachineScore Then
    '            MachineScore = DEDSCORE
    '        End If
    '#End Region
    '#Region "Calcualte Final Score"
    '        Dim FinalScore As Double
    '        FinalScore = BLS + MachineScore
    '#End Region
    '#Region "Mark Final Score Against Thresholds"
    '        Dim RED As Double
    '        Dim GREEN As Double
    '        If FinalScore < RED Then
    '            MsgBox("Part is not suitable for Additive Manufacture")
    '        ElseIf FinalScore > GREEN Then
    '            MsgBox("Part is Suitable for Additive Manufacture")
    '        Else
    '            MsgBox("Part might be suitable for Additive Manufacture after some adjustments")
    '        End If
    '#End Region
    '    End Sub


End Class