Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq

Public Class SummaryForm
    Private scoredProfiles As List(Of ScoredProfile)
    Private isDetailsExpanded As Boolean = False
    Private Const MIN_COLLAPSE_GAP As Integer = 10   ' gap left between panel and button when collapsed
    Private Const BASE_FORM_HEIGHT As Integer = 235  ' use your existing base height
    Private DeveloperMode As Boolean = False

    ' Constructor to receive scored profiles
    Public Sub New(profiles As List(Of ScoredProfile))
        InitializeComponent()

        Me.scoredProfiles = profiles
        EnableDoubleBuffer(PanelDetails)
        EnableDoubleBuffer(ListBoxResults)
        ' Start collapsed and invisible
        PanelDetails.Height = 0
        PanelDetails.Visible = False
        PopulateResults()
    End Sub

    Private Sub EnableDoubleBuffer(ctrl As Control)
        Dim prop = GetType(Control).GetProperty("DoubleBuffered",
        Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        prop.SetValue(ctrl, True, Nothing)
    End Sub

    Private Sub PopulateResults()
        ListBoxResults.Items.Clear()

        ' Sort profiles by score descending from LINQ - transfers collections from main to our results form.
        Dim sortedProfiles = scoredProfiles.OrderByDescending(Function(p) p.Score).Take(6).ToList()

        ' Determine overall suitability
        Dim bestScore = sortedProfiles.First().Score
        If bestScore >= 70 Then
            LabelAssessment.Text = "Part is suitable for Additive Manufacturing"
            LabelAssessment.ForeColor = Color.FromArgb(46, 125, 50) ' Green
        ElseIf bestScore >= 40 Then
            LabelAssessment.Text = "Part is moderately suitable for Additive Manufacturing - Consider design modifications"
            LabelAssessment.ForeColor = Color.FromArgb(251, 140, 0) ' Orange
        Else
            LabelAssessment.Text = "Part is not recommended for Additive Manufacturing"
            LabelAssessment.ForeColor = Color.FromArgb(211, 47, 47) ' Red
        End If
        LabelAssessment.Font = New Font(LabelAssessment.Font, FontStyle.Bold)

        ' Add profiles to listbox
        For Each profile In sortedProfiles
            ListBoxResults.Items.Add(profile)
        Next

        ' Show best options
        Dim greenProfiles = sortedProfiles.Where(Function(p) p.Score >= 70).ToList()
        If greenProfiles.Any() Then
            Dim bestTechs = String.Join(", ", greenProfiles.Select(Function(p) p.Technology))
            LabelBestOptions.Text = bestTechs
            If greenProfiles.Count = 1 Then
                LabelBestOptionsHeader.Text = "Best Option:"
            Else
                LabelBestOptionsHeader.Text = "Best Options:"
            End If
        Else
            LabelBestOptions.Text = ""
            LabelBestOptionsHeader.Text = "No highly suitable options found"
        End If
    End Sub

    ' Custom drawing for colored circles in ListBox
    Private Sub ListBoxResults_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBoxResults.DrawItem
        If e.Index < 0 Then Return

        e.DrawBackground()

        Dim profile = CType(ListBoxResults.Items(e.Index), ScoredProfile)

        ' Determine circle color based on score
        Dim circleColor As Color
        If profile.Score >= 70 Then
            circleColor = Color.FromArgb(46, 125, 50) ' Green
        ElseIf profile.Score >= 40 Then
            circleColor = Color.FromArgb(251, 140, 0) ' Orange
        Else
            circleColor = Color.FromArgb(211, 47, 47) ' Red
        End If

        ' Draw colored circle
        'Using brush As New SolidBrush(circleColor)
        'e.Graphics.FillEllipse(brush, e.Bounds.Left + 5, e.Bounds.Top + 5, 15, 15)
        ' End Using
        Dim circleDiameter As Integer = 12
        Dim circleX As Integer = e.Bounds.Left + 5
        Dim circleY As Integer = e.Bounds.Top + (e.Bounds.Height - circleDiameter) \ 2
        Dim circleRect As New Rectangle(circleX, circleY, circleDiameter, circleDiameter)

        Using brush As New SolidBrush(circleColor)
            e.Graphics.FillEllipse(brush, circleRect)
        End Using
        Using pen As New Pen(Color.Black, 1)
            e.Graphics.DrawEllipse(pen, circleRect)
        End Using

        ' Draw text
        Dim text As String = $"{profile.Technology} - {profile.Material}: {profile.Score:F0}/100"
        Dim textRect As New Rectangle(circleRect.Right + 8, e.Bounds.Top, e.Bounds.Width - (circleRect.Right + 8), e.Bounds.Height)

        Using textBrush As New SolidBrush(Color.Black) ' force black for readability
            Using sf As New StringFormat()
                sf.LineAlignment = StringAlignment.Center
                sf.Alignment = StringAlignment.Near
                e.Graphics.DrawString(text, e.Font, textBrush, textRect, sf)
            End Using
        End Using

        e.DrawFocusRectangle()
    End Sub

    ' Details button click - expand/collapse
    Private Sub ButtonDetails_Click(sender As Object, e As EventArgs) Handles ButtonDetails.Click
        If isDetailsExpanded Then
            CollapseDetails()
        Else
            ExpandDetails()
        End If
    End Sub

    Private Sub ExpandDetails()
        PopulateDetailsPanel()

        ' measure content height
        Dim contentBottom As Integer = 0
        For Each ctrl As Control In PanelDetails.Controls
            contentBottom = Math.Max(contentBottom, ctrl.Bottom)
        Next

        Dim padBottom As Integer = 10 ' extra padding you asked for
        Dim panelTargetHeight As Integer = Math.Max(100, contentBottom + padBottom)

        ' put panel under the button
        PanelDetails.Location = New Point(ButtonDetails.Left, ButtonDetails.Bottom + 5)
        PanelDetails.Height = 0
        PanelDetails.Visible = True

        ' animate both panel and form height
        Dim timer As New Timer() With {.Interval = 10}
        ButtonDetails.Enabled = False
        Me.SuspendLayout()

        AddHandler timer.Tick, Sub()
                                   Dim stepSize As Integer = 12
                                   If PanelDetails.Height >= panelTargetHeight Then
                                       timer.Stop()
                                       timer.Dispose()
                                       PanelDetails.Height = panelTargetHeight
                                       Me.Height = BASE_FORM_HEIGHT + PanelDetails.Height + 25
                                       ButtonDetails.Enabled = True
                                       Me.ResumeLayout()
                                   Else
                                       PanelDetails.Height = Math.Min(PanelDetails.Height + stepSize, panelTargetHeight)
                                       Me.Height = BASE_FORM_HEIGHT + PanelDetails.Height + 10
                                   End If
                               End Sub

        timer.Start()

        ButtonDetails.Text = "▲ Hide Details"
        isDetailsExpanded = True
    End Sub

    Private Sub CollapseDetails()
        Dim timer As New Timer() With {.Interval = 10}
        ButtonDetails.Enabled = False
        Me.SuspendLayout()

        AddHandler timer.Tick, Sub()
                                   Dim stepSize As Integer = 12
                                   If PanelDetails.Height <= MIN_COLLAPSE_GAP Then
                                       timer.Stop()
                                       timer.Dispose()
                                       PanelDetails.Height = MIN_COLLAPSE_GAP
                                       ' keep the small visible strip so the button isn't covered
                                       Me.Height = BASE_FORM_HEIGHT + PanelDetails.Height + 20
                                       ButtonDetails.Enabled = True
                                       Me.ResumeLayout()
                                   Else
                                       PanelDetails.Height = Math.Max(PanelDetails.Height - stepSize, MIN_COLLAPSE_GAP)
                                       Me.Height = BASE_FORM_HEIGHT + PanelDetails.Height + 10
                                   End If
                               End Sub

        timer.Start()

        ButtonDetails.Text = "▼ Show Details"
        isDetailsExpanded = False
    End Sub

    Private Sub PopulateDetailsPanel()
        ' Clear existing controls
        PanelDetails.Controls.Clear()

        Dim yPosition As Integer = 10

        ' Section 1: Geometric Analysis
        Dim lblGeoTitle As New Label()
        lblGeoTitle.Text = "Geometric Analysis:"
        lblGeoTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblGeoTitle.Location = New Point(10, yPosition)
        lblGeoTitle.AutoSize = True
        PanelDetails.Controls.Add(lblGeoTitle)
        yPosition += 25

        If DeveloperMode Then
            Dim txtGeoData As New TextBox()
            txtGeoData.Multiline = True
            txtGeoData.ReadOnly = True
            txtGeoData.BorderStyle = BorderStyle.None
            txtGeoData.BackColor = Me.BackColor
            txtGeoData.Location = New Point(20, yPosition)
            txtGeoData.Size = New Size(420, 70)
            txtGeoData.Text = "Surface Area: [Placeholder] cm²" & vbCrLf &
                      "Volume: [Placeholder] cm³" & vbCrLf &
                      "Complexity Ratio: [Placeholder]" & vbCrLf &
                      "Bounding Box: [Placeholder]"
            PanelDetails.Controls.Add(txtGeoData)
        Else
            Dim lblGeoData As New Label()
            lblGeoData.Text = "Surface Area: [Placeholder] cm²" & vbCrLf &
                      "Volume: [Placeholder] cm³" & vbCrLf &
                      "Complexity Ratio: [Placeholder]" & vbCrLf &
                      "Bounding Box: [Placeholder]"
            lblGeoData.Location = New Point(20, yPosition)
            lblGeoData.Size = New Size(420, 70)
            PanelDetails.Controls.Add(lblGeoData)
        End If
        yPosition += 80

        ' Section 2: Technology Limitations
        Dim lblLimitTitle As New Label()
        lblLimitTitle.Text = "Why Other Technologies Scored Lower:"
        lblLimitTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblLimitTitle.Location = New Point(10, yPosition)
        lblLimitTitle.AutoSize = True
        PanelDetails.Controls.Add(lblLimitTitle)
        yPosition += 25

        Dim lblLimitData As New Label()
        lblLimitData.Text = "[Placeholder: Analysis of low-scoring technologies]" & vbCrLf &
                           "• Material Jetting: Score penalty due to [reason]" & vbCrLf &
                           "• Binder Jetting: Unsuitable because [reason]"
        lblLimitData.Location = New Point(20, yPosition)
        lblLimitData.Size = New Size(420, 60)
        PanelDetails.Controls.Add(lblLimitData)
        yPosition += 70

        ' Section 3: Improvements
        Dim lblImproveTitle As New Label()
        lblImproveTitle.Text = "Potential Design Improvements:"
        lblImproveTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblImproveTitle.Location = New Point(10, yPosition)
        lblImproveTitle.AutoSize = True
        PanelDetails.Controls.Add(lblImproveTitle)
        yPosition += 25

        Dim lblImproveData As New Label()
        lblImproveData.Text = "[Placeholder: Suggestions for design optimization]" & vbCrLf &
                             "• Consider reducing precision requirements" & vbCrLf &
                             "• Optimize part orientation to minimize overhangs"
        lblImproveData.Location = New Point(20, yPosition)
        lblImproveData.Size = New Size(420, 50)
        PanelDetails.Controls.Add(lblImproveData)
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property


End Class

' Helper class for scored profiles
Public Class ScoredProfile
    Public Property Technology As String
    Public Property Material As String
    Public Property Score As Double
End Class
