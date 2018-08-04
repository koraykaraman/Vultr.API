Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class OperatingSystemClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve a list of available operating systems. If the "windows" flag is true, a Windows license will be included with the instance, which will increase the cost.
        ''' </summary>
        ''' <returns>List of available operating systems.</returns>
        Function GetOperatingSystems() As OperatingSystemResult
            Dim answer As New Dictionary(Of Integer, OperatingSystem)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("regions/list?availability=yes", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, OperatingSystem))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New OperatingSystemResult With {.ApiResponse = httpResponse, .OperatingSystems = answer}
        End Function
    End Class
End Namespace

