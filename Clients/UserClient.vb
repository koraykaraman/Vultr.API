Imports System.IO
Imports Newtonsoft.Json
Imports Vultr.API.Models.Responses

Namespace API.Clients
    Public Class UserClient
        ReadOnly _ApiKey As String

        Public Sub New(ByVal ApiKey As String)
            _ApiKey = ApiKey
        End Sub

        ''' <summary>
        ''' Retrieve a list of any users associated with this account.
        ''' </summary>
        ''' <returns>List of active Users.</returns>
        Function GetUsers() As UserResult
            Dim answer As New List(Of User)
            Dim httpResponse = Extensions.ApiClient.ApiExecute("user/list", _ApiKey)
            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer = JsonConvert.DeserializeObject(Of List(Of User))(If(st = "[]", "{}", st))
                End Using
            End If
            Return New UserResult With {.ApiResponse = httpResponse, .Users = answer}
        End Function

        ''' <summary>
        ''' Create a new user.
        ''' </summary>
        ''' <param name="User">New User class with email, name, password.</param>
        ''' <returns>Returns backup list and HTTP API Respopnse.</returns>
        Function CreateUser(ByVal User As User) As UserCreateResult

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("email", User.email))
            dict.Add(New KeyValuePair(Of String, Object)("name", User.name))
            dict.Add(New KeyValuePair(Of String, Object)("api_enabled", User.api_enabled))
            dict.Add(New KeyValuePair(Of String, Object)("password", User.password))
            For i = 0 To User.acls.Count - 1
                dict.Add(New KeyValuePair(Of String, Object)("acls[]", User.acls(i)))
            Next

            Dim answer As New UserCreateResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("user/create", _ApiKey, dict, "POST")

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                    answer.User = JsonConvert.DeserializeObject(Of User)(If(st = "[]", "{}", st))
                End Using
            End If

            Return New UserCreateResult With {.ApiResponse = httpResponse, .User = answer.User}

        End Function

        ''' <summary>
        ''' Delete a user.
        ''' </summary>
        ''' <param name="User">Updated usesr class with parameters.</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function DeleteUser(ByVal User As User) As UserUpdateResult

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("USERID", User.USERID))
            For i = 0 To User.acls.Count - 1
                dict.Add(New KeyValuePair(Of String, Object)("acls[]", User.acls(i)))
            Next
            If String.IsNullOrWhiteSpace(User.email) = False Then
                dict.Add(New KeyValuePair(Of String, Object)("email", User.email))
            End If
            If String.IsNullOrWhiteSpace(User.name) = False Then
                dict.Add(New KeyValuePair(Of String, Object)("name", User.name))
            End If
            If String.IsNullOrWhiteSpace(User.api_enabled) = False Then
                dict.Add(New KeyValuePair(Of String, Object)("api_enabled", User.api_enabled))
            End If
            If String.IsNullOrWhiteSpace(User.password) = False Then
                dict.Add(New KeyValuePair(Of String, Object)("password", User.password))
            End If

            Dim answer As New UserUpdateResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("user/update", _ApiKey, dict, "POST")

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If

            Return New UserUpdateResult With {.ApiResponse = httpResponse}

        End Function


        ''' <summary>
        ''' Update the details for a user.
        ''' </summary>
        ''' <param name="USERID">ID of the user to delete</param>
        ''' <returns>No response, check HTTP result code.</returns>
        Function UpdateUser(ByVal USERID As String) As UserDeleteResult

            Dim dict As New List(Of KeyValuePair(Of String, Object))
            dict.Add(New KeyValuePair(Of String, Object)("USERID", USERID))

            Dim answer As New UserDeleteResult
            Dim httpResponse = Extensions.ApiClient.ApiExecute("user/delete", _ApiKey, dict, "POST")

            If httpResponse.StatusCode = 200 Then
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    Dim st = streamReader.ReadToEnd()
                End Using
            End If

            Return New UserDeleteResult With {.ApiResponse = httpResponse}

        End Function
    End Class
End Namespace
