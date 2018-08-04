Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class DNSClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all domains associated with the current account.
        ''' </summary>
        ''' <returns>List of all domains associated with the current account.</returns>
        Function GetDomains() As DomainResult
            Dim answer As New List(Of Domain)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of List(Of Domain))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New DomainResult With {.ApiResponse = httpResponse, .Domains = answer}
        End Function

        ''' <summary>
        ''' List all the records associated with a particular domain.
        ''' </summary>
        ''' <param name="domain">Domain to list records for</param>
        ''' <returns>List of all records associated with the current domain.</returns>
        Function GetDomainRecords(ByVal domain As String) As RecordResult
            Dim answer As New List(Of Record)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/records?domain=" & domain, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of List(Of Record))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New RecordResult With {.ApiResponse = httpResponse, .Records = answer}
        End Function

        ''' <summary>
        ''' Create a domain name in DNS.
        ''' </summary>
        ''' <param name="domain">Domain name to create</param>
        ''' <param name="serverip">Server IP to use when creating default records (A and MX)</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function CreateDomain(ByVal domain As String, ByVal serverip As IPAddress) As DomainCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))
            dict.Add(New KeyValuePair(Of String, Object)("serverip", serverip.ToString))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/create_domain", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New DomainCreateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Add a DNS record.
        ''' </summary>
        ''' <param name="domain">Domain name to add record to</param>
        ''' <param name="Record">Details of a record</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function CreateDomain(ByVal Record As Record, ByVal domain As String) As DomainCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))
            dict.Add(New KeyValuePair(Of String, Object)("data", Record.data))
            dict.Add(New KeyValuePair(Of String, Object)("name", Record.name))
            dict.Add(New KeyValuePair(Of String, Object)("type", Record.type))
            dict.Add(New KeyValuePair(Of String, Object)("ttl", Record.ttl))
            dict.Add(New KeyValuePair(Of String, Object)("priority", Record.priority))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/create_record", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New DomainCreateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Delete a domain name and all associated records.
        ''' </summary>
        ''' <param name="domain">Domain name to delete</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteDomain(ByVal domain As String) As DomainDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/delete_domain", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New DomainDeleteResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Delete an individual DNS record.
        ''' </summary>
        ''' <param name="domain">Domain name to delete record from</param>
        ''' <param name="RECORDID">ID of record to delete. See GetDomainRecords()</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteRecord(ByVal domain As String, ByVal RECORDID As Integer) As RecordDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))
            dict.Add(New KeyValuePair(Of String, Object)("RECORDID", RECORDID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/delete_record", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New RecordDeleteResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Enable DNSSEC for a domain. 
        ''' </summary>
        ''' <param name="domain">Domain name to update record. DNSSEC will be enabled for the given domain</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DNSSECEnable(ByVal domain As String) As DomainUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))
            dict.Add(New KeyValuePair(Of String, Object)("enable", "yes"))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/dnssec_enable", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New DomainUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Get the DNSSEC keys (if enabled) for a domain. 
        ''' </summary>
        ''' <param name="domain">Domain from which to gather DNSSEC keys.</param>
        ''' <returns>DNSSEC keys</returns>
        Function DNSSECInfo(ByVal domain As String) As DNSSECKeyResult
            Dim answer As New JArray
            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/dnssec_info?domain=" & domain, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JArray.Parse(st)
                End Using
            End If
            Return New DNSSECKeyResult With {.ApiResponse = httpResponse, .DNSSECKeys = answer}
        End Function

        ''' <summary>
        ''' Get the SOA record information for a domain. 
        ''' </summary>
        ''' <param name="domain">Domain from which to gather SOA information</param>
        ''' <returns>SOA Record</returns>
        Function SOAInfo(ByVal domain As String) As SOAInfoResult
            Dim answer As New SOARecord
            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/soa_info?domain=" & domain, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of SOARecord)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New SOAInfoResult With {.ApiResponse = httpResponse, .Record = answer}
        End Function

        ''' <summary>
        ''' Update the SOA record information for a domain. 
        ''' </summary>
        ''' <param name="domain">Domain name to update record</param>
        ''' <param name="SOARecord">SOA record</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function SOAUpdate(ByVal domain As String, ByVal SOARecord As SOARecord) As DomainUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))
            dict.Add(New KeyValuePair(Of String, Object)("nsprimary", SOARecord.nsprimary))
            dict.Add(New KeyValuePair(Of String, Object)("email", SOARecord.email))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/soa_update", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New DomainUpdateResult With {.ApiResponse = httpResponse}
        End Function


        ''' <summary>
        ''' Update the SOA record information for a domain. 
        ''' </summary>
        ''' <param name="domain">Domain name to update record</param>
        ''' <param name="Record">DNS record</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function UpdateRecord(ByVal domain As String, ByVal Record As Record) As DomainUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("domain", domain))
            dict.Add(New KeyValuePair(Of String, Object)("RECORDID", Record.RECORDID))
            dict.Add(New KeyValuePair(Of String, Object)("name", Record.name))
            dict.Add(New KeyValuePair(Of String, Object)("data", Record.data))
            dict.Add(New KeyValuePair(Of String, Object)("ttl", Record.ttl))
            dict.Add(New KeyValuePair(Of String, Object)("priority", Record.priority))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("dns/update_record", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New DomainUpdateResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace