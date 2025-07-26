Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Inventor

Public Class mainUserForm


    Private Sub BindingSource1_CurrentChanged(sender As Object, e As EventArgs) Handles BindingSource1.CurrentChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPrecisionOfPart.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    'When Compute Button is pressed
    Private Sub ButtonCompute_Click(sender As Object, e As EventArgs) Handles ButtonCompute.Click
        'this is code to initiate the draft analyssi to veiw the overhangs of a model from a user selected face, i dont know how to get the computer to interperet the data
        'Dim BaseFace As Object = GetBaseFace()
        'Dim invdoc As Document
        'invdoc = g_inventorApplication.ActiveDocument
        'Dim dranaly As DraftAnalyses = invdoc.AnalysisManager.DraftAnalyses.Add(0, 45, BaseFace)
        'Dim draftdata = dranaly.AttributeSets()


#Region "dims for user input"
        'Dims from Machine Selection
        Dim MEXAVA As Boolean = CheckBoxMaterialExtrusion.CheckState
        Dim MJTAVA As Boolean = CheckBoxMaterialJetting.CheckState
        Dim BJTAVA As Boolean = CheckBoxBinderJetting.CheckState
        Dim VPPAVA As Boolean = CheckBoxVatPhotopolymerisation.CheckState
        Dim PBFAVA As Boolean = CheckBoxPowederBedFusion.CheckState
        Dim DEDAVA As Boolean = CheckBoxDirectedEnergyDeposition.CheckState
        Dim IMFP As Boolean = CheckBoxIMFP.CheckState
        'dims from attribute input
        Dim Mat As String = ComboBoxMaterial.Text
        Dim UserPOP As String = ComboBoxPrecisionOfPart.Text
        Dim UserLTS As String = ComboBoxLeadTimeSignificance.Text
        Dim UserPPE As String = ComboBoxPostProcessingEffort.Text
        Dim UserQOP As String = ComboBoxVolumeOfProduction.Text
        Dim UserIPP As String = ComboBoxIntendedUseOfPart.Text
#End Region
#Region "Weightings"
        Dim POP As Double
        Dim PPE As Double
        Dim OVH As Double
        Dim IUP As Double
        Dim COP As Double
        Dim IMF As Double
        Dim QOP As Double
        Dim LTS As Double
        Dim COM As Double
        Dim IMP As Double
#End Region
#Region "Material Extrusion Weighting"
        Dim MEXPOP As Double
        Dim MEXPPE As Double
        Dim MEXOVH As Double
        Dim MEXIUP As Double
#End Region
#Region "Material Jetting Weighting"
        Dim MJTPOP As Double
        Dim MJTPPE As Double
        Dim MJTOVH As Double
        Dim MJTIUP As Double
#End Region
#Region "Binder Jetting Weighting"
        Dim BJTPOP As Double
        Dim BJTPPE As Double
        Dim BJTOVH As Double
        Dim BJTIUP As Double
#End Region
#Region "Vat Photopolymerisation Weighting"
        Dim VPPPOP As Double
        Dim VPPPPE As Double
        Dim VPPOVH As Double
        Dim VPPIUP As Double
#End Region
#Region "Powder Bed Fusion Weighting"
        Dim PBFPOP As Double
        Dim PBFPPE As Double
        Dim PBFOVH As Double
        Dim PBFIUP As Double
#End Region
#Region "Directed Enery Deposition Weighting"
        Dim DEDPOP As Double
        Dim DEDPPE As Double
        Dim DEDOVH As Double
        Dim DEDIUP As Double
#End Region
#Region "Avaiable Technology Detection"
        Dim ISMEXAVA As Integer
        If MEXAVA = True Then
            ISMEXAVA = 1
        Else
            ISMEXAVA = -1000
        End If
        Dim ISMJTAVA As Integer
        If MJTAVA = True Then
            ISMJTAVA = 1
        Else
            ISMJTAVA = -1000
        End If
        Dim ISBJTAVA As Integer
        If BJTAVA = True Then
            ISBJTAVA = 1
        Else
            ISBJTAVA = -1000
        End If
        Dim ISVPPAVA As Integer
        If VPPAVA = True Then
            ISVPPAVA = 1
        Else
            ISVPPAVA = -1000
        End If
        Dim ISPBFAVA As Integer
        If PBFAVA = True Then
            ISPBFAVA = 1
        Else
            ISPBFAVA = -1000
        End If
        Dim ISDEDAVA As Integer
        If DEDAVA = True Then
            ISDEDAVA = 1
        Else
            ISDEDAVA = -1000
        End If
