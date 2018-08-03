Imports System.Net
Namespace API.Models.Responses

    Public Class Account
        Public Property balance As String
        Public Property pending_charges As String
        Public Property last_payment_date As String
        Public Property last_payment_amount As String
    End Class

    Public Structure AccountResult
        Public Property Account As Account
        Public Property ApiResponse As HttpWebResponse
    End Structure

End Namespace
