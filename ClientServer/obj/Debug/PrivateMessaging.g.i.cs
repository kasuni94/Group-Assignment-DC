﻿#pragma checksum "..\..\PrivateMessaging.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2F6C74F1995EFF4D2A8A5C476637BC2785E862CB3AB0CE4F5DA8EC0643CBF823"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GamingLobbyClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GamingLobbyClient {
    
    
    /// <summary>
    /// PrivateMessaging
    /// </summary>
    public partial class PrivateMessaging : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 109 "..\..\PrivateMessaging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBackToLobby;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\PrivateMessaging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblInbox;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\PrivateMessaging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbUserList;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\PrivateMessaging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstMessages;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\PrivateMessaging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMessageInput;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\PrivateMessaging.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSend;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientServer;component/privatemessaging.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PrivateMessaging.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnBackToLobby = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\PrivateMessaging.xaml"
            this.btnBackToLobby.Click += new System.Windows.RoutedEventHandler(this.BtnBackToLobby_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblInbox = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.cmbUserList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.lstMessages = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.txtMessageInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnSend = ((System.Windows.Controls.Button)(target));
            
            #line 129 "..\..\PrivateMessaging.xaml"
            this.btnSend.Click += new System.Windows.RoutedEventHandler(this.BtnSend_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

