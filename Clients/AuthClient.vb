Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class AuthClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub


        ''' <summary>
        ''' Retrieve information about the current API key.
        ''' </summary>
        ''' <returns>Returns API key details and HTTP API Respopnse.</returns>
        Function GetInfo() As AuthResult
            Dim answer As New AuthResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("auth/info", _ApiKey)

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.Auth = JsonConvert.DeserializeObject(Of Auth)(st)
                End Using
            End If

            Return New AuthResult With {.ApiResponse = httpResponse, .Auth = answer.Auth}

        End Function

    End Class
End Namespace

