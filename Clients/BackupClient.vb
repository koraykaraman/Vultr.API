Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses
Imports System.Collections.Specialized

Namespace API.Clients
    Public Class BackupClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all backups on the current account.
        ''' </summary>
        ''' <returns>Returns backup list and HTTP API Respopnse.</returns>
        Function GetBackups() As BackupResult

            Dim answer As New BackupResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("backup/list", _ApiKey)

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.Backups = JsonConvert.DeserializeObject(Of Dictionary(Of String, Backup))(If(st = "[]", "{}", st))
                End Using
            End If

            Return New BackupResult With {.ApiResponse = httpResponse, .Backups = answer.Backups}

        End Function

        ''' <summary>
        ''' Filter result set to only contain this backup.
        ''' </summary>
        ''' <param name="BackupId">BackupId for getting result for backup.</param>
        ''' <returns>Returns backup and HTTP API Respopnse.</returns>
        Function GetBackupByBackupId(ByVal BackupId As String) As BackupResult

            Dim dict As New List(Of KeyValuePair(Of String, String))
            dict.Add(New KeyValuePair(Of String, String)("BACKUPID", BackupId))

            Dim answer As New BackupResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("backup/list", _ApiKey, dict)

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.Backups = JsonConvert.DeserializeObject(Of Dictionary(Of String, Backup))(If(st = "[]", "{}", st))
                End Using
            End If

            Return New BackupResult With {.ApiResponse = httpResponse, .Backups = answer.Backups}

        End Function

        ''' <summary>
        ''' Filter result set to only contain backups of this subscription object.
        ''' </summary>
        ''' <param name="SubId">Filter result set to only contain backups of this subscription object</param>
        ''' <returns>Returns backup and HTTP API Respopnse.</returns>
        Function GetBackupBySUBID(ByVal SubId As String) As BackupResult

            Dim dict As New List(Of KeyValuePair(Of String, String))
            dict.Add(New KeyValuePair(Of String, String)("SUBID", SubId))

            Dim answer As New BackupResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("backup/list", _ApiKey, dict)

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.Backups = JsonConvert.DeserializeObject(Of Dictionary(Of String, Backup))(If(st = "[]", "{}", st))
                End Using
            End If

            Return New BackupResult With {.ApiResponse = httpResponse, .Backups = answer.Backups}

        End Function
    End Class
End Namespace

