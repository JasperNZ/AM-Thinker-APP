Imports Microsoft.VisualBasic

' A helper class to detect conventional manufacturing features that may be useful in assessing a low score for
' a given part to be evaluated for additive manufacturing suitability. Ideally, a boolean is likely to be used for each 
' feature to be used for scoring as well as a comment to the user that explains why conventional machining may be more 
' viable for the evaluated part.

'brainstorming some features to check:
' roational symmetry - good for lathes and turning machines
' external features - surfaces good for tool access by a 3-axis cnc?
' internal features - cavities and voids possibly bad for a 3-axis cnc?
' some sort of function looking for weird splines, unsuitable for lathing?
' maybe create bounding box and check how much material removed and see if worth it?

Public Class ConventionalMachiningUtility
    Public Shared Function testHello() As String
        Return "Hello, World!"
    End Function
End Class
