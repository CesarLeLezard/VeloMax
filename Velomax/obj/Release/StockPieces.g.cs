﻿#pragma checksum "..\..\StockPieces.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1A19AE847F142BA491816F9925EE9DFCFBE645EF090393C9D91A73545386D6E5"
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
    /// StockPieces
    /// </summary>
    public partial class StockPieces : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\StockPieces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lTitre;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\StockPieces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bNvPiece;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\StockPieces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgPieces;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\StockPieces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image lBoite;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\StockPieces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bDetailsPiece;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\StockPieces.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSupprimer;
        
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
            System.Uri resourceLocater = new System.Uri("/Velomax;component/stockpieces.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StockPieces.xaml"
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
            this.lTitre = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.bNvPiece = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\StockPieces.xaml"
            this.bNvPiece.Click += new System.Windows.RoutedEventHandler(this.bNvPiece_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgPieces = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.lBoite = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.bDetailsPiece = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.bSupprimer = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

