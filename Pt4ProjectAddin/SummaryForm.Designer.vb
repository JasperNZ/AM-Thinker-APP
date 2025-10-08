<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SummaryForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SummaryForm))
        Me.LabelAssessment = New System.Windows.Forms.Label()
        Me.LabelBestOptions = New System.Windows.Forms.Label()
        Me.ListBoxResults = New System.Windows.Forms.ListBox()
        Me.ButtonDetails = New System.Windows.Forms.Button()
        Me.PanelDetails = New System.Windows.Forms.Panel()
        Me.LabelBestOptionsHeader = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelAssessment
        '
        Me.LabelAssessment.AutoSize = True
        Me.LabelAssessment.Location = New System.Drawing.Point(12, 9)
        Me.LabelAssessment.Name = "LabelAssessment"
        Me.LabelAssessment.Size = New System.Drawing.Size(128, 13)
        Me.LabelAssessment.TabIndex = 0
        Me.LabelAssessment.Text = "Overall Assessment Label"
        '
        'LabelBestOptions
        '
        Me.LabelBestOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelBestOptions.Location = New System.Drawing.Point(80, 95)
        Me.LabelBestOptions.Name = "LabelBestOptions"
        Me.LabelBestOptions.Size = New System.Drawing.Size(392, 32)
        Me.LabelBestOptions.TabIndex = 1
        Me.LabelBestOptions.Text = "Options:"
        '
        'ListBoxResults
        '
        Me.ListBoxResults.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxResults.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ListBoxResults.FormattingEnabled = True
        Me.ListBoxResults.ItemHeight = 20
        Me.ListBoxResults.Location = New System.Drawing.Point(12, 28)
        Me.ListBoxResults.Name = "ListBoxResults"
        Me.ListBoxResults.Size = New System.Drawing.Size(460, 44)
        Me.ListBoxResults.TabIndex = 3
        '
        'ButtonDetails
        '
        Me.ButtonDetails.Location = New System.Drawing.Point(12, 130)
        Me.ButtonDetails.Name = "ButtonDetails"
        Me.ButtonDetails.Size = New System.Drawing.Size(108, 23)
        Me.ButtonDetails.TabIndex = 4
        Me.ButtonDetails.Text = "▼ Show Details"
        Me.ButtonDetails.UseVisualStyleBackColor = True
        '
        'PanelDetails
        '
        Me.PanelDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelDetails.AutoScroll = True
        Me.PanelDetails.Location = New System.Drawing.Point(15, 188)
        Me.PanelDetails.Name = "PanelDetails"
        Me.PanelDetails.Size = New System.Drawing.Size(457, 0)
        Me.PanelDetails.TabIndex = 5
        Me.PanelDetails.Visible = False
        '
        'LabelBestOptionsHeader
        '
        Me.LabelBestOptionsHeader.AutoSize = True
        Me.LabelBestOptionsHeader.Location = New System.Drawing.Point(12, 95)
        Me.LabelBestOptionsHeader.Name = "LabelBestOptionsHeader"
        Me.LabelBestOptionsHeader.Size = New System.Drawing.Size(70, 13)
        Me.LabelBestOptionsHeader.TabIndex = 6
        Me.LabelBestOptionsHeader.Text = "Best Options:"
        '
        'SummaryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(484, 164)
        Me.Controls.Add(Me.LabelBestOptionsHeader)
        Me.Controls.Add(Me.PanelDetails)
        Me.Controls.Add(Me.ButtonDetails)
        Me.Controls.Add(Me.ListBoxResults)
        Me.Controls.Add(Me.LabelBestOptions)
        Me.Controls.Add(Me.LabelAssessment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SummaryForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AM Thinker - Analysis Results"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelAssessment As Windows.Forms.Label
    Friend WithEvents LabelBestOptions As Windows.Forms.Label
    Friend WithEvents ListBoxResults As Windows.Forms.ListBox
    Friend WithEvents ButtonDetails As Windows.Forms.Button
    Friend WithEvents PanelDetails As Windows.Forms.Panel
    Friend WithEvents LabelBestOptionsHeader As Windows.Forms.Label
End Class