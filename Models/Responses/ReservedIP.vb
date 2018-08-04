
Imports System.Net

Namespace API.Models.Responses
    Public Class ReservedIP
        Public Property SUBID As Integer
        Public Property DCID As Integer
        Public Property ip_type As String
        Public Property subnet As String
        Public Property subnet_size As Integer
        Public Property label As String
        Public Property attached_SUBID As Integer
    End Class

    Public Structure ReservedIPResult
        Public Property ReservedIPs As Dictionary(Of String, ReservedIP)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure ReservedIPCreateResult
        Public Property ReservedIP As ReservedIP
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure ReservedIPConvertResult
        Public Property ReservedIP As ReservedIP
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure ReservedIPUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure


End Namespace