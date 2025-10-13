'------------------------------------------------------------------------------
' <summary>
'	Backend of the Inventor Add-in, handling the creation of the button in the ribbon and launching the user form.
' </summary>
' <author>Jasper Koid</author>
' <created>24-AUG-2025</created>
'------------------------------------------------------------------------------
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Inventor
Imports Microsoft.Win32

Namespace Pt4ProjectAddin
    <ProgIdAttribute("Pt4ProjectAddin.StandardAddInServer"),
    GuidAttribute("106eb6fb-6864-4598-9d95-c7e7a143af98")>
    Public Class StandardAddInServer
        Implements Inventor.ApplicationAddInServer

        Private WithEvents m_uiEvents As UserInterfaceEvents
        Private WithEvents m_sampleButton As ButtonDefinition


#Region "ApplicationAddInServer Members"
        ' This method is called by Inventor when it loads the AddIn. The AddInSiteObject provides access  
        ' to the Inventor Application object. The FirstTime flag indicates if the AddIn is loaded for
        ' the first time. However, with the introduction of the ribbon this argument is always true.
        Public Sub Activate(ByVal addInSiteObject As Inventor.ApplicationAddInSite, ByVal firstTime As Boolean) Implements Inventor.ApplicationAddInServer.Activate
            Try
                ' Initialise AddIn members FIRST
                g_inventorApplication = addInSiteObject.Application

                ' Connect to the user-interface events to handle a ribbon reset
                m_uiEvents = g_inventorApplication.UserInterfaceManager.UserInterfaceEvents

                ' Create icon using TestIcon helper
                Dim iconHelper As New IconHelper()
                Dim bmp As Bitmap = iconHelper.LoadBitmapFromResources()
                Dim SiconHelper As New SmallTestIcon()
                Dim Sbmp As Bitmap = SiconHelper.SLoadBitmapFromResources()

                ' Convert to IPictureDisp for Inventor
                Dim iconDisp As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(bmp)
                Dim SiconDisp As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(Sbmp)

                ' Now you can safely get ControlDefinitions
                Dim controlDefs As Inventor.ControlDefinitions = g_inventorApplication.CommandManager.ControlDefinitions

                ' Create the button with the icon
                m_sampleButton = controlDefs.AddButtonDefinition("AM Thinker",
                    "AM Thinker ID",
                    CommandTypesEnum.kShapeEditCmdType,
                    AddInClientID,
                    "Launch AM Thinker Tool",
                    "Open the AM Thinker Add-in for analysis",
                    SiconDisp, iconDisp)
                If firstTime Then
                    AddToUserInterface()
                    'MsgBox("Button Created: " & m_sampleButton.DisplayName)
                    'MessageBox.Show(System.Runtime.InteropServices.Marshal.GetType().Assembly.FullName)
                    'MessageBox.Show(Environment.Version.ToString())
                End If
            Catch ex As Exception
                MsgBox("Activate Error: " & ex.Message)
            End Try
        End Sub

        ' This method is called by Inventor when the AddIn is unloaded. The AddIn will be
        ' unloaded either manually by the user or when the Inventor session is terminated.
        Public Sub Deactivate() Implements Inventor.ApplicationAddInServer.Deactivate

            ' TODO:  Add ApplicationAddInServer.Deactivate implementation

            ' Release objects.
            m_uiEvents = Nothing
            g_inventorApplication = Nothing

            System.GC.Collect()
            System.GC.WaitForPendingFinalizers()
        End Sub

        ' This property is provided to allow the AddIn to expose an API of its own to other 
        ' programs. Typically, this  would be done by implementing the AddIn's API
        ' interface in a class and returning that class object through this property.
        Public ReadOnly Property Automation() As Object Implements Inventor.ApplicationAddInServer.Automation
            Get
                Return Nothing
            End Get
        End Property

        ' Note:this method is now obsolete, you should use the 
        ' ControlDefinition functionality for implementing commands.
        Public Sub ExecuteCommand(ByVal commandID As Integer) Implements Inventor.ApplicationAddInServer.ExecuteCommand
        End Sub

#End Region

#Region "User interface definition"
        ' Sub where the user-interface creation is done.  This is called when
        ' the add-in loaded and also if the user interface is reset.
        Private Sub AddToUserInterface()
            ' This is where you'll add code to add buttons to the ribbon.

            '** Sample to illustrate creating a button on a new panel of the Tools tab of the Part ribbon.

            '' Get the part ribbon.
            Dim partRibbon As Ribbon = g_inventorApplication.UserInterfaceManager.Ribbons.Item("Part")

            '' Get the "Tools" tab.
            Dim toolsTab As RibbonTab = partRibbon.RibbonTabs.Item("id_TabTools")

            '' Create a new panel.
            Dim customPanel As RibbonPanel = toolsTab.RibbonPanels.Add("AM Thinker", "AM Thinker ID", AddInClientID())

            '' Add a button.
            customPanel.CommandControls.AddButton(m_sampleButton, True)
        End Sub

        Private Sub m_uiEvents_OnResetRibbonInterface(Context As NameValueMap) Handles m_uiEvents.OnResetRibbonInterface
            ' The ribbon was reset, so add back the add-ins user-interface.
            AddToUserInterface()
        End Sub

        ' Sample handler for the button.
        Private Sub m_sampleButton_OnExecute(Context As NameValueMap) Handles m_sampleButton.OnExecute
            'MsgBox("Button was clicked.")
            Dim MainUserForm1 As New MainUserForm()
            MainUserForm1.Show(New WindowWrapper(CType(g_inventorApplication.MainFrameHWND, IntPtr)))

            ' Reposition relative to Inventor’s main window
            Dim invBounds As Rectangle = Screen.FromHandle(CType(g_inventorApplication.MainFrameHWND, IntPtr)).Bounds

            ' Example: below ribbon + near design history
            ' Adjust offsets based on your UI preferences
            MainUserForm1.Top = invBounds.Top + 145  ' offset down from ribbon
            MainUserForm1.Left = invBounds.Left + 145 ' offset right from history tree
        End Sub
