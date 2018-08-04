Imports System.Net

Namespace API.Models.Responses
    Public Class FirewallRule
        Public Property rulenumber As Integer
        Public Property action As String
        Public Property protocol As String
        Public Property port As String
        Public Property subnet As String
        Public Property subnet_size As Integer
    End Class

    Public Class FirewallGroup
        Public Property FIREWALLGROUPID As String
        Public Property description As String
        Public Property date_created As String
        Public Property date_modified As String
        Public Property instance_count As Integer
        Public Property rule_count As Integer
        Public Property max_rule_count As Integer
    End Class

    Public Structure FirewallRuleResult
        Public Property FirewallRules As Dictionary(Of String, FirewallRule)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallGroupResult
        Public Property FirewallGroups As Dictionary(Of String, FirewallGroup)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallRuleCreateResult
        Public Property FirewallRule As FirewallRule
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallRuleDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallRuleUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallGroupCreateResult
        Public Property FirewallGroup As FirewallGroup
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallGroupDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure FirewallGroupUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class IPTYPE
        Private Key As String

        Public Shared ReadOnly IPV4 As IPTYPE = New IPTYPE("v4")
        Public Shared ReadOnly IPV6 As IPTYPE = New IPTYPE("v6")

        Private Sub New(key As String)
            Me.Key = key
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Key
        End Function
    End Class

    Public Class FirewallDirection
        Private Key As String

        Public Shared ReadOnly DIRECTIONIN As FirewallDirection = New FirewallDirection("in")
        Public Shared ReadOnly DIRECTIONOUT As FirewallDirection = New FirewallDirection("out")

        Private Sub New(key As String)
            Me.Key = key
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Key
        End Function
    End Class
End Namespace