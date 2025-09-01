Imports System.Collections.Generic

'Plastic MEX class
Public Class PlasticMEX
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Material Extrusion"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 5}, {"High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 1}, {"Medium", 5}, {"High", 10}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 5}, {"High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"One Off Part", 10}, {"Low Volume Production", 5}, {"High Volume Production", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 2
        ElseIf rawComplexity < 0.45 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 10
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 1
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

'Plastic MJT class
Public Class PlasticMJT
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Material Jetting"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 6}, {"High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 1}, {"Medium", 5}, {"High", 10}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 5}, {"High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 4}, {"High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 2
        ElseIf rawComplexity < 0.45 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 10
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 1
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

'Plastic BJT class
Public Class PlasticBJT
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Binder Jetting"
    End Sub


    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 6}, {"High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 1}, {"Medium", 5}, {"High", 10}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 5}, {"High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 4}, {"High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 2
        ElseIf rawComplexity < 0.45 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 10
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 1
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

'Plastic VPP class 
Public Class PlasticVPP
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Vat Photopolymerisation"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 6}, {"High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 1}, {"Medium", 5}, {"High", 10}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 5}, {"High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 4}, {"High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 2
        ElseIf rawComplexity < 0.45 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 10
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 1
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

'Plastic PBF class
Public Class PlasticPBF
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Powder Bed Fusion"
    End Sub


    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 6}, {"High", 1}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 1}, {"Medium", 5}, {"High", 10}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 5}, {"High", 1}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 10}, {"Medium", 4}, {"High", 1}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 2
        ElseIf rawComplexity < 0.45 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 10
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 1
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

'Plastic DED class 
Public Class PlasticDED
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Directed Energy Deposition"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    'As DED does not support plastics, all values are 0
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 0}, {"Medium", 0}, {"High", 0}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 0}, {"Medium", 0}, {"High", 0}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 0}, {"Medium", 0}, {"High", 0}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 0}, {"Medium", 0}, {"High", 0}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.35 Then
            Return 0
        ElseIf rawComplexity < 0.45 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 0
        ElseIf rawOverhang < 45 Then
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
