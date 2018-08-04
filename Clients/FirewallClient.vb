Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class FirewallClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all firewall groups on the current account.
        ''' </summary>
        ''' <returns>List of all firewall groups on the current account.</returns>
        Function GetFirewallGroups() As FirewallGroupResult
            Dim answer As New Dictionary(Of String, FirewallGroup)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/group_list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, FirewallGroup))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New FirewallGroupResult With {.ApiResponse = httpResponse, .FirewallGroups = answer}
        End Function

        ''' <summary>
        ''' Create a new firewall group on the current account.
        ''' </summary>
        ''' <param name="description">Description of firewall group.</param>
        ''' <returns>FirewallGroup element with only FIREWALLGROUPID.</returns>
        Function CreateFirewallGroup(ByVal description As String) As FirewallGroupCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("description", description))
            Dim answer As New FirewallGroup
            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/group_create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of FirewallGroup)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New FirewallGroupCreateResult With {.ApiResponse = httpResponse, .FirewallGroup = answer}
        End Function

        ''' <summary>
        ''' Delete a firewall group. Use this function with caution because the firewall group being deleted will be detached from all servers. This can result in open ports accessible to the internet.
        ''' </summary>
        ''' <param name="FIREWALLGROUPID">Unique identifier for Firewall group to delete. These can be found Using the GetFirewallGroups()</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteFirewallGroup(ByVal FIREWALLGROUPID As String) As FirewallGroupDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("FIREWALLGROUPID", FIREWALLGROUPID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/group_delete", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New FirewallGroupDeleteResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Update firewall group on the current account.
        ''' </summary>
        ''' <param name="description">Description of firewall group.</param>
        ''' <param name="FIREWALLGROUPID">Unique identifier for Firewall group to delete. These can be found Using the GetFirewallGroups()</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function UpdateFirewallGroup(ByVal description As String, ByVal FIREWALLGROUPID As String) As FirewallGroupUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("description", description))
            dict.Add(New KeyValuePair(Of String, Object)("FIREWALLGROUPID", FIREWALLGROUPID))
            Dim answer As New FirewallGroup
            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/group_set_description", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New FirewallGroupUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' List the rules in a firewall group.
        ''' </summary>
        ''' <param name="FIREWALLGROUPID">Target firewall group. See GetFirewallGroups()</param>
        ''' <param name="ip_type">IP address type. Possible values: "IPV4", "IPV6"</param>
        ''' <param name="direction">Direction of rule.</param>
        ''' <returns>List of all firewall roles on the current account.</returns>
        Function GetFirewallRules(ByVal FIREWALLGROUPID As String, ByVal ip_type As IPTYPE, ByVal direction As FirewallDirection) As FirewallRuleResult
            If IsNothing(ip_type) Then
                ip_type = IPTYPE.IPV4
            End If
            Dim answer As New Dictionary(Of String, FirewallRule)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/rule_list?FIREWALLGROUPID=" & FIREWALLGROUPID & "&direction=in&ip_type=" & ip_type.ToString, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, FirewallRule))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New FirewallRuleResult With {.ApiResponse = httpResponse, .FirewallRules = answer}
        End Function

        ''' <summary>
        ''' Create a rule in a firewall group.
        ''' </summary>
        ''' <param name="FirewallRule">New FirewallRule object.</param>
        ''' <returns>FirewallGroup element with only FIREWALLGROUPID.</returns>
        Function CreateFirewallRule(ByVal FIREWALLGROUPID As String, ByVal FirewallRule As FirewallRule, ByVal FirewallDirection As FirewallDirection) As FirewallRuleCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("FIREWALLGROUPID", FIREWALLGROUPID))
            dict.Add(New KeyValuePair(Of String, Object)("direction", FirewallDirection.ToString))
            dict.Add(New KeyValuePair(Of String, Object)("action", FirewallRule.action))
            dict.Add(New KeyValuePair(Of String, Object)("port", FirewallRule.port))
            dict.Add(New KeyValuePair(Of String, Object)("protocol", FirewallRule.protocol))
            dict.Add(New KeyValuePair(Of String, Object)("subnet", FirewallRule.subnet))
            dict.Add(New KeyValuePair(Of String, Object)("subnet_size", FirewallRule.subnet_size))

            Dim answer As New FirewallRule
            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/rule_create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of FirewallRule)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New FirewallRuleCreateResult With {.ApiResponse = httpResponse, .FirewallRule = answer}
        End Function

        ''' <summary>
        ''' Delete a rule in a firewall group.
        ''' </summary>
        ''' <param name="FIREWALLGROUPID">Unique identifier for Firewall group to delete. These can be found Using the GetFirewallGroups()</param>
        ''' <param name="rulenumber">Unique identifier for Firewall rule to delete. These can be found Using the GetFirewallRules()</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteFirewallRule(ByVal FIREWALLGROUPID As String, ByVal rulenumber As Integer) As FirewallRuleDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("FIREWALLGROUPID", FIREWALLGROUPID))
            dict.Add(New KeyValuePair(Of String, Object)("rulenumber", rulenumber))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("firewall/rule_delete", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New FirewallRuleDeleteResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace