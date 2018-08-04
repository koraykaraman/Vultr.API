Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class NetworkClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all private networks on the current account.
        ''' </summary>
        ''' <returns>List of all private networks on the current account.</returns>
        Function GetNetworks() As NetworkResult
            Dim answer As New Dictionary(Of String, Network)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("network/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, Network))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New NetworkResult With {.ApiResponse = httpResponse, .Networks = answer}
        End Function

        ''' <summary>
        ''' Create a new private network. A private network can only be used at the location for which it was created.
        ''' </summary>
        ''' <returns>Network element with only NETWORKID.</returns>
        Function CreateNetwork(ByVal Network As Network) As NetworkCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("DCID", Network.DCID))
            dict.Add(New KeyValuePair(Of String, Object)("description", Network.description))
            dict.Add(New KeyValuePair(Of String, Object)("v4_subnet", Network.v4_subnet))
            dict.Add(New KeyValuePair(Of String, Object)("v4_subnet_mask", Network.v4_subnet_mask))

            Dim answer As New Network
            Dim httpResponse = Extensions.ApiClient.ApiExecute("network/create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Network)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New NetworkCreateResult With {.ApiResponse = httpResponse, .Network = answer}
        End Function

        ''' <summary>
        ''' Destroy (delete) a private network. Before destroying, a network must be disabled from all instances. See Server Functions DisablePrivateNetwork().
        ''' </summary>
        ''' <param name="NetworkId">Unique identifier for this network.  These can be found using the GetNetworks() call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteNetwork(ByVal NetworkId As String) As NetworkDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("NETWORKID", NetworkId))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("network/destroy", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New NetworkDeleteResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace
