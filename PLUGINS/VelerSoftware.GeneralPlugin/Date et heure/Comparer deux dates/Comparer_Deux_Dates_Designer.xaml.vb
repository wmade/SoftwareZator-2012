﻿Public Class Comparer_Deux_Dates_Designer

    Private Sub ActionChanged(ByVal sender As System.Object, ByVal propertyName As String, ByVal e As System.EventArgs)
        If propertyName = "Param8" Then Me.ComboBox1.Text = DirectCast(Me.ModelItem.GetCurrentValue, Comparer_Deux_Dates).Param8
    End Sub

    Private Sub ActivityDesigner_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        ' Initialisation de la langue et des ressources
        System.Threading.Thread.CurrentThread.CurrentUICulture = VelerSoftware.Plugins3.CurrentCulture
        RM = New System.Resources.ResourceManager("VelerSoftware.GeneralPlugin.Comparer_Deux_Dates", GetType(Comparer_Deux_Dates).Assembly)

        Me.Label2.Content = RM.GetString("Designer_Text")
        Me.TextBlock1.Text = RM.GetString("Designer_Collaspe")

        Me.ComboBox1.Items.Clear()
        For Each aaa As VelerSoftware.SZVB.Projet.Variable In DirectCast(Me.ModelItem.GetCurrentValue, Comparer_Deux_Dates).Tools.GetCurrentProjectVariableList
            If Not aaa.Array Then
                Me.ComboBox1.Items.Add(aaa.Name)
            End If
        Next

        Me.ComboBox1.SelectedItem = DirectCast(Me.ModelItem.GetCurrentValue, Comparer_Deux_Dates).Param8

        AddHandler DirectCast(Me.ModelItem.GetCurrentValue, Comparer_Deux_Dates).ActionChanged, AddressOf ActionChanged
    End Sub

    Private Sub ComboBox1_DropDownOpened(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.DropDownOpened
        Dim txt As String = Me.ComboBox1.Text

        Me.ComboBox1.Items.Clear()
        For Each aaa As VelerSoftware.SZVB.Projet.Variable In DirectCast(Me.ModelItem.GetCurrentValue, Comparer_Deux_Dates).Tools.GetCurrentProjectVariableList
            If Not aaa.Array Then
                Me.ComboBox1.Items.Add(aaa.Name)
            End If
        Next

        Me.ComboBox1.SelectedItem = txt
    End Sub
End Class