<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mainUserForm
    Inherits System.Windows.Forms.Form

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
        Me.ButtonCompute = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ComboBoxPrecisionOfPart = New System.Windows.Forms.ComboBox()
        Me.ComboBoxLeadTimeSignificance = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxVolumeOfProduction = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxPostProcessingEffort = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxIntendedUseOfPart = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxMaterial = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxDirectedEnergyDeposition = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPowederBedFusion = New System.Windows.Forms.CheckBox()
        Me.CheckBoxVatPhotopolymerisation = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBinderJetting = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMaterialJetting = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMaterialExtrusion = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxIMFP = New System.Windows.Forms.CheckBox()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonCompute
        '
        Me.ButtonCompute.Location = New System.Drawing.Point(25, 323)
        Me.ButtonCompute.Name = "ButtonCompute"
        Me.ButtonCompute.Size = New System.Drawing.Size(182, 52)
        Me.ButtonCompute.TabIndex = 0
        Me.ButtonCompute.Text = "Compute"
        Me.ButtonCompute.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Precision of Part"
        '
        'BindingSource1
        '
        '
        'ComboBoxPrecisionOfPart
        '
        Me.ComboBoxPrecisionOfPart.AllowDrop = True
        Me.ComboBoxPrecisionOfPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPrecisionOfPart.FormattingEnabled = True
        Me.ComboBoxPrecisionOfPart.Items.AddRange(New Object() {"High", "Medium", "Low"})
        Me.ComboBoxPrecisionOfPart.Location = New System.Drawing.Point(199, 42)
        Me.ComboBoxPrecisionOfPart.Name = "ComboBoxPrecisionOfPart"
        Me.ComboBoxPrecisionOfPart.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxPrecisionOfPart.TabIndex = 2
        '
        'ComboBoxLeadTimeSignificance
        '
        Me.ComboBoxLeadTimeSignificance.AllowDrop = True
        Me.ComboBoxLeadTimeSignificance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxLeadTimeSignificance.FormattingEnabled = True
        Me.ComboBoxLeadTimeSignificance.Items.AddRange(New Object() {"High", "Medium", "Low"})
        Me.ComboBoxLeadTimeSignificance.Location = New System.Drawing.Point(199, 76)
        Me.ComboBoxLeadTimeSignificance.Name = "ComboBoxLeadTimeSignificance"
        Me.ComboBoxLeadTimeSignificance.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxLeadTimeSignificance.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(173, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Lead Time Significance"
        '
        'ComboBoxVolumeOfProduction
        '
        Me.ComboBoxVolumeOfProduction.AllowDrop = True
        Me.ComboBoxVolumeOfProduction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxVolumeOfProduction.FormattingEnabled = True
        Me.ComboBoxVolumeOfProduction.Items.AddRange(New Object() {"One Off Part", "Low Volume Production", "High Volume Production"})
        Me.ComboBoxVolumeOfProduction.Location = New System.Drawing.Point(199, 144)
        Me.ComboBoxVolumeOfProduction.Name = "ComboBoxVolumeOfProduction"
        Me.ComboBoxVolumeOfProduction.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxVolumeOfProduction.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 147)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Volume of Production"
        '
        'ComboBoxPostProcessingEffort
        '
        Me.ComboBoxPostProcessingEffort.AllowDrop = True
        Me.ComboBoxPostProcessingEffort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPostProcessingEffort.FormattingEnabled = True
        Me.ComboBoxPostProcessingEffort.Items.AddRange(New Object() {"High", "Medium", "Low"})
        Me.ComboBoxPostProcessingEffort.Location = New System.Drawing.Point(199, 110)
        Me.ComboBoxPostProcessingEffort.Name = "ComboBoxPostProcessingEffort"
        Me.ComboBoxPostProcessingEffort.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxPostProcessingEffort.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(167, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Post Processing Effort"
        '
        'ComboBoxIntendedUseOfPart
        '
        Me.ComboBoxIntendedUseOfPart.AllowDrop = True
        Me.ComboBoxIntendedUseOfPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxIntendedUseOfPart.FormattingEnabled = True
        Me.ComboBoxIntendedUseOfPart.Items.AddRange(New Object() {"Unique Custom Part", "Critical Spare part", "Mass Production", "Prototype"})
        Me.ComboBoxIntendedUseOfPart.Location = New System.Drawing.Point(199, 178)
        Me.ComboBoxIntendedUseOfPart.Name = "ComboBoxIntendedUseOfPart"
        Me.ComboBoxIntendedUseOfPart.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxIntendedUseOfPart.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 181)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 20)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Intended Use of Part"
        '
        'ComboBoxMaterial
        '
        Me.ComboBoxMaterial.AllowDrop = True
        Me.ComboBoxMaterial.FormattingEnabled = True
        Me.ComboBoxMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxMaterial.Items.AddRange(New Object() {"Metal", "Plastic", "Ceramic"})
        Me.ComboBoxMaterial.Location = New System.Drawing.Point(199, 212)
        Me.ComboBoxMaterial.Name = "ComboBoxMaterial"
        Me.ComboBoxMaterial.Size = New System.Drawing.Size(181, 28)
        Me.ComboBoxMaterial.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 215)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Material"
        '
        'CheckBoxDirectedEnergyDeposition
        '
        Me.CheckBoxDirectedEnergyDeposition.AutoSize = True
        Me.CheckBoxDirectedEnergyDeposition.Location = New System.Drawing.Point(6, 175)
        Me.CheckBoxDirectedEnergyDeposition.Name = "CheckBoxDirectedEnergyDeposition"
        Me.CheckBoxDirectedEnergyDeposition.Size = New System.Drawing.Size(229, 24)
        Me.CheckBoxDirectedEnergyDeposition.TabIndex = 19
        Me.CheckBoxDirectedEnergyDeposition.Text = "Directed Energy Deposition"
        Me.CheckBoxDirectedEnergyDeposition.UseVisualStyleBackColor = True
        '
        'CheckBoxPowederBedFusion
        '
        Me.CheckBoxPowederBedFusion.AutoSize = True
        Me.CheckBoxPowederBedFusion.Location = New System.Drawing.Point(6, 145)
        Me.CheckBoxPowederBedFusion.Name = "CheckBoxPowederBedFusion"
        Me.CheckBoxPowederBedFusion.Size = New System.Drawing.Size(173, 24)
        Me.CheckBoxPowederBedFusion.TabIndex = 18
        Me.CheckBoxPowederBedFusion.Text = "Powder Bed Fusion"
        Me.CheckBoxPowederBedFusion.UseVisualStyleBackColor = True
        '
        'CheckBoxVatPhotopolymerisation
        '
        Me.CheckBoxVatPhotopolymerisation.AutoSize = True
        Me.CheckBoxVatPhotopolymerisation.Location = New System.Drawing.Point(6, 115)
        Me.CheckBoxVatPhotopolymerisation.Name = "CheckBoxVatPhotopolymerisation"
        Me.CheckBoxVatPhotopolymerisation.Size = New System.Drawing.Size(207, 24)
        Me.CheckBoxVatPhotopolymerisation.TabIndex = 17
        Me.CheckBoxVatPhotopolymerisation.Text = "Vat Photopolymerisation"
        Me.CheckBoxVatPhotopolymerisation.UseVisualStyleBackColor = True
        '
        'CheckBoxBinderJetting
        '
        Me.CheckBoxBinderJetting.AutoSize = True
        Me.CheckBoxBinderJetting.Location = New System.Drawing.Point(6, 85)
        Me.CheckBoxBinderJetting.Name = "CheckBoxBinderJetting"
        Me.CheckBoxBinderJetting.Size = New System.Drawing.Size(133, 24)
        Me.CheckBoxBinderJetting.TabIndex = 16
        Me.CheckBoxBinderJetting.Text = "Binder Jetting"
        Me.CheckBoxBinderJetting.UseVisualStyleBackColor = True
        '
        'CheckBoxMaterialJetting
        '
        Me.CheckBoxMaterialJetting.AutoSize = True
        Me.CheckBoxMaterialJetting.Location = New System.Drawing.Point(6, 55)
        Me.CheckBoxMaterialJetting.Name = "CheckBoxMaterialJetting"
        Me.CheckBoxMaterialJetting.Size = New System.Drawing.Size(143, 24)
        Me.CheckBoxMaterialJetting.TabIndex = 15
        Me.CheckBoxMaterialJetting.Text = "Material Jetting"
        Me.CheckBoxMaterialJetting.UseVisualStyleBackColor = True
        '
        'CheckBoxMaterialExtrusion
        '
        Me.CheckBoxMaterialExtrusion.AutoSize = True
        Me.CheckBoxMaterialExtrusion.Location = New System.Drawing.Point(6, 25)
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
        Me.GroupBox1.Location = New System.Drawing.Point(434, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(252, 210)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Available AM Technology"
        '
        'CheckBoxIMFP
        '
        Me.CheckBoxIMFP.AutoSize = True
        Me.CheckBoxIMFP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIMFP.Location = New System.Drawing.Point(25, 272)
        Me.CheckBoxIMFP.Name = "CheckBoxIMFP"
        Me.CheckBoxIMFP.Size = New System.Drawing.Size(314, 24)
        Me.CheckBoxIMFP.TabIndex = 22
        Me.CheckBoxIMFP.Text = "Impossible Machining Features Present"
        Me.CheckBoxIMFP.UseVisualStyleBackColor = True
        '
        'mainUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 393)
        Me.Controls.Add(Me.CheckBoxIMFP)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBoxMaterial)
        Me.Controls.Add(Me.ComboBoxIntendedUseOfPart)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxVolumeOfProduction)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxPostProcessingEffort)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBoxLeadTimeSignificance)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBoxPrecisionOfPart)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonCompute)
        Me.Name = "mainUserForm"
        Me.Text = "AM Thinker - Analyzing your parts and needs!"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonCompute As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BindingSource1 As Windows.Forms.BindingSource
    Friend WithEvents ComboBoxPrecisionOfPart As Windows.Forms.ComboBox
    Friend WithEvents ComboBoxLeadTimeSignificance As Windows.Forms.ComboBox
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
End Class
