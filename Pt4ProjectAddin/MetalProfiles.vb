Imports System.Collections.Generic

'Metal MEX class
Public Class MetalMEX
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Metal"
        Me.Technology = "MEX"
    End Sub

    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 3}, {"Medium", 6}, {"High", 10}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 4}, {"Medium", 7}, {"High", 9}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 5}, {"High", 8}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 4}, {"High", 7}}}
            }
        End Get
    End Property

    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 1.5 Then
            Return 2
        ElseIf rawComplexity < 3.0 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Interpret raw overhang (in degrees or %) ? assign score
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 1
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 8
        End If
    End Function

    ' Interpret boolean for impossible features ? assign score
    Protected Overrides Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double
        If rawImpossibleFeatures Then
            Return 10
        Else
            Return 0
        End If
    End Function

    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Prototype"
                adjustedWeights("Precision") *= 0.5
                adjustedWeights("LeadTime") *= 1.5
            Case "Production"
                adjustedWeights("Precision") *= 1.5
                adjustedWeights("LeadTime") *= 0.5
            Case "Functional Testing"
                adjustedWeights("Precision") *= 1.2
                adjustedWeights("LeadTime") *= 1.2
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function


End Class

'Metal MJT class
Public Class MetalMJT
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Metal"
        Me.Technology = "MJT"
    End Sub

    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 3}, {"Medium", 6}, {"High", 10}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 4}, {"Medium", 7}, {"High", 9}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 5}, {"High", 8}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 4}, {"High", 7}}}
            }
        End Get
    End Property

    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 1.5 Then
            Return 2
        ElseIf rawComplexity < 3.0 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Interpret raw overhang (in degrees or %) ? assign score
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 1
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 8
        End If
    End Function

    ' Interpret boolean for impossible features ? assign score
    Protected Overrides Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double
        If rawImpossibleFeatures Then
            Return 10
        Else
            Return 0
        End If
    End Function

    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Prototype"
                adjustedWeights("Precision") *= 0.5
                adjustedWeights("LeadTime") *= 1.5
            Case "Production"
                adjustedWeights("Precision") *= 1.5
                adjustedWeights("LeadTime") *= 0.5
            Case "Functional Testing"
                adjustedWeights("Precision") *= 1.2
                adjustedWeights("LeadTime") *= 1.2
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function


End Class

'Metal BJT class
Public Class MetalBJT
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Metal"
        Me.Technology = "BJT"
    End Sub

    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 3}, {"Medium", 6}, {"High", 10}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 4}, {"Medium", 7}, {"High", 9}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 5}, {"High", 8}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 4}, {"High", 7}}}
            }
        End Get
    End Property

    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 1.5 Then
            Return 2
        ElseIf rawComplexity < 3.0 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Interpret raw overhang (in degrees or %) ? assign score
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 1
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 8
        End If
    End Function

    ' Interpret boolean for impossible features ? assign score
    Protected Overrides Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double
        If rawImpossibleFeatures Then
            Return 10
        Else
            Return 0
        End If
    End Function

    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Prototype"
                adjustedWeights("Precision") *= 0.5
                adjustedWeights("LeadTime") *= 1.5
            Case "Production"
                adjustedWeights("Precision") *= 1.5
                adjustedWeights("LeadTime") *= 0.5
            Case "Functional Testing"
                adjustedWeights("Precision") *= 1.2
                adjustedWeights("LeadTime") *= 1.2
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function


End Class

'Metal VPP class (should not be possible and all 0s, but there for future editing)
Public Class MetalVPP
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Metal"
        Me.Technology = "VPP"
    End Sub

    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 3}, {"Medium", 6}, {"High", 10}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 4}, {"Medium", 7}, {"High", 9}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 5}, {"High", 8}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 4}, {"High", 7}}}
            }
        End Get
    End Property

    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 1.5 Then
            Return 2
        ElseIf rawComplexity < 3.0 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Interpret raw overhang (in degrees or %) ? assign score
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 1
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 8
        End If
    End Function

    ' Interpret boolean for impossible features ? assign score
    Protected Overrides Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double
        If rawImpossibleFeatures Then
            Return 10
        Else
            Return 0
        End If
    End Function

    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Prototype"
                adjustedWeights("Precision") *= 0.5
                adjustedWeights("LeadTime") *= 1.5
            Case "Production"
                adjustedWeights("Precision") *= 1.5
                adjustedWeights("LeadTime") *= 0.5
            Case "Functional Testing"
                adjustedWeights("Precision") *= 1.2
                adjustedWeights("LeadTime") *= 1.2
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function


