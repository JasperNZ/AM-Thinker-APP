Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports Pt4ProjectAddin.My.Resources

Public Class TestIcon

    ' Load Bitmap from My.Resources (auto-generated resource properties)
    Public Function LoadBitmapFromResources() As Bitmap
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
        Return New Bitmap(Resources.TestImage)
    End Function

    'function not used, but as backup
    ' Load Bitmap from file in the same folder as the executing assembly
    Private Function LoadBitmapFromFile(fileName As String) As Bitmap
        Dim folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        Dim fullPath = Path.Combine(folderPath, fileName)

        If Not File.Exists(fullPath) Then
            Throw New FileNotFoundException("Icon file not found: " & fullPath)
        End If

        Return New Bitmap(fullPath)
    End Function

End Class
