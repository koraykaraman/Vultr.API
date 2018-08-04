Imports System.Net
Imports Newtonsoft.Json.Linq

Namespace API.Models.Responses
    Public Class Domain
        Public Property domain As String
        Public Property date_created As String
    End Class

    Public Class Record
        Public Property type As String
        Public Property name As String
        Public Property data As String
        Public Property priority As Integer
        Public Property RECORDID As Integer
        Public Property ttl As Integer
    End Class

    Public Class SOARecord
        Public Property nsprimary As String
        Public Property email As String
    End Class

    Public Structure RecordResult
        Public Property Records As List(Of Record)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure DomainResult
        Public Property Domains As List(Of Domain)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure DomainCreateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure DomainUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure SOAInfoResult
        Public Property Record As SOARecord
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure DomainDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure RecordDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure


    Public Structure DNSSECKeyResult
        Public Property DNSSECKeys As JArray
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace