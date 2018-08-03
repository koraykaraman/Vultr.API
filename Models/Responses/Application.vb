Imports System.Net

Namespace API.Models.Responses

    ''' <summary>
    ''' Application APPID, name, short_name, deploy_name and surcharge.
    ''' </summary>
    Public Class Application
        ''' <summary>
        ''' Application ID
        ''' </summary>
        ''' <returns>Integer</returns>
        ''' <example>1</example>
        Public Property APPID As String
        ''' <summary>
        ''' Application name as string
        ''' </summary>
        ''' <returns>String</returns>
        ''' <example>LEMP</example>
        Public Property name As String
        ''' <summary>
        ''' Application short name
        ''' </summary>
        ''' <returns>lemp</returns>
        Public Property short_name As String
        ''' <summary>
        ''' Application deploy name
        ''' </summary>
        ''' <returns>String</returns>
        ''' <example>LEMP on CentOS 6 x64</example>
        Public Property deploy_name As String
        ''' <summary>
        ''' Application's surcharge
        ''' </summary>
        ''' <returns>Double</returns>
        ''' <example>0</example>
        Public Property surcharge As String
    End Class

    ''' <summary>
    ''' Applications and HTTP API Response
    ''' </summary>
    Public Structure ApplicationResult
        ''' <summary>
        ''' Returns API Result with Applications Dictionary (Of String, Application)
        ''' </summary>
        ''' <returns>Applications Dictionary</returns>
        Public Property Applications As Dictionary(Of String, Application)
        ''' <summary>
        ''' Returns API Result with HttpWebResponse
        ''' </summary>
        ''' <returns>HttpWebResponse</returns>
        Public Property ApiResponse As HttpWebResponse
    End Structure

End Namespace