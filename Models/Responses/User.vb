Imports System.ComponentModel
Imports System.Net

Namespace API.Models.Responses
    Public Class User
        Public Property USERID As String
        Public Property name As String
        Public Property password As String
        Public Property email As String
        <DefaultValue("yes")>
        Public Property api_enabled As String
        Public Property acls As acls()
    End Class

    Public Structure UserResult
        Public Property Users As List(Of User)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure UserCreateResult
        Public Property User As User
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure UserDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure UserUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class acls
        Private Key As String

        Public Shared ReadOnly ManageUsers As acls = New acls("manage_users")
        Public Shared ReadOnly Subscriptions As acls = New acls("subscriptions")
        Public Shared ReadOnly Provisioning As acls = New acls("provisioning")
        Public Shared ReadOnly Billing As acls = New acls("billing")
        Public Shared ReadOnly Support As acls = New acls("support")
        Public Shared ReadOnly Abuse As acls = New acls("abuse")
        Public Shared ReadOnly Dns As acls = New acls("dns")
        Public Shared ReadOnly Upgrade As acls = New acls("upgrade")


        Private Sub New(key As String)
            Me.Key = key
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Key
        End Function
    End Class
End Namespace

