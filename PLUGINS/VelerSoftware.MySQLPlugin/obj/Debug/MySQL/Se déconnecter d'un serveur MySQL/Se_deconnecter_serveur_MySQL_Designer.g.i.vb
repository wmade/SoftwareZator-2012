﻿#ExternalChecksum("..\..\..\..\MySQL\Se déconnecter d'un serveur MySQL\Se_deconnecter_serveur_MySQL_Designer.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","F804908A48DC27454239EB21FBF0E7EF")
'------------------------------------------------------------------------------
' <auto-generated>
'     Ce code a été généré par un outil.
'     Version du runtime :4.0.30319.17929
'
'     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est régénéré.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Activities.Presentation
Imports System.Activities.Presentation.View
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell


'''<summary>
'''Se_deconnecter_serveur_MySQL_Designer
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class Se_deconnecter_serveur_MySQL_Designer
    Inherits System.Activities.Presentation.ActivityDesigner
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\..\MySQL\Se déconnecter d'un serveur MySQL\Se_deconnecter_serveur_MySQL_Designer.xaml",52)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents TextBlock1 As System.Windows.Controls.TextBlock
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\MySQL\Se déconnecter d'un serveur MySQL\Se_deconnecter_serveur_MySQL_Designer.xaml",60)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Label2 As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\MySQL\Se déconnecter d'un serveur MySQL\Se_deconnecter_serveur_MySQL_Designer.xaml",61)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ComboBox1 As System.Windows.Controls.ComboBox
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/VelerSoftware.MySQLPlugin;component/mysql/se%20d%c3%a9connecter%20d'un%20serveur"& _ 
                "%20mysql/se_deconnecter_serveur_mysql_designer.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\MySQL\Se déconnecter d'un serveur MySQL\Se_deconnecter_serveur_MySQL_Designer.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.TextBlock1 = CType(target,System.Windows.Controls.TextBlock)
            Return
        End If
        If (connectionId = 2) Then
            Me.Label2 = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 3) Then
            Me.ComboBox1 = CType(target,System.Windows.Controls.ComboBox)
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

