Imports System.Net

Namespace API.Extensions
    Public Class ApiClient

        Public Shared ReadOnly VultrApiUrl As String = "https://api.vultr.com/v1/"

        Public Shared Function ApiExecute(ByVal AccessPoint As String, ByVal ApiKey As String) As HttpWebResponse
            Dim httpWebRequest As HttpWebRequest = WebRequest.Create(VultrApiUrl & AccessPoint)
            httpWebRequest.UserAgent = "VultrAPI.Net"
            httpWebRequest.ContentType = "application/json"
            httpWebRequest.Method = "GET"
            httpWebRequest.Headers.Add("API-Key", ApiKey)
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            ServicePointManager.DefaultConnectionLimit = 9999
            Dim httpResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
            Return httpResponse
        End Function

    End Class
End Namespace
