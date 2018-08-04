Imports System.Net

Namespace API.Models.Responses
    Public Class StartupScript
        Public Property SCRIPTID As String
        Public Property date_created As String
        Public Property date_modified As String
        Public Property name As String
        Public Property type As String
        Public Property script As String
    End Class

    Public Structure StartupScriptCreateResult
        Public Property StartupScript As StartupScript
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure StartupScriptDeleteResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure StartupScriptUpdateResult
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Structure StartupScriptResult
        Public Property StartupScripts As Dictionary(Of String, StartupScript)
        Public Property ApiResponse As HttpWebResponse
    End Structure

    Public Class ScriptType
        Private Key As String

        Public Shared ReadOnly BOOT As ScriptType = New ScriptType("boot")
        Public Shared ReadOnly PXE As ScriptType = New ScriptType("pxe")


        Private Sub New(key As String)
            Me.Key = key
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Key
        End Function
    End Class
End Namespace