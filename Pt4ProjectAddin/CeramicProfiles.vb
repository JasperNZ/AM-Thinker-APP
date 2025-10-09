Imports System.Collections.Generic

'Ceramic MEX class
Public Class CeramicMEX
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Ceramic"
        Me.Technology = "Material Extrusion"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 8}, {"Medium", 1}, {"High", 1}, {"Very High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 3}, {"High", 3}, {"Very High", 1}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.2 Then
            Return 2
        ElseIf rawComplexity < 0.6 Then
            Return 4
        ElseIf rawComplexity < 1.5 Then
            Return 6
        ElseIf rawComplexity < 4.0 Then
            Return 8
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent < 5 Then
            Return 10
        ElseIf rawOverhangPercent < 15 Then
            Return 8
        ElseIf rawOverhangPercent < 30 Then
            Return 6
        ElseIf rawOverhangPercent < 50 Then
            Return 4
        Else
            Return 2
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(impossibleFeaturesPresent As Boolean) As Double
        If impossibleFeaturesPresent Then
            Return 10
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Unique Custom Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Critical Spare Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Mass Production"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Functional Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Aesthetic Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function
End Class

'Ceramic MJT class - set 0s
Public Class CeramicMJT
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Ceramic"
        Me.Technology = "Material Jetting"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(impossibleFeaturesPresent As Boolean) As Double
        If impossibleFeaturesPresent Then
            Return 0
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Return adjustedWeights
    End Function
End Class

'Ceramic BJT class
Public Class CeramicBJT
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Ceramic"
        Me.Technology = "Binder Jetting"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 5}, {"High", 1}, {"Very High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 10}, {"High", 5}, {"Very High", 1}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.2 Then
            Return 2
        ElseIf rawComplexity < 0.6 Then
            Return 4
        ElseIf rawComplexity < 1.5 Then
            Return 6
        ElseIf rawComplexity < 4.0 Then
            Return 8
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent < 5 Then
            Return 10
        ElseIf rawOverhangPercent < 15 Then
            Return 10
        ElseIf rawOverhangPercent < 30 Then
            Return 10
        ElseIf rawOverhangPercent < 50 Then
            Return 10
        Else
            Return 10
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(impossibleFeaturesPresent As Boolean) As Double
        If impossibleFeaturesPresent Then
            Return 10
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Unique Custom Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Critical Spare Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Mass Production"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Functional Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Aesthetic Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function
End Class

'Ceramic VPP class 
Public Class CeramicVPP
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Ceramic"
        Me.Technology = "Vat Photopolymerisation"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 8}, {"High", 6}, {"Very High", 3}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 3}, {"High", 3}, {"Very High", 1}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.2 Then
            Return 2
        ElseIf rawComplexity < 0.6 Then
            Return 4
        ElseIf rawComplexity < 1.5 Then
            Return 6
        ElseIf rawComplexity < 4.0 Then
            Return 8
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent < 5 Then
            Return 10
        ElseIf rawOverhangPercent < 15 Then
            Return 8
        ElseIf rawOverhangPercent < 30 Then
            Return 6
        ElseIf rawOverhangPercent < 50 Then
            Return 4
        Else
            Return 2
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(impossibleFeaturesPresent As Boolean) As Double
        If impossibleFeaturesPresent Then
            Return 10
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Unique Custom Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Critical Spare Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Mass Production"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Functional Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Aesthetic Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function
End Class

'Ceramic PBF class
Public Class CeramicPBF
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Ceramic"
        Me.Technology = "Powder Bed Fusion"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 10}, {"High", 5}, {"Very High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 10}, {"Medium", 6}, {"High", 1}, {"Very High", 1}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Very Low", 10}, {"Low", 7}, {"Medium", 5}, {"High", 3}, {"Very High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.2 Then
            Return 2
        ElseIf rawComplexity < 0.6 Then
            Return 4
        ElseIf rawComplexity < 1.5 Then
            Return 6
        ElseIf rawComplexity < 4.0 Then
            Return 8
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent < 5 Then
            Return 10
        ElseIf rawOverhangPercent < 15 Then
            Return 10
        ElseIf rawOverhangPercent < 30 Then
            Return 10
        ElseIf rawOverhangPercent < 50 Then
            Return 10
        Else
            Return 10
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(impossibleFeaturesPresent As Boolean) As Double
        If impossibleFeaturesPresent Then
            Return 10
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Unique Custom Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Critical Spare Part"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Mass Production"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Functional Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case "Aesthetic Prototype"
                adjustedWeights("Precision") += 0
                adjustedWeights("LeadTime") += 0
                adjustedWeights("PostProcessing") += 0
                adjustedWeights("Volume") += 0
                adjustedWeights("Complexity") += 0
                adjustedWeights("Overhang") += 0
                adjustedWeights("ImpossibleFeatures") += 0
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function
End Class

'Ceramic DED class - set 0s
Public Class CeramicDED
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Ceramic"
        Me.Technology = "Directed Energy Deposition"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(impossibleFeaturesPresent As Boolean) As Double
        If impossibleFeaturesPresent Then
            Return 0
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Return adjustedWeights
    End Function
End Class