#End Region

    End Class
End Namespace

Public Module Globals
    ' Inventor application object.
    Public g_inventorApplication As Inventor.Application

#Region "Function to get the add-in client ID."
    ' This function uses reflection to get the GuidAttribute associated with the add-in.
    Public Function AddInClientID() As String
        Dim guid As String = ""
        Try
            Dim t As Type = GetType(Pt4ProjectAddin.StandardAddInServer)
            Dim customAttributes() As Object = t.GetCustomAttributes(GetType(GuidAttribute), False)
            Dim guidAttribute As GuidAttribute = CType(customAttributes(0), GuidAttribute)
            guid = "{" + guidAttribute.Value.ToString() + "}"
        Catch
        End Try

        Return guid
    End Function
#End Region

#Region "hWnd Wrapper Class"
    ' This class is used to wrap a Win32 hWnd as a .Net IWind32Window class.
    ' This is primarily used for parenting a dialog to the Inventor window.
    '
    ' For example:
    ' myForm.Show(New WindowWrapper(g_inventorApplication.MainFrameHWND))
    '
    Public Class WindowWrapper
        Implements System.Windows.Forms.IWin32Window
        Public Sub New(ByVal handle As IntPtr)
            _hwnd = handle
        End Sub

        Public ReadOnly Property Handle() As IntPtr _
          Implements System.Windows.Forms.IWin32Window.Handle
            Get
                Return _hwnd
            End Get
        End Property

        Private _hwnd As IntPtr
    End Class
#End Region

#Region "Image Converter"
    ' Class used to convert bitmaps and icons from their .Net native types into
    ' an IPictureDisp object which is what the Inventor API requires. A typical
    ' usage is shown below where MyIcon is a bitmap or icon that's available
    ' as a resource of the project.
    'Private Sub LoadIcon()
    '    Dim bytes As Byte() = My.Resources.Resources.TestImageIcon

    '    Using ms As New IO.MemoryStream(bytes)
    '        Dim smallBitmap As New Bitmap(ms)
    '        Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(smallBitmap)
    '        ' … assign smallIcon to your control definition here …
    '    End Using
    'End Sub

    <ComImport(), Guid("00020400-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)>
    Public Interface IPictureDisp
    End Interface

    Public NotInheritable Class PictureDispConverter
        <DllImport("OleAut32.dll", EntryPoint:="OleCreatePictureIndirect", ExactSpelling:=True, PreserveSig:=False)>
        Private Shared Function OleCreatePictureIndirect(
            <MarshalAs(UnmanagedType.AsAny)> ByVal picdesc As Object,
            ByRef iid As Guid,
            <MarshalAs(UnmanagedType.Bool)> ByVal fOwn As Boolean) As IPictureDisp
        End Function

        Shared iPictureDispGuid As Guid = GetType(IPictureDisp).GUID

        Private NotInheritable Class PICTDESC
            Private Sub New()
            End Sub

            'Picture Types
            Public Const PICTYPE_BITMAP As Short = 1
            Public Const PICTYPE_ICON As Short = 3

            <StructLayout(LayoutKind.Sequential)>
            Public Class Icon
                Friend cbSizeOfStruct As Integer = Marshal.SizeOf(GetType(PICTDESC.Icon))
                Friend picType As Integer = PICTDESC.PICTYPE_ICON
                Friend hicon As IntPtr = IntPtr.Zero
                Friend unused1 As Integer
                Friend unused2 As Integer

                Friend Sub New(ByVal icon As System.Drawing.Icon)
                    Me.hicon = icon.ToBitmap().GetHicon()
                End Sub
            End Class

            <StructLayout(LayoutKind.Sequential)>
            Public Class Bitmap
                Friend cbSizeOfStruct As Integer = Marshal.SizeOf(GetType(PICTDESC.Bitmap))
                Friend picType As Integer = PICTDESC.PICTYPE_BITMAP
                Friend hbitmap As IntPtr = IntPtr.Zero
                Friend hpal As IntPtr = IntPtr.Zero
                Friend unused As Integer

                Friend Sub New(ByVal bitmap As System.Drawing.Bitmap)
                    Me.hbitmap = bitmap.GetHbitmap()
                End Sub
            End Class
        End Class

        Public Shared Function ToIPictureDisp(ByVal icon As System.Drawing.Icon) As IPictureDisp
            Dim pictIcon As New PICTDESC.Icon(icon)
            Return OleCreatePictureIndirect(pictIcon, iPictureDispGuid, True)
        End Function

        Public Shared Function ToIPictureDisp(ByVal bmp As System.Drawing.Bitmap) As IPictureDisp
            Dim pictBmp As New PICTDESC.Bitmap(bmp)
            Return OleCreatePictureIndirect(pictBmp, iPictureDispGuid, True)
        End Function
    End Class

#End Region
End Module
