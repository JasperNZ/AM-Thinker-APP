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
#Region "dims for user input"
        'Dims from Machine Selection
        Dim MEXT As Boolean = CheckBoxMaterialExtrusion.CheckState
        Dim MJET As Boolean = CheckBoxMaterialJetting.CheckState
        Dim BJET As Boolean = CheckBoxBinderJetting.CheckState
        Dim VPHO As Boolean = CheckBoxVatPhotopolymerisation.CheckState
        Dim PBFU As Boolean = CheckBoxPowederBedFusion.CheckState
        Dim DEDE As Boolean = CheckBoxDirectedEnergyDeposition.CheckState
        'dims from attribute input
        Dim Mat As String = ComboBoxMaterial.Text
        Dim POP As String = ComboBoxPrecisionOfPart.Text
        Dim LTS As String = ComboBoxLeadTimeSignificance.Text
        Dim PPE As String = ComboBoxPostProcessingEffort.Text
        Dim VOP As String = ComboBoxVolumeOfProduction.Text
        Dim IPOP As String = ComboBoxIntendedUseOfPart.Text
#End Region
        'comments for metal weight dims also apply for all other weight dims, these weights will vary from selected material and machine using required attributes determined by the user
#Region "metal weighting dims"
        'weight for presision of part = high
        Dim POPHM As Double = 1
        'weight for presision of part = medium
        Dim POPMM As Double = 3
        'weight for presision of part = low
        Dim POPLM As Double = 5
        'weight lead time significance = high
        Dim LTSHM As Double = 5
        'weight for lead time significance = medium
        Dim LTSMM As Double = 3
        'weight for lead time significance = low
        Dim LTSLM As Double = 1
        'weight for post process effort = high
        Dim PPEHM As Double = 1
        'weight for post process effort = medium
        Dim PPEMM As Double = 3
        'weight for post process effort = low
        Dim PPELM As Double = 5
        'weight fpr volume of production = One off peice
        Dim VOPOM As Double = 10
        'weight for volume of production = Low volume production
        Dim VOPLM As Double = 5
        'weight for volume of production = mass production
        Dim VOPHM As Double = 1
        'weight for intended purpose of part = unique part
        Dim IPOPUM As Double = 20
        'weight for intended purpose of part =  critcal spare part
        Dim IPOPCM As Double = 20
        'weight for intended purpose of part = mass production
        Dim IPOPMM As Double = 8
#End Region
#Region "plastic weighting dims"
        Dim POPHP As Double = 1
        Dim POPMP As Double = 3
        Dim POPLP As Double = 5
        Dim LTSHP As Double = 1
        Dim LTSMP As Double = 3
        Dim LTSLP As Double = 5
        Dim PPEHP As Double = 1
        Dim PPEMP As Double = 3
        Dim PPELP As Double = 5
        Dim VOPOP As Double = 10
        Dim VOPLP As Double = 5
        Dim VOPHP As Double = 1
        Dim IPOPUP As Double = 20
        Dim IPOPCP As Double = 20
        Dim IPOPMP As Double = 8
#End Region
#Region "ceramic weighting dims"
        Dim POPHC As Double = 1
        Dim POPMC As Double = 3
        Dim POPLC As Double = 5
        Dim LTSHC As Double = 1
        Dim LTSMC As Double = 3
        Dim LTSLC As Double = 5
        Dim PPEHC As Double = 1
        Dim PPEMC As Double = 3
        Dim PPELC As Double = 5
        Dim VOPOC As Double = 10
        Dim VOPLC As Double = 5
        Dim VOPHC As Double = 1
        Dim IPOPUC As Double = 20
        Dim IPOPCC As Double = 20
        Dim IPOPMC As Double = 8
