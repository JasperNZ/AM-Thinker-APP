Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
'------------------------------------------------------------------------------
' <summary>
'	A custom form to display the summary of results after computation, including expandable details.
'   Aggregates the score profiles, sorts them with displayed traffic colours system. 
'   Also shows geometric and conventional manufacturing analysis in an expandable panel.
' </summary>
' <author>Jasper Koid</author>
' <created>05-OCT-2025</created>
'------------------------------------------------------------------------------

Imports System.Linq
Imports System.Reflection
Imports System.Diagnostics

Public Class SummaryForm
    Private scoredProfiles As List(Of ScoredProfile)
    Private geometry As GeometrySummary
    Private tradResults As TraditionalChecks
    Private adviceList As List(Of String)
    Private isDetailsExpanded As Boolean = False
    Private DeveloperMode As Boolean = False

    ' Constants for spacing (relative units work better than absolute).
    Private Const BUTTON_BOTTOM_MARGIN As Integer = 15  ' Distance from button to form bottom when collapsed.
    Private Const PANEL_BUTTON_GAP As Integer = 5       ' Gap between button and panel.
    Private isAnimating As Boolean = False
    Private collapsedFormHeight As Integer = 0

    ' Do not touch these values - used for screen fitting logic.
    Private Const SAFETY_SCREEN_MARGIN As Integer = 8
    Private Const ANIM_STEPS As Integer = 20   ' Number of steps the animation tries to use.
    Public Sub New(profiles As List(Of ScoredProfile), geo As GeometrySummary, trad As TraditionalChecks, advice As List(Of String))
        InitializeComponent()
        ' Create panel if not already done in Designer.
        Dim ListBoxContainerPanel As New Panel With {
            .Name = "ListBoxContainerPanel",
            .Location = ListBoxResults.Location,
            .Size = ListBoxResults.Size,
            .Anchor = ListBoxResults.Anchor,
            .BorderStyle = BorderStyle.None
        }

        ' Reparent the ListBox.
        ListBoxResults.Parent = ListBoxContainerPanel
        ListBoxResults.Dock = DockStyle.Fill

        ' Replace old ListBox position with panel.
        Me.Controls.Add(ListBoxContainerPanel)

        Me.scoredProfiles = profiles
        Me.geometry = geo
        Me.tradResults = trad
        Me.adviceList = advice
        EnableDoubleBuffer(PanelDetails)
        EnableDoubleBuffer(ListBoxResults)
        EnableDoubleBuffer(ListBoxContainerPanel)

        ' Initial setup - panel invisible and collapsed.
        PanelDetails.Height = 0
        PanelDetails.Visible = False

        PopulateResults()

        ' Capture the collapsed height only AFTER form is fully loaded.
        AddHandler Me.Load, Sub(s, e)
                                ' Ensure PanelDetails doesn't automatically stretch with form height.
                                ' Position button at bottom with margin (initial collapsed placement)
                                ButtonDetails.Top = Me.ClientSize.Height - ButtonDetails.Height - BUTTON_BOTTOM_MARGIN
                                ButtonDetails.Left = 12

                                ' Position panel directly under button (but invisible).
                                PanelDetails.Top = ButtonDetails.Bottom + PANEL_BUTTON_GAP
                                PanelDetails.Left = ButtonDetails.Left
                                PanelDetails.Width = Me.ClientSize.Width - 2 * ButtonDetails.Left

                                ' Store this as reference collapsed height.
                                collapsedFormHeight = Me.Height
                            End Sub
    End Sub

    ' Enable double buffering on a control to reduce flicker during animations.
    Private Sub EnableDoubleBuffer(ctrl As Control)
        Dim prop = GetType(Control).GetProperty("DoubleBuffered",
        Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        prop.SetValue(ctrl, True, Nothing)
    End Sub

    ' Function populates the ListBox with scored profiles and updates assessment labels.
    Private Sub PopulateResults()
        ListBoxResults.Items.Clear()

        Dim sortedProfiles = scoredProfiles.OrderByDescending(Function(p) p.Score).Take(6).ToList()

        ' Determine overall suitability.
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

        ' Show best options.
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

    ' Custom draw item to include colored circle and formatted text.
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

    ' Toggle details panel on button click.
    Private Sub ButtonDetails_Click(sender As Object, e As EventArgs) Handles ButtonDetails.Click
        If isDetailsExpanded Then
            CollapseDetails()
        Else
            ExpandDetails()
        End If
    End Sub

    ' Expand the details panel with animation.
    Private Sub ExpandDetails()
        ' Ensure panel width is correct before measuring.
        PanelDetails.Width = Math.Max(120, Me.ClientSize.Width - PanelDetails.Left - 12)

        ' Measure & populate and get the full content height (unclamped).
        Dim contentHeight As Integer = 0
        contentHeight = MeasureAndPopulateDetails(contentHeight)

        ' Determine how much vertical space we can use below the form.
        Dim scrWorking As Rectangle = Screen.FromControl(Me).WorkingArea
        Dim availableBelow As Integer = Math.Max(0, scrWorking.Bottom - Me.Bottom - SAFETY_SCREEN_MARGIN)

        ' target panel height = min(content, available below). If content > available, panel will scroll.
        Dim targetPanelHeight As Integer = If(availableBelow > 0, Math.Min(contentHeight, Math.Max(80, availableBelow)), Math.Min(contentHeight, contentHeight))

        ' Decide multiplier based on screen height
        Dim multiplier As Double

        If scrWorking.Height >= 1015 Then
            ' Small desktop monitor :( 1032
            multiplier = 0.46
        ElseIf scrWorking.Height >= 1008 Then
            ' Laptop Screen case for presentation 1008
            multiplier = 0.6
        Else
            ' Otherwise very small screens
            multiplier = 0.7
        End If

        'MessageBox.Show($"Working area height = {scrWorking.Height}")

        Dim maxFormHeight As Integer = CInt(scrWorking.Height * multiplier)
        Dim maxAllowedPanelHeight As Integer = maxFormHeight - collapsedFormHeight
        If targetPanelHeight > maxAllowedPanelHeight Then
            targetPanelHeight = maxAllowedPanelHeight
        End If


        ' Prepare for animation.
        PanelDetails.Height = 0
        PanelDetails.Top = ButtonDetails.Bottom + PANEL_BUTTON_GAP
        PanelDetails.Visible = True

        ButtonDetails.Enabled = False
        isAnimating = True

        ' DPI-aware step size.
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
        Dim durationMs As Integer = 700  ' Total animation time.
        Dim startH As Integer = PanelDetails.Height
        Dim endH As Integer = targetPanelHeight

        Dim animTimer As New Timer() With {.Interval = 15}
        AddHandler animTimer.Tick, Sub()
                                       Dim t = Math.Min(1.0, sw.Elapsed.TotalMilliseconds / durationMs)
                                       ' Use ease-out curve for smoother feel.
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

    ' Collapse the details panel with animation.
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


    ' Function to measure and populate the details panel, returning total content height.
    Private Function MeasureAndPopulateDetails(ByRef contentTotalHeight As Integer) As Integer
        ' Populates PanelDetails and returns the total content height (unclamped).
        ' Also sets PanelDetails.AutoScrollMinSize so scrolling works when content > available height.
        PanelDetails.SuspendLayout()
        PanelDetails.Controls.Clear()

        ' Gather geometric data.
        Dim surfaceArea As Double = geometry.SurfaceArea()
        Dim volume As Double = geometry.Volume()
        Dim bbox As Double = geometry.BoundingBoxVolume()
        Dim complexityRatio As Double = geometry.ComplexityRatio()
        Dim overhangArea As Double = geometry.OverhangArea()
        Dim overhangRatio As Double = geometry.OverhangRatio()

        Dim geomSummary As String =
            $"Surface Area: {surfaceArea:0.00} cm²" & vbCrLf &
            $"Volume: {volume:0.00} cm³" & vbCrLf &
            $"Bounding Box Volume: {bbox:0.00} cm³" & vbCrLf &
            $"Part Complexity (A/V): {complexityRatio:0.000}" & vbCrLf &
            $"Overhang Area (<45° from horizontal): {overhangArea:0.00} cm²" & vbCrLf &
            $"Overhang ratio: {overhangRatio:0.00}"

        Dim manufacturingSummary As String =
            $"Rotational Symmetry: {tradResults.HasRotationalSymmetry}" & vbCrLf &
            $"Simple Surfaces: {tradResults.HasSimpleSurfaces}" & vbCrLf &
            $"External Accessibility: {tradResults.HasExternalAccessibility}" & vbCrLf &
            $"Simple Geometry: {tradResults.HasSimpleGeometry}" & vbCrLf &
            $"Minimal Material Removal: {tradResults.HasMinimalMaterialRemoval}" & vbCrLf &
            $"No Undercuts: {tradResults.HasNoUndercuts}" & vbCrLf &
            $"Minimum (2mm) Wall Thickness: {tradResults.HasMinimumWallThickness}" & vbCrLf &
            $"Uniform Wall Thickness: {tradResults.HasUniformWallThickness}" & vbCrLf &
            $"Draft Angles Present: {tradResults.HasDraftAngles}" & vbCrLf &
            $"Acceptable Aspect Ratios: {tradResults.HasAcceptableAspectRatios}" & vbCrLf &
            $"No Sharp Corners: {tradResults.HasNoSharpCorners}"

        Dim sortedProfiles = scoredProfiles.OrderByDescending(Function(p) p.Score).Take(6).ToList()
        Dim unavailableSummary As String = ""

        Dim zeroProfiles = scoredProfiles.Where(Function(p) p.Score = 0).ToList()
        If zeroProfiles.Any() Then
            For Each zp In zeroProfiles
                unavailableSummary &= $"• {zp.Material} - {zp.Technology} is currently not an available technology" & vbCrLf
            Next
        End If




        Dim leftPad As Integer = 10
        Dim rightPad As Integer = 10
        Dim innerWidth As Integer = Math.Max(50, PanelDetails.ClientSize.Width - leftPad - rightPad)

        ' Prepare content blocks (text + font + isTitle)
        Dim blocks As New List(Of Tuple(Of String, Font, Boolean)) From {
            Tuple.Create("Geometric Analysis:", New Font("Segoe UI", 10, FontStyle.Bold), True),
            Tuple.Create(geomSummary, SystemFonts.DefaultFont, False)
        }

        If adviceList IsNot Nothing AndAlso adviceList.Count > 0 Then
            blocks.Add(Tuple.Create("Improvement Suggestions:", New Font("Segoe UI", 10, FontStyle.Bold), True))
            blocks.Add(Tuple.Create(String.Join(vbCrLf, adviceList), SystemFonts.DefaultFont, False))
        End If

        If Not String.IsNullOrEmpty(unavailableSummary) Then
            blocks.Add(Tuple.Create("Technology Availability:", New Font("Segoe UI", 10, FontStyle.Bold), True))
            blocks.Add(Tuple.Create(unavailableSummary, SystemFonts.DefaultFont, False))
        End If

        ' Add Traditional Manufacturing Assessment at the very end
        blocks.Add(Tuple.Create("Traditional Manufacturing Assessment:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        blocks.Add(Tuple.Create(manufacturingSummary, SystemFonts.DefaultFont, False))


        'TODO - Felix should be filling this with icon bug fix?
        'blocks.Add(Tuple.Create("Why Other Technologies Scored Lower:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        'blocks.Add(Tuple.Create("• Material Jetting: [reason]" & vbCrLf & "• Binder Jetting: [reason]" & vbCrLf & "[More explanation...]", SystemFonts.DefaultFont, False))
        'blocks.Add(Tuple.Create("Potential Improvements:", New Font("Segoe UI", 10, FontStyle.Bold), True))
        'blocks.Add(Tuple.Create("• Optimize orientation" & vbCrLf & "• Reduce overhangs" & vbCrLf & "[Other items...]", SystemFonts.DefaultFont, False))

        ' Helper to measure wrapped text height using TextRenderer (WordBreak).
        Dim MeasureWrappedHeight = Function(txt As String, fnt As Font, width As Integer) As Integer
                                       Dim flags As TextFormatFlags = TextFormatFlags.WordBreak Or TextFormatFlags.TextBoxControl
                                       ' high height limit; measure in pixels
                                       Dim measured = TextRenderer.MeasureText(txt, fnt, New Size(width, 10000), flags)
                                       Return measured.Height
                                   End Function

        ' First pass: measure using innerWidth.
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

            ' use word-wrap style
            Dim lbl As New Label With {
                .Text = txt,
                .Font = f,
                .Location = New Point(leftPad, curY),
                .Size = New Size(w, h),
                .AutoSize = False,
                .BorderStyle = BorderStyle.None
            }

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