End Class

'Metal PBF class
Public Class MetalPBF
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Metal"
        Me.Technology = "PBF"
    End Sub

    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 3}, {"Medium", 6}, {"High", 10}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 4}, {"Medium", 7}, {"High", 9}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 5}, {"High", 8}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 4}, {"High", 7}}}
            }
        End Get
    End Property

    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 1.5 Then
            Return 2
        ElseIf rawComplexity < 3.0 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Interpret raw overhang (in degrees or %) ? assign score
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 1
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 8
        End If
    End Function

    ' Interpret boolean for impossible features ? assign score
    Protected Overrides Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double
        If rawImpossibleFeatures Then
            Return 10
        Else
            Return 0
        End If
    End Function

    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Prototype"
                adjustedWeights("Precision") *= 0.5
                adjustedWeights("LeadTime") *= 1.5
            Case "Production"
                adjustedWeights("Precision") *= 1.5
                adjustedWeights("LeadTime") *= 0.5
            Case "Functional Testing"
                adjustedWeights("Precision") *= 1.2
                adjustedWeights("LeadTime") *= 1.2
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function
End Class

'Metal DED class
Public Class MetalDED
    Inherits AMProfile

    Public Sub New()
        MyBase.New()
        Me.Material = "Metal"
        Me.Technology = "DED"
    End Sub

    Protected Overrides ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Integer))
        Get
            Return New Dictionary(Of String, Dictionary(Of String, Integer)) From {
                {"Precision", New Dictionary(Of String, Integer) From {{"Low", 3}, {"Medium", 6}, {"High", 10}}},
                {"LeadTime", New Dictionary(Of String, Integer) From {{"Low", 4}, {"Medium", 7}, {"High", 9}}},
                {"PostProcessing", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 5}, {"High", 8}}},
                {"Volume", New Dictionary(Of String, Integer) From {{"Low", 2}, {"Medium", 4}, {"High", 7}}}
            }
        End Get
    End Property

    Protected Overrides Function InterpretPartComplexity(rawComplexity As Double) As Double
        If rawComplexity < 1.5 Then
            Return 2
        ElseIf rawComplexity < 3.0 Then
            Return 6
        Else
            Return 10
        End If
    End Function

    ' Interpret raw overhang (in degrees or %) ? assign score
    Protected Overrides Function InterpretOverhangComplexity(rawOverhang As Double) As Double
        If rawOverhang < 20 Then
            Return 1
        ElseIf rawOverhang < 45 Then
            Return 5
        Else
            Return 8
        End If
    End Function

    ' Interpret boolean for impossible features ? assign score
    Protected Overrides Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double
        If rawImpossibleFeatures Then
            Return 10
        Else
            Return 0
        End If
    End Function

    Protected Overrides Function IntendedPartPurpose(partPurpose As String) As Dictionary(Of String, Double)
        Dim adjustedWeights As New Dictionary(Of String, Double)(Weights)
        Select Case partPurpose
            Case "Prototype"
                adjustedWeights("Precision") *= 0.5
                adjustedWeights("LeadTime") *= 1.5
            Case "Production"
                adjustedWeights("Precision") *= 1.5
                adjustedWeights("LeadTime") *= 0.5
            Case "Functional Testing"
                adjustedWeights("Precision") *= 1.2
                adjustedWeights("LeadTime") *= 1.2
            Case Else
                ' Default case: no changes
        End Select
        Return adjustedWeights
    End Function


End Class