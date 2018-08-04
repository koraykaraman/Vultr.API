Imports System.Net

Namespace API.Models.Responses
    Public Class Network
        Public Property DCID As String
        Public Property NETWORKID As String
        Public Property date_created As String
        Public Property description As String
        Public Property v4_subnet As String
        Public Property v4_subnet_mask As Integer
    End Class

    Public Structure NetworkCreateResult
        Public Property Network As Network
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure NetworkDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure NetworkResult
        Public Property Networks As Dictionary(Of String, Network)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace