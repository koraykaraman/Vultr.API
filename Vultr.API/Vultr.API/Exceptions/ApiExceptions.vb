Imports System.Net

Namespace API.Exceptions
    Public Class ApiException
        Inherits Exception

        Private ReadOnly _errors As IDictionary(Of Integer, String) = New Dictionary(Of Integer, String) From {
            {401, "Access Denied"},
            {404, "Not Found"},
            {429, "Rate Limit Exceeded"}
        }
        Public Property StatusCode As HttpStatusCode

        Public Overrides ReadOnly Property Message As String
            Get
                Return If(_errors.ContainsKey(CInt(StatusCode)), _errors(CInt(StatusCode)), "Unknown API error")
            End Get
        End Property

        Public Sub New(ByVal statusCode As HttpStatusCode)
            statusCode = statusCode
        End Sub

    End Class
End Namespace
