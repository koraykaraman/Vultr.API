Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class StartupScriptClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' List all startup scripts on the current account. Scripts of type "boot" are executed by the server's operating system on the first boot. Scripts of type "pxe" are executed by iPXE when the server itself starts up.
        ''' </summary>
        ''' <returns>List of all startup scripts on the current account.</returns>
        Function GetStartupScripts() As StartupScriptResult
            Dim answer As New Dictionary(Of String, StartupScript)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("startupscript/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of Dictionary(Of String, StartupScript))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New StartupScriptResult With {.ApiResponse = httpResponse, .StartupScripts = answer}
        End Function

        ''' <summary>
        ''' Create a startup script..
        ''' </summary>
        ''' <param name="name">Name of the newly created startup script.</param>
        ''' <param name="script">Startup script contents.</param>
        ''' <param name="ScriptType">Type of startup script. Default is 'boot'.</param>
        ''' <returns>StartupScript element with only SCRIPTID.</returns>
        Function CreateStartupScript(ByVal name As String, ByVal script As String, ByVal ScriptType As ScriptType) As StartupScriptCreateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("name", name))
            dict.Add(New KeyValuePair(Of String, Object)("script", script))
            dict.Add(New KeyValuePair(Of String, Object)("type", ScriptType.ToString))

            Dim answer As New StartupScript
            Dim httpResponse = Extensions.ApiClient.ApiExecute("startupscript/create", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of StartupScript)(If(st = "[]", "{}", st))
                End Using
            End If
            Return New StartupScriptCreateResult With {.ApiResponse = httpResponse, .StartupScript = answer}
        End Function

        ''' <summary>
        ''' Remove a SSH key. Note that this will not remove the key from any machines that already have it.
        ''' </summary>
        ''' <param name="SCRIPTID">Unique identifier for this startup script. These can be found using the GetStartupScripts()</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteStartupScript(ByVal SCRIPTID As String) As StartupScriptDeleteResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SCRIPTID", SCRIPTID))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("startupscript/destroy", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New StartupScriptDeleteResult With {.ApiResponse = httpResponse}
        End Function

        ''' <summary>
        ''' Update an existing startup script.
        ''' </summary>
        ''' <param name="SCRIPTID">SCRIPTID of script to update. These can be found using the GetStartupScripts()</param>
        ''' <param name="name">Name of the newly created startup script.</param>
        ''' <param name="script">Startup script contents.</param>
        ''' <param name="ScriptType">Type of startup script. Default is 'boot'.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function UpdateSSHKey(ByVal SCRIPTID As String, ByVal name As String, ByVal script As String, ByVal ScriptType As ScriptType) As StartupScriptUpdateResult
            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("SCRIPTID", SCRIPTID))
            dict.Add(New KeyValuePair(Of String, Object)("name", name))
            dict.Add(New KeyValuePair(Of String, Object)("script", script))
            dict.Add(New KeyValuePair(Of String, Object)("type", ScriptType.ToString))

            Dim httpResponse = Extensions.ApiClient.ApiExecute("startupscript/update", _ApiKey, dict, "POST")
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If
            Return New StartupScriptUpdateResult With {.ApiResponse = httpResponse}
        End Function
    End Class
End Namespace