Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients

    Public Class ServerClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all active or pending virtual machines on the current account. The "status" field represents the status of the subscription And will be one of pending | active | suspended | closed. If the status Is "active", you can check "power_status" to determine if the VPS Is powered on Or Not. When status Is "active", you may also use "server_state" for a more detailed status of: none | locked | installingbooting | isomounting | ok. The API does Not provide any way To determine If the initial installation has completed Or Not. The "v6_network", "v6_main_ip", And "v6_network_size" fields are deprecated In favor Of "v6_networks". The "kvm_url" value will change periodically. It Is Not advised To cache this value. If you need To filter the list, review the parameters For this Function. Currently, only one filter at a time may be applied (SUBID, tag, label, main_ip).
        ''' </summary>
        ''' <returns>List of active or panding servers.</returns>
        Function GetServers() As ServerResult
            Dim answer As New Dictionary(Of String, Server)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, Server))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ServerResult With {.ApiResponse = httpResponse, .Servers = answer}
        End Function

        ''' <summary>
        ''' Changes the virtual machine to a different application. All data will be permanently lost.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <param name="AppId">Application to use. See AvailableApps() call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function ChangeServerApp(ByVal SubId As Integer, ByVal AppId As Integer) As ServerResult

            Dim dict As New List(Of KeyValuePair(Of String, String))
            dict.Add(New KeyValuePair(Of String, String)("SUBID", SubId))
            dict.Add(New KeyValuePair(Of String, String)("APPID", AppId))
            Dim answer As New Dictionary(Of String, Server)

            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/app_change", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, Server))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ServerResult With {.ApiResponse = httpResponse, .Servers = answer}
        End Function

        ''' <summary>
        ''' Retrieves a list of applications to which a virtual machine can be changed. Always check against this list before trying to switch applications because it is not possible to switch between every application combination.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <returns>List of available apps.</returns>
        Function AvailableApps(ByVal SubId As Integer) As ApplicationResult
            Dim answer As New ApplicationResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/app_change_list?SUBID=" & SubId, "")

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
