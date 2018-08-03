Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients

    Public Class ApplicationClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve a list of available applications. These refer to applications that can be launched when creating a Vultr VPS.
        ''' </summary>
        ''' <returns>Returns application list and HTTP API Respopnse.</returns>
        Function GetApplications() As ApplicationResult

            Dim answer As New ApplicationResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("app/list", "")

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.Applications = JsonConvert.DeserializeObject(Of Dictionary(Of String, Application))(st)
                End Using
            End If

            Return New ApplicationResult With {.ApiResponse = httpResponse, .Applications = answer.Applications}

        End Function
    End Class

End Namespace

