Imports System.Net

Namespace API.Models.Responses
    Public Class Snapshot
        Public Property SNAPSHOTID As String
        Public Property date_created As String
        Public Property description As String
        Public Property size As String
        Public Property status As String
        Public Property OSID As String
        Public Property APPID As String
    End Class

    Public Structure SnapshotCreateResult
        Public Property Snapshot As Snapshot
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure SnapshotDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure SnapshotResult
        Public Property Snapshots As Dictionary(Of String, Snapshot)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace