﻿#pragma checksum "..\..\StockVelos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4C9FA114A81AC73F157D47F6F577E022B33E14375312B905286A35DD9FF62D7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Velomax;


namespace Velomax {
    
    
    /// <summary>
    /// StockVelos
    /// </summary>
    public partial class StockVelos : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgVelos;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lTitre;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image lBoite;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bNvVelo;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bDetailsModele;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSupprimer;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\StockVelos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bListePieces;
        
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
            System.Uri resourceLocater = new System.Uri("/Velomax;component/stockvelos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StockVelos.xaml"
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
            this.dgVelos = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.lTitre = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lBoite = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.bNvVelo = ((System.Windows.Controls.Button)(target));
            
            #line 126 "..\..\StockVelos.xaml"
            this.bNvVelo.Click += new System.Windows.RoutedEventHandler(this.bNvVelo_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bDetailsModele = ((System.Windows.Controls.Button)(target));
            
            #line 131 "..\..\StockVelos.xaml"
            this.bDetailsModele.Click += new System.Windows.RoutedEventHandler(this.bDetailsModele_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bSupprimer = ((System.Windows.Controls.Button)(target));
            
            #line 136 "..\..\StockVelos.xaml"
            this.bSupprimer.Click += new System.Windows.RoutedEventHandler(this.bSupprimer_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bListePieces = ((System.Windows.Controls.Button)(target));
            
            #line 141 "..\..\StockVelos.xaml"
            this.bListePieces.Click += new System.Windows.RoutedEventHandler(this.bListePieces_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

