Imports System.Net

Namespace API.Models.Responses
    Public Class Block
        Public Property SUBID As Integer
        Public Property date_created As String
        Public Property cost_per_month As Integer
        Public Property status As String
        Public Property size_gb As Integer
        Public Property DCID As Integer
        Public Property attached_to_SUBID As Integer?
        Public Property label As String
    End Class

    Public Structure BlockCreateResult
        Public Property Block As Block
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure BlockResult
        Public Property Blocks As List(Of Block)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure BlockUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace