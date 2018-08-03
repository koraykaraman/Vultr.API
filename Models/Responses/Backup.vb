Imports System.Net

Namespace API.Models.Responses

    Public Class Backup
        Public Property BACKUPID As String
        Public Property date_created As String
        Public Property description As String
        Public Property size As String
        Public Property status As String
    End Class

    Public Structure BackupResult
        Public Property Backups As Dictionary(Of String, Backup)
        Public Property ApiResponse As HttpWebResponse
    End Structure

End Namespace