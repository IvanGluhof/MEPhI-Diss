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
using System.Windows.Media.Animation;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const bool c_simpleLighting = true;
        const int c_numPoints = 100 * 1000;

        public MainWindow()
        {
            InitializeComponent();
            Viewport3D vp = new Viewport3D();
            vp = Main_11();
            this.Content = vp;
            
        }

        [STAThread]
        public static Viewport3D Main_11()
        {
            Viewport3D vp = new Viewport3D();

            //MainWindow.Background = Brushes.Black;

            //
            // Since we're not supporting HitTesting or scene clipping,
            // turn these features off.
            //

            vp.IsHitTestVisible = false;
            vp.ClipToBounds = false;

            //
            // Create our scene.
            //

            ModelVisual3D scene = new ModelVisual3D();

            //
            // Add lights.
            //

            ModelVisual3D ambientLightVisual = new ModelVisual3D();
            ambientLightVisual.Content = new AmbientLight(
                Color.FromScRgb(1.0f /* a */, .3f /* r */, .3f /* g */, .3f /* b */));

            ModelVisual3D directionalLightVisual = new ModelVisual3D();
            directionalLightVisual.Content = new DirectionalLight(
                Color.FromScRgb(1.0f /* a */, 1.0f /* r */, 1.0f /* g */, 1.0f /* b */),
                new Vector3D(-1.0, -1.0, -1.0));

            scene.Children.Add(ambientLightVisual);

            if (!c_simpleLighting)
            {
                scene.Children.Add(directionalLightVisual);
            }

            //
            // Add scatter plot
            //

            ModelVisual3D plotVisual = MakePlot(c_numPoints, 10 /* size */);

            AxisAngleRotation3D rotation = new AxisAngleRotation3D(
                new Vector3D(0, 0, 1) /* z-direction */, 0 /* degrees */);
            plotVisual.Transform = new RotateTransform3D(rotation);

            scene.Children.Add(plotVisual);

            //
            // Point camera
            //

            vp.Camera = new PerspectiveCamera(
                new Point3D(15, 0, 10), // Position
                new Vector3D(-1.5, 0, -1), // LookDirection
                new Vector3D(0, 0, 1), // UpDirection
                90 // Field of View
                );

            //
            // Add Animation
            //

            DoubleAnimation myAngleAnimation = new DoubleAnimation(
                0,
                360,
                new Duration(new TimeSpan(0, 0, 0, 20, 0)),
                FillBehavior.Stop
                );

            myAngleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // DesiredFrameRate can help reduce CPU cost and improve smoothness.
            // It can also cause beating if used improperly. Use with caution!
            Timeline.SetDesiredFrameRate(myAngleAnimation, 20 /* fps */);

            rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, myAngleAnimation);

            //
            // Put it all together!
            //

            vp.Children.Add(scene);

            return vp;
        }

        static private ModelVisual3D MakePlot(int numPoints, double size)
        {
            ModelVisual3D model = new ModelVisual3D();

            Random rand = new Random((int)System.DateTime.Now.Ticks);

            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int n = 0; n < numPoints; n++)
            {
                double x = (rand.NextDouble() - 0.5) * size;
                double y = (rand.NextDouble() - 0.5) * size;
                double z = (rand.NextDouble() - 0.5) * size;

                Point3D location = new Point3D(x, y, z);

                AddBox(
                    mesh,
                    location,             // Location
                    new Size3D(.1, .1, .1), // Size
                    rand.NextDouble()     // Color (0 = Red, 1 = Blue, .5 = Purple)
                    );
            }


            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
            gradient.GradientStops.Add(new GradientStop(Colors.Blue, 1.0));

            Material material;

            if (c_simpleLighting)
            {
                material = new DiffuseMaterial(gradient);
            }
            else
            {
                MaterialGroup group = new MaterialGroup();
                group.Children.Add(new DiffuseMaterial(gradient));
                group.Children.Add(
                    new SpecularMaterial(Brushes.White, 5.0 /* exponent */ ));

                material = group;
            }

            GeometryModel3D geometry = new GeometryModel3D(mesh, material);

            // This data won't change, so we can freeze it for a small perf win.
            geometry.Freeze();

            model.Content = geometry;

            return model;
        }

        static private void AddBox(MeshGeometry3D mesh, Point3D p, Size3D size, double t)
        {
            double[,] corners = new double[3, 2]; // axis  {x,y,z},  {-,+}

            corners[0, 0] = p.X - (size.X / 2.0);
            corners[0, 1] = p.X + (size.X / 2.0);

            corners[1, 0] = p.Y - (size.Y / 2.0);
            corners[1, 1] = p.Y + (size.Y / 2.0);

            corners[2, 0] = p.Z - (size.Z / 2.0);
            corners[2, 1] = p.Z + (size.Z / 2.0);

            Point3D[] points = new Point3D[8];

            for (int z = 0; z < 2; z++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        points[(z * 4) + (y * 2) + x] =
                            new Point3D(corners[0, x], corners[1, y], corners[2, z]);
                    }
                }
            }

            AddPlane(mesh, points[6], points[4], points[5], points[7], t);
            AddPlane(mesh, points[7], points[5], points[1], points[3], t);

            AddPlane(mesh, points[3], points[1], points[0], points[2], t);
            AddPlane(mesh, points[2], points[0], points[4], points[6], t);

            AddPlane(mesh, points[2], points[6], points[7], points[3], t);
            AddPlane(mesh, points[4], points[0], points[1], points[5], t);

        }

        static private void AddPlane(MeshGeometry3D mesh,
            Point3D a, Point3D b, Point3D c, Point3D d, double t)
        {
            mesh.Positions.Add(a);
            mesh.TextureCoordinates.Add(new Point(t, 0));

            mesh.Positions.Add(b);
            mesh.TextureCoordinates.Add(new Point(t, 0));

            mesh.Positions.Add(c);
            mesh.TextureCoordinates.Add(new Point(t, 0));

            mesh.Positions.Add(d);
            mesh.TextureCoordinates.Add(new Point(t, 0));

            mesh.TriangleIndices.Add(mesh.Positions.Count - 4); // a
            mesh.TriangleIndices.Add(mesh.Positions.Count - 3); // b
            mesh.TriangleIndices.Add(mesh.Positions.Count - 2); // c

            mesh.TriangleIndices.Add(mesh.Positions.Count - 2); // c
            mesh.TriangleIndices.Add(mesh.Positions.Count - 1); // d
            mesh.TriangleIndices.Add(mesh.Positions.Count - 4); // a
        }
    }
}