#End Region
#Region "MEXT weighting dims"
        Dim POPHMEXT As Double = 1
        Dim POPMMEXT As Double = 3
        Dim POPLMEXT As Double = 5
        Dim LTSHMEXT As Double = 1
        Dim LTSMMEXT As Double = 3
        Dim LTSLMEXT As Double = 5
        Dim PPEHMEXT As Double = 1
        Dim PPEMMEXT As Double = 3
        Dim PPELMEXT As Double = 5
        Dim VOPOMEXT As Double = 10
        Dim VOPLMEXT As Double = 5
        Dim VOPHMEXT As Double = 1
        Dim IPOPUMEXT As Double = 20
        Dim IPOPCMEXT As Double = 20
        Dim IPOPMMEXT As Double = 8
#End Region
#Region "MJET weighting dims"
        Dim POPHMJET As Double = 1
        Dim POPMMJET As Double = 3
        Dim POPLMJET As Double = 5
        Dim LTSHMJET As Double = 1
        Dim LTSMMJET As Double = 3
        Dim LTSLMJET As Double = 5
        Dim PPEHMJET As Double = 1
        Dim PPEMMJET As Double = 3
        Dim PPELMJET As Double = 5
        Dim VOPOMJET As Double = 10
        Dim VOPLMJET As Double = 5
        Dim VOPHMJET As Double = 1
        Dim IPOPUMJET As Double = 20
        Dim IPOPCMJET As Double = 20
        Dim IPOPMMJET As Double = 8
#End Region
#Region "BJET weighting dims"
        Dim POPHBJET As Double = 1
        Dim POPMBJET As Double = 3
        Dim POPLBJET As Double = 5
        Dim LTSHBJET As Double = 1
        Dim LTSMBJET As Double = 3
        Dim LTSLBJET As Double = 5
        Dim PPEHBJET As Double = 1
        Dim PPEMBJET As Double = 3
        Dim PPELBJET As Double = 5
        Dim VOPOBJET As Double = 10
        Dim VOPLBJET As Double = 5
        Dim VOPHBJET As Double = 1
        Dim IPOPUBJET As Double = 20
        Dim IPOPCBJET As Double = 20
        Dim IPOPMBJET As Double = 8
#End Region
#Region "VPHO weighting dims"
        Dim POPHVPHO As Double = 1
        Dim POPMVPHO As Double = 3
        Dim POPLVPHO As Double = 5
        Dim LTSHVPHO As Double = 1
        Dim LTSMVPHO As Double = 3
        Dim LTSLVPHO As Double = 5
        Dim PPEHVPHO As Double = 1
        Dim PPEMVPHO As Double = 3
        Dim PPELVPHO As Double = 5
        Dim VOPOVPHO As Double = 10
        Dim VOPLVPHO As Double = 5
        Dim VOPHVPHO As Double = 1
        Dim IPOPUVPHO As Double = 20
        Dim IPOPCVPHO As Double = 20
        Dim IPOPMVPHO As Double = 8
#End Region
#Region "PBFU weighting dims"
        Dim POPHPBFU As Double = 1
        Dim POPMPBFU As Double = 3
        Dim POPLPBFU As Double = 5
        Dim LTSHPBFU As Double = 1
        Dim LTSMPBFU As Double = 3
        Dim LTSLPBFU As Double = 5
        Dim PPEHPBFU As Double = 1
        Dim PPEMPBFU As Double = 3
        Dim PPELPBFU As Double = 5
        Dim VOPOPBFU As Double = 10
        Dim VOPLPBFU As Double = 5
        Dim VOPHPBFU As Double = 1
        Dim IPOPUPBFU As Double = 20
        Dim IPOPCPBFU As Double = 20
        Dim IPOPMPBFU As Double = 8
#End Region
#Region "DEDE weighting dims"
        Dim POPHDEDE As Double = 1
        Dim POPMDEDE As Double = 3
        Dim POPLDEDE As Double = 5
        Dim LTSHDEDE As Double = 1
        Dim LTSMDEDE As Double = 3
        Dim LTSLDEDE As Double = 5
        Dim PPEHDEDE As Double = 1
        Dim PPEMDEDE As Double = 3
        Dim PPELDEDE As Double = 5
        Dim VOPODEDE As Double = 10
        Dim VOPLDEDE As Double = 5
        Dim VOPHDEDE As Double = 1
        Dim IPOPUDEDE As Double = 20
        Dim IPOPCDEDE As Double = 20
        Dim IPOPMDEDE As Double = 8
