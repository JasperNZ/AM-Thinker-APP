Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics

'File to call error messages, useful for debugging and deal with users causing errors
'needs to use System.Windows.Forms for message boxes, so I'm praying it doesn't crash everything
Public Class ErrorHandler
    'function if Profile factory has issue.
    Public Shared Sub ProfileGenerationError(message As String, ex As Exception)
        MessageBox.Show($"Error initializing profile factory: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    'function if user does not select material
    Public Shared Sub NoMaterialSelected()
        MessageBox.Show("Please select a material.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    'User does not select AM technology
    Public Shared Sub NoTechnologySelected()
        'MessageBox.Show("Please select an AM technology.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        MessageBox.Show("No compatible AM technologies selected or available for the chosen material.", "No Technologies", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    'user does not select part purpose
    Public Shared Sub NoPartPurposeSelected()
        MessageBox.Show("Please select a part purpose.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    'user does not select precision
    Public Shared Sub NoPrecisionSelected()
        MessageBox.Show("Please select a precision requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    'user does not select lead time
    Public Shared Sub NoLeadTimeSelected()
        MessageBox.Show("Please select a lead time requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    'user does not select post processing
    Public Shared Sub NoPostProcessingSelected()
        MessageBox.Show("Please select a post-processing requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    'user does not select volume
    Public Shared Sub NoVolumeSelected()
        MessageBox.Show("Please select a volume requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' Centralized validation method
    Public Shared Function ValidateSelections(form As Form) As Boolean
        Dim groupBox As GroupBox = DirectCast(form.Controls("groupRequirements"), GroupBox)

        Dim validations As New Dictionary(Of ComboBox, Action) From {
        {DirectCast(groupBox.Controls("ComboBoxMaterial"), ComboBox), AddressOf NoMaterialSelected},
        {DirectCast(groupBox.Controls("ComboBoxIntendedUseOfPart"), ComboBox), AddressOf NoPartPurposeSelected},
        {DirectCast(groupBox.Controls("ComboBoxPrecisionOfPart"), ComboBox), AddressOf NoPrecisionSelected},
        {DirectCast(groupBox.Controls("ComboBoxLeadTimeSignificance"), ComboBox), AddressOf NoLeadTimeSelected},
        {DirectCast(groupBox.Controls("ComboBoxPostProcessingEffort"), ComboBox), AddressOf NoPostProcessingSelected},
        {DirectCast(groupBox.Controls("ComboBoxVolumeOfProduction"), ComboBox), AddressOf NoVolumeSelected}
    }

        For Each item In validations
            If String.IsNullOrWhiteSpace(item.Key.Text) Then
                item.Value.Invoke()
                Return False ' Validation failed
            End If
        Next
        Return True ' All inputs are valid
    End Function
End Class
