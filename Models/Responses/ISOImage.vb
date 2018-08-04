Imports System.ComponentModel
Imports System.Net

Namespace API.Models.Responses
    Public Class ISOImage
        Public Property ISOID As Integer
        Public Property name As String
        Public Property description As String
        Public Property date_created As String
        Public Property filename As String
        Public Property size As Integer
        Public Property md5sum As String
        Public Property status As String
    End Class

    Public Structure ISOImageCreateResult
        Public Property ISOImage As ISOImage
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure ISOImageResult
        Public Property ISOImages As Dictionary(Of String, ISOImage)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace
