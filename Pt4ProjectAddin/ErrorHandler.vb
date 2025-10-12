'------------------------------------------------------------------------------
' <summary>
'	Helper Class dealing with user input errors and displaying appropriate messages.
' <author>Jasper Koid</author>
' <created>24-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics

' File to call error messages, useful for debugging and deal with users causing errors.
Public Class ErrorHandler
    'Function if Profile factory has issue.
    Public Shared Sub ProfileGenerationError(message As String, ex As Exception)
        MessageBox.Show($"Error initializing profile factory: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    'Function if user does not select material.
    Public Shared Sub NoMaterialSelected()
        MessageBox.Show("Please select a material.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' User does not select AM technology.
    Public Shared Sub NoTechnologySelected()
        MessageBox.Show("Please select an AM technology.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' User does not select part purpose.
    Public Shared Sub NoPartPurposeSelected()
        MessageBox.Show("Please select the part's intended application.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' User does not select precision.
    Public Shared Sub NoPrecisionSelected()
        MessageBox.Show("Please select a precision requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' User does not select lead time.
    Public Shared Sub NoLeadTimeSelected()
        MessageBox.Show("Please select a lead time requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' User does not select post processing effort.
    Public Shared Sub NoPostProcessingSelected()
        MessageBox.Show("Please select a post-processing requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' User does not select volume.
    Public Shared Sub NoVolumeSelected()
        MessageBox.Show("Please select a volume requirement.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' Centralised validation method, does not need to check NMFPand Overhang checkboxes as they are optional.
    Public Shared Function ValidateSelections(form As Form) As Boolean
        ' Check if all required ComboBoxes have a selection.
        Dim groupRequirements As GroupBox = DirectCast(form.Controls("GroupRequirements"), GroupBox)
        Dim validations As New Dictionary(Of ComboBox, Action) From {
        {DirectCast(groupRequirements.Controls("ComboBoxPrecisionOfPart"), ComboBox), AddressOf NoPrecisionSelected},
        {DirectCast(groupRequirements.Controls("ComboBoxLeadTime"), ComboBox), AddressOf NoLeadTimeSelected},
        {DirectCast(groupRequirements.Controls("ComboBoxPostProcessingEffort"), ComboBox), AddressOf NoPostProcessingSelected},
        {DirectCast(groupRequirements.Controls("ComboBoxVolumeOfProduction"), ComboBox), AddressOf NoVolumeSelected},
        {DirectCast(groupRequirements.Controls("ComboBoxIntendedApplication"), ComboBox), AddressOf NoPartPurposeSelected},
        {DirectCast(groupRequirements.Controls("ComboBoxMaterial"), ComboBox), AddressOf NoMaterialSelected}
        }

        For Each item In validations
            If String.IsNullOrWhiteSpace(item.Key.Text) Then
                item.Value.Invoke()
                Return False ' Validation failed
            End If
        Next

        ' Check if at least one technology CheckBox is selected.
        Dim groupTechnologies As GroupBox = DirectCast(form.Controls("GroupTechnologies"), GroupBox)
        Dim anyTechSelected As Boolean = False

        For Each control In groupTechnologies.Controls
            If TypeOf control Is CheckBox Then
                Dim checkbox As CheckBox = DirectCast(control, CheckBox)
                If checkbox.Checked Then
                    anyTechSelected = True
                    Exit For
                End If
            End If
        Next
        If Not anyTechSelected Then
            NoTechnologySelected()
            Return False
        End If

        Return True ' All inputs are valid
    End Function
End Class
