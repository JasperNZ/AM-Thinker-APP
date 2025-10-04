<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SummaryForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SummaryForm))
        Me.LabelAssessment = New System.Windows.Forms.Label()
        Me.LabelBestOptions = New System.Windows.Forms.Label()
        Me.ListBoxResults = New System.Windows.Forms.ListBox()
        Me.ButtonDetails = New System.Windows.Forms.Button()
        Me.PanelDetails = New System.Windows.Forms.Panel()
        Me.LabelImprovements = New System.Windows.Forms.Label()
        Me.LabelGeometry = New System.Windows.Forms.Label()
        Me.LabelLimitations = New System.Windows.Forms.Label()
        Me.PanelDetails.SuspendLayout()
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
        Me.LabelBestOptions.AutoSize = True
        Me.LabelBestOptions.Location = New System.Drawing.Point(12, 137)
        Me.LabelBestOptions.Name = "LabelBestOptions"
        Me.LabelBestOptions.Size = New System.Drawing.Size(46, 13)
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
        Me.ListBoxResults.Size = New System.Drawing.Size(460, 84)
        Me.ListBoxResults.TabIndex = 3
        '
        'ButtonDetails
        '
        Me.ButtonDetails.Location = New System.Drawing.Point(12, 187)
        Me.ButtonDetails.Name = "ButtonDetails"
        Me.ButtonDetails.Size = New System.Drawing.Size(108, 23)
        Me.ButtonDetails.TabIndex = 4
        Me.ButtonDetails.Text = "▼ Show Details"
        Me.ButtonDetails.UseVisualStyleBackColor = True
        '
        'PanelDetails
        '
        Me.PanelDetails.Controls.Add(Me.LabelImprovements)
        Me.PanelDetails.Controls.Add(Me.LabelGeometry)
        Me.PanelDetails.Controls.Add(Me.LabelLimitations)
        Me.PanelDetails.Location = New System.Drawing.Point(15, 216)
        Me.PanelDetails.Name = "PanelDetails"
        Me.PanelDetails.Size = New System.Drawing.Size(457, 100)
        Me.PanelDetails.TabIndex = 5
        Me.PanelDetails.Visible = False
        '
        'LabelImprovements
        '
        Me.LabelImprovements.AutoSize = True
        Me.LabelImprovements.Location = New System.Drawing.Point(4, 75)
        Me.LabelImprovements.Name = "LabelImprovements"
        Me.LabelImprovements.Size = New System.Drawing.Size(130, 13)
        Me.LabelImprovements.TabIndex = 8
        Me.LabelImprovements.Text = "Suggested Improvements:"
        '
        'LabelGeometry
        '
        Me.LabelGeometry.AutoSize = True
        Me.LabelGeometry.Location = New System.Drawing.Point(3, 13)
        Me.LabelGeometry.Name = "LabelGeometry"
        Me.LabelGeometry.Size = New System.Drawing.Size(102, 13)
        Me.LabelGeometry.TabIndex = 6
        Me.LabelGeometry.Text = "Geometric Analysis: "
        '
        'LabelLimitations
        '
        Me.LabelLimitations.AutoSize = True
        Me.LabelLimitations.Location = New System.Drawing.Point(3, 44)
        Me.LabelLimitations.Name = "LabelLimitations"
        Me.LabelLimitations.Size = New System.Drawing.Size(118, 13)
        Me.LabelLimitations.TabIndex = 7
        Me.LabelLimitations.Text = "Technology Limitations:"
        '
        'SummaryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 322)
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
        Me.PanelDetails.ResumeLayout(False)
        Me.PanelDetails.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelAssessment As Windows.Forms.Label
    Friend WithEvents LabelBestOptions As Windows.Forms.Label
    Friend WithEvents ListBoxResults As Windows.Forms.ListBox
    Friend WithEvents ButtonDetails As Windows.Forms.Button
    Friend WithEvents PanelDetails As Windows.Forms.Panel
    Friend WithEvents LabelImprovements As Windows.Forms.Label
    Friend WithEvents LabelGeometry As Windows.Forms.Label
    Friend WithEvents LabelLimitations As Windows.Forms.Label
End Class
