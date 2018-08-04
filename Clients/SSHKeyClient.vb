Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class SSHKeyClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all the SSH keys on the current account.
        ''' </summary>
        ''' <returns>List of all the SSH keys on the current account.</returns>
        Function GetSSHKeys() As SSHKeyResult
            Dim answer As New Dictionary(Of String, SSHKey)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("sshkey/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, SSHKey))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New SSHKeyResult With {.ApiResponse = httpResponse, .SSHKeys = answer}
        End Function

        ''' <summary>
        ''' Create a new SSH Key.
        ''' </summary>
        ''' <param name="name">Name of the SSH key</param>
        ''' <param name="ssh_key">SSH public key (in authorized_keys format)</param>
        ''' <returns>SSHKey element with only SSHKEYID.</returns>
        Function CreateSSHKey(ByVal name As String, ByVal ssh_key As String) As SSHKeyCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("name", name))
            dict.Add(New KeyValuePair(Of String, Object)("ssh_key", ssh_key))

            Dim answer As New SSHKey
            Dim httpResponse = Extensions.ApiClient.ApiExecute("sshkey/create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of SSHKey)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New SSHKeyCreateResult With {.ApiResponse = httpResponse, .SSHKey = answer}
        End Function

        ''' <summary>
        ''' Remove a SSH key. Note that this will not remove the key from any machines that already have it.
        ''' </summary>
        ''' <param name="SSHKEYID">Unique identifier for this SSH key.  These can be found using the GetSSHKeys()</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteSSHKey(ByVal SSHKEYID As String) As SSHKeyDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SSHKEYID", SSHKEYID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("sshkey/destroy", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New SSHKeyDeleteResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Update an existing SSH Key. Note that this will only update newly installed machines. The key will not be updated on any existing machines.
        ''' </summary>
        ''' <param name="SSHKey">Unique identifier for this snapshot. These can be found Using the GetSSHKeys() Call.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function UpdateSSHKey(ByVal SSHKey As SSHKey) As SSHKeyUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("name", SSHKey.name))
            dict.Add(New KeyValuePair(Of String, Object)("SSHKEYID", SSHKey.SSHKEYID))
            dict.Add(New KeyValuePair(Of String, Object)("ssh_key", SSHKey.ssh_key))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("sshkey/update", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New SSHKeyUpdateResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace