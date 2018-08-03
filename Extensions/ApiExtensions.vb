Imports System.Collections.Specialized
Imports System.IO
Imports System.Net
Imports System.Text

Namespace API.Extensions
    Public Class ApiClient

        Public Shared ReadOnly VultrApiUrl As String = "https://api.vultr.com/v1/"

        Public Shared Function ApiExecute(ByVal AccessPoint As String, ByVal ApiKey As String, Optional ByVal Parameters As List(Of KeyValuePair(Of String, String)) = Nothing, Optional ByVal Method As String = "GET") As HttpWebResponse

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            ServicePointManager.DefaultConnectionLimit = 9999

            Dim httpWebRequest As HttpWebRequest = WebRequest.Create(VultrApiUrl & AccessPoint)
            httpWebRequest.UserAgent = "VultrAPI.Net"
            httpWebRequest.ContentType = "application/json"
            httpWebRequest.Method = Method
            If String.IsNullOrWhiteSpace(ApiKey) = False Then
                httpWebRequest.Headers.Add("API-Key", ApiKey)
            End If


            If Method = "GET" Then
                If IsNothing(Parameters) = False Then
                    For Each pair As KeyValuePair(Of String, String) In Parameters
                        httpWebRequest.Headers.Add(pair.Key, pair.Value)
                    Next
                End If
            Else
                If IsNothing(Parameters) = False Then
                    Dim postData As String = ""
                    For Each pair As KeyValuePair(Of String, String) In Parameters
                        postData += If(postData = "", "", "&") & pair.Key & "=" & pair.Value
                    Next
                    Dim encoding As New UTF8Encoding
                    Dim byteData As Byte() = encoding.GetBytes(postData)
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded"
                    httpWebRequest.ContentLength = byteData.Length
                    Dim postreqstream As Stream = httpWebRequest.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                End If
            End If



            Dim httpResponse = DirectCast(httpWebRequest.GetResponse(), HttpWebResponse)
            Return httpResponse

        End Function
    End Class
End Namespace
