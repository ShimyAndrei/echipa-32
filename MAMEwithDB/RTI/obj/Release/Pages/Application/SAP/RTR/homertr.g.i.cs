﻿#pragma checksum "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "192624877AAAED3A7419CF2CBE8E2BC4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Converters;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace RTI.Pages.Application.SAP.RTR {
    
    
    /// <summary>
    /// homertr
    /// </summary>
    public partial class homertr : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox rtis;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ctc;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button kb1;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button kb2;
        
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
            System.Uri resourceLocater = new System.Uri("/RTI;component/pages/application/sap/rtr/homertr.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
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
            this.rtis = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ctc = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
            this.ctc.Click += new System.Windows.RoutedEventHandler(this.ctc_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.kb1 = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
            this.kb1.Click += new System.Windows.RoutedEventHandler(this.kb1_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.kb2 = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\..\..\Pages\Application\SAP\RTR\homertr.xaml"
            this.kb2.Click += new System.Windows.RoutedEventHandler(this.kb2_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