#End Region
#Region "Usable Technology Detection"
        Dim MATMEX As Integer
        Dim MATVPP As Integer
        Dim MATPBF As Integer
        Dim MATDED As Integer
        Dim MATMJT As Integer
        Dim MATBJT As Integer
        If Mat = "Plastic" Then
            MATMEX = 1
            MATVPP = 1
            MATPBF = 1
            MATDED = -1000
            MATMJT = 1
            MATBJT = 1
        ElseIf Mat = "Metal" Then
            MATMEX = -1000
            MATVPP = -1000
            MATPBF = 1
            MATDED = 1
            MATMJT = -1000
            MATBJT = 1
        Else
            MATMEX = -1000
            MATVPP = -1000
            MATPBF = -1000
            MATDED = -1000
            MATMJT = -1000
            MATBJT = 1
        End If
#End Region
#Region "User input to numerical value"
        Dim NumPOP As Integer
        If UserPOP = "High" Then
            NumPOP = 1
        ElseIf UserPOP = "Medium" Then
            NumPOP = 3
        Else
            NumPOP = 5
        End If
        Dim NumPPE As Integer
        If UserPPE = "High" Then
            NumPPE = 1
        ElseIf UserPPE = "Medium" Then
            NumPPE = 3
        Else
            NumPPE = 5
        End If
        Dim NumLTS As Integer
        If UserLTS = "High" Then
            NumLTS = 1
        ElseIf UserLTS = "Medium" Then
            NumLTS = 3
        Else
            NumLTS = 5
        End If
        Dim NumQOP As Integer
        If UserQOP = "High Volume Production" Then
            NumQOP = 1
        ElseIf UserQOP = "Low Volume Production" Then
            NumQOP = 3
        Else
            NumQOP = 5
        End If
        Dim PartUse As Integer
        If UserIPP = "Mass Production" Then
            PartUse = 1
        ElseIf UserIPP = "Prototype" Then
            PartUse = 3
        Else
            PartUse = 5
        End If
#End Region
#Region "Calculate complexity"
        Dim NumCOM As Double
        NumCOM = GetPartComplexity()
#End Region
#Region "Impossible Machining Features Numerical Value"
        Dim NumIMFP As Integer
        If IMFP = "True" Then
            NumIMFP = 5
        Else
            NumIMFP = 1
        End If
#End Region
#Region "Baseline Score Calculation"
        Dim BLS As Double
        BLS = NumCOM * COM + NumIMFP * IMP + NumQOP * QOP + NumLTS * LTS
#End Region
#Region "Machine Score"
        Dim MEXSCORE As Double
        MEXSCORE = MEXPOP * POP + MEXPPE * PPE + MEXOVH * OVH + MEXIUP * IUP + ISMEXAVA + MATMEX
        Dim MJTSCORE As Double
        MJTSCORE = MJTPOP * POP + MJTPPE * PPE + MJTOVH * OVH + MJTIUP * IUP + ISMJTAVA + MATMJT
        Dim BJTSCORE As Double
        BJTSCORE = BJTPOP * POP + BJTPPE * PPE + BJTOVH * OVH + BJTIUP * IUP + ISBJTAVA + MATBJT
        Dim VPPSCORE As Double
        VPPSCORE = VPPPOP * POP + VPPPPE * PPE + VPPOVH * OVH + VPPIUP * IUP + ISVPPAVA + MATVPP
        Dim PBFSCORE As Double
        PBFSCORE = PBFPOP * POP + PBFPPE * PPE + PBFOVH * OVH + PBFIUP * IUP + ISPBFAVA + MATPBF
        Dim DEDSCORE As Double
        DEDSCORE = DEDPOP * POP + DEDPPE * PPE + DEDOVH * OVH + DEDIUP * IUP + ISDEDAVA + MATDED
#End Region
#Region "Selecting Best Machine Score"
        Dim MachineScore As Double
        MachineScore = MEXSCORE
        If MJTSCORE > MachineScore Then
            MachineScore = MJTSCORE
        ElseIf BJTSCORE > MachineScore Then
            MachineScore = BJTSCORE
        ElseIf VPPSCORE > MachineScore Then
            MachineScore = VPPSCORE
        ElseIf PBFSCORE > MachineScore Then
            MachineScore = PBFSCORE
        ElseIf DEDSCORE > MachineScore Then
            MachineScore = DEDSCORE
        End If
#End Region
#Region "Calcualte Final Score"
        Dim FinalScore As Double
        FinalScore = BLS + MachineScore
#End Region
#Region "Mark Final Score Against Thresholds"
        Dim RED As Double
        Dim GREEN As Double
        If FinalScore < RED Then

        End If
#End Region
    End Sub


End Class