Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq

Public Class SummaryForm
    Private scoredProfiles As List(Of ScoredProfile)
    Private isDetailsExpanded As Boolean = False
    Private DeveloperMode As Boolean = False

    ' Store the COLLAPSED form height (measured once at form load)
    Private collapsedFormHeight As Integer

    ' Constants for spacing (relative units work better than absolute)
    Private Const BUTTON_BOTTOM_MARGIN As Integer = 15  ' Distance from button to form bottom when collapsed
    Private Const PANEL_BUTTON_GAP As Integer = 5       ' Gap between button and panel
    Private isAnimating As Boolean = False
    Public Sub New(profiles As List(Of ScoredProfile))
        InitializeComponent()

        Me.scoredProfiles = profiles
        EnableDoubleBuffer(PanelDetails)
        EnableDoubleBuffer(ListBoxResults)

        ' Initial setup - panel invisible and collapsed
        PanelDetails.Height = 0
        PanelDetails.Visible = False

        PopulateResults()

        ' CRITICAL: Capture the collapsed height AFTER form is fully loaded
        AddHandler Me.Load, Sub(s, e)
                                ' Ensure PanelDetails doesn't automatically stretch with form height
                                'PanelDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

                                ' Position button at bottom with margin (initial collapsed placement)
                                ButtonDetails.Top = Me.ClientSize.Height - ButtonDetails.Height - BUTTON_BOTTOM_MARGIN
                                ButtonDetails.Left = 12

                                ' Position panel directly under button (but invisible)
                                PanelDetails.Top = ButtonDetails.Bottom + PANEL_BUTTON_GAP
                                PanelDetails.Left = ButtonDetails.Left
                                PanelDetails.Width = Me.ClientSize.Width - 2 * ButtonDetails.Left

                                ' Store this as our reference collapsed height
                                collapsedFormHeight = Me.Height
                            End Sub
    End Sub

    Private Sub EnableDoubleBuffer(ctrl As Control)
        Dim prop = GetType(Control).GetProperty("DoubleBuffered",
        Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        prop.SetValue(ctrl, True, Nothing)
    End Sub

    Private Sub PopulateResults()
        ListBoxResults.Items.Clear()

        Dim sortedProfiles = scoredProfiles.OrderByDescending(Function(p) p.Score).Take(6).ToList()

        ' Determine overall suitability
        Dim bestScore = sortedProfiles.First().Score
        If bestScore >= 70 Then
            LabelAssessment.Text = "Part is suitable for Additive Manufacturing"
            LabelAssessment.ForeColor = Color.FromArgb(46, 125, 50)
        ElseIf bestScore >= 40 Then
            LabelAssessment.Text = "Part is moderately suitable for Additive Manufacturing - Consider design modifications"
            LabelAssessment.ForeColor = Color.FromArgb(251, 140, 0)
        Else
            LabelAssessment.Text = "Part is not recommended for Additive Manufacturing"
            LabelAssessment.ForeColor = Color.FromArgb(211, 47, 47)
        End If
        LabelAssessment.Font = New Font(LabelAssessment.Font, FontStyle.Bold)

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

    Private Sub ListBoxResults_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBoxResults.DrawItem
        If e.Index < 0 Then Return

        e.DrawBackground()

        Dim profile = CType(ListBoxResults.Items(e.Index), ScoredProfile)

        Dim circleColor As Color
        If profile.Score >= 70 Then
            circleColor = Color.FromArgb(46, 125, 50)
        ElseIf profile.Score >= 40 Then
            circleColor = Color.FromArgb(251, 140, 0)
        Else
            circleColor = Color.FromArgb(211, 47, 47)
        End If

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

        Dim text As String = $"{profile.Technology} - {profile.Material}: {profile.Score:F0}/100"
        Dim textRect As New Rectangle(circleRect.Right + 8, e.Bounds.Top, e.Bounds.Width - (circleRect.Right + 8), e.Bounds.Height)

        Using textBrush As New SolidBrush(Color.Black)
            Using sf As New StringFormat()
                sf.LineAlignment = StringAlignment.Center
                sf.Alignment = StringAlignment.Near
                e.Graphics.DrawString(text, e.Font, textBrush, textRect, sf)
            End Using
        End Using

        e.DrawFocusRectangle()
    End Sub

    Private Sub ButtonDetails_Click(sender As Object, e As EventArgs) Handles ButtonDetails.Click
        If isDetailsExpanded Then
            CollapseDetails()
        Else
            ExpandDetails()
        End If
    End Sub

    Private Sub ExpandDetails()
        ' Ensure panel width is correct before creating content
        PanelDetails.Width = Me.ClientSize.Width - 2 * PanelDetails.Left

        PopulateDetailsPanel()

        ' Compute actual content bottom (we set AutoScrollMinSize, but still compute)
        Dim contentBottom As Integer = 0
        For Each ctrl As Control In PanelDetails.Controls
            If ctrl.Visible Then
                contentBottom = Math.Max(contentBottom, ctrl.Bottom)
            End If
        Next

        Dim paddingBottom As Integer = 15
        Dim targetPanelHeight As Integer = Math.Max(100, contentBottom + paddingBottom)

        ' Prepare for animation
        PanelDetails.Height = 0
        PanelDetails.Visible = True

        Dim timer As New Timer() With {.Interval = 10}
        ButtonDetails.Enabled = False
        isAnimating = True

        Dim targetFormHeight As Integer = collapsedFormHeight + targetPanelHeight

        AddHandler timer.Tick, Sub()
                                   Dim stepSize As Integer = Math.Max(12, targetPanelHeight \ 25)

                                   If PanelDetails.Height >= targetPanelHeight Then
                                       timer.Stop()
                                       timer.Dispose()
                                       PanelDetails.Height = targetPanelHeight
                                       Me.Height = targetFormHeight
                                       ButtonDetails.Enabled = True
                                       isAnimating = False
                                       isDetailsExpanded = True  ' flip only after animation finishes
                                   Else
                                       Dim newPanelHeight = Math.Min(PanelDetails.Height + stepSize, targetPanelHeight)
                                       Dim heightIncrease = newPanelHeight - PanelDetails.Height

                                       PanelDetails.Height = newPanelHeight
                                       Me.Height = Me.Height + heightIncrease
                                   End If
                               End Sub

        timer.Start()

        ButtonDetails.Text = "▲ Hide Details"
    End Sub


    Private Sub CollapseDetails()
        Dim timer As New Timer() With {.Interval = 10}
        ButtonDetails.Enabled = False
        isAnimating = True

        Dim startingPanelHeight = PanelDetails.Height

        AddHandler timer.Tick, Sub()
                                   Dim stepSize As Integer = Math.Max(12, Math.Max(1, startingPanelHeight \ 25))

                                   If PanelDetails.Height <= 0 Then
                                       timer.Stop()
                                       timer.Dispose()
                                       PanelDetails.Height = 0
                                       PanelDetails.Visible = False
                                       Me.Height = collapsedFormHeight
                                       ButtonDetails.Enabled = True
                                       isAnimating = False
                                       isDetailsExpanded = False  ' flip only after animation completes
                                   Else
                                       Dim newPanelHeight = Math.Max(PanelDetails.Height - stepSize, 0)
                                       Dim heightDecrease = PanelDetails.Height - newPanelHeight

                                       PanelDetails.Height = newPanelHeight
                                       Me.Height = Me.Height - heightDecrease
                                   End If
                               End Sub

        timer.Start()

        ButtonDetails.Text = "▼ Show Details"
    End Sub

    Private Sub PopulateDetailsPanel()
        PanelDetails.SuspendLayout()
        PanelDetails.Controls.Clear()

        Dim yPosition As Integer = 10
        Dim leftMargin As Integer = 10
        Dim controlWidth As Integer = Math.Max(100, PanelDetails.Width - 2 * leftMargin)

        ' Helper: measure word-wrapped text height
        Dim MeasureTextHeight = Function(text As String, fnt As Font, width As Integer) As Integer
                                    Dim sz = TextRenderer.MeasureText(text, fnt, New Size(width, Integer.MaxValue), TextFormatFlags.WordBreak)
                                    Return sz.Height
                                End Function

        ' Section 1: Geometric Analysis (title)
        Dim lblGeoTitle As New Label() With {
        .Text = "Geometric Analysis:",
        .Font = New Font("Segoe UI", 10, FontStyle.Bold),
        .Location = New Point(leftMargin, yPosition),
        .AutoSize = True
    }
        PanelDetails.Controls.Add(lblGeoTitle)
        yPosition += lblGeoTitle.Height + 8

        ' Section 1 data (measured for wrap)
        Dim geoText = "Surface Area: [Placeholder] cm²" & vbCrLf &
                  "Volume: [Placeholder] cm³" & vbCrLf &
                  "Complexity Ratio: [Placeholder]" & vbCrLf &
                  "Bounding Box: [Placeholder]"

        Dim geoHeight = MeasureTextHeight(geoText, SystemFonts.DefaultFont, controlWidth - 10)
        Dim lblGeoData As New Label() With {
        .Text = geoText,
        .Location = New Point(leftMargin + 10, yPosition),
        .Size = New Size(controlWidth - 10, geoHeight)
    }
        PanelDetails.Controls.Add(lblGeoData)
        yPosition += lblGeoData.Height + 15

        ' Section 2: Technology Limitations
        Dim lblLimitTitle As New Label() With {
        .Text = "Why Other Technologies Scored Lower:",
        .Font = New Font("Segoe UI", 10, FontStyle.Bold),
        .Location = New Point(leftMargin, yPosition),
        .AutoSize = True
    }
        PanelDetails.Controls.Add(lblLimitTitle)
        yPosition += lblLimitTitle.Height + 8

        Dim limitText = "[Placeholder: Analysis of low-scoring technologies]" & vbCrLf &
                    "• Material Jetting: Score penalty due to [reason]" & vbCrLf &
                    "• Binder Jetting: Unsuitable because [reason]"

        Dim limitHeight = MeasureTextHeight(limitText, SystemFonts.DefaultFont, controlWidth - 10)
        Dim lblLimitData As New Label() With {
        .Text = limitText,
        .Location = New Point(leftMargin + 10, yPosition),
        .Size = New Size(controlWidth - 10, limitHeight)
    }
        PanelDetails.Controls.Add(lblLimitData)
        yPosition += lblLimitData.Height + 15

        ' Section 3: Improvements
        Dim lblImproveTitle As New Label() With {
        .Text = "Potential Design Improvements:",
        .Font = New Font("Segoe UI", 10, FontStyle.Bold),
        .Location = New Point(leftMargin, yPosition),
        .AutoSize = True
    }
        PanelDetails.Controls.Add(lblImproveTitle)
        yPosition += lblImproveTitle.Height + 8

        Dim improveText = "[Placeholder: Suggestions for design optimization]" & vbCrLf &
                      "• Consider reducing precision requirements" & vbCrLf &
                      "• Optimize part orientation to minimize overhangs"

        Dim improveHeight = MeasureTextHeight(improveText, SystemFonts.DefaultFont, controlWidth - 10)
        Dim lblImproveData As New Label() With {
        .Text = improveText,
        .Location = New Point(leftMargin + 10, yPosition),
        .Size = New Size(controlWidth - 10, improveHeight)
    }
        PanelDetails.Controls.Add(lblImproveData)
        yPosition += lblImproveData.Height + 5

        ' Set minimum scrollable area equal to content height so AutoScroll works
        PanelDetails.AutoScrollMinSize = New Size(0, yPosition + 10)

        PanelDetails.ResumeLayout()
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    ' Handle form resize to maintain button position
    Private Sub SummaryForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If isAnimating Then
            Return
        End If

        ' Always keep panel positioned directly under the button.
        PanelDetails.Top = ButtonDetails.Bottom + PANEL_BUTTON_GAP

        If Not isDetailsExpanded AndAlso collapsedFormHeight > 0 Then
            ' Only reposition the button when not expanded and not animating
            ButtonDetails.Top = Me.ClientSize.Height - ButtonDetails.Height - BUTTON_BOTTOM_MARGIN
            PanelDetails.Top = ButtonDetails.Bottom + PANEL_BUTTON_GAP
        End If
    End Sub
End Class

Public Class ScoredProfile
    Public Property Technology As String
    Public Property Material As String
    Public Property Score As Double
End Class