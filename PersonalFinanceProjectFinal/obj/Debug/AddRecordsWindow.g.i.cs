﻿#pragma checksum "..\..\AddRecordsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D2B1DEB5E2E20F246266190024D1E8C6B1ABB071"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PersonalFinanceProjectFinal;
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


namespace PersonalFinanceProjectFinal {
    
    
    /// <summary>
    /// AddRecordsWindow
    /// </summary>
    public partial class AddRecordsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\AddRecordsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddExpense;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\AddRecordsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddIncome;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\AddRecordsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReturnDashboard;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\AddRecordsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame frmDashboard;
        
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
            System.Uri resourceLocater = new System.Uri("/PersonalFinanceProjectFinal;component/addrecordswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddRecordsWindow.xaml"
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
            
            #line 10 "..\..\AddRecordsWindow.xaml"
            ((PersonalFinanceProjectFinal.AddRecordsWindow)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAddExpense = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\AddRecordsWindow.xaml"
            this.btnAddExpense.Click += new System.Windows.RoutedEventHandler(this.btnAddExpense_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnAddIncome = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\AddRecordsWindow.xaml"
            this.btnAddIncome.Click += new System.Windows.RoutedEventHandler(this.btnAddIncome_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnReturnDashboard = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\AddRecordsWindow.xaml"
            this.btnReturnDashboard.Click += new System.Windows.RoutedEventHandler(this.btnReturnDashboard_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.frmDashboard = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

