Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class RegionClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve a list of all active regions. Note that just because a region is listed here, does not mean that there is room for new servers.
        ''' </summary>
        ''' <returns>List of active regions.</returns>
        Function GetRegions() As RegionResult
            Dim answer As New Dictionary(Of Integer, Region)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("regions/list?availability=yes", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, Region))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New RegionResult With {.ApiResponse = httpResponse, .Regions = answer}
        End Function

        ''' <summary>
        ''' Retrieve a list of the VPSPLANIDs currently available in this location. 
        ''' </summary>
        ''' <param name="DCID">Location to check availability.</param>
        ''' <param name="type">The type of plans for which to include availability. Possible values: "all", "vc2", "ssd", "vdc2", "dedicated".</param>
        ''' <returns>List of the VPSPLANIDs currently available in this location.</returns>
        Function GetAvailablePlans(ByVal DCID As Integer, Optional ByVal [type] As String = "all") As RegionAvailabilityResult
            If type = "all" Or type = "vc2" Or type = "ssd" Or type = "vdc2" Or type = "dedicated" Then
                type = type
            Else
                type = "all"
            End If
            Dim answer As New PlanIds
            Dim httpResponse = Extensions.ApiClient.ApiExecute("regions/availability?DCID=" & DCID & "&type=" & type, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of PlanIds)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New RegionAvailabilityResult With {.ApiResponse = httpResponse, .PlanIds = answer}
        End Function

        ''' <summary>
        ''' Retrieve a list of the METALPLANIDs currently available in this location.
        ''' </summary>
        ''' <param name="DCID">Location to check availability.</param>
        ''' <returns>List of the Bare Metal Plans currently available in this location.</returns>
        Function GetAvailableBareMetalPlans(ByVal DCID As Integer) As RegionAvailabilityResult
            Dim answer As New PlanIds
            Dim httpResponse = Extensions.ApiClient.ApiExecute("regions/availability_baremetal?DCID=" & DCID, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of PlanIds)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New RegionAvailabilityResult With {.ApiResponse = httpResponse, .PlanIds = answer}
        End Function


        ''' <summary>
        ''' Retrieve a list of the vc2 VPSPLANIDs currently available in this location.
        ''' </summary>
        ''' <param name="DCID">Location to check availability.</param>
        ''' <returns>List of the vc2 VPSPLANIDs currently available in this location.</returns>
        Function GetAvailableVC2Plans(ByVal DCID As Integer) As RegionAvailabilityResult
            Dim answer As New PlanIds
            Dim httpResponse = Extensions.ApiClient.ApiExecute("regions/availability_vc2?DCID=" & DCID, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of PlanIds)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New RegionAvailabilityResult With {.ApiResponse = httpResponse, .PlanIds = answer}
        End Function

        ''' <summary>
        ''' Retrieve a list of the vdc2 VPSPLANIDs currently available in this location.
        ''' </summary>
        ''' <param name="DCID">Location to check availability.</param>
        ''' <returns>List of the vdc2 VPSPLANIDs currently available in this location.</returns>
        Function GetAvailableVDC2Plans(ByVal DCID As Integer) As RegionAvailabilityResult
            Dim answer As New PlanIds
            Dim httpResponse = Extensions.ApiClient.ApiExecute("regions/availability_vdc2?DCID=" & DCID, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of PlanIds)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New RegionAvailabilityResult With {.ApiResponse = httpResponse, .PlanIds = answer}
        End Function
    End Class
End Namespace