Imports System.Net

Namespace API.Models.Responses

    Public Class Region
        Public Property DCID As String
        Public Property name As String
        Public Property country As String
        Public Property continent As String
        Public Property state As String
        Public Property ddos_protection As Boolean
        Public Property block_storage As Boolean
        Public Property regioncode As String
        Public Property availability As Integer()
    End Class

    Public Structure RegionResult
        Public Property Regions As Dictionary(Of Integer, Region)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class PlanIds
        Public Shared Property PlanIds As Integer()
    End Class

    Public Structure RegionAvailabilityResult
        Public Property PlanIds As PlanIds
        Public Property ApiResponse As HttpWebResponse
    End Structure

End Namespace