#End Region
        'dims for selected weightings (after reviewing user inputs)
#Region "Used material/machine dims"
        'selected weighting for material selected
        Dim RPOP As Double
        Dim RLTS As Double
        Dim RPPE As Double
        Dim RVOP As Double
        Dim RIPOP As Double
        'selected weightings for machine selection
        Dim MEXTPOP As Double
        Dim MEXTLTS As Double
        Dim MEXTPPE As Double
        Dim MEXTVOP As Double
        Dim MEXTIPOP As Double
        Dim MJETPOP As Double
        Dim MJETLTS As Double
        Dim MJETPPE As Double
        Dim MJETVOP As Double
        Dim MJETIPOP As Double
        Dim BJETPOP As Double
        Dim BJETLTS As Double
        Dim BJETPPE As Double
        Dim BJETVOP As Double
        Dim BJETIPOP As Double
        Dim VPHOPOP As Double
        Dim VPHOLTS As Double
        Dim VPHOPPE As Double
        Dim VPHOVOP As Double
        Dim VPHOIPOP As Double
        Dim PBFUPOP As Double
        Dim PBFULTS As Double
        Dim PBFUPPE As Double
        Dim PBFUVOP As Double
        Dim PBFUIPOP As Double
        Dim DEDEPOP As Double
        Dim DEDELTS As Double
        Dim DEDEPPE As Double
        Dim DEDEVOP As Double
        Dim DEDEIPOP As Double
        'Result from weighting combination
        Dim Result As Double
        Dim MEXTRESULT As Double
        Dim MJETRESULT As Double
        Dim BJETRESULT As Double
        Dim VPHORESULT As Double
        Dim PBFURESULT As Double
        Dim DEDERESULT As Double
        Dim EndResult As Double
#End Region
        'selecting whats weightings to use
#Region "selecting what material weights to use"
        'identify selected material
        If Mat = "Metal" Then
            'identify what catagory of presitions of part the user has selected
            If POP = "High" Then
                'assign the result presision of part to have the value of the selected catagory
                RPOP = POPHM
            ElseIf POP = "Medium" Then
                RPOP = POPMM
            Else
                RPOP = POPLM
            End If

            If LTS = "High" Then
                RLTS = LTSHM
            ElseIf LTS = "Medium" Then
                RLTS = LTSMM
            Else
                RLTS = LTSLM
            End If

            If PPE = "High" Then
                RPPE = PPEHM
            ElseIf PPE = "Medium" Then
                RPPE = PPEMM
            Else
                RPPE = PPELM
            End If

            If VOP = "One Off Part" Then
                RVOP = VOPOM
            ElseIf VOP = "Low Volume Production" Then
                RVOP = VOPLM
            Else
                RVOP = VOPHM
            End If

            If IPOP = "Unique Custom Made" Then
                RIPOP = IPOPUM
            ElseIf IPOP = "Critical Spare Part" Then
                RIPOP = IPOPCM
            Else
                RIPOP = IPOPMM
            End If

        ElseIf Mat = "Plastic" Then
            If POP = "High" Then
                RPOP = POPHP
            ElseIf POP = "Medium" Then
                RPOP = POPMP
            Else
                RPOP = POPLP
            End If

            If LTS = "High" Then
                RLTS = LTSHP
            ElseIf LTS = "Medium" Then
                RLTS = LTSMP
            Else
                RLTS = LTSLP
            End If

            If PPE = "High" Then
                RPPE = PPEHP
            ElseIf PPE = "Medium" Then
                RPPE = PPEMP
            Else
                RPPE = PPELP
            End If

            If VOP = "One Off Part" Then
                RVOP = VOPOP
            ElseIf VOP = "Low Volume Production" Then
                RVOP = VOPLP
            Else
                RVOP = VOPHP
            End If

            If IPOP = "Unique Custom Made" Then
                RIPOP = IPOPUP
            ElseIf IPOP = "Critical Spare Part" Then
                RIPOP = IPOPCP
            Else
                RIPOP = IPOPMP
            End If

        Else
            If POP = "High" Then
                RPOP = POPHC
            ElseIf POP = "Medium" Then
                RPOP = POPMC
            Else
                RPOP = POPLC
            End If

            If LTS = "High" Then
                RLTS = LTSHC
            ElseIf LTS = "Medium" Then
                RLTS = LTSMC
            Else
                RLTS = LTSLC
            End If

            If PPE = "High" Then
                RPPE = PPEHC
            ElseIf PPE = "Medium" Then
                RPPE = PPEMC
            Else
                RPPE = PPELC
            End If

            If VOP = "One Off Part" Then
                RVOP = VOPOC
            ElseIf VOP = "Low Volume Production" Then
                RVOP = VOPLC
            Else
                RVOP = VOPHC
            End If

            If IPOP = "Unique Custom Made" Then
                RIPOP = IPOPUC
            ElseIf IPOP = "Critical Spare Part" Then
                RIPOP = IPOPCC
            Else
                RIPOP = IPOPMC
            End If
        End If
