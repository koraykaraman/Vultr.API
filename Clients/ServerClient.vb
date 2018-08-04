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
        Function AppChange(ByVal SubId As Integer, ByVal AppId As Integer) As ServerResult

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SubId))
            dict.Add(New KeyValuePair(Of String, Object)("APPID", AppId))
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
        ''' <returns>List of available apps</returns>
        Function AppsAvailable(ByVal SubId As Integer) As ApplicationResult
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
        ''' <summary>
        ''' Disables automatic backups On a server. Once disabled, backups can only be enabled again by customer support.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function BackupDisable(ByVal SubId As Integer) As BackupResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SubId))
            Dim answer As New Dictionary(Of String, Backup)

            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/backup_disable", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, Backup))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New BackupResult With {.ApiResponse = httpResponse, .Backups = answer}
        End Function
        ''' <summary>
        ''' Enables automatic backups on a server.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function BackupEnable(ByVal SubId As Integer) As BackupResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SubId))
            Dim answer As New Dictionary(Of String, Backup)

            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/backup_enable", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, Backup))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New BackupResult With {.ApiResponse = httpResponse, .Backups = answer}
        End Function
        ''' <summary>
        ''' Retrieves the backup schedule for a server. All time values are in UTC.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function BackupScheduleGet(ByVal SubId As Integer) As ScheduleResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SubId))
            Dim answer As New Schedule

            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/backup_get_schedule", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Schedule)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ScheduleResult With {.ApiResponse = httpResponse, .Schedule = answer}
        End Function
        ''' <summary>
        ''' Sets the backup schedule for a server. All time values are in UTC.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <param name="Schedule">Schedule a backup object for a server</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function BackupScheduleSet(ByVal SubId As Integer, ByVal Schedule As Schedule) As ScheduleResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SubId))
            dict.Add(New KeyValuePair(Of String, Object)("cron_type", Schedule.cron_type))
            dict.Add(New KeyValuePair(Of String, Object)("hour", Schedule.hour))
            dict.Add(New KeyValuePair(Of String, Object)("dow", Schedule.dow))
            dict.Add(New KeyValuePair(Of String, Object)("dom", Schedule.dom))

            Dim answer As New Schedule

            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/backup_get_schedule", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Schedule)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ScheduleResult With {.ApiResponse = httpResponse, .Schedule = answer}
        End Function
        ''' <summary>
        ''' Get the bandwidth used by a virtual machine.
        ''' </summary>
        ''' <param name="SubId">Unique identifier for this subscription. These can be found using the GetServers() call.</param>
        ''' <returns>Bandwidth used by a virtual machine day by day.</returns>
        Function BandwidthGet(ByVal SubId As Integer) As BandwidthResult
            Dim answer As New BandwidthResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("server/bandwidth?SUBID=" & SubId, _ApiKey)

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.BandWidth = JsonConvert.DeserializeObject(Of BandWidth)(st)
                End Using
            End If

            Return New BandwidthResult With {.ApiResponse = httpResponse, .BandWidth = answer.BandWidth}

        End Function
        ''' <summary>
        ''' Create a new virtual machine. You will start being billed for this immediately. The response only contains the SUBID for the new machine. You should use v1/server/list to poll and wait for the machine to be created (as this does not happen instantly). In order to create a server using a snapshot, use OSID 164 and specify a SNAPSHOTID. Similarly, to create a server using an ISO use OSID 159 and specify an ISOID.
        ''' </summary>
        ''' <returns>List of active or panding servers.</returns>
        Function CreateServer() As ServerResult
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
    End Class

End Namespace
