using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDotNet;
using RDotNet.Graphics;
using System.Reflection;
using System.IO;
using System.Security.Policy;

namespace WinForms_RNet_testing
{
    public partial class Form_RNET : Form
    {
        static string dll_path = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\R.dll").Replace("\\", "//");
        REngine engine = REngine.GetInstance(dll_path);

        public Form_RNET()
        {
            InitializeComponent();

            // REngine requires explicit initialization.
            // You can set some parameters.
            engine.Initialize();
            textBox1.Text = "2";
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);           
        }

        /// <summary>
        /// Событие при выходе из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationExit(object sender, EventArgs e)
        {
            // you should always dispose of the REngine properly.
            // After disposing of the engine, you cannot reinitialize nor reuse it
            engine.Dispose();
        }

        /// <summary>
        /// Пример №1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test1_Click(object sender, EventArgs e)
        {
            listBox_RNET.Items.Clear();

            REngine.SetEnvironmentVariables(); // <-- May be omitted (опущена); the next line would call it.
            
            // A somewhat contrived but customary Hello World:
            CharacterVector charVec = engine.CreateCharacterVector(new[] { "Hello, R world!, .NET speaking" });
            
            engine.SetSymbol("greetings", charVec);
            engine.Evaluate("str(greetings)"); // print out in the console

            string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();

            listBox_RNET.Items.Add("Пример №1 - аля Hello World");
            listBox_RNET.Items.Add("R answered: '{0}' " + a[0]).ToString();
        }

        /// <summary>
        /// Пример №2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test2_Click(object sender, EventArgs e)
        {
            listBox_RNET.Items.Clear();

            var e1 = engine.Evaluate("x <- 3");
            // You can now access x defined in the R environment
            NumericVector x = engine.GetSymbol("x").AsNumeric();
            engine.Evaluate("y <- 1:10");
            NumericVector y = engine.GetSymbol("y").AsNumeric();

            listBox_RNET.Items.Add("Пример №2 - типы в R.NET");
            listBox_RNET.Items.Add(e1.ToString());
            listBox_RNET.Items.Add(x);
            listBox_RNET.Items.Add(y);
        }

        /// <summary>
        /// Пример №3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test3_Click(object sender, EventArgs e)
        {
            listBox_RNET.Items.Clear();

            // .NET Framework array to R vector.
            NumericVector group1 = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });
            engine.SetSymbol("group1", group1);
            // Direct parsing from R script.
            NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();

            // Test difference of mean and get the P-value.
            GenericVector testResult = engine.Evaluate("t.test(group1, group2)").AsList();
            double p = testResult["p.value"].AsNumeric().First();

            listBox_RNET.Items.Add("Пример №3");
            listBox_RNET.Items.Add("Group1: [{0}] " + string.Join(", ", group1));
            listBox_RNET.Items.Add("Group2: [{0}] " + string.Join(", ", group2));
            listBox_RNET.Items.Add("P-value = {0:0.000}" + p);
        }

        /// <summary>
        /// Пример №4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test4_Click(object sender, EventArgs e)
        {
            listBox_RNET.Items.Clear();

            engine.Evaluate("cases <- expand.grid(x=c('a','b','c'), y=1:3)");
            var df1 = engine.Evaluate("expand.grid(x=c('A','B','C'), y=1:3)").AsDataFrame();

            // Invoking functions; Previously you may have needed custom function definitions
            var myFunc = engine.Evaluate("function(x, y) { expand.grid(x=x, y=y) }").AsFunction();
            var v1 = engine.CreateIntegerVector(new[] { 1, 2, 3 });
            var v2 = engine.CreateCharacterVector(new[] { "a", "b", "c" });
            var df = myFunc.Invoke(new SymbolicExpression[] { v1, v2 }).AsDataFrame();

            // As of R.NET 1.5.10, more function call syntaxes are supported.
            var expandGrid = engine.Evaluate("expand.grid").AsFunction();
            var d = new Dictionary<string, SymbolicExpression>();
            d["x"] = v1;
            d["y"] = v2;
            df = expandGrid.Invoke(d).AsDataFrame();
        }

        /// <summary>
        /// Пример №5 - MDS Testing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test5_Click(object sender, EventArgs e)
        {
            var k = textBox1.Text;
            int numVal = Int32.Parse(k);

            string path = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xlsx").Replace("\\", "//");

            string script = @"
            library(xlsx) #подключение библиотеки
            setwd('E://Data') #установка рабочей директории
            
            mydata <- read.xlsx('123.xlsx', 1) #чтение xlsx файла
            d <- dist(mydata) # euclidean distances between the rows
            fit <- cmdscale(d,eig=TRUE, k=" + numVal + @") # k is the number of dim
            write.xlsx(fit$points, '" + path + @"') # создание Excel файла";

            engine.Evaluate(script);
        }

        /// <summary>
        /// Пример №6 - 3D Plot testing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_test6_Click(object sender, EventArgs e)
        {
            string script = @"setwd('E:/Data') #установка рабочей директории
            library(xlsx) #подключение библиотеки
            mydata <- read.xlsx('123.xlsx', 1) #чтение xlsx файла
            library(rgl)
            plot3d(mydata, col='red', size=3) 
            ";

            engine.Evaluate(script);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
