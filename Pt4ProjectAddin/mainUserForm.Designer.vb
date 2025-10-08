' File contains the design layout of the main user form and the backend logic.
' Stylistic decision to use dropdownlists to avoid user input errors.
' Material selection as dropdown list to avoid AM analysis conflicts.
Imports System.Drawing
Imports System.Windows.Forms


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mainUserForm
    Inherits System.Windows.Forms.Form

    Private toolTip1 As System.Windows.Forms.ToolTip

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainUserForm))
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.HighlightCheckBox = New System.Windows.Forms.CheckBox()
        Me.ButtonCompute = New System.Windows.Forms.Button()
        Me.ComboBoxPrecisionOfPart = New System.Windows.Forms.ComboBox()
        Me.ComboBoxLeadTime = New System.Windows.Forms.ComboBox()
        Me.ComboBoxVolumeOfProduction = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPostProcessingEffort = New System.Windows.Forms.ComboBox()
        Me.ComboBoxIntendedUseOfPart = New System.Windows.Forms.ComboBox()
        Me.ComboBoxMaterial = New System.Windows.Forms.ComboBox()
        Me.CheckBoxDirectedEnergyDeposition = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPowederBedFusion = New System.Windows.Forms.CheckBox()
        Me.CheckBoxVatPhotopolymerisation = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBinderJetting = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMaterialJetting = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMaterialExtrusion = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxIMFP = New System.Windows.Forms.CheckBox()
        Me.groupRequirements = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelInstructions = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.groupRequirements.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolTip1
        '
        Me.toolTip1.AutoPopDelay = 2000
        Me.toolTip1.InitialDelay = 100
        Me.toolTip1.ReshowDelay = 10
        Me.toolTip1.ShowAlways = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 30)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Precision of Part"
        Me.toolTip1.SetToolTip(Me.Label1, "Select the required accuracy of the part." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 1mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 0.5mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium: 0.2" &
        "mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 0.1mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: 0.05mm")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 51)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Lead Time"
        Me.toolTip1.SetToolTip(Me.Label2, "Select the allowable lead time of the part." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 3 days" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 1 week" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Mediu" &
        "m: 3 weeks" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 2 months" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: 3 months")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 96)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(108, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Volume of Production"
        Me.toolTip1.SetToolTip(Me.Label3, "Select expected production quantity." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 5 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 6-50 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium:" &
        " 51-200 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 201-1000 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: +1000 units")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 73)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Post Processing Effort"
        Me.toolTip1.SetToolTip(Me.Label4, "Select level of post-processing required." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 1 hour" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 6 hours" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium" &
        ": 1 day" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 3 days" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: +1 week")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 118)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Intended Use of Part"
        Me.toolTip1.SetToolTip(Me.Label5, "Select the purpose of the part.")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 140)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Material"
        Me.toolTip1.SetToolTip(Me.Label6, "Select the base material type for manufacturing.")
        '
        'HighlightCheckBox
        '
        Me.HighlightCheckBox.AutoSize = True
        Me.HighlightCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.HighlightCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HighlightCheckBox.Location = New System.Drawing.Point(362, 184)
        Me.HighlightCheckBox.Margin = New System.Windows.Forms.Padding(2)
        Me.HighlightCheckBox.Name = "HighlightCheckBox"
        Me.HighlightCheckBox.Size = New System.Drawing.Size(117, 17)
        Me.HighlightCheckBox.TabIndex = 25
        Me.HighlightCheckBox.Text = "Highlight Overhang"
        Me.toolTip1.SetToolTip(Me.HighlightCheckBox, "Select a reference face in your model before running analysis." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "When checked, hig" &
        "hlight faces that exceed overhang angle.")
        Me.HighlightCheckBox.UseVisualStyleBackColor = True
        '
        'ButtonCompute
        '
        Me.ButtonCompute.Location = New System.Drawing.Point(17, 210)
        Me.ButtonCompute.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCompute.Name = "ButtonCompute"
        Me.ButtonCompute.Size = New System.Drawing.Size(121, 34)
        Me.ButtonCompute.TabIndex = 0
        Me.ButtonCompute.Text = "Compute"
        Me.ButtonCompute.UseVisualStyleBackColor = True
        '
        'ComboBoxPrecisionOfPart
        '
        Me.ComboBoxPrecisionOfPart.AllowDrop = True
        Me.ComboBoxPrecisionOfPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPrecisionOfPart.FormattingEnabled = True
        Me.ComboBoxPrecisionOfPart.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxPrecisionOfPart.Location = New System.Drawing.Point(133, 27)
        Me.ComboBoxPrecisionOfPart.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPrecisionOfPart.Name = "ComboBoxPrecisionOfPart"
        Me.ComboBoxPrecisionOfPart.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxPrecisionOfPart.TabIndex = 2
        '
        'ComboBoxLeadTime
        '
        Me.ComboBoxLeadTime.AllowDrop = True
        Me.ComboBoxLeadTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxLeadTime.FormattingEnabled = True
        Me.ComboBoxLeadTime.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxLeadTime.Location = New System.Drawing.Point(133, 49)
        Me.ComboBoxLeadTime.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxLeadTime.Name = "ComboBoxLeadTime"
        Me.ComboBoxLeadTime.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxLeadTime.TabIndex = 4
        '
        'ComboBoxVolumeOfProduction
        '
        Me.ComboBoxVolumeOfProduction.AllowDrop = True
        Me.ComboBoxVolumeOfProduction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxVolumeOfProduction.FormattingEnabled = True
        Me.ComboBoxVolumeOfProduction.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxVolumeOfProduction.Location = New System.Drawing.Point(133, 94)
        Me.ComboBoxVolumeOfProduction.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxVolumeOfProduction.Name = "ComboBoxVolumeOfProduction"
        Me.ComboBoxVolumeOfProduction.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxVolumeOfProduction.TabIndex = 8
        '
        'ComboBoxPostProcessingEffort
        '
        Me.ComboBoxPostProcessingEffort.AllowDrop = True
        Me.ComboBoxPostProcessingEffort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPostProcessingEffort.FormattingEnabled = True
        Me.ComboBoxPostProcessingEffort.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxPostProcessingEffort.Location = New System.Drawing.Point(133, 72)
        Me.ComboBoxPostProcessingEffort.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPostProcessingEffort.Name = "ComboBoxPostProcessingEffort"
        Me.ComboBoxPostProcessingEffort.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxPostProcessingEffort.TabIndex = 6
        '
        'ComboBoxIntendedUseOfPart
        '
        Me.ComboBoxIntendedUseOfPart.AllowDrop = True
        Me.ComboBoxIntendedUseOfPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxIntendedUseOfPart.FormattingEnabled = True
        Me.ComboBoxIntendedUseOfPart.Items.AddRange(New Object() {"Unique Custom Part", "Critical Spare Part", "Mass Production", "Functional Prototype", "Aesthetic Prototype"})
        Me.ComboBoxIntendedUseOfPart.Location = New System.Drawing.Point(133, 116)
        Me.ComboBoxIntendedUseOfPart.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxIntendedUseOfPart.Name = "ComboBoxIntendedUseOfPart"
        Me.ComboBoxIntendedUseOfPart.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxIntendedUseOfPart.TabIndex = 10
        '
        'ComboBoxMaterial
        '
        Me.ComboBoxMaterial.AllowDrop = True
        Me.ComboBoxMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxMaterial.FormattingEnabled = True
        Me.ComboBoxMaterial.Items.AddRange(New Object() {"Metal", "Plastic", "Ceramic"})
        Me.ComboBoxMaterial.Location = New System.Drawing.Point(133, 138)
        Me.ComboBoxMaterial.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxMaterial.Name = "ComboBoxMaterial"
        Me.ComboBoxMaterial.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxMaterial.TabIndex = 11
        '
        'CheckBoxDirectedEnergyDeposition
        '
        Me.CheckBoxDirectedEnergyDeposition.AutoSize = True
        Me.CheckBoxDirectedEnergyDeposition.Location = New System.Drawing.Point(4, 137)
        Me.CheckBoxDirectedEnergyDeposition.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxDirectedEnergyDeposition.Name = "CheckBoxDirectedEnergyDeposition"
        Me.CheckBoxDirectedEnergyDeposition.Size = New System.Drawing.Size(155, 17)
        Me.CheckBoxDirectedEnergyDeposition.TabIndex = 19
        Me.CheckBoxDirectedEnergyDeposition.Text = "Directed Energy Deposition"
        Me.CheckBoxDirectedEnergyDeposition.UseVisualStyleBackColor = True
        '
        'CheckBoxPowederBedFusion
        '
        Me.CheckBoxPowederBedFusion.AutoSize = True
        Me.CheckBoxPowederBedFusion.Location = New System.Drawing.Point(4, 115)
        Me.CheckBoxPowederBedFusion.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxPowederBedFusion.Name = "CheckBoxPowederBedFusion"
        Me.CheckBoxPowederBedFusion.Size = New System.Drawing.Size(118, 17)
        Me.CheckBoxPowederBedFusion.TabIndex = 18
        Me.CheckBoxPowederBedFusion.Text = "Powder Bed Fusion"
        Me.CheckBoxPowederBedFusion.UseVisualStyleBackColor = True
        '
        'CheckBoxVatPhotopolymerisation
        '
        Me.CheckBoxVatPhotopolymerisation.AutoSize = True
        Me.CheckBoxVatPhotopolymerisation.Location = New System.Drawing.Point(4, 93)
        Me.CheckBoxVatPhotopolymerisation.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxVatPhotopolymerisation.Name = "CheckBoxVatPhotopolymerisation"
        Me.CheckBoxVatPhotopolymerisation.Size = New System.Drawing.Size(139, 17)
        Me.CheckBoxVatPhotopolymerisation.TabIndex = 17
        Me.CheckBoxVatPhotopolymerisation.Text = "Vat Photopolymerisation"
        Me.CheckBoxVatPhotopolymerisation.UseVisualStyleBackColor = True
        '
        'CheckBoxBinderJetting
        '
        Me.CheckBoxBinderJetting.AutoSize = True
        Me.CheckBoxBinderJetting.Location = New System.Drawing.Point(4, 71)
        Me.CheckBoxBinderJetting.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxBinderJetting.Name = "CheckBoxBinderJetting"
        Me.CheckBoxBinderJetting.Size = New System.Drawing.Size(90, 17)
        Me.CheckBoxBinderJetting.TabIndex = 16
        Me.CheckBoxBinderJetting.Text = "Binder Jetting"
        Me.CheckBoxBinderJetting.UseVisualStyleBackColor = True
        '
        'CheckBoxMaterialJetting
        '
        Me.CheckBoxMaterialJetting.AutoSize = True
        Me.CheckBoxMaterialJetting.Location = New System.Drawing.Point(4, 49)
        Me.CheckBoxMaterialJetting.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxMaterialJetting.Name = "CheckBoxMaterialJetting"
        Me.CheckBoxMaterialJetting.Size = New System.Drawing.Size(97, 17)
        Me.CheckBoxMaterialJetting.TabIndex = 15
        Me.CheckBoxMaterialJetting.Text = "Material Jetting"
        Me.CheckBoxMaterialJetting.UseVisualStyleBackColor = True
        '
        'CheckBoxMaterialExtrusion
        '
        Me.CheckBoxMaterialExtrusion.AutoSize = True
        Me.CheckBoxMaterialExtrusion.Location = New System.Drawing.Point(4, 27)
        Me.CheckBoxMaterialExtrusion.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxMaterialExtrusion.Name = "CheckBoxMaterialExtrusion"
        Me.CheckBoxMaterialExtrusion.Size = New System.Drawing.Size(109, 17)
        Me.CheckBoxMaterialExtrusion.TabIndex = 14
        Me.CheckBoxMaterialExtrusion.Text = "Material Extrusion"
        Me.CheckBoxMaterialExtrusion.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxMaterialExtrusion)
        Me.GroupBox1.Controls.Add(Me.CheckBoxDirectedEnergyDeposition)
        Me.GroupBox1.Controls.Add(Me.CheckBoxMaterialJetting)
        Me.GroupBox1.Controls.Add(Me.CheckBoxPowederBedFusion)
        Me.GroupBox1.Controls.Add(Me.CheckBoxBinderJetting)
        Me.GroupBox1.Controls.Add(Me.CheckBoxVatPhotopolymerisation)
        Me.GroupBox1.Location = New System.Drawing.Point(309, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(183, 165)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Available AM Technology"
        '
        'CheckBoxIMFP
        '
        Me.CheckBoxIMFP.AutoSize = True
        Me.CheckBoxIMFP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIMFP.Location = New System.Drawing.Point(17, 184)
        Me.CheckBoxIMFP.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxIMFP.Name = "CheckBoxIMFP"
        Me.CheckBoxIMFP.Size = New System.Drawing.Size(187, 17)
        Me.CheckBoxIMFP.TabIndex = 22
        Me.CheckBoxIMFP.Text = "Non-Machinable Features Present"
        Me.CheckBoxIMFP.UseVisualStyleBackColor = True
        '
        'groupRequirements
        '
        Me.groupRequirements.Controls.Add(Me.Label6)
        Me.groupRequirements.Controls.Add(Me.ComboBoxMaterial)
        Me.groupRequirements.Controls.Add(Me.ComboBoxIntendedUseOfPart)
        Me.groupRequirements.Controls.Add(Me.Label5)
        Me.groupRequirements.Controls.Add(Me.ComboBoxVolumeOfProduction)
        Me.groupRequirements.Controls.Add(Me.Label3)
        Me.groupRequirements.Controls.Add(Me.ComboBoxPostProcessingEffort)
        Me.groupRequirements.Controls.Add(Me.Label4)
        Me.groupRequirements.Controls.Add(Me.ComboBoxLeadTime)
        Me.groupRequirements.Controls.Add(Me.Label2)
        Me.groupRequirements.Controls.Add(Me.ComboBoxPrecisionOfPart)
        Me.groupRequirements.Controls.Add(Me.Label1)
        Me.groupRequirements.Location = New System.Drawing.Point(11, 15)
        Me.groupRequirements.Margin = New System.Windows.Forms.Padding(2)
        Me.groupRequirements.Name = "groupRequirements"
        Me.groupRequirements.Padding = New System.Windows.Forms.Padding(2)
        Me.groupRequirements.Size = New System.Drawing.Size(269, 165)
        Me.groupRequirements.TabIndex = 23
        Me.groupRequirements.TabStop = False
        Me.groupRequirements.Text = "Input Part Requirements"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Button1.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button1.Location = New System.Drawing.Point(337, 210)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(142, 34)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Post-Processing Calculator"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'LabelInstructions
        '
        Me.LabelInstructions.AutoSize = True
        Me.LabelInstructions.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Italic)
        Me.LabelInstructions.ForeColor = System.Drawing.Color.Gray
        Me.LabelInstructions.Location = New System.Drawing.Point(8, 257)
        Me.LabelInstructions.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelInstructions.Name = "LabelInstructions"
        Me.LabelInstructions.Size = New System.Drawing.Size(300, 13)
        Me.LabelInstructions.TabIndex = 13
        Me.LabelInstructions.Text = "Tip: Select a face before clicking Compute to detect overhangs."
        '
        'mainUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 279)
        Me.Controls.Add(Me.LabelInstructions)
        Me.Controls.Add(Me.HighlightCheckBox)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBoxIMFP)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonCompute)
        Me.Controls.Add(Me.groupRequirements)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "mainUserForm"
        Me.Text = "AM Thinker - Analysing your parts and needs!"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.groupRequirements.ResumeLayout(False)
        Me.groupRequirements.PerformLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Underline all labels that act as tooltips
        Me.Label1.Font = New Font(Me.Label1.Font, FontStyle.Underline)
        Me.Label2.Font = New Font(Me.Label2.Font, FontStyle.Underline)
        Me.Label3.Font = New Font(Me.Label3.Font, FontStyle.Underline)
        Me.Label4.Font = New Font(Me.Label4.Font, FontStyle.Underline)
        Me.Label5.Font = New Font(Me.Label5.Font, FontStyle.Underline)
        Me.Label6.Font = New Font(Me.Label6.Font, FontStyle.Underline)
    End Sub

    'Form controls declaration
    Friend WithEvents ButtonCompute As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BindingSource1 As Windows.Forms.BindingSource
    Friend WithEvents ComboBoxPrecisionOfPart As Windows.Forms.ComboBox
    Friend WithEvents ComboBoxLeadTime As Windows.Forms.ComboBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents ComboBoxVolumeOfProduction As Windows.Forms.ComboBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents ComboBoxPostProcessingEffort As Windows.Forms.ComboBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents ComboBoxIntendedUseOfPart As Windows.Forms.ComboBox
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents ComboBoxMaterial As Windows.Forms.ComboBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents CheckBoxDirectedEnergyDeposition As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPowederBedFusion As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxVatPhotopolymerisation As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxBinderJetting As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMaterialJetting As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMaterialExtrusion As Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents CheckBoxIMFP As Windows.Forms.CheckBox
    Friend WithEvents groupRequirements As Windows.Forms.GroupBox
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents HighlightCheckBox As CheckBox
    Friend WithEvents LabelInstructions As Label
End Class
