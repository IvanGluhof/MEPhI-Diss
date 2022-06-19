using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// для ZedGraph
using ZedGraph;
//для Excel
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;

namespace ZedGraph_Testing
{
    public partial class main_form : Form
    {
        DataTable _datatable = new DataTable();
        bool loaded = false;
        string[] column_array1;
        string[] column_array2;

        public main_form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox_graphics.Items.Add("Sally");
            listBox_graphics.Items.Add("Craig");
        }

        //------------------------------Меню-----------------------------------
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_excel = new OpenFileDialog();

            open_excel.DefaultExt = "*.xls;*.xlsx"; //Расширение файла по умолчанию
            open_excel.Filter = "Excel Sheet(*.xlsx)|*.xlsx"; //Фильтр имен файлов
            open_excel.Title = "Выберите документ для загрузки данных"; //Заголовок диалогового окна.

            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet NewSheet;
            ExcelObj.Range SheetRange;
         
            if (open_excel.ShowDialog() == DialogResult.OK)
            {
                label_file_name.Text = open_excel.FileName;

                workbook = app.Workbooks.Open(open_excel.FileName, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

                NewSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
                SheetRange = NewSheet.UsedRange;

                for (int ClmNum = 1; ClmNum <= SheetRange.Columns.Count; ClmNum++)
                {
                    _datatable.Columns.Add(new DataColumn((SheetRange.Cells[1, ClmNum] as ExcelObj.Range).Value2.ToString()));
                }

                for (int Rnum = 2; Rnum <= SheetRange.Rows.Count; Rnum++)
                {
                    DataRow _datarow = _datatable.NewRow();
                    for (int Cnum = 1; Cnum <= SheetRange.Columns.Count; Cnum++)
                    {
                        if ((SheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                        {
                            _datarow[Cnum - 1] =
                            (SheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    _datatable.Rows.Add(_datarow);
                    _datatable.AcceptChanges();
                }

                dataGridView.DataSource = _datatable;
                loaded = true;
                app.Quit();
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show("Вы действительно хотите выйти из приложение?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //------------------------------Меню-----------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            if (loaded == true)
            {
                this.tabControl.SelectedTab = tabPage_graphics;
                DrawMatrixGraph();
            }
            else
            {
                DialogResult _result = MessageBox.Show("Нет данных для анализа", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DrawMatrixGraph()
        {
            //ZedGraph.MasterPane matrixPane = zedGraph.MasterPane; // Создаем экземпляр класса MasterPane, где будет матрица графиков
            //matrixPane.PaneList.Clear(); // Очистка MasterPane

            int a = _datatable.Columns.Count - 1;
            int b = a * a;

            double xmin = -40;
            double xmax = 40;

            string[] columnNames = _datatable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();  

            for (int j = 0; j < a; j++)
            {
                
                for (int i = 0; i < a; i++)
                { 
                    //GraphPane pane = new GraphPane(); // Создаем экземпляр класса GraphPane, представляющий собой один график
                    PointPairList list = new PointPairList();
                    list.Clear();
                    column_array1 = _datatable.AsEnumerable().Select(r => r[j].ToString()).ToArray();

                    if (i == j)
                    {
                        if (j == a)
                        {
                            column_array2 = _datatable.AsEnumerable().Select(r => r[i - 1].ToString()).ToArray();
                           // pane.XAxis.Title.Text = "Ось X" + columnNames[i-1];
                           // pane.YAxis.Title.Text = "Ось Y" + columnNames[j];
                        }
                        else
                        {
                            column_array2 = _datatable.AsEnumerable().Select(r => r[i + 1].ToString()).ToArray();
                           // pane.XAxis.Title.Text = "Ось X" + columnNames[i+1];
                           // pane.YAxis.Title.Text = "Ось Y" + columnNames[j];
                        }
                    }
                    else
                    {
                        column_array2 = _datatable.AsEnumerable().Select(r => r[i].ToString()).ToArray();
                      //  pane.XAxis.Title.Text = "Ось X" + columnNames[i];
                      //  pane.YAxis.Title.Text = "Ось Y" + columnNames[j];
                    }
                    for (int k = 0; k < column_array1.Length; k++)
                    {
                        string tmp_x = column_array1[k];
                        double x = Convert.ToDouble(tmp_x);

                        string tmp_y = column_array2[k];
                        double y = Convert.ToDouble(tmp_y);

                        list.Add(x, y);
                    }
                    //matrixPane.Add(pane); // Добавим новый график в MasterPane
                    
                    // Создадим кривую с названием "Scatter".
                    // Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
                    // Опорные точки - ромбики (SymbolType.Diamond)
                   // LineItem myCurve = pane.AddCurve("Scatter", list, Color.Blue, SymbolType.Circle);
                   // myCurve.Line.IsVisible = false;
                }

            }

            // Будем размещать добавленные графики в MasterPane
            using (Graphics g = CreateGraphics())
            {
                //matrixPane.SetLayout(g, PaneLayout.SquareColPreferred); // Расположение графиков NxN
            }

            // Обновим оси и перерисуем график
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }

        private void DrawSingleGraph(GraphPane pane, int a)
        {        
            pane.CurveList.Clear();
            
            double xmin = -40;
            double xmax = 40;

            for (int j = 0; j < a; j++)
            {
                for (int i = 0; i < a; i++)
                {
                    PointPairList list = new PointPairList();
                    list.Clear();

                    column_array1 = _datatable.AsEnumerable().Select(r => r[j].ToString()).ToArray();
                    column_array2 = _datatable.AsEnumerable().Select(r => r[i].ToString()).ToArray();

                    for (int k = 0; k < column_array1.Length; k++)
                    {
                        string tmp_x = column_array1[k];
                        double x = Convert.ToDouble(tmp_x);

                        string tmp_y = column_array2[k];
                        double y = Convert.ToDouble(tmp_y);

                        list.Add(x, y);
                        
                    }
                        // Создадим кривую с названием "Scatter".
                        // Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
                        // Опорные точки - ромбики (SymbolType.Diamond)
                        LineItem myCurve = pane.AddCurve("Scatter", list, Color.Blue, SymbolType.Circle);
                        myCurve.Line.IsVisible = false;
                }
               
            }
            /*/ !!!
            // Создадим кривую с названием "Scatter".
            // Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
            // Опорные точки - ромбики (SymbolType.Diamond)
            LineItem myCurve = pane.AddCurve("Scatter", list, Color.Blue, SymbolType.Circle);

            // !!!
            // У кривой линия будет невидимой
            myCurve.Line.IsVisible = false;

            // !!!
            // Цвет заполнения отметок (ромбиков) - колубой
            myCurve.Symbol.Fill.Color = Color.Blue;

            // !!!
            // Тип заполнения - сплошная заливка
            myCurve.Symbol.Fill.Type = FillType.Solid;

            // !!!
            // Размер ромбиков
            myCurve.Symbol.Size = 7;


            // Устанавливаем интересующий нас интервал по оси X
            pane.XAxis.Scale.Min = xmin;
            pane.XAxis.Scale.Max = xmax;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
            */
        }
        private void DrawGraph()
        {
            column_array1 = _datatable.AsEnumerable().Select(r => r[0].ToString()).ToArray();
            column_array2 = _datatable.AsEnumerable().Select(r => r[1].ToString()).ToArray();

            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();

            // Интервал, в котором будут лежать точки
            int xmin = -200;
            int xmax = 200;

            int ymin = -200;
            int ymax = 200;

            // Заполняем список точек
            for (int i = 0; i < column_array1.Length; i++)
            {
                try
                {
                    string tmp_x = column_array1[i];
                    double x = checked(Convert.ToDouble(tmp_x));

                    string tmp_y = column_array2[i];
                    double y = checked(Convert.ToDouble(tmp_y));

                    list.Add(x, y);
                }
                catch (OverflowException ex)
                {
                    DialogResult _result = MessageBox.Show(ex.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // !!!
            // Создадим кривую с названием "Scatter".
            // Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
            // Опорные точки - ромбики (SymbolType.Diamond)
            LineItem myCurve = pane.AddCurve("Scatter", list, Color.Blue, SymbolType.Circle);

            // !!!
            // У кривой линия будет невидимой
            myCurve.Line.IsVisible = false;

            // !!!
            // Цвет заполнения отметок (ромбиков) - колубой
            myCurve.Symbol.Fill.Color = Color.Blue;

            // !!!
            // Тип заполнения - сплошная заливка
            myCurve.Symbol.Fill.Type = FillType.Solid;

            // !!!
            // Размер ромбиков
            myCurve.Symbol.Size = 7;


            // Устанавливаем интересующий нас интервал по оси X
            pane.XAxis.Scale.Min = xmin;
            pane.XAxis.Scale.Max = xmax;

            // Устанавливаем интересующий нас интервал по оси Y
            pane.YAxis.Scale.Min = ymin;
            pane.YAxis.Scale.Max = ymax;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }
    }
}
