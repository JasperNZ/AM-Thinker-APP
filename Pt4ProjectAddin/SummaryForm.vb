Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports System.Reflection
Imports System.Diagnostics

Public Class SummaryForm
    Private scoredProfiles As List(Of ScoredProfile)
    Private geometry As GeometrySummary
    Private convResults As ConventionalChecks
    Private isDetailsExpanded As Boolean = False
    Private DeveloperMode As Boolean = False

    ' Constants for spacing (relative units work better than absolute)
    Private Const BUTTON_BOTTOM_MARGIN As Integer = 15  ' Distance from button to form bottom when collapsed
    Private Const PANEL_BUTTON_GAP As Integer = 5       ' Gap between button and panel
    Private isAnimating As Boolean = False
    Private collapsedFormHeight As Integer = 0

    ' Tweak these as you like
    Private Const SAFETY_SCREEN_MARGIN As Integer = 8
    Private Const ANIM_STEPS As Integer = 20   ' how many steps the animation tries to use
    Public Sub New(profiles As List(Of ScoredProfile), geo As GeometrySummary, conv As ConventionalChecks)
        InitializeComponent()
        ' Create panel if not already done in Designer
        Dim ListBoxContainerPanel As New Panel()
        ListBoxContainerPanel.Name = "ListBoxContainerPanel"
        ListBoxContainerPanel.Location = ListBoxResults.Location
        ListBoxContainerPanel.Size = ListBoxResults.Size
        ListBoxContainerPanel.Anchor = ListBoxResults.Anchor
        ListBoxContainerPanel.BorderStyle = BorderStyle.None

        ' Reparent the ListBox
        ListBoxResults.Parent = ListBoxContainerPanel
        ListBoxResults.Dock = DockStyle.Fill

        ' Replace old ListBox position with panel
        Me.Controls.Add(ListBoxContainerPanel)

        Me.scoredProfiles = profiles
        Me.geometry = geo
        Me.convResults = conv
        EnableDoubleBuffer(PanelDetails)
        EnableDoubleBuffer(ListBoxResults)
        EnableDoubleBuffer(ListBoxContainerPanel)

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
        ' Ensure panel width is correct before measuring
        PanelDetails.Width = Math.Max(120, Me.ClientSize.Width - PanelDetails.Left - 12)

        ' Measure & populate and get the full content height (unclamped)
        Dim contentHeight As Integer = 0
        contentHeight = MeasureAndPopulateDetails(contentHeight)

        ' Determine how much vertical space we can use below the form
        Dim scrWorking As Rectangle = Screen.FromControl(Me).WorkingArea
        Dim availableBelow As Integer = Math.Max(0, scrWorking.Bottom - Me.Bottom - SAFETY_SCREEN_MARGIN)

        ' target panel height = min(content, available below). If content > available, panel will scroll.
        Dim targetPanelHeight As Integer = If(availableBelow > 0, Math.Min(contentHeight, Math.Max(80, availableBelow)), Math.Min(contentHeight, contentHeight))

        ' Prepare for animation
        PanelDetails.Height = 0
        PanelDetails.Top = ButtonDetails.Bottom + PANEL_BUTTON_GAP
        PanelDetails.Visible = True

        ButtonDetails.Enabled = False
        isAnimating = True

        ' DPI-aware step size
        Dim dpiScale As Single = 1.0F
        Try
            dpiScale = If(Me.DeviceDpi > 0, Me.DeviceDpi / 96.0F, Me.CreateGraphics().DpiY / 96.0F)
        Catch
            dpiScale = 1.0F
        End Try
        Dim approxSteps = Math.Max(6, ANIM_STEPS)
        Dim stepSize As Integer = Math.Max(6, CInt(Math.Round(targetPanelHeight / approxSteps / dpiScale)))

        Dim sw As New Stopwatch()
        sw.Start()
        Dim durationMs As Integer = 700  ' total animation time
        Dim startH As Integer = PanelDetails.Height
        Dim endH As Integer = targetPanelHeight

        Dim animTimer As New Timer() With {.Interval = 15}
        AddHandler animTimer.Tick, Sub()
                                       Dim t = Math.Min(1.0, sw.Elapsed.TotalMilliseconds / durationMs)
                                       ' Use ease-out curve for smoother feel
                                       Dim easedT = 1 - Math.Pow(1 - t, 3)
                                       Dim newH = CInt(startH + (endH - startH) * easedT)

                                       Dim delta = newH - PanelDetails.Height
                                       PanelDetails.Height = newH
                                       Me.Height += delta

                                       If t >= 1.0 Then
                                           animTimer.Stop()
                                           animTimer.Dispose()
                                           PanelDetails.Height = endH
                                           Me.Height = collapsedFormHeight + endH
                                           ButtonDetails.Enabled = True
                                           isAnimating = False
                                           isDetailsExpanded = True
                                           ButtonDetails.Text = "▲ Hide Details"
                                       End If
                                   End Sub
        animTimer.Start()
    End Sub

    Private Sub CollapseDetails()
        If Not isDetailsExpanded OrElse isAnimating Then
            Return
        End If

        Dim startingPanelHeight = PanelDetails.Height
        Dim dpiScale As Single = 1.0F
        Try
            dpiScale = If(Me.DeviceDpi > 0, Me.DeviceDpi / 96.0F, Me.CreateGraphics().DpiY / 96.0F)
        Catch
            dpiScale = 1.0F
        End Try
        Dim approxSteps = Math.Max(6, ANIM_STEPS)
        Dim stepSize As Integer = Math.Max(6, CInt(Math.Round(Math.Max(10, startingPanelHeight) / approxSteps / dpiScale)))



        Dim sw As New Stopwatch()
        sw.Start()
        Dim durationMs As Integer = 700
        Dim startH As Integer = PanelDetails.Height
        Dim endH As Integer = 0

        Dim animTimer As New Timer() With {.Interval = 15}
        ButtonDetails.Enabled = False
        isAnimating = True

        AddHandler animTimer.Tick, Sub()
                                       Dim t = Math.Min(1.0, sw.Elapsed.TotalMilliseconds / durationMs)
                                       Dim easedT = 1 - Math.Pow(1 - t, 3)  ' same ease-out curve

                                       Dim newH = CInt(startH + (endH - startH) * easedT)
                                       Dim delta = newH - PanelDetails.Height
                                       PanelDetails.Height = newH
                                       Me.Height += delta

                                       If t >= 1.0 Then
                                           animTimer.Stop()
                                           animTimer.Dispose()
                                           PanelDetails.Height = 0
                                           Me.Height = collapsedFormHeight
                                           PanelDetails.Visible = False
                                           ButtonDetails.Enabled = True
                                           isAnimating = False
                                           isDetailsExpanded = False
                                           ButtonDetails.Text = "▼ Show Details"
                                       End If
                                   End Sub

        animTimer.Start()
    End Sub


    'Private Sub PopulateDetailsPanel()
    '    PanelDetails.SuspendLayout()
    '    PanelDetails.Controls.Clear()

    '    Dim yPosition As Integer = 10
    '    Dim leftMargin As Integer = 10
    '    Dim controlWidth As Integer = Math.Max(100, PanelDetails.Width - 2 * leftMargin)

    '    ' Helper: measure word-wrapped text height
    '    Dim MeasureTextHeight = Function(text As String, fnt As Font, width As Integer) As Integer
    '                                Dim sz = TextRenderer.MeasureText(text, fnt, New Size(width, Integer.MaxValue), TextFormatFlags.WordBreak)
    '                                Return sz.Height
    '                            End Function

    '    ' Section 1: Geometric Analysis (title)
    '    Dim lblGeoTitle As New Label() With {
    '    .Text = "Geometric Analysis:",
    '    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
    '    .Location = New Point(leftMargin, yPosition),
    '    .AutoSize = True
    '}
    '    PanelDetails.Controls.Add(lblGeoTitle)
    '    yPosition += lblGeoTitle.Height + 8

    '    ' Section 1 data (measured for wrap)
    '    Dim geoText = "Surface Area: [Placeholder] cm²" & vbCrLf &
    '              "Volume: [Placeholder] cm³" & vbCrLf &
    '              "Complexity Ratio: [Placeholder]" & vbCrLf &
    '              "Bounding Box: [Placeholder]"

    '    Dim geoHeight = MeasureTextHeight(geoText, SystemFonts.DefaultFont, controlWidth - 10)
    '    Dim lblGeoData As New Label() With {
    '    .Text = geoText,
    '    .Location = New Point(leftMargin + 10, yPosition),
    '    .Size = New Size(controlWidth - 10, geoHeight)
    '}
    '    PanelDetails.Controls.Add(lblGeoData)
    '    yPosition += lblGeoData.Height + 15

    '    ' Section 2: Technology Limitations
    '    Dim lblLimitTitle As New Label() With {
    '    .Text = "Why Other Technologies Scored Lower:",
    '    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
    '    .Location = New Point(leftMargin, yPosition),
    '    .AutoSize = True
    '}
    '    PanelDetails.Controls.Add(lblLimitTitle)
    '    yPosition += lblLimitTitle.Height + 8

    '    Dim limitText = "[Placeholder: Analysis of low-scoring technologies]" & vbCrLf &
    '                "• Material Jetting: Score penalty due to [reason]" & vbCrLf &
    '                "• Binder Jetting: Unsuitable because [reason]"

    '    Dim limitHeight = MeasureTextHeight(limitText, SystemFonts.DefaultFont, controlWidth - 10)
    '    Dim lblLimitData As New Label() With {
    '    .Text = limitText,
    '    .Location = New Point(leftMargin + 10, yPosition),
    '    .Size = New Size(controlWidth - 10, limitHeight)
    '}
    '    PanelDetails.Controls.Add(lblLimitData)
    '    yPosition += lblLimitData.Height + 15

    '    ' Section 3: Improvements
    '    Dim lblImproveTitle As New Label() With {
    '    .Text = "Potential Design Improvements:",
    '    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
    '    .Location = New Point(leftMargin, yPosition),
    '    .AutoSize = True
    '}
    '    PanelDetails.Controls.Add(lblImproveTitle)
    '    yPosition += lblImproveTitle.Height + 8

    '    Dim improveText = "[Placeholder: Suggestions for design optimization]" & vbCrLf &
    '                  "• Consider reducing precision requirements" & vbCrLf &
    '                  "• Optimize part orientation to minimize overhangs"

    '    Dim improveHeight = MeasureTextHeight(improveText, SystemFonts.DefaultFont, controlWidth - 10)
    '    Dim lblImproveData As New Label() With {
    '    .Text = improveText,
    '    .Location = New Point(leftMargin + 10, yPosition),
    '    .Size = New Size(controlWidth - 10, improveHeight)
    '}
    '    PanelDetails.Controls.Add(lblImproveData)
    '    yPosition += lblImproveData.Height + 5

    '    ' Set minimum scrollable area equal to content height so AutoScroll works
    '    PanelDetails.AutoScrollMinSize = New Size(0, yPosition + 10)

    '    PanelDetails.ResumeLayout()
    'End Sub


    'future work would allow adaption of preferred units,currently automatically work in cm, 2, 3.
    Private Function MeasureAndPopulateDetails(ByRef contentTotalHeight As Integer) As Integer
        ' Populates PanelDetails and returns the total content height (unclamped).
        ' Also sets PanelDetails.AutoScrollMinSize so scrolling works when content > available height.
        PanelDetails.SuspendLayout()
        PanelDetails.Controls.Clear()


        'testing testing
        Dim surfaceArea As Double = 0
        Dim volume As Double = 0
        Dim bbox As Double = 0
        Dim complexityRatio As Double = 0
        Dim overhangArea As Double = 0
        Dim overhangPercentage As Double = 0

        Try
            surfaceArea = geometry.SurfaceArea()
            volume = geometry.Volume()
            bbox = geometry.BoundingBoxVolume()
            complexityRatio = geometry.ComplexityRatio()

            ' Optionally, calculate overhang area if user previously selected a reference face
            ' Dim selectedFace As Face = selectedRefFace
            overhangArea = geometry.OverhangArea()
            overhangPercentage = geometry.OverhangPercentage()
        Catch ex As Exception
            ' gracefully degrade to zero if part unavailable
        End Try

        Dim geomSummary As String =
    $"Surface Area: {surfaceArea:0.00} cm²" & vbCrLf &
    $"Volume: {volume:0.00} cm³" & vbCrLf &
    $"Bounding Box: {bbox:0.00} cm³" & vbCrLf &
    $"Complexity Ratio: {complexityRatio:0.000}" & vbCrLf &
    $"Overhang Area (>45°): {overhangArea:0.00} cm²" & vbCrLf &
    $"Overhang Area ratio): {overhangPercentage:0.00}"

        Dim machiningSummary As String =
    $"Rotational Symmetry: {convResults.HasRotationalSymmetry}" & vbCrLf &
    $"Simple Surfaces: {convResults.HasSimpleSurfaces}" & vbCrLf &
    $"External Accessibility: {convResults.HasExternalAccessibility}" & vbCrLf &
    $"Simple Geometry: {convResults.HasSimpleGeometry}" & vbCrLf &
    $"Minimal Material Removal: {convResults.HasMinimalMaterialRemoval}" & vbCrLf &
    $"No Undercuts: {convResults.HasNoUndercuts}"

        Dim leftPad As Integer = 10
        Dim rightPad As Integer = 10
        Dim innerWidth As Integer = Math.Max(50, PanelDetails.ClientSize.Width - leftPad - rightPad)

        ' Prepare content blocks (text + font + isTitle)
        Dim blocks As New List(Of Tuple(Of String, Font, Boolean))
        blocks.Add(Tuple.Create("Geometric Analysis:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        blocks.Add(Tuple.Create(geomSummary, SystemFonts.DefaultFont, False))
        blocks.Add(Tuple.Create("Conventional Machining Assessment:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        blocks.Add(Tuple.Create(machiningSummary, SystemFonts.DefaultFont, False))
        'blocks.Add(Tuple.Create("Why Other Technologies Scored Lower:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        'blocks.Add(Tuple.Create("• Material Jetting: [reason]" & vbCrLf & "• Binder Jetting: [reason]" & vbCrLf & "[More explanation...]", SystemFonts.DefaultFont, False))
        'blocks.Add(Tuple.Create("Potential Improvements:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        'blocks.Add(Tuple.Create("• Optimize orientation" & vbCrLf & "• Reduce overhangs" & vbCrLf & "[Other items...]", SystemFonts.DefaultFont, False))

        ' helper to measure wrapped text height using TextRenderer (WordBreak)
        Dim MeasureWrappedHeight = Function(txt As String, fnt As Font, width As Integer) As Integer
                                       Dim flags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl
                                       ' high height limit; measure in pixels
                                       Dim measured = TextRenderer.MeasureText(txt, fnt, New Size(width, 10000), flags)
                                       Return measured.Height
                                   End Function

        ' First pass: measure using innerWidth
        Dim yPos As Integer = 10
        For Each b In blocks
            Dim isTitle = b.Item3
            Dim f = b.Item2
            Dim txt = b.Item1

            Dim h As Integer
            If isTitle Then
                ' Titles usually single-line; measure single-line height
                h = TextRenderer.MeasureText(txt, f).Height
            Else
                h = MeasureWrappedHeight(txt, f, innerWidth)
            End If

            yPos += h + 6 ' spacing after each block
        Next

        Dim firstPassHeight As Integer = yPos

        ' Determine maximum panel height available on screen (so we can decide if scrollbar will appear)
        Dim scrWorking As Rectangle = Screen.FromControl(Me).WorkingArea
        ' available below the form (we'll expand downward)
        Dim availableBelow As Integer = Math.Max(0, scrWorking.Bottom - Me.Bottom - SAFETY_SCREEN_MARGIN)

        ' If content won't fit below, account for scrollbar width and re-measure with less width
        Dim finalMeasureWidth As Integer = innerWidth
        If firstPassHeight > availableBelow AndAlso availableBelow > 0 Then
            finalMeasureWidth = Math.Max(40, innerWidth - SystemInformation.VerticalScrollBarWidth - 4)
            ' Recompute total height using narrower width
            yPos = 10
            For Each b In blocks
                Dim isTitle = b.Item3
                Dim f = b.Item2
                Dim txt = b.Item1

                Dim h As Integer
                If isTitle Then
                    h = TextRenderer.MeasureText(txt, f).Height
                Else
                    h = MeasureWrappedHeight(txt, f, finalMeasureWidth)
                End If

                yPos += h + 6
            Next
        End If

        contentTotalHeight = yPos
        ' Create actual Label controls now using finalMeasureWidth and heights
        Dim curY As Integer = 10
        For Each b In blocks
            Dim isTitle = b.Item3
            Dim f = b.Item2
            Dim txt = b.Item1

            Dim h As Integer
            Dim w As Integer = finalMeasureWidth
            If isTitle Then
                h = TextRenderer.MeasureText(txt, f).Height
            Else
                h = MeasureWrappedHeight(txt, f, w)
            End If

            Dim lbl As New Label() With {
            .Text = txt,
            .Font = f,
            .Location = New Point(leftPad, curY),
            .Size = New Size(w, h),
            .AutoSize = False
        }
            ' use word-wrap style
            lbl.BorderStyle = BorderStyle.None

            PanelDetails.Controls.Add(lbl)
            curY += h + 6
        Next

        Dim paddingBottom As Integer = 12
        PanelDetails.AutoScrollMinSize = New Size(0, Math.Max(0, contentTotalHeight + paddingBottom))

        PanelDetails.ResumeLayout()

        Return contentTotalHeight
    End Function


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