Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class PlanClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve a list of all active plans. Plans that are no longer available will not be shown. The "windows" field Is no longer in use, And will always be false. Windows licenses will be automatically added to any plan as necessary. The "deprecated" field indicates that the plan will be going away in the future. New deployments of it will still be accepted, but you should begin to transition away from its usage. Typically, deprecated plans are available for 30 days after they are deprecated. The sandbox plan Is Not available In the API.
        ''' </summary>
        ''' <param name="type">The type of plans to return. Possible values: "all", "vc2", "ssd", "vdc2", "dedicated".</param>
        ''' <returns>List of active or deprecated plans.</returns>
        Function GetPlans(Optional ByVal [type] As String = "all") As PlanResult
            If type = "all" Or type = "vc2" Or type = "ssd" Or type = "vdc2" Or type = "dedicated" Then
                type = type
            Else
                type = "all"
            End If
            Dim answer As New Dictionary(Of Integer, Plan)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("plans/list?type=" & type, _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, Plan))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New PlanResult With {.ApiResponse = httpResponse, .Plans = answer}
        End Function

        ''' <summary>
        ''' Retrieve a list of all active bare metal plans. Plans that are no longer available will not be shown. The 'deprecated' field indicates that the plan will be going away in the future. New deployments of it will still be accepted, but you should begin to transition away from its usage. Typically, deprecated plans are available for 30 days after they have been deprecated.
        ''' </summary>
        ''' <returns>List of active or deprecated bare metal plans.</returns>
        Function GetBaremetalPlans() As BareMetalPlanResult
            Dim answer As New Dictionary(Of Integer, BareMetalPlan)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("plans/list_baremetal", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, BareMetalPlan))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New BareMetalPlanResult With {.ApiResponse = httpResponse, .Plans = answer}
        End Function

        ''' <summary>
        ''' Retrieve a list of all active vc2 plans. Plans that are no longer available will not be shown. The 'deprecated' field indicates that the plan will be going away in the future. New deployments of it will still be accepted, but you should begin to transition away from its usage. Typically, deprecated plans are available for 30 days after they are deprecated. Note: The sandbox plan Is Not available In the API.
        ''' </summary>
        ''' <returns>List of active or deprecated VC2 plans.</returns>
        Function GetVC2Plans() As VC2PlanResult
            Dim answer As New Dictionary(Of Integer, VC2Plan)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("plans/list_vc2", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, VC2Plan))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New VC2PlanResult With {.ApiResponse = httpResponse, .Plans = answer}
        End Function

        ''' <summary>
        ''' Retrieve a list of all active vdc2 plans. Plans that are no longer available will not be shown. The 'deprecated' field indicates that the plan will be going away in the future. New deployments of it will still be accepted, but you should begin to transition away from its usage. Typically, deprecated plans are available for 30 days after they are deprecated.
        ''' </summary>
        ''' <returns>List of active or deprecated VDC2 plans.</returns>
        Function GetVDC2Plans() As VDC2PlanResult
            Dim answer As New Dictionary(Of Integer, VDC2Plan)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("plans/list_vdc2", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of Integer, VDC2Plan))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New VDC2PlanResult With {.ApiResponse = httpResponse, .Plans = answer}
        End Function

    End Class
End Namespace