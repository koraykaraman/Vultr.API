Imports System.Net
Imports Vultr.API.Clients

Namespace API
    Public Class VultrClient
        ''' <summary>
        ''' Account Client for Account operations
        ''' </summary>
        ''' <returns>Account and HTTP Response</returns>
        Public Property Account As AccountClient
        ''' <summary>
        ''' Application operations
        ''' </summary>
        ''' <returns>Application and HTTP Response</returns>
        Public Property Application As ApplicationClient
        ''' <summary>
        ''' Auth operations
        ''' </summary>
        ''' <returns>Auth and HTTP Response</returns>
        Public Property Auth As AuthClient
        ''' <summary>
        ''' Backup operations
        ''' </summary>
        ''' <returns>Auth and HTTP Response</returns>
        Public Property Backup As BackupClient
        ''' <summary>
        ''' Server operations
        ''' </summary>
        ''' <returns>Server and HTTP Response</returns>
        Public Property Server As ServerClient
        Public Shared ReadOnly VultrApiUrl As String = "https://api.vultr.com/v1/"

        Public Sub New(ByVal ApiKey As String)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            ServicePointManager.DefaultConnectionLimit = 9999

            Account = New AccountClient(ApiKey)
            Application = New ApplicationClient(ApiKey)
            Auth = New AuthClient(ApiKey)
            Backup = New BackupClient(ApiKey)
            Server = New ServerClient(ApiKey)
        End Sub

    End Class
End Namespace

