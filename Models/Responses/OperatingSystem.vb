Imports System.Net

Namespace API.Models.Responses
    Public Class OperatingSystem
        Public Property OSID As String
        Public Property name As String
        Public Property arch As String
        Public Property family As String
        Public Property windows As Boolean
    End Class

    Public Structure OperatingSystemResult
        Public Property OperatingSystems As Dictionary(Of Integer, OperatingSystem)
        Public Property ApiResponse As HttpWebResponse
    End Structure
End Namespace

