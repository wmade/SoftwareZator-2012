﻿<System.ComponentModel.Designer(GetType(VelerSoftware.AccessPlugin.Ajouter_Ligne_Table_Access_Designer))> _
Public Class Ajouter_Ligne_Table_Access
    Inherits VelerSoftware.Plugins3.Action

    Public Sub New()
        ' Initialisation de la langue et des ressources
        System.Threading.Thread.CurrentThread.CurrentUICulture = VelerSoftware.Plugins3.CurrentCulture
        RM = New System.Resources.ResourceManager("VelerSoftware.AccessPlugin.Ajouter_Ligne_Table_Access", GetType(Ajouter_Ligne_Table_Access).Assembly)

        With Me
            .DisplayName = RM.GetString("DisplayName")
            .Description = RM.GetString("Description")
            .Category = RM.GetString("Category")
            .ToolBoxImage = My.Resources.add_table_row
            .CompatibleClass = False
            .CompatibleFonctions = True
            .FileHelp = RM.GetString("Help_File")

            .Voice_Recognition_Dictionary.Add(RM.GetString("Dictionary1"))
            .Voice_Recognition_Dictionary.Add(RM.GetString("Dictionary2"))

            .ClassCode = "VelerSoftware.AccessPlugin.vb"
            .References.Add("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
            .References.Add("Interop.ADOX.dll")
        End With
    End Sub

    Public Overrides Function Main() As Boolean
        ' Initialisation de la langue et des ressources
        System.Threading.Thread.CurrentThread.CurrentUICulture = VelerSoftware.Plugins3.CurrentCulture
        RM = New System.Resources.ResourceManager("VelerSoftware.AccessPlugin.Ajouter_Ligne_Table_Access", GetType(Ajouter_Ligne_Table_Access).Assembly)
        Using frm As New Ajouter_Ligne_Table_Access_Form
            frm.Tools = Me.Tools
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.UseCustomVBCode = frm.CodeEditor_Used
                Me.CustomVBCode = New CodeDom.CodeSnippetStatement(frm.CodeEditor_Text)
                Me.Param1 = frm.Param1
                Me.Param2 = frm.Param2
                Me.Param3 = frm.Param3
                Me.Param4 = frm.Param4

                Return True
            Else
                Return False
            End If
            frm.Dispose()
        End Using
    End Function

    Public Overrides Function Modify() As Boolean
        ' Initialisation de la langue et des ressources
        System.Threading.Thread.CurrentThread.CurrentUICulture = VelerSoftware.Plugins3.CurrentCulture
        RM = New System.Resources.ResourceManager("VelerSoftware.AccessPlugin.Ajouter_Ligne_Table_Access", GetType(Ajouter_Ligne_Table_Access).Assembly)
        Using frm As New Ajouter_Ligne_Table_Access_Form
            frm.Tools = Me.Tools
            frm.Param1 = Me.Param1
            frm.Param2 = Me.Param2
            frm.Param3 = Me.Param3
            frm.Param4 = Me.Param4
            frm.CodeEditor_Shown = Me.UseCustomVBCode
            frm.CodeEditor_Used = Me.UseCustomVBCode

            Dim sourceWriter As New IO.StringWriter()
            If Not Me.CustomVBCode Is Nothing Then CodeDom.Compiler.CodeDomProvider.CreateProvider("VB").GenerateCodeFromStatement(Me.CustomVBCode, sourceWriter, New CodeDom.Compiler.CodeGeneratorOptions())
            sourceWriter.Close()
            frm.CodeEditor_Text = sourceWriter.ToString

            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.UseCustomVBCode = frm.CodeEditor_Used
                Me.CustomVBCode = New CodeDom.CodeSnippetStatement(frm.CodeEditor_Text)
                Me.Param1 = frm.Param1
                Me.Param2 = frm.Param2
                Me.Param3 = frm.Param3
                Me.Param4 = frm.Param4
                Return True
            Else
                Return False
            End If
            frm.Dispose()
        End Using
    End Function

    Public Overrides Function GetVBCode(ByVal FromFunction As Boolean) As System.CodeDom.CodeObject
        If Me.UseCustomVBCode Then
            Return Me.CustomVBCode
        Else
            If FromFunction Then
                Dim columns_param As String
                columns_param = Nothing
                If Not Me.Param4 = Nothing Then
                    For Each strr As String In Me.Param4.Split(";")
                        columns_param += Tools.TransformKeyVariables(strr, True, True) & ", "
                    Next
                End If
                If Not columns_param = Nothing Then columns_param = columns_param.TrimEnd(" ").TrimEnd(",")

                If Not Me.Param2 = Nothing Then
                    Return New CodeDom.CodeAssignStatement(New CodeDom.CodeVariableReferenceExpression(Me.Param2), New CodeDom.CodeSnippetExpression("VelerSoftware_AccessPlugin.AddRowTable_Access(" & Tools.TransformKeyVariables(Me.Param1, True, False) & ", " & Tools.TransformKeyVariables(Me.Param3, False, False) & ", New System.Collections.Generic.List(Of Object)({" & columns_param & "}))"))
                Else
                    Return New CodeDom.CodeSnippetStatement("VelerSoftware_AccessPlugin.AddRowTable_Access(" & Tools.TransformKeyVariables(Me.Param1, True, False) & ", " & Tools.TransformKeyVariables(Me.Param3, False, False) & ", New System.Collections.Generic.List(Of Object)({" & columns_param & "}))")
                End If
            Else
                Return Nothing
            End If
        End If
    End Function

    Public Overrides Function ResolveError(ByVal ErrorToResole As SZVB.Projet.Erreur, ByVal e As System.EventArgs) As Boolean
        Dim result As Boolean
        Dim i_progress, i2_progress As Integer
        i_progress = 0
        i2_progress = 0

        System.Threading.Thread.CurrentThread.CurrentUICulture = VelerSoftware.Plugins3.CurrentCulture
        RM = New System.Resources.ResourceManager("VelerSoftware.AccessPlugin.Ajouter_Ligne_Table_Access", GetType(Ajouter_Ligne_Table_Access).Assembly)

        If ErrorToResole.Type = SZVB.Projet.Erreur.ErrorType.Building Then

            If ErrorToResole.ErrorNumber = "BC30311" Then ' La variable recevant le résultat est devenu une variable tableau 
                ErrorToResole.ErrorExplain = String.Format(System.Globalization.CultureInfo.InvariantCulture, RM.GetString("Error_Explain_BC30311"), Me.Param1)
                ErrorToResole.ErrorSolutionExplain = String.Format(System.Globalization.CultureInfo.InvariantCulture, RM.GetString("Error_Solution_Explain_BC30311"))

                Return False

            ElseIf ErrorToResole.ErrorNumber = "BC30451" Then ' La variable n'existe plus 
                Dim oki As Boolean
                For Each var As VelerSoftware.SZVB.Projet.Variable In Tools.GetCurrentProjectVariableList
                    If var.Name = Me.Param1 Then oki = True
                Next
                If Not oki Then Tools.GetCurrentProjectVariableList.Add(New VelerSoftware.SZVB.Projet.Variable(Me.Param1, False, Nothing, Nothing))
                ErrorToResole.ErrorExplain = String.Format(System.Globalization.CultureInfo.InvariantCulture, RM.GetString("Error_Explain_BC30451"), Me.Param1)
                ErrorToResole.ErrorSolutionExplain = String.Format(System.Globalization.CultureInfo.InvariantCulture, RM.GetString("Error_Solution_Explain_BC30451"), Me.Param1)

                Return True

            Else
                Return False

            End If

        End If

        Return result
    End Function

End Class
