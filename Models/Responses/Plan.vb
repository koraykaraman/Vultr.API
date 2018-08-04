Imports System.Net

Namespace API.Models.Responses

    Public Class Plan
        Public Property VPSPLANID As String
        Public Property name As String
        Public Property vcpu_count As String
        Public Property ram As String
        Public Property disk As String
        Public Property bandwidth As String
        Public Property price_per_month As String
        Public Property windows As Boolean
        Public Property plan_type As String
        Public Property available_locations As Integer()
        Public Property deprecated As Boolean
    End Class

    Public Structure PlanResult
        Public Property Plans As Dictionary(Of Integer, Plan)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class BareMetalPlan
        Public Property METALPLANID As String
        Public Property name As String
        Public Property cpu_count As Integer
        Public Property ram As Integer
        Public Property disk As String
        Public Property bandwidth_tb As Integer
        Public Property price_per_month As Integer
        Public Property plan_type As String
        Public Property deprecated As Boolean
        Public Property available_locations As Integer()
    End Class

    Public Structure BareMetalPlanResult
        Public Property Plans As Dictionary(Of Integer, BareMetalPlan)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class VC2Plan
        Public Property VPSPLANID As String
        Public Property name As String
        Public Property vcpu_count As String
        Public Property ram As String
        Public Property disk As String
        Public Property bandwidth As String
        Public Property price_per_month As String
        Public Property plan_type As String
        Public Property deprecated As Boolean
        Public Property available_locations As Integer()
    End Class

    Public Structure VC2PlanResult
        Public Property Plans As Dictionary(Of Integer, VC2Plan)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class VDC2Plan
        Public Property VPSPLANID As String
        Public Property name As String
        Public Property vcpu_count As String
        Public Property ram As String
        Public Property disk As String
        Public Property bandwidth As String
        Public Property price_per_month As String
        Public Property plan_type As String
        Public Property deprecated As Boolean
        Public Property available_locations As Integer()
    End Class

    Public Structure VDC2PlanResult
        Public Property Plans As Dictionary(Of Integer, VDC2Plan)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace