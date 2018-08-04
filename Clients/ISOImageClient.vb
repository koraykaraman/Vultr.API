Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class ISOImageClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all ISOs currently available on this account.
        ''' </summary>
        ''' <returns>List of all ISOs currently available on this account.</returns>
        Function GetISOImages() As ISOImageResult
            Dim answer As New Dictionary(Of String, ISOImage)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("iso/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, ISOImage))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ISOImageResult With {.ApiResponse = httpResponse, .ISOImages = answer}
        End Function
        ''' <summary>
        ''' List public ISOs offered in the Vultr ISO library.
        ''' </summary>
        ''' <returns>List of all public ISOs offered in the Vultr ISO library.</returns>
        Function GetPublicISOImages() As ISOImageResult
            Dim answer As New Dictionary(Of String, ISOImage)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("iso/list_public", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, ISOImage))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New ISOImageResult With {.ApiResponse = httpResponse, .ISOImages = answer}
        End Function

        ''' <summary>
        ''' Create a new ISO image on the current account. The ISO image will be downloaded from a given URL. Download status can be checked with the GetISOImages call.
        ''' </summary>
        ''' <param name="URL">Remote URL from where the ISO will be downloaded.</param>
        ''' <returns>Returns backup list and HTTP API Respopnse.</returns>
        Function CreateISOImage(ByVal URL As Uri) As ISOImageCreateResult

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("url", URL.AbsoluteUri))

            Dim answer As New ISOImageCreateResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("iso/create_from_url", _ApiKey, dict, "POST")

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.ISOImage = JsonConvert.DeserializeObject(Of ISOImage)(If(st = "[]", "{}", st))
                End Using
            End If

            Return New ISOImageCreateResult With {.ApiResponse = httpResponse, .ISOImage = answer.ISOImage}

        End Function
    End Class
End Namespace
