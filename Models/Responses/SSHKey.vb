Imports System.Net

Namespace API.Models.Responses
    Public Class SSHKey
        Public Property SSHKEYID As String
        Public Property date_created As Object
        Public Property name As String
        Public Property ssh_key As String
    End Class

    Public Structure SSHKeyCreateResult
        Public Property SSHKey As SSHKey
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure SSHKeyDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure SSHKeyUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure SSHKeyResult
        Public Property SSHKeys As Dictionary(Of String, SSHKey)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace