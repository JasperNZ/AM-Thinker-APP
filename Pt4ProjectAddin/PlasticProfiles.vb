Imports System.Collections.Generic
Imports System.Diagnostics.Eventing.Reader

'Plastic MEX class
Public Class PlasticMEX
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Material Extrusion"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Double)) From {
            {"Precision", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.4}, {"High", 0.2}, {"Very High", 0.1}
            }},
            {"LeadTime", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.3}, {"High", 0.1}, {"Very High", 0}
            }},
            {"PostProcessing", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }},
            {"Volume", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }}}
        End Get
    End Property


    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity <= 0.527 Then
            Return 0.2
        ElseIf rawComplexity <= 1.21 Then
            Return 0.4
        ElseIf rawComplexity <= 3.046 Then
            Return 0.6
        ElseIf rawComplexity <= 9.051 Then
            Return 0.8
        Else
            Return 1
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent <= 0.05 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.15 Then
            Return 0.8
        ElseIf rawOverhangPercent <= 0.3 Then
            Return 0.6
        ElseIf rawOverhangPercent <= 0.5 Then
            Return 0.4
        Else
            Return 0.2
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(nonMachinableFeaturesPresent As Boolean) As Double
        If nonMachinableFeaturesPresent Then
            Return 1
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)
        Dim m As New Dictionary(Of String, Double)

        Select Case partPurpose
            Case "Unique Custom Part"
                m("Precision") = 1.0
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Critical Spare Part"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.5
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Mass Production"
                m("Precision") = 1.5
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.5
                m("Volume") = 1.5
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Functional Prototype"
                m("Precision") = 1.0
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.5

            Case "Aesthetic Prototype"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.5
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case Else
                For Each key In New String() {"Precision", "LeadTime", "PostProcessing", "Volume", "Complexity", "Overhang", "ImpossibleFeatures"}
                    m(key) = 1.0
                Next
        End Select

        Return m
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
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Double)) From {
            {"Precision", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 1.0}, {"High", 1.0}, {"Very High", 1.0}
            }},
            {"LeadTime", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.3}, {"High", 0.1}, {"Very High", 0}
            }},
            {"PostProcessing", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }},
            {"Volume", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }}}
        End Get
    End Property


    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity <= 0.527 Then
            Return 0.2
        ElseIf rawComplexity <= 1.21 Then
            Return 0.4
        ElseIf rawComplexity <= 3.046 Then
            Return 0.6
        ElseIf rawComplexity <= 9.051 Then
            Return 0.8
        Else
            Return 1
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent <= 0.05 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.15 Then
            Return 0.8
        ElseIf rawOverhangPercent <= 0.3 Then
            Return 0.6
        ElseIf rawOverhangPercent <= 0.5 Then
            Return 0.4
        Else
            Return 0.2
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(nonMachinableFeaturesPresent As Boolean) As Double
        If nonMachinableFeaturesPresent Then
            Return 1
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)
        Dim m As New Dictionary(Of String, Double)

        Select Case partPurpose
            Case "Unique Custom Part"
                m("Precision") = 1.0
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Critical Spare Part"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.5
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Mass Production"
                m("Precision") = 1.5
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.5
                m("Volume") = 1.5
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Functional Prototype"
                m("Precision") = 1.0
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.5

            Case "Aesthetic Prototype"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.5
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case Else
                For Each key In New String() {"Precision", "LeadTime", "PostProcessing", "Volume", "Complexity", "Overhang", "ImpossibleFeatures"}
                    m(key) = 1.0
                Next
        End Select

        Return m
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
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Double)) From {
            {"Precision", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.4}, {"High", 0.5}, {"Very High", 0.25}
            }},
            {"LeadTime", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.3}, {"High", 0.1}, {"Very High", 0}
            }},
            {"PostProcessing", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }},
            {"Volume", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }}}
        End Get
    End Property


    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity <= 0.527 Then
            Return 0.2
        ElseIf rawComplexity <= 1.21 Then
            Return 0.4
        ElseIf rawComplexity <= 3.046 Then
            Return 0.6
        ElseIf rawComplexity <= 9.051 Then
            Return 0.8
        Else
            Return 1
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent <= 0.05 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.15 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.3 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.5 Then
            Return 1
        Else
            Return 1
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(nonMachinableFeaturesPresent As Boolean) As Double
        If nonMachinableFeaturesPresent Then
            Return 1
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)
        Dim m As New Dictionary(Of String, Double)

        Select Case partPurpose
            Case "Unique Custom Part"
                m("Precision") = 1.0
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Critical Spare Part"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.5
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Mass Production"
                m("Precision") = 1.5
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.5
                m("Volume") = 1.5
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Functional Prototype"
                m("Precision") = 1.0
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.5

            Case "Aesthetic Prototype"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.5
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case Else
                For Each key In New String() {"Precision", "LeadTime", "PostProcessing", "Volume", "Complexity", "Overhang", "ImpossibleFeatures"}
                    m(key) = 1.0
                Next
        End Select

        Return m
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
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Double)) From {
            {"Precision", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 1.0}, {"High", 0.67}, {"Very High", 0.33}
            }},
            {"LeadTime", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.3}, {"High", 0.1}, {"Very High", 0}
            }},
            {"PostProcessing", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }},
            {"Volume", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }}}
        End Get
    End Property


    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity <= 0.527 Then
            Return 0.2
        ElseIf rawComplexity <= 1.21 Then
            Return 0.4
        ElseIf rawComplexity <= 3.046 Then
            Return 0.6
        ElseIf rawComplexity <= 9.051 Then
            Return 0.8
        Else
            Return 1
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent <= 0.05 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.15 Then
            Return 0.8
        ElseIf rawOverhangPercent <= 0.3 Then
            Return 0.6
        ElseIf rawOverhangPercent <= 0.5 Then
            Return 0.4
        Else
            Return 0.2
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(nonMachinableFeaturesPresent As Boolean) As Double
        If nonMachinableFeaturesPresent Then
            Return 1
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)
        Dim m As New Dictionary(Of String, Double)

        Select Case partPurpose
            Case "Unique Custom Part"
                m("Precision") = 1.0
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Critical Spare Part"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.5
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Mass Production"
                m("Precision") = 1.5
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.5
                m("Volume") = 1.5
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Functional Prototype"
                m("Precision") = 1.0
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.5

            Case "Aesthetic Prototype"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.5
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case Else
                For Each key In New String() {"Precision", "LeadTime", "PostProcessing", "Volume", "Complexity", "Overhang", "ImpossibleFeatures"}
                    m(key) = 1.0
                Next
        End Select

        Return m
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
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Double)) From {
            {"Precision", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.67}, {"High", 0.33}, {"Very High", 0.17}
            }},
            {"LeadTime", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 1.0}, {"Medium", 0.3}, {"High", 0.1}, {"Very High", 0}
            }},
            {"PostProcessing", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }},
            {"Volume", New Dictionary(Of String, Double) From {
                {"Very Low", 1.0}, {"Low", 0.7}, {"Medium", 0.5}, {"High", 0.3}, {"Very High", 0.1}
            }}}
        End Get
    End Property


    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity <= 0.527 Then
            Return 0.2
        ElseIf rawComplexity <= 1.21 Then
            Return 0.4
        ElseIf rawComplexity <= 3.046 Then
            Return 0.6
        ElseIf rawComplexity <= 9.051 Then
            Return 0.8
        Else
            Return 1
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent <= 0.05 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.15 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.3 Then
            Return 1
        ElseIf rawOverhangPercent <= 0.5 Then
            Return 1
        Else
            Return 1
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(nonMachinableFeaturesPresent As Boolean) As Double
        If nonMachinableFeaturesPresent Then
            Return 1
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)
        Dim m As New Dictionary(Of String, Double)

        Select Case partPurpose
            Case "Unique Custom Part"
                m("Precision") = 1.0
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Critical Spare Part"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.5
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Mass Production"
                m("Precision") = 1.5
                m("LeadTime") = 1.0
                m("PostProcessing") = 1.5
                m("Volume") = 1.5
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case "Functional Prototype"
                m("Precision") = 1.0
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.0
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.5

            Case "Aesthetic Prototype"
                m("Precision") = 1.5
                m("LeadTime") = 1.5
                m("PostProcessing") = 1.5
                m("Volume") = 1.0
                m("Complexity") = 1.0
                m("Overhang") = 1.0
                m("ImpossibleFeatures") = 1.0

            Case Else
                For Each key In New String() {"Precision", "LeadTime", "PostProcessing", "Volume", "Complexity", "Overhang", "ImpossibleFeatures"}
                    m(key) = 1.0
                Next
        End Select

        Return m
    End Function
