Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class BlockClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve a list of any active block storage subscriptions on this account.
        ''' </summary>
        ''' <param name="SUBID">Unique identifier of a subscription. Only the subscription object will be returned.</param>
        ''' <returns>List of all any active block storage subscriptions on this account.</returns>
        Function GetBlocks(Optional ByVal SUBID As Integer = Nothing) As BlockResult
            Dim answer As New List(Of Block)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/list" & If(IsNothing(SUBID), "", "?SUBID=" & SUBID), _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of List(Of Block))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New BlockResult With {.ApiResponse = httpResponse, .Blocks = answer}
        End Function

        ''' <summary>
        ''' Resize the block storage volume to a new size. WARNING: When shrinking the volume, you must manually shrink the filesystem And partitions beforehand, Or you will lose data.
        ''' </summary>
        ''' <param name="SUBID">ID of the block storage subscription to resize</param>
        ''' <param name="size_gb">New size (in GB) of the block storage subscription</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function ResizeBlock(ByVal SUBID As Integer, ByVal size_gb As Integer) As BlockUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("size_gb", size_gb))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/resize", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New BlockUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Set the label of a block storage subscription.
        ''' </summary>
        ''' <param name="SUBID">ID of the block storage subscription to rename</param>
        ''' <param name="label">Text label that will be shown in the control panel.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function RenameBlock(ByVal SUBID As Integer, ByVal label As String) As BlockUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("label", label))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/label_set", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New BlockUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Create a block storage subscription.
        ''' </summary>
        ''' <param name="DCID">DCID of the location to create this subscription in. See GetRegions()</param>
        ''' <param name="Block">Block object of this subscription. (Size_gb and label will be used only)</param>
        ''' <returns>Return block object with only SUBID.</returns>
        Function CreateBlock(ByVal DCID As Integer, ByVal Block As Block) As BlockCreateResult
            Dim answer As New Block

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("DCID", DCID))
            dict.Add(New KeyValuePair(Of String, Object)("size_gb", Block.size_gb))
            dict.Add(New KeyValuePair(Of String, Object)("label", Block.label))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Block)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New BlockCreateResult With {.ApiResponse = httpResponse, .Block = answer}
        End Function

        ''' <summary>
        ''' Attach a block storage subscription to a VPS subscription. The block storage volume must not be attached to any other VPS subscriptions for this to work.
        ''' </summary>
        ''' <param name="SUBID">ID of the block storage subscription to attach</param>
        ''' <param name="attach_to_SUBID">ID of the VPS subscription to mount the block storage subscription to</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function AttachBlock(ByVal SUBID As Integer, ByVal attach_to_SUBID As Integer) As BlockUpdateResult
            Dim answer As New Block

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))
            dict.Add(New KeyValuePair(Of String, Object)("attach_to_SUBID", attach_to_SUBID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/attach", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New BlockUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Detach a block storage subscription from the currently attached instance.
        ''' </summary>
        ''' <param name="SUBID">ID of the block storage subscription to detach</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DetachBlock(ByVal SUBID As Integer) As BlockUpdateResult
            Dim answer As New Block

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/detach", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New BlockUpdateResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Delete a block storage subscription. All data will be permanently lost. There is no going back from this call.
        ''' </summary>
        ''' <param name="SUBID">ID of the block storage subscription to delete</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteBlock(ByVal SUBID As Integer) As BlockUpdateResult
            Dim answer As New Block

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SUBID", SUBID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("block/delete", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New BlockUpdateResult With {.ApiResponse = httpResponse}
        End Function
    End Class


End Namespace