'------------------------------------------------------------------------------
' <summary>
'     File deals with conversion of byte array from resources into a Bitmap for use as an icon.
'     Also do not delete any images from resources, as a bug might occur where the resource is not found (including the cat face).
' </summary>
' <author>Jasper Koid</author>
' <created>28-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports Pt4ProjectAddin.My.Resources

Public Class IconHelper
    ''' <summary>
    '''     UH unusual bug of TestImage being detected as either Bitmap or Byte type...
    '''     Originally intended to load Byte, then continue to convert to a Bitmap. Now just used as Bitmap directly.
    ''' </summary>
    Public Function LoadBitmapFromResources() As Bitmap
        ' Convert the Byte() from resource into a Bitmap
        '  Dim imgBytes As Byte() = Resources.TestImage
        '  If imgBytes Is Nothing Then
        '  Throw New Exception("Resource 'TestImage' not found.")
        '   End If

        '  Using ms As New MemoryStream(imgBytes)
        ' Return New Bitmap(ms)
        ' End Using
        Return New Bitmap(Resources.TestImage)
    End Function

    ' Function not used, but as backup.
    ' Load Bitmap from file in the same folder as the executing assembly.
    Private Function LoadBitmapFromFile(fileName As String) As Bitmap
        Dim folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        Dim fullPath = Path.Combine(folderPath, fileName)

        If Not File.Exists(fullPath) Then
            Throw New FileNotFoundException("Icon file not found: " & fullPath)
        End If

        Return New Bitmap(fullPath)
    End Function

End Class
Public Class SmallTestIcon

    ' Load Bitmap from My.Resources (auto-generated resource properties)
    Public Function SLoadBitmapFromResources() As Bitmap
        ' Convert the Byte() from resource into a Bitmap
        '  Dim imgBytes As Byte() = Resources.TestImage
        '  If imgBytes Is Nothing Then
        '  Throw New Exception("Resource 'TestImage' not found.")
        '   End If

        '  Using ms As New MemoryStream(imgBytes)
        ' Return New Bitmap(ms)
        ' End Using

        'UH unusual bug of TestImage being detected as either Bitmap or Byte type...
        'Further research needed for this.
        Return New Bitmap(Resources.SmallTestImage)
    End Function

    'function not used, but as backup
    ' Load Bitmap from file in the same folder as the executing assembly
    Private Function SLoadBitmapFromFile(fileName As String) As Bitmap
        Dim folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        Dim fullPath = Path.Combine(folderPath, fileName)

        If Not File.Exists(fullPath) Then
            Throw New FileNotFoundException("Icon file not found: " & fullPath)
        End If

        Return New Bitmap(fullPath)
    End Function

End Class