#End Region
#Region "selectiong what machine weightings to use"
        'if MEXT was selected then assign weights to MEXT, this can be done for multiple technologies
        If MEXT = True Then
            If POP = "High" Then
                MEXTPOP = POPHMEXT
            ElseIf POP = "Medium" Then
                MEXTPOP = POPMMEXT
            Else
                MEXTPOP = POPLMEXT
            End If
            If LTS = "High" Then
                MEXTLTS = LTSHMEXT
            ElseIf LTS = "Medium" Then
                MEXTLTS = LTSMMEXT
            Else
                MEXTLTS = LTSLMEXT
            End If
            If PPE = "High" Then
                MEXTPPE = PPEHMEXT
            ElseIf PPE = "Medium" Then
                MEXTPPE = PPEMMEXT
            Else
                MEXTPPE = PPELMEXT
            End If
            If VOP = "One Off Part" Then
                MEXTVOP = VOPOMEXT
            ElseIf VOP = "Low Volume Production" Then
                MEXTVOP = VOPLMEXT
            Else
                MEXTVOP = VOPHMEXT
            End If

            If IPOP = "Unique Custom Made" Then
                MEXTIPOP = IPOPUMEXT
            ElseIf IPOP = "Critical Spare Part" Then
                MEXTIPOP = IPOPCMEXT
            Else
                MEXTIPOP = IPOPMMEXT
            End If
        End If
        'if MJET was selected then assign weights to MJET
        If MJET = True Then
            If POP = "High" Then
                MJETPOP = POPHMJET
            ElseIf POP = "Medium" Then
                MJETPOP = POPMMJET
            Else
                MJETPOP = POPLMJET
            End If
            If LTS = "High" Then
                MJETLTS = LTSHMJET
            ElseIf LTS = "Medium" Then
                MJETLTS = LTSMMJET
            Else
                MJETLTS = LTSLMJET
            End If
            If PPE = "High" Then
                MJETPPE = PPEHMJET
            ElseIf PPE = "Medium" Then
                MJETPPE = PPEMMJET
            Else
                MJETPPE = PPELMJET
            End If
            If VOP = "One Off Part" Then
                MJETVOP = VOPOMJET
            ElseIf VOP = "Low Volume Production" Then
                MJETVOP = VOPLMJET
            Else
                MJETVOP = VOPHMJET
            End If
            If IPOP = "Unique Custom Made" Then
                MJETIPOP = IPOPUMJET
            ElseIf IPOP = "Critical Spare Part" Then
                MJETIPOP = IPOPCMJET
            Else
                MJETIPOP = IPOPMMJET
            End If
        End If
        If BJET = True Then
            If POP = "High" Then
                BJETPOP = POPHBJET
            ElseIf POP = "Medium" Then
                BJETPOP = POPMBJET
            Else
                BJETPOP = POPLBJET
            End If
            If LTS = "High" Then
                BJETLTS = LTSHBJET
            ElseIf LTS = "Medium" Then
                BJETLTS = LTSMBJET
            Else
                BJETLTS = LTSLBJET
            End If
            If PPE = "High" Then
                BJETPPE = PPEHBJET
            ElseIf PPE = "Medium" Then
                BJETPPE = PPEMBJET
            Else
                BJETPPE = PPELBJET
            End If
            If VOP = "One Off Part" Then
                BJETVOP = VOPOBJET
            ElseIf VOP = "Low Volume Production" Then
                BJETVOP = VOPLBJET
            Else
                BJETVOP = VOPHBJET
            End If
            If IPOP = "Unique Custom Made" Then
                BJETIPOP = IPOPUBJET
            ElseIf IPOP = "Critical Spare Part" Then
                BJETIPOP = IPOPCBJET
            Else
                BJETIPOP = IPOPMBJET
            End If
        End If
        If VPHO = True Then
            If POP = "High" Then
                VPHOPOP = POPHVPHO
            ElseIf POP = "Medium" Then
                VPHOPOP = POPMVPHO
            Else
                VPHOPOP = POPLVPHO
            End If
            If LTS = "High" Then
                VPHOLTS = LTSHVPHO
            ElseIf LTS = "Medium" Then
                VPHOLTS = LTSMVPHO
            Else
                VPHOLTS = LTSLVPHO
            End If
            If PPE = "High" Then
                VPHOPPE = PPEHVPHO
            ElseIf PPE = "Medium" Then
                VPHOPPE = PPEMVPHO
            Else
                VPHOPPE = PPELVPHO
            End If
            If VOP = "One Off Part" Then
                VPHOVOP = VOPOVPHO
            ElseIf VOP = "Low Volume Production" Then
                VPHOVOP = VOPLVPHO
            Else
                VPHOVOP = VOPHVPHO
            End If
            If IPOP = "Unique Custom Made" Then
                VPHOIPOP = IPOPUVPHO
            ElseIf IPOP = "Critical Spare Part" Then
                VPHOIPOP = IPOPCVPHO
            Else
                VPHOIPOP = IPOPMVPHO
            End If
        End If
        If PBFU = True Then
            If POP = "High" Then
                PBFUPOP = POPHPBFU
            ElseIf POP = "Medium" Then
                PBFUPOP = POPMPBFU
            Else
                PBFUPOP = POPLPBFU
            End If
            If LTS = "High" Then
                PBFULTS = LTSHPBFU
            ElseIf LTS = "Medium" Then
                PBFULTS = LTSMPBFU
            Else
                PBFULTS = LTSLPBFU
            End If
            If PPE = "High" Then
                PBFUPPE = PPEHPBFU
            ElseIf PPE = "Medium" Then
                PBFUPPE = PPEMPBFU
            Else
                PBFUPPE = PPELPBFU
            End If
            If VOP = "One Off Part" Then
                PBFUVOP = VOPOPBFU
            ElseIf VOP = "Low Volume Production" Then
                PBFUVOP = VOPLPBFU
            Else
                PBFUVOP = VOPHPBFU
            End If
            If IPOP = "Unique Custom Made" Then
                PBFUIPOP = IPOPUPBFU
            ElseIf IPOP = "Critical Spare Part" Then
                PBFUIPOP = IPOPCPBFU
            Else
                PBFUIPOP = IPOPMPBFU
            End If
        End If
        If DEDE = True Then
            If POP = "High" Then
                DEDEPOP = POPHDEDE
            ElseIf POP = "Medium" Then
                DEDEPOP = POPMDEDE
            Else
                DEDEPOP = POPLDEDE
            End If
            If LTS = "High" Then
                DEDELTS = LTSHDEDE
            ElseIf LTS = "Medium" Then
                DEDELTS = LTSMDEDE
            Else
                DEDELTS = LTSLDEDE
            End If
            If PPE = "High" Then
                DEDEPPE = PPEHDEDE
            ElseIf PPE = "Medium" Then
                DEDEPPE = PPEMDEDE
            Else
                DEDEPPE = PPELDEDE
            End If
            If VOP = "One Off Part" Then
                DEDEVOP = VOPODEDE
            ElseIf VOP = "Low Volume Production" Then
                DEDEVOP = VOPLDEDE
            Else
                DEDEVOP = VOPHDEDE
            End If
            If IPOP = "Unique Custom Made" Then
                DEDEIPOP = IPOPUDEDE
            ElseIf IPOP = "Critical Spare Part" Then
                DEDEIPOP = IPOPCDEDE
            Else
                DEDEIPOP = IPOPMDEDE
            End If
        End If
