Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class AccountClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve information about the current account.
        ''' </summary>
        ''' <returns>Returns account information and HTTP API Respopnse.</returns>
        Function GetInfo() As AccountResult
            Dim answer As New Account
            Dim httpResponse = Extensions.ApiClient.ApiExecute("account/info", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    answer = JsonConvert.DeserializeObject(Of Account)(streamReader.ReadToEnd())
                End Using
            End If
            Return New AccountResult With {.ApiResponse = httpResponse, .Account = answer}
        End Function

    End Class
End Namespace
