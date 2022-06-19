using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Media3D;

namespace WPF_3D_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Media.Media3D.Point3D point0 = new Point3D(-0.5, 0, 0);
            System.Windows.Media.Media3D.Point3D point1 = new Point3D(0.5, 0.5, 0.3);
            System.Windows.Media.Media3D.Point3D point2 = new Point3D(0, 0.5, 0);

            System.Windows.Media.Media3D.MeshGeometry3D triangleMesh = new MeshGeometry3D();

            triangleMesh.Positions.Add(point0);
            triangleMesh.Positions.Add(point1);
            triangleMesh.Positions.Add(point2);

            int n0 = 0;
            int n1 = 1;
            int n2 = 2;

            triangleMesh.TriangleIndices.Add(n0);
            triangleMesh.TriangleIndices.Add(n1);
            triangleMesh.TriangleIndices.Add(n2);

            System.Windows.Media.Media3D.Vector3D norm = new Vector3D(0, 0, 1);
            triangleMesh.Normals.Add(norm);
            triangleMesh.Normals.Add(norm);
            triangleMesh.Normals.Add(norm);

            System.Windows.Media.Media3D.Material frontMaterial =
            new DiffuseMaterial(new SolidColorBrush(Colors.Blue));

            System.Windows.Media.Media3D.GeometryModel3D triangleModel =
            new GeometryModel3D(triangleMesh, frontMaterial);

            triangleModel.Transform = new Transform3DGroup();

            System.Windows.Media.Media3D.ModelVisual3D visualModel = new ModelVisual3D();
            visualModel.Content = triangleModel;

            this.mainViewport.Children.Add(visualModel);
        }
    }
}
