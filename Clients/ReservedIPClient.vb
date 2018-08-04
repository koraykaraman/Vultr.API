
Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class ReservedIPClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all the active reserved IPs on this account. The "subnet_size" field is the size of the network assigned to this subscription. This will typically be a /64 for IPv6, or a /32 for IPv4.
        ''' </summary>
        ''' <returns>List of all the active reserved IPs on this account.</returns>
        Function GetReservedIPs() As ReservedIPResult
            Dim answer As New Dictionary(Of String, ReservedIP)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("reservedip/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, ReservedIP))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ReservedIPResult With {.ApiResponse = httpResponse, .ReservedIPs = answer}
        End Function

        ''' <summary>
        ''' Create a new reserved IP. Reserved IPs can only be used within the same datacenter for which they were created.
        ''' </summary>
        ''' <param name="DCID">Location to create this reserved IP in.</param>
        ''' <param name="ip_type">'v4' or 'v6' Type of reserved IP to create</param>
        ''' <param name="label">Label for this reserved IP.</param>
        ''' <returns>ReservedIP element with only SUBID.</returns>
        Function CreateReservedIp(ByVal DCID As Integer, ByVal ip_type As IPTYPE, ByVal label As String) As ReservedIPCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("DCID", DCID))
            dict.Add(New KeyValuePair(Of String, Object)("ip_type", ip_type.ToString))
            dict.Add(New KeyValuePair(Of String, Object)("label", label))

            Dim answer As New ReservedIP
            Dim httpResponse = Extensions.ApiClient.ApiExecute("reservedip/create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of ReservedIP)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ReservedIPCreateResult With {.ApiResponse = httpResponse, .ReservedIP = answer}
        End Function

        ''' <summary>
        ''' Convert an existing IP on a subscription to a reserved IP. Returns the SUBID of the newly created reserved IP.
        ''' </summary>
        ''' <param name="SUBID">SUBID of the server that currently has the IP address you want to convert</param>
        ''' <param name="ip_address">IP address you want to convert (v4 must be a /32, v6 must be a /64)</param>
        ''' <param name="label">Label for this reserved IP.</param>
        ''' <returns>ReservedIP element with only SUBID.</returns>
        Function ConvertReservedIp(ByVal SUBID As Integer, ByVal ip_address As String, ByVal label As String) As ReservedIPConvertResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("ip_address", ip_address))
            dict.Add(New KeyValuePair(Of String, Object)("label", label))

            Dim answer As New ReservedIP
            Dim httpResponse = Extensions.ApiClient.ApiExecute("reservedip/convert", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of ReservedIP)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ReservedIPConvertResult With {.ApiResponse = httpResponse, .ReservedIP = answer}
        End Function

        ''' <summary>
        ''' Attach a reserved IP to an existing subscription. This feature operates normally when networking conditions are stable, but it is not reliable for conditions when high availability is needed. For HA, see our High Availability on Vultr with Floating IP and BGP guide.
        ''' </summary>
        ''' <param name="attach_SUBID">Unique identifier of the target server.</param>
        ''' <param name="ip_address">Reserved IP to be attached. Include the subnet size in this parameter (e.g: /32 or /64).</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function AttachReservedIp(ByVal attach_SUBID As Integer, ByVal ip_address As String) As ReservedIPUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("attach_SUBID", attach_SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("ip_address", ip_address))
            Dim httpResponse = Extensions.ApiClient.ApiExecute("reservedip/attach", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New ReservedIPUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Detach a reserved IP to an existing subscription. This feature operates normally when networking conditions are stable, but it is not reliable for conditions when high availability is needed. For HA, see our High Availability on Vultr with Floating IP and BGP guide.
        ''' </summary>
        ''' <param name="detach_SUBID">Unique identifier of the target server.</param>
        ''' <param name="ip_address">Reserved IP to be detached. Include the subnet size in this parameter (e.g: /32 or /64).</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DetachReservedIp(ByVal detach_SUBID As Integer, ByVal ip_address As String) As ReservedIPUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("detach_SUBID", detach_SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("ip_address", ip_address))
            Dim httpResponse = Extensions.ApiClient.ApiExecute("reservedip/detach", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New ReservedIPUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Remove a reserved IP from your account. After making this call, you will not be able to recover the IP address.
        ''' </summary>
        ''' <param name="ip_address">Reserved IP to remove from your account.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteReservedIp(ByVal ip_address As String) As ReservedIPUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("ip_address", ip_address))
            Dim httpResponse = Extensions.ApiClient.ApiExecute("reservedip/destroy", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New ReservedIPUpdateResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace