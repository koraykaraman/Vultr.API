Imports System.Net

Namespace API.Models.Responses

    Public Class Auth
        Public Property acls As String()
        Public Property email As String
        Public Property name As String
    End Class

    Public Structure AuthResult
        Public Property Auth As Auth
        Public Property ApiResponse As HttpWebResponse
    End Structure

End Namespace