End Class

'Plastic DED class - 0
Public Class PlasticDED
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Plastic"
        Me.Technology = "Directed Energy Deposition"
    End Sub

    'Individual mappings for each of the categorical input for each AM profile
    'As DED does not support plastics, all values are 0
    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Double)) From {
                {"Precision", New Dictionary(Of String, Double) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"LeadTime", New Dictionary(Of String, Double) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"PostProcessing", New Dictionary(Of String, Double) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}},
                {"Volume", New Dictionary(Of String, Double) From {{"Very Low", 0}, {"Low", 0}, {"Medium", 0}, {"High", 0}, {"Very High", 0}}}
            }
        End Get
    End Property

    'Receives part complexity, a high enough value indicates suitaible for DfAM
    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 0.2 Then
            Return 0
        ElseIf rawComplexity < 0.6 Then
            Return 0
        ElseIf rawComplexity < 1.5 Then
            Return 0
        ElseIf rawComplexity < 4.0 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Receives overhang area, a low enough value indicates good/ideal DfAM
    Protected Overrides Function InterpretOverhangComplexity(rawOverhangPercent As Double) As Double
        If rawOverhangPercent < 5 Then
            Return 0
        ElseIf rawOverhangPercent < 15 Then
            Return 0
        ElseIf rawOverhangPercent < 30 Then
            Return 0
        ElseIf rawOverhangPercent < 50 Then
            Return 0
        Else
            Return 0
        End If
    End Function

    ' Function outputs a value by checking if user states impossible features are present
    Protected Overrides Function InterpretImpossibleFeatures(nonMachinableFeaturesPresent As Boolean) As Double
        If nonMachinableFeaturesPresent Then
            Return 0
        Else
            Return 0
        End If
    End Function

    'Function receives the purpose of the part to adjust the weightings.
    'TODO: experiment to adjust suitable criterias with logic and then test with spreadsheet of parts
    Protected Overrides Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)
        Dim m As New Dictionary(Of String, Double)

        Select Case partPurpose
            Case "Unique Custom Part"
                m("Precision") = 1.0
                m("LeadTime") = 0.7
                m("PostProcessing") = 1.3
                m("Volume") = 0.4
                m("Complexity") = 1.3
                m("Overhang") = 1.1
                m("ImpossibleFeatures") = 1.4

            Case "Critical Spare Part"
                m("Precision") = 1.5
                m("LeadTime") = 1.4
                m("PostProcessing") = 1.4
                m("Volume") = 0.5
                m("Complexity") = 0.6
                m("Overhang") = 0.7
                m("ImpossibleFeatures") = 0.5

            Case "Mass Production"
                m("Precision") = 1.2
                m("LeadTime") = 0.9
                m("PostProcessing") = 1.6
                m("Volume") = 1.8
                m("Complexity") = 0.4
                m("Overhang") = 0.5
                m("ImpossibleFeatures") = 0.2

            Case "Functional Prototype"
                m("Precision") = 0.9
                m("LeadTime") = 1.2
                m("PostProcessing") = 0.8
                m("Volume") = 0.4
                m("Complexity") = 1.1
                m("Overhang") = 0.9
                m("ImpossibleFeatures") = 0.8

            Case "Aesthetic Prototype"
                m("Precision") = 1.4
                m("LeadTime") = 0.8
                m("PostProcessing") = 1.5
                m("Volume") = 0.5
                m("Complexity") = 0.7
                m("Overhang") = 0.6
                m("ImpossibleFeatures") = 0.3

            Case Else
                For Each key In New String() {"Precision", "LeadTime", "PostProcessing", "Volume", "Complexity", "Overhang", "ImpossibleFeatures"}
                    m(key) = 1.0
                Next
        End Select

        Return m
    End Function
End Class