#End Region
        'calculate the complexity from fucntion in StandardAddInServer
        Dim complexity As Double
        complexity = GetPartComplexity()

        'complexity weighting
        Dim CompM As Double = 0.4
        Dim CompP As Double = 0.4
        Dim CompC As Double = 0.4
        Dim Comp As Double
        'apply complexity value based on what material is selected
        If Mat = "Metal" Then
            Comp = CompM
        ElseIf Mat = "Plastic" Then
            Comp = CompP
        Else
            Comp = CompC
        End If
        'calculate complexity result
        Dim RComp = complexity * Comp
        'calculate attribute result
        Result = RPOP + RLTS + RPPE + RVOP + RIPOP + RComp
        'calculate individual machine results
        MEXTRESULT = Result + MEXTPOP + MEXTLTS + MEXTPPE + MEXTVOP + MEXTIPOP
        MJETRESULT = Result + MJETPOP + MJETLTS + MJETPPE + MJETVOP + MJETIPOP
        BJETRESULT = Result + BJETPOP + BJETLTS + BJETPPE + BJETVOP + BJETIPOP
        VPHORESULT = Result + VPHOPOP + VPHOLTS + VPHOPPE + VPHOVOP + VPHOIPOP
        PBFURESULT = Result + PBFUPOP + PBFULTS + PBFUPPE + PBFUVOP + PBFUIPOP
        DEDERESULT = Result + DEDEPOP + DEDELTS + DEDEPPE + DEDEVOP + DEDEIPOP
        'create a string tag for each technology
        Dim MEXTTAG As String = "Material Extrusion"
        Dim MJETTAG As String = "Material Jetting"
        Dim BJETTAG As String = "Binder Jetting"
        Dim VPHOTAG As String = "Vat Photopolymerisation"
        Dim PBFUTAG As String = "Powder Bed Fusion"
        Dim DEDETAG As String = "Directed Energy Deposition"
        Dim EndResultTag As String = "Null"
        ' find which machine has the best result
        If MEXTRESULT > MJETRESULT Then
            EndResult = MEXTRESULT
            EndResultTag = MEXTTAG
        ElseIf MJETRESULT > MEXTRESULT Then
            EndResult = MJETRESULT
            EndResultTag = MJETTAG
        Else EndResult = MEXTRESULT
        End If
        If BJETRESULT > EndResult Then
            EndResult = BJETRESULT
            EndResultTag = BJETTAG
        End If
        If VPHORESULT > EndResult Then
            EndResult = VPHORESULT
            EndResultTag = VPHOTAG
        End If
        If PBFURESULT > EndResult Then
            EndResult = PBFURESULT
            EndResultTag = PBFUTAG
        End If
        If DEDERESULT > EndResult Then
            EndResult = DEDERESULT
            EndResultTag = DEDETAG
        End If
        'mark the end result agains the point thresholds
        If EndResult >= 70 Then
            MsgBox("Design is Practicle for applying AM technology, using " & EndResultTag)
        ElseIf EndResult <= 30 Then
            MsgBox("Design is much more suited for Conventional Manufacturing")
        Else
            MsgBox("Design needs to be tuned before AM is practicle")
        End If

    End Sub
End Class