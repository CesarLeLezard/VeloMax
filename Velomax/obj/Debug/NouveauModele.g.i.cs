﻿#pragma checksum "..\..\NouveauModele.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2484EDFE8AA127CAE5446A68DC102462B1759BB7392C9779029CDB52DACE3A7E"
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
    /// NouveauModele
    /// </summary>
    public partial class NouveauModele : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbIdModele;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbGrandeurs;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbAuto;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbLignesProduits;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDateIntro;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDateDisc;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbQuantite;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNomModele;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPrix;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\NouveauModele.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bValider;
        
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
            System.Uri resourceLocater = new System.Uri("/Velomax;component/nouveaumodele.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NouveauModele.xaml"
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
            this.tbIdModele = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cbGrandeurs = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cbAuto = ((System.Windows.Controls.CheckBox)(target));
            
            #line 14 "..\..\NouveauModele.xaml"
            this.cbAuto.Checked += new System.Windows.RoutedEventHandler(this.cbAuto_Checked);
            
            #line default
            #line hidden
            
            #line 14 "..\..\NouveauModele.xaml"
            this.cbAuto.Unchecked += new System.Windows.RoutedEventHandler(this.cbAuto_Unchecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbLignesProduits = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.dpDateIntro = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.dpDateDisc = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.tbQuantite = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbNomModele = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.tbPrix = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.bValider = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\NouveauModele.xaml"
            this.bValider.Click += new System.Windows.RoutedEventHandler(this.bValider_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

