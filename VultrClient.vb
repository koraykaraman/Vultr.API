Imports Vultr.API.Clients

Namespace API
    Public Class VultrClient
        Public Property Account As AccountClient

        Public Sub New(ByVal ApiKey As String)
            Account = New AccountClient(ApiKey)
        End Sub

    End Class
End Namespace

