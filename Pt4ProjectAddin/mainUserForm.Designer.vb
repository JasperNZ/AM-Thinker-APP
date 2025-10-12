'------------------------------------------------------------------------------
' <summary>
'   Handles static design of the MainUserForm, including all labels, buttons, checkboxes and comboboxes.
'   Recommend changes done by using the Visual Studio Designer, do not allow auto-adjust of size due to text resizing issues.
'   Note the choice of singular materials needing to be justified and implementation of dropdownmenus for user-friendliness.
' </summary>
' <author>Jasper Koid</author>
' <created>24-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Drawing
Imports System.Windows.Forms


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainUserForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainUserForm))
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabelPrecisionOfPart = New System.Windows.Forms.Label()
        Me.LabelLeadTime = New System.Windows.Forms.Label()
        Me.LabelVolumeOfProduction = New System.Windows.Forms.Label()
        Me.LabelPostProcessingEffort = New System.Windows.Forms.Label()
        Me.LabelIntendedApplication = New System.Windows.Forms.Label()
        Me.LabelMaterial = New System.Windows.Forms.Label()
        Me.CheckBoxHighlight = New System.Windows.Forms.CheckBox()
        Me.ButtonCompute = New System.Windows.Forms.Button()
        Me.ComboBoxPrecisionOfPart = New System.Windows.Forms.ComboBox()
        Me.ComboBoxLeadTime = New System.Windows.Forms.ComboBox()
        Me.ComboBoxVolumeOfProduction = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPostProcessingEffort = New System.Windows.Forms.ComboBox()
        Me.ComboBoxIntendedApplication = New System.Windows.Forms.ComboBox()
        Me.ComboBoxMaterial = New System.Windows.Forms.ComboBox()
        Me.CheckBoxDirectedEnergyDeposition = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPowederBedFusion = New System.Windows.Forms.CheckBox()
        Me.CheckBoxVatPhotopolymerisation = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBinderJetting = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMaterialJetting = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMaterialExtrusion = New System.Windows.Forms.CheckBox()
        Me.GroupTechnologies = New System.Windows.Forms.GroupBox()
        Me.CheckBoxNonMachinableFeaturesPresent = New System.Windows.Forms.CheckBox()
        Me.GroupRequirements = New System.Windows.Forms.GroupBox()
        Me.ButtonPostProcess = New System.Windows.Forms.Button()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelInstructions = New System.Windows.Forms.Label()
        Me.GroupTechnologies.SuspendLayout()
        Me.GroupRequirements.SuspendLayout()
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
        'LabelPrecisionOfPart
        '
        Me.LabelPrecisionOfPart.AutoSize = True
        Me.LabelPrecisionOfPart.Location = New System.Drawing.Point(14, 30)
        Me.LabelPrecisionOfPart.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPrecisionOfPart.Name = "LabelPrecisionOfPart"
        Me.LabelPrecisionOfPart.Size = New System.Drawing.Size(84, 13)
        Me.LabelPrecisionOfPart.TabIndex = 1
        Me.LabelPrecisionOfPart.Text = "Precision of Part"
        Me.toolTip1.SetToolTip(Me.LabelPrecisionOfPart, "Select the required accuracy of the part." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 1mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 0.5mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium: 0.2" &
        "mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 0.1mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: 0.05mm")
        '
        'LabelLeadTime
        '
        Me.LabelLeadTime.AutoSize = True
        Me.LabelLeadTime.Location = New System.Drawing.Point(14, 51)
        Me.LabelLeadTime.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelLeadTime.Name = "LabelLeadTime"
        Me.LabelLeadTime.Size = New System.Drawing.Size(57, 13)
        Me.LabelLeadTime.TabIndex = 3
        Me.LabelLeadTime.Text = "Lead Time"
        Me.toolTip1.SetToolTip(Me.LabelLeadTime, "Select the allowable lead time of the part." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 3 days" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 1 week" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Mediu" &
        "m: 3 weeks" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 2 months" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: 3 months")
        '
        'LabelVolumeOfProduction
        '
        Me.LabelVolumeOfProduction.AutoSize = True
        Me.LabelVolumeOfProduction.Location = New System.Drawing.Point(14, 96)
        Me.LabelVolumeOfProduction.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelVolumeOfProduction.Name = "LabelVolumeOfProduction"
        Me.LabelVolumeOfProduction.Size = New System.Drawing.Size(108, 13)
        Me.LabelVolumeOfProduction.TabIndex = 7
        Me.LabelVolumeOfProduction.Text = "Volume of Production"
        Me.toolTip1.SetToolTip(Me.LabelVolumeOfProduction, "Select expected production quantity." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 5 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 6-50 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium:" &
        " 51-200 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 201-1000 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: +1000 units")
        '
        'LabelPostProcessingEffort
        '
        Me.LabelPostProcessingEffort.AutoSize = True
        Me.LabelPostProcessingEffort.Location = New System.Drawing.Point(14, 73)
        Me.LabelPostProcessingEffort.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPostProcessingEffort.Name = "LabelPostProcessingEffort"
        Me.LabelPostProcessingEffort.Size = New System.Drawing.Size(111, 13)
        Me.LabelPostProcessingEffort.TabIndex = 5
        Me.LabelPostProcessingEffort.Text = "Post-Processing Effort"
        Me.toolTip1.SetToolTip(Me.LabelPostProcessingEffort, "Select level of post-processing required." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 1 hour" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 6 hours" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium" &
        ": 1 day" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 3 days" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: +1 week")
        '
        'LabelIntendedApplication
        '
        Me.LabelIntendedApplication.AutoSize = True
        Me.LabelIntendedApplication.Location = New System.Drawing.Point(14, 118)
        Me.LabelIntendedApplication.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelIntendedApplication.Name = "LabelIntendedApplication"
        Me.LabelIntendedApplication.Size = New System.Drawing.Size(104, 13)
        Me.LabelIntendedApplication.TabIndex = 9
        Me.LabelIntendedApplication.Text = "Intended Application"
        Me.toolTip1.SetToolTip(Me.LabelIntendedApplication, "Select the purpose of the part.")
        '
        'LabelMaterial
        '
        Me.LabelMaterial.AutoSize = True
        Me.LabelMaterial.Location = New System.Drawing.Point(14, 140)
        Me.LabelMaterial.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelMaterial.Name = "LabelMaterial"
        Me.LabelMaterial.Size = New System.Drawing.Size(44, 13)
        Me.LabelMaterial.TabIndex = 12
        Me.LabelMaterial.Text = "Material"
        Me.toolTip1.SetToolTip(Me.LabelMaterial, "Select the base material type for manufacturing.")
        '
        'CheckBoxHighlight
        '
        Me.CheckBoxHighlight.AutoSize = True
        Me.CheckBoxHighlight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxHighlight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxHighlight.Location = New System.Drawing.Point(362, 184)
        Me.CheckBoxHighlight.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxHighlight.Name = "CheckBoxHighlight"
        Me.CheckBoxHighlight.Size = New System.Drawing.Size(117, 17)
        Me.CheckBoxHighlight.TabIndex = 25
        Me.CheckBoxHighlight.Text = "Highlight Overhang"
        Me.toolTip1.SetToolTip(Me.CheckBoxHighlight, "Select a reference face in your model before running analysis." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "When checked, hig" &
        "hlight faces that exceed overhang angle.")
        Me.CheckBoxHighlight.UseVisualStyleBackColor = True
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
        'ComboBoxIntendedApplication
        '
        Me.ComboBoxIntendedApplication.AllowDrop = True
        Me.ComboBoxIntendedApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxIntendedApplication.FormattingEnabled = True
        Me.ComboBoxIntendedApplication.Items.AddRange(New Object() {"Unique Custom Part", "Critical Spare Part", "Mass Production", "Functional Prototype", "Aesthetic Prototype"})
        Me.ComboBoxIntendedApplication.Location = New System.Drawing.Point(133, 116)
        Me.ComboBoxIntendedApplication.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxIntendedApplication.Name = "ComboBoxIntendedApplication"
        Me.ComboBoxIntendedApplication.Size = New System.Drawing.Size(122, 21)
        Me.ComboBoxIntendedApplication.TabIndex = 10
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
        'GroupTechnologies
        '
        Me.GroupTechnologies.Controls.Add(Me.CheckBoxMaterialExtrusion)
        Me.GroupTechnologies.Controls.Add(Me.CheckBoxDirectedEnergyDeposition)
        Me.GroupTechnologies.Controls.Add(Me.CheckBoxMaterialJetting)
        Me.GroupTechnologies.Controls.Add(Me.CheckBoxPowederBedFusion)
        Me.GroupTechnologies.Controls.Add(Me.CheckBoxBinderJetting)
        Me.GroupTechnologies.Controls.Add(Me.CheckBoxVatPhotopolymerisation)
        Me.GroupTechnologies.Location = New System.Drawing.Point(309, 15)
        Me.GroupTechnologies.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupTechnologies.Name = "GroupTechnologies"
        Me.GroupTechnologies.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupTechnologies.Size = New System.Drawing.Size(183, 165)
        Me.GroupTechnologies.TabIndex = 20
        Me.GroupTechnologies.TabStop = False
        Me.GroupTechnologies.Text = "Select Available AM Technology"
        '
        'CheckBoxNonMachinableFeaturesPresent
        '
        Me.CheckBoxNonMachinableFeaturesPresent.AutoSize = True
        Me.CheckBoxNonMachinableFeaturesPresent.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxNonMachinableFeaturesPresent.Location = New System.Drawing.Point(17, 184)
        Me.CheckBoxNonMachinableFeaturesPresent.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxNonMachinableFeaturesPresent.Name = "CheckBoxNonMachinableFeaturesPresent"
        Me.CheckBoxNonMachinableFeaturesPresent.Size = New System.Drawing.Size(187, 17)
        Me.CheckBoxNonMachinableFeaturesPresent.TabIndex = 22
        Me.CheckBoxNonMachinableFeaturesPresent.Text = "Non-Machinable Features Present"
        Me.CheckBoxNonMachinableFeaturesPresent.UseVisualStyleBackColor = True
        '
        'GroupRequirements
        '
        Me.GroupRequirements.Controls.Add(Me.LabelMaterial)
        Me.GroupRequirements.Controls.Add(Me.ComboBoxMaterial)
        Me.GroupRequirements.Controls.Add(Me.ComboBoxIntendedApplication)
        Me.GroupRequirements.Controls.Add(Me.LabelIntendedApplication)
        Me.GroupRequirements.Controls.Add(Me.ComboBoxVolumeOfProduction)
        Me.GroupRequirements.Controls.Add(Me.LabelVolumeOfProduction)
        Me.GroupRequirements.Controls.Add(Me.ComboBoxPostProcessingEffort)
        Me.GroupRequirements.Controls.Add(Me.LabelPostProcessingEffort)
        Me.GroupRequirements.Controls.Add(Me.ComboBoxLeadTime)
        Me.GroupRequirements.Controls.Add(Me.LabelLeadTime)
        Me.GroupRequirements.Controls.Add(Me.ComboBoxPrecisionOfPart)
        Me.GroupRequirements.Controls.Add(Me.LabelPrecisionOfPart)
        Me.GroupRequirements.Location = New System.Drawing.Point(11, 15)
        Me.GroupRequirements.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupRequirements.Name = "GroupRequirements"
        Me.GroupRequirements.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupRequirements.Size = New System.Drawing.Size(269, 165)
        Me.GroupRequirements.TabIndex = 23
        Me.GroupRequirements.TabStop = False
        Me.GroupRequirements.Text = "Input Part Requirements"
        '
        'ButtonPostProcess
        '
        Me.ButtonPostProcess.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.ButtonPostProcess.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.ButtonPostProcess.Location = New System.Drawing.Point(337, 210)
        Me.ButtonPostProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPostProcess.Name = "ButtonPostProcess"
        Me.ButtonPostProcess.Size = New System.Drawing.Size(142, 34)
        Me.ButtonPostProcess.TabIndex = 24
        Me.ButtonPostProcess.Text = "Post-Processing Calculator"
        Me.ButtonPostProcess.UseVisualStyleBackColor = False
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
        'MainUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 279)
        Me.Controls.Add(Me.LabelInstructions)
        Me.Controls.Add(Me.CheckBoxHighlight)
        Me.Controls.Add(Me.ButtonPostProcess)
        Me.Controls.Add(Me.CheckBoxNonMachinableFeaturesPresent)
        Me.Controls.Add(Me.GroupTechnologies)
        Me.Controls.Add(Me.ButtonCompute)
        Me.Controls.Add(Me.GroupRequirements)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "MainUserForm"
        Me.Text = "AM Thinker - Analysing your parts and needs!"
        Me.GroupTechnologies.ResumeLayout(False)
        Me.GroupTechnologies.PerformLayout()
        Me.GroupRequirements.ResumeLayout(False)
        Me.GroupRequirements.PerformLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Underline all labels that act as tooltips
        Me.LabelPrecisionOfPart.Font = New Font(Me.LabelPrecisionOfPart.Font, FontStyle.Underline)
        Me.LabelLeadTime.Font = New Font(Me.LabelLeadTime.Font, FontStyle.Underline)
        Me.LabelVolumeOfProduction.Font = New Font(Me.LabelVolumeOfProduction.Font, FontStyle.Underline)
        Me.LabelPostProcessingEffort.Font = New Font(Me.LabelPostProcessingEffort.Font, FontStyle.Underline)
        Me.LabelIntendedApplication.Font = New Font(Me.LabelIntendedApplication.Font, FontStyle.Underline)
        Me.LabelMaterial.Font = New Font(Me.LabelMaterial.Font, FontStyle.Underline)
    End Sub

    'Form controls declaration
    Friend WithEvents ButtonCompute As Windows.Forms.Button
    Friend WithEvents LabelPrecisionOfPart As Windows.Forms.Label
    Friend WithEvents BindingSource1 As Windows.Forms.BindingSource
    Friend WithEvents ComboBoxPrecisionOfPart As Windows.Forms.ComboBox
    Friend WithEvents ComboBoxLeadTime As Windows.Forms.ComboBox
    Friend WithEvents LabelLeadTime As Windows.Forms.Label
    Friend WithEvents ComboBoxVolumeOfProduction As Windows.Forms.ComboBox
    Friend WithEvents LabelVolumeOfProduction As Windows.Forms.Label
    Friend WithEvents ComboBoxPostProcessingEffort As Windows.Forms.ComboBox
    Friend WithEvents LabelPostProcessingEffort As Windows.Forms.Label
    Friend WithEvents ComboBoxIntendedApplication As Windows.Forms.ComboBox
    Friend WithEvents LabelIntendedApplication As Windows.Forms.Label
    Friend WithEvents ComboBoxMaterial As Windows.Forms.ComboBox
    Friend WithEvents LabelMaterial As Windows.Forms.Label
    Friend WithEvents CheckBoxDirectedEnergyDeposition As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPowederBedFusion As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxVatPhotopolymerisation As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxBinderJetting As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMaterialJetting As Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMaterialExtrusion As Windows.Forms.CheckBox
    Friend WithEvents GroupTechnologies As Windows.Forms.GroupBox
    Friend WithEvents CheckBoxNonMachinableFeaturesPresent As Windows.Forms.CheckBox
    Friend WithEvents GroupRequirements As Windows.Forms.GroupBox
    Friend WithEvents ButtonPostProcess As Windows.Forms.Button
    Friend WithEvents CheckBoxHighlight As CheckBox
    Friend WithEvents LabelInstructions As Label
End Class
