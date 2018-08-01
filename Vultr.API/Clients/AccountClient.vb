Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Vultr.API.Extensions
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class AccountClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        Public Shared ReadOnly VultrApiUrl As String = "https://api.vultr.com/v1/"

        Function GetInfo() As AccountResult
            Dim httpWebRequest As HttpWebRequest = WebRequest.Create(VultrApiUrl & "account/info")
            httpWebRequest.UserAgent = "VultrAPI.Net"
            httpWebRequest.ContentType = "application/json"
            httpWebRequest.Method = "GET"
            httpWebRequest.Headers.Add("API-Key", _ApiKey)
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            ServicePointManager.DefaultConnectionLimit = 9999
            Dim answer As New Account
            Dim httpResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    answer = JsonConvert.DeserializeObject(Of Account)(streamReader.ReadToEnd())
                End Using
            End If
            Return New AccountResult With {.ApiResponse = httpResponse, .Account = answer}
        End Function

    End Class
End Namespace
