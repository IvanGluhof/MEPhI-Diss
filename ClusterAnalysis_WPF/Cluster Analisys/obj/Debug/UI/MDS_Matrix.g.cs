#pragma checksum "..\..\..\UI\MDS_Matrix.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E7DC7CF54A67A8529BD4419351C700A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cluster_Analisys;
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


namespace Cluster_Analisys {
    
    
    /// <summary>
    /// MDS_Matrix
    /// </summary>
    public partial class MDS_Matrix : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\UI\MDS_Matrix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer Matrix2D_Scroll;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\UI\MDS_Matrix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel Matrix2D_MatrixPanel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\UI\MDS_Matrix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel Matrix_WorkPanel;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\UI\MDS_Matrix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGrid_Excel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\UI\MDS_Matrix.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_clear;
        
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
            System.Uri resourceLocater = new System.Uri("/Cluster Analisys;component/ui/mds_matrix.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\MDS_Matrix.xaml"
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
            this.Matrix2D_Scroll = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.Matrix2D_MatrixPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 3:
            this.Matrix_WorkPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 4:
            this.DataGrid_Excel = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.button_clear = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\UI\MDS_Matrix.xaml"
            this.button_clear.Click += new System.Windows.RoutedEventHandler(this.button_clear_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

