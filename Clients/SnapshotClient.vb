Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class SnapshotClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all snapshots on the current account.
        ''' </summary>
        ''' <returns>List of all snapshots on the current account.</returns>
        Function GetSnapshots() As SnapshotResult
            Dim answer As New Dictionary(Of String, Snapshot)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("snapshot/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, Snapshot))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New SnapshotResult With {.ApiResponse = httpResponse, .Snapshots = answer}
        End Function

        ''' <summary>
        ''' Create a snapshot from an existing virtual machine. The virtual machine does not need to be stopped.
        ''' </summary>
        ''' <param name="SUBID">Identifier of the virtual machine to create a snapshot from.</param>
        ''' <param name="Description">Description of snapshot contents</param>
        ''' <returns>Network element with only NETWORKID.</returns>
        Function CreateSnapshot(ByVal SUBID As Integer, ByVal Description As String) As SnapshotCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("description", Description))

            Dim answer As New Snapshot
            Dim httpResponse = Extensions.ApiClient.ApiExecute("snapshot/create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Snapshot)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New SnapshotCreateResult With {.ApiResponse = httpResponse, .Snapshot = answer}
        End Function

        ''' <summary>
        ''' Destroy (delete) a snapshot. There is no going back from this call.
        ''' </summary>
        ''' <param name="SNAPSHOTID">Unique identifier for this snapshot. These can be found Using the GetSnapshots() Call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteSnapshot(ByVal SNAPSHOTID As String) As SnapshotDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SNAPSHOTID", SNAPSHOTID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("snapshot/destroy", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New SnapshotDeleteResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace