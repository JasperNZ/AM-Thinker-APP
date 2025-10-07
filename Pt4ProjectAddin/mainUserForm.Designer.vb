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
        Me.Label1.Location = New System.Drawing.Point(21, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Precision of Part"
        Me.toolTip1.SetToolTip(Me.Label1, "Select the required accuracy of the part." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 1mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 0.5mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium: 0.2mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 0.1mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: 0.05mm")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Lead Time"
        Me.toolTip1.SetToolTip(Me.Label2, "Select the allowable lead time of the part." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 3 days" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 1 week" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium: 3 weeks" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 2 months" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: 3 months")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Volume of Production"
        Me.toolTip1.SetToolTip(Me.Label3, "Select expected production quantity." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 5 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 6-50 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium: 51-200 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 201-1000 units" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: +1000 units")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(167, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Post Processing Effort"
        Me.toolTip1.SetToolTip(Me.Label4, "Select level of post-processing required." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very Low: 1 hour" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Low: 6 hours" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Medium: 1 day" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "High: 3 days" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Very High: +1 week")
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 182)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 20)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Intended Use of Part"
        Me.toolTip1.SetToolTip(Me.Label5, "Select the purpose of the part.")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 215)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Material"
        Me.toolTip1.SetToolTip(Me.Label6, "Select the base material type for manufacturing.")
        '
        'ButtonCompute
        '
        Me.ButtonCompute.Location = New System.Drawing.Point(26, 323)
        Me.ButtonCompute.Name = "ButtonCompute"
        Me.ButtonCompute.Size = New System.Drawing.Size(182, 52)
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
        Me.ComboBoxPrecisionOfPart.Location = New System.Drawing.Point(200, 42)
        Me.ComboBoxPrecisionOfPart.Name = "ComboBoxPrecisionOfPart"
        Me.ComboBoxPrecisionOfPart.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxPrecisionOfPart.TabIndex = 2
        '
        'ComboBoxLeadTime
        '
        Me.ComboBoxLeadTime.AllowDrop = True
        Me.ComboBoxLeadTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxLeadTime.FormattingEnabled = True
        Me.ComboBoxLeadTime.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxLeadTime.Location = New System.Drawing.Point(200, 75)
        Me.ComboBoxLeadTime.Name = "ComboBoxLeadTime"
        Me.ComboBoxLeadTime.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxLeadTime.TabIndex = 4
        '
        'ComboBoxVolumeOfProduction
        '
        Me.ComboBoxVolumeOfProduction.AllowDrop = True
        Me.ComboBoxVolumeOfProduction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxVolumeOfProduction.FormattingEnabled = True
        Me.ComboBoxVolumeOfProduction.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxVolumeOfProduction.Location = New System.Drawing.Point(200, 145)
        Me.ComboBoxVolumeOfProduction.Name = "ComboBoxVolumeOfProduction"
        Me.ComboBoxVolumeOfProduction.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxVolumeOfProduction.TabIndex = 8
        '
        'ComboBoxPostProcessingEffort
        '
        Me.ComboBoxPostProcessingEffort.AllowDrop = True
        Me.ComboBoxPostProcessingEffort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPostProcessingEffort.FormattingEnabled = True
        Me.ComboBoxPostProcessingEffort.Items.AddRange(New Object() {"Very Low", "Low", "Medium", "High", "Very High"})
        Me.ComboBoxPostProcessingEffort.Location = New System.Drawing.Point(200, 111)
        Me.ComboBoxPostProcessingEffort.Name = "ComboBoxPostProcessingEffort"
        Me.ComboBoxPostProcessingEffort.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxPostProcessingEffort.TabIndex = 6
        '
        'ComboBoxIntendedUseOfPart
        '
        Me.ComboBoxIntendedUseOfPart.AllowDrop = True
        Me.ComboBoxIntendedUseOfPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxIntendedUseOfPart.FormattingEnabled = True
        Me.ComboBoxIntendedUseOfPart.Items.AddRange(New Object() {"Unique Custom Part", "Critical Spare Part", "Mass Production", "Functional Prototype", "Aesthetic Prototype"})
        Me.ComboBoxIntendedUseOfPart.Location = New System.Drawing.Point(200, 178)
        Me.ComboBoxIntendedUseOfPart.Name = "ComboBoxIntendedUseOfPart"
        Me.ComboBoxIntendedUseOfPart.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxIntendedUseOfPart.TabIndex = 10
        '
        'ComboBoxMaterial
        '
        Me.ComboBoxMaterial.AllowDrop = True
        Me.ComboBoxMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxMaterial.FormattingEnabled = True
        Me.ComboBoxMaterial.Items.AddRange(New Object() {"Metal", "Plastic", "Ceramic"})
        Me.ComboBoxMaterial.Location = New System.Drawing.Point(200, 212)
        Me.ComboBoxMaterial.Name = "ComboBoxMaterial"
        Me.ComboBoxMaterial.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxMaterial.TabIndex = 11
        '
        'CheckBoxDirectedEnergyDeposition
        '
        Me.CheckBoxDirectedEnergyDeposition.AutoSize = True
        Me.CheckBoxDirectedEnergyDeposition.Location = New System.Drawing.Point(6, 211)
        Me.CheckBoxDirectedEnergyDeposition.Name = "CheckBoxDirectedEnergyDeposition"
        Me.CheckBoxDirectedEnergyDeposition.Size = New System.Drawing.Size(229, 24)
        Me.CheckBoxDirectedEnergyDeposition.TabIndex = 19
        Me.CheckBoxDirectedEnergyDeposition.Text = "Directed Energy Deposition"
        Me.CheckBoxDirectedEnergyDeposition.UseVisualStyleBackColor = True
        '
        'CheckBoxPowederBedFusion
        '
        Me.CheckBoxPowederBedFusion.AutoSize = True
        Me.CheckBoxPowederBedFusion.Location = New System.Drawing.Point(6, 177)
        Me.CheckBoxPowederBedFusion.Name = "CheckBoxPowederBedFusion"
        Me.CheckBoxPowederBedFusion.Size = New System.Drawing.Size(173, 24)
        Me.CheckBoxPowederBedFusion.TabIndex = 18
        Me.CheckBoxPowederBedFusion.Text = "Powder Bed Fusion"
        Me.CheckBoxPowederBedFusion.UseVisualStyleBackColor = True
        '
        'CheckBoxVatPhotopolymerisation
        '
        Me.CheckBoxVatPhotopolymerisation.AutoSize = True
        Me.CheckBoxVatPhotopolymerisation.Location = New System.Drawing.Point(6, 143)
        Me.CheckBoxVatPhotopolymerisation.Name = "CheckBoxVatPhotopolymerisation"
        Me.CheckBoxVatPhotopolymerisation.Size = New System.Drawing.Size(207, 24)
        Me.CheckBoxVatPhotopolymerisation.TabIndex = 17
        Me.CheckBoxVatPhotopolymerisation.Text = "Vat Photopolymerisation"
        Me.CheckBoxVatPhotopolymerisation.UseVisualStyleBackColor = True
        '
        'CheckBoxBinderJetting
        '
        Me.CheckBoxBinderJetting.AutoSize = True
        Me.CheckBoxBinderJetting.Location = New System.Drawing.Point(6, 109)
        Me.CheckBoxBinderJetting.Name = "CheckBoxBinderJetting"
        Me.CheckBoxBinderJetting.Size = New System.Drawing.Size(133, 24)
        Me.CheckBoxBinderJetting.TabIndex = 16
        Me.CheckBoxBinderJetting.Text = "Binder Jetting"
        Me.CheckBoxBinderJetting.UseVisualStyleBackColor = True
        '
        'CheckBoxMaterialJetting
        '
        Me.CheckBoxMaterialJetting.AutoSize = True
        Me.CheckBoxMaterialJetting.Location = New System.Drawing.Point(6, 75)
        Me.CheckBoxMaterialJetting.Name = "CheckBoxMaterialJetting"
        Me.CheckBoxMaterialJetting.Size = New System.Drawing.Size(143, 24)
        Me.CheckBoxMaterialJetting.TabIndex = 15
        Me.CheckBoxMaterialJetting.Text = "Material Jetting"
        Me.CheckBoxMaterialJetting.UseVisualStyleBackColor = True
        '
        'CheckBoxMaterialExtrusion
        '
        Me.CheckBoxMaterialExtrusion.AutoSize = True
        Me.CheckBoxMaterialExtrusion.Location = New System.Drawing.Point(6, 42)
        Me.CheckBoxMaterialExtrusion.Name = "CheckBoxMaterialExtrusion"
        Me.CheckBoxMaterialExtrusion.Size = New System.Drawing.Size(161, 24)
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
        Me.GroupBox1.Location = New System.Drawing.Point(464, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(274, 254)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Available AM Technology"
        '
        'CheckBoxIMFP
        '
        Me.CheckBoxIMFP.AutoSize = True
        Me.CheckBoxIMFP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIMFP.Location = New System.Drawing.Point(26, 283)
        Me.CheckBoxIMFP.Name = "CheckBoxIMFP"
        Me.CheckBoxIMFP.Size = New System.Drawing.Size(314, 24)
        Me.CheckBoxIMFP.TabIndex = 22
        Me.CheckBoxIMFP.Text = "Impossible Machining Features Present"
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
        Me.groupRequirements.Location = New System.Drawing.Point(16, 23)
        Me.groupRequirements.Name = "groupRequirements"
        Me.groupRequirements.Size = New System.Drawing.Size(404, 254)
        Me.groupRequirements.TabIndex = 23
        Me.groupRequirements.TabStop = False
        Me.groupRequirements.Text = "Input Part Requirements"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Button1.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.Button1.Location = New System.Drawing.Point(506, 323)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(213, 52)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Post-Processing Calculator"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'mainUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 392)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBoxIMFP)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonCompute)
        Me.Controls.Add(Me.groupRequirements)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
End Class
