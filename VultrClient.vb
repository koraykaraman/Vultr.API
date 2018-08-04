﻿Imports System.Net
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
        ''' <returns>Backup and HTTP Response</returns>
        Public Property Backup As BackupClient
        ''' <summary>
        ''' Block operations
        ''' </summary>
        ''' <returns>Block and HTTP Response</returns>
        Public Property Block As BlockClient
        ''' <summary>
        ''' DNS operations
        ''' </summary>
        ''' <returns>DNS and HTTP Response</returns>
        Public Property DNS As DNSClient
        ''' <summary>
        ''' Firewall operations
        ''' </summary>
        ''' <returns>Firewall and HTTP Response</returns>
        Public Property Firewall As FirewallClient
        ''' <summary>
        ''' ISO Image operations
        ''' </summary>
        ''' <returns>ISO Image and HTTP Response</returns>
        ''' 
        Public Property ISOImage As ISOImageClient
        ''' <summary>
        ''' Network operations
        ''' </summary>
        ''' <returns>Network and HTTP Response</returns>
        Public Property Network As NetworkClient
        ''' <summary>
        ''' Plan operations
        ''' </summary>
        ''' <returns>Plan and HTTP Response</returns>
        Public Property Plan As PlanClient
        ''' <summary>
        ''' Region operations
        ''' </summary>
        ''' <returns>Region and HTTP Response</returns>
        Public Property Region As RegionClient
        ''' <summary>
        ''' Server operations
        ''' </summary>
        ''' <returns>Server and HTTP Response</returns>
        ''' 
        Public Property Server As ServerClient
        ''' <summary>
        ''' Snapshot operations
        ''' </summary>
        ''' <returns>Snapshot and HTTP Response</returns>
        '''
        Public Property Snapshot As SnapshotClient
        ''' <summary>
        ''' SSHKey operations
        ''' </summary>
        ''' <returns>SSHKey and HTTP Response</returns>
        '''
        Public Property SSHKey As SSHKeyClient
        ''' <summary>
        ''' Startup Script operations
        ''' </summary>
        ''' <returns>StartupScript and HTTP Response</returns>
        '''
        Public Property StartupScript As StartupScriptClient
        ''' <summary>
        ''' Operating system operations
        ''' </summary>
        ''' <returns>Operating system and HTTP Response</returns>
        Public Property OperatingSystem As OperatingSystemClient
        ''' <summary>
        ''' User operations
        ''' </summary>
        ''' <returns>Users and HTTP Response</returns>
        Public Property User As UserClient

        Public Shared ReadOnly VultrApiUrl As String = "https://api.vultr.com/v1/"

        Public Sub New(ByVal ApiKey As String)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            ServicePointManager.DefaultConnectionLimit = 9999

            Account = New AccountClient(ApiKey)
            Application = New ApplicationClient(ApiKey)
            Auth = New AuthClient(ApiKey)
            Backup = New BackupClient(ApiKey)
            Block = New BlockClient(ApiKey)
            DNS = New DNSClient(ApiKey)
            Firewall = New FirewallClient(ApiKey)
            ISOImage = New ISOImageClient(ApiKey)
            Network = New NetworkClient(ApiKey)
            OperatingSystem = New OperatingSystemClient(ApiKey)
            Plan = New PlanClient(ApiKey)
            Region = New RegionClient(ApiKey)
            Server = New ServerClient(ApiKey)
            Snapshot = New SnapshotClient(ApiKey)
            SSHKey = New SSHKeyClient(ApiKey)
            StartupScript = New StartupScriptClient(ApiKey)
            User = New UserClient(ApiKey)

        End Sub

    End Class
End Namespace

