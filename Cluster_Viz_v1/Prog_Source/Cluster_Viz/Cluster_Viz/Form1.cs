using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//для Excel
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;

//для OxyPlot
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace Cluster_Viz
{
    partial class main_form : Form
    {
        #region Data
        DataTable excel_data = new DataTable();

        bool excel_loaded = false;
        bool labels_shown = false;

        string[] column_array_to_X;
        string[] column_array_to_Y;

        string[] x_axis;
        string[] y_axis;
        string[] plot_title;

        List<Matrix_Cluster> list_matrix_cluster = new List<Matrix_Cluster>();
        List<Work_Cluster> work_matrix_cluster = new List<Work_Cluster>();

        List<ScatterSeries> list_matrix_series = new List<ScatterSeries>();
        List<ScatterSeries> list_work_series = new List<ScatterSeries>();

        List<Matrix_Model> list_matrix_models = new List<Matrix_Model>();
        List<Work_Model> list_work_models = new List<Work_Model>();

        public static int id;
        #endregion

        #region Form
        public main_form()
        {
            InitializeComponent();
            Load_Menu();
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }
        #endregion

        private void button_build_matrix_Click(object sender, EventArgs e)
        {
            if (excel_loaded == true)
            {
                this.tab_control.SelectedTab = tab_page_2D;
                tab_page_2D.Text = "Графики 2D. Таблица: " + excel_data.TableName;
                Draw_Plots();
            }
            else
            {
                DialogResult _result = MessageBox.Show("Нет данных для анализа", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Draw_Plots()
        {
            work_area.Controls.Add(work_plot = new OxyPlot.WindowsForms.PlotView());
            work_plot.Height = work_area.Height;
            work_plot.Width = work_area.Width;

            int _columns = excel_data.Columns.Count;
            int _rows = excel_data.Columns.Count;

            int h = panel_2D.Height / _columns;
            int w = panel_2D.Width / _columns;

            _columns = excel_data.Columns.Count - 1;
            Excel_to_Series();

            int x_poz = 0;
            int y_poz = 0;
            int plot_index = 0;

            for (int j = 0; j < _rows; j++)
            {
                x_poz = 0;
                for (int i = 0; i < _columns; i++)
                {
                    panel_2D.Controls.Add(plot_2D = new OxyPlot.WindowsForms.PlotView());
                    plot_2D.Height = h;
                    plot_2D.Width = w;
                    plot_2D.Location = new System.Drawing.Point(x_poz, y_poz);

                    Create_Model(plot_index);

                    x_poz = x_poz + w;
                    plot_index = plot_index + 1;
                }
                y_poz = y_poz + h + 1;
            }
        }

        private void Excel_to_Series()
        {
            int matrix_columns = excel_data.Columns.Count - 1;
            int size = excel_data.Columns.Count * matrix_columns;
            int index = 0;

            x_axis = new string[size];
            y_axis = new string[size];
            plot_title = new string[size];

            string[] columnNames = excel_data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

            for (int j = 0; j <= matrix_columns; j++)
            {
                column_array_to_X = excel_data.AsEnumerable().Select(r => r[j].ToString()).ToArray();
                for (int i = 0; i <= matrix_columns; i++)
                {
                    if (i == j)
                    {
                        i = i + 1;
                        if (j == matrix_columns)
                        {
                            break;
                        }
                        else
                        {
                            column_array_to_Y = excel_data.AsEnumerable().Select(r => r[i].ToString()).ToArray();
                            x_axis[index] = columnNames[j].ToString();
                            y_axis[index] = columnNames[i].ToString();
                            plot_title[index] = columnNames[j].ToString() + " " + columnNames[i].ToString();
                            //Ось X = "Ось " + columnNames[j];
                            //Ось Y = "Ось " + columnNames[i];
                        }
                    }
                    else
                    {
                        column_array_to_Y = excel_data.AsEnumerable().Select(r => r[i].ToString()).ToArray();
                        x_axis[index] = columnNames[j].ToString();
                        y_axis[index] = columnNames[i].ToString();
                        plot_title[index] = columnNames[j].ToString() + " " + columnNames[i].ToString();
                        //Ось X = "Ось " + columnNames[j];
                        //Ось Y = "Ось " + columnNames[i];
                    }

                    ScatterSeries matrix_points = new ScatterSeries
                    {
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 1.5,
                        MarkerFill = OxyColors.DarkGreen,
                    };

                    ScatterSeries work_points = new ScatterSeries
                    {
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 1.5,
                        MarkerFill = OxyColors.DarkGreen,
                    };

                    for (int k = 0; k < column_array_to_X.Length; k++)
                    {
                        string tmp_x = column_array_to_X[k];
                        double x = Convert.ToDouble(tmp_x);

                        string tmp_y = column_array_to_Y[k];
                        double y = Convert.ToDouble(tmp_y);

                        matrix_points.Points.Add(new ScatterPoint(x, y));
                        work_points.Points.Add(new ScatterPoint(x, y));
                    }

                    list_matrix_series.Add(matrix_points);
                    list_work_series.Add(work_points);

                    index = index + 1;
                }
            }
        }

        private void Create_Model(int plot_index)
        {
            Series series = null;
            Matrix_Model matrix_model = new Matrix_Model(plot_title, plot_index);
            Work_Model work_model = new Work_Model(plot_title, plot_index, x_axis, y_axis);

            matrix_model.Load_Mouse_Events(work_model, list_matrix_series, list_work_series, series, work_plot);
            work_model.Load_Mouse_Events(work_model, list_work_series, list_matrix_series, list_matrix_models);
           
            matrix_model.Series.Add(list_matrix_series[plot_index]);

            series = matrix_model.Series[0];
            series.SelectionMode = OxyPlot.SelectionMode.Multiple;

            list_matrix_models.Add(matrix_model);
            list_work_models.Add(work_model);

            plot_2D.Model = matrix_model;
        }

        private void Load_Menu()
        {
            main_menu_file_open.Click += (s, e) =>
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
                    excel_data.TableName = open_excel.SafeFileName;
                    workbook = app.Workbooks.Open(open_excel.FileName, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value);

                    NewSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
                    SheetRange = NewSheet.UsedRange;

                    for (int ClmNum = 1; ClmNum <= SheetRange.Columns.Count; ClmNum++)
                    {
                        excel_data.Columns.Add(new DataColumn((SheetRange.Cells[1, ClmNum] as ExcelObj.Range).Value2.ToString()));
                    }

                    for (int Rnum = 2; Rnum <= SheetRange.Rows.Count; Rnum++)
                    {
                        DataRow _datarow = excel_data.NewRow();
                        for (int Cnum = 1; Cnum <= SheetRange.Columns.Count; Cnum++)
                        {
                            if ((SheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                            {
                                _datarow[Cnum - 1] =
                                (SheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
                            }
                        }
                        excel_data.Rows.Add(_datarow);
                        excel_data.AcceptChanges();
                    }
                    excel_dataGridView.DataSource = excel_data;
                    excel_loaded = true;
                    app.Quit();
                }
            };

            main_menu_file_exit.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите выйти из приложение?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };
        }

        #region Work_Model_Buttons
        private void button_add_cluster_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list_matrix_models.Count; i++)
            {
                Matrix_Cluster matrix_cluster = new Matrix_Cluster
                {
                    MarkerType = MarkerType.Diamond,
                    MarkerSize = 3,
                    Selectable = false       
                };
                Work_Cluster work_cluster = new Work_Cluster
                {
                    MarkerType = MarkerType.Diamond,
                    MarkerSize = 3,
                    Selectable = false
                };

                foreach (ScatterPoint point in list_matrix_series[i].Points)
                {
                    int point_index = list_matrix_series[i].Points.FindIndex(a => a == point);

                    if (list_matrix_series[i].IsItemSelected(point_index))
                    {
                        matrix_cluster.Points.Add(point);
                        work_cluster.Points.Add(point);
                    }
                }

                list_matrix_models[i].Series.Add(matrix_cluster);
                list_work_models[i].Series.Add(work_cluster);
            }

            foreach (Matrix_Model model in list_matrix_models)
            {
                model.InvalidatePlot(true);
            }
            list_work_models[id].InvalidatePlot(true);
            Clear_Selection_All();
        }

        private void button_plot_clear_all_Click(object sender, EventArgs e)
        {
            Clear_Selection_All();
        }

        private void button_plot_clear_current_Click(object sender, EventArgs e)
        {
            list_work_series[id].ClearSelection();
            list_work_models[id].InvalidatePlot(true);
        }

        private void button_show_labels_Click(object sender, EventArgs e)
        {
            if (labels_shown == false)
            {
                for (int i = 0; i < list_work_series.Count; i++)
                {
                    list_work_series[i].LabelFormatString = "{0}, {1}";
                }
                list_work_models[id].InvalidatePlot(true);
                labels_shown = true;
            }
            else
            {
                for (int i = 0; i < list_work_series.Count; i++)
                {
                    list_work_series[i].LabelFormatString = "";
                }
                list_work_models[id].InvalidatePlot(true);
                labels_shown = false;
            }
        }
        #endregion

        public void Clear_Selection_All()
        {
            for (int i = 0; i < list_matrix_series.Count; i++)
            {
                list_work_series[i].ClearSelection();
                list_matrix_series[i].ClearSelection();
            }
            foreach (Matrix_Model plot in list_matrix_models)
            {
                plot.InvalidatePlot(true);
            }
            list_work_models[id].InvalidatePlot(true);
        }
    }

    #region Cluster Classes
    public partial class Matrix_Cluster : ScatterSeries
    {
        public int index;
        public string cluster_name;
    }

    public partial class Work_Cluster : ScatterSeries
    {
        public int index;
        public string cluster_name;
    }
    #endregion

    #region Models Classes
    public partial class Matrix_Model : PlotModel
    {
        public int index;
        public bool model_pressed = false;

        public Matrix_Model(string[] op_tit, int plot_index)
        {
            Title = op_tit[plot_index];
            TitleFontSize = 12.0;

            index = plot_index;
            IsLegendVisible = false;
            PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1);
            PlotMargins = new OxyThickness(0, 0, 0, 0);
            Padding = new OxyThickness(5);
            Background = OxyColors.White;
            SelectionColor = OxyColors.Crimson;

            Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                TickStyle = OxyPlot.Axes.TickStyle.None,
                //MajorGridlineStyle = LineStyle.Dash,
                //MinorGridlineStyle = LineStyle.Dash,
                MaximumPadding = 0.1,
                MinimumPadding = 0.1,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                FontSize = 0.1,
                SelectionMode = OxyPlot.SelectionMode.Multiple
            });

            Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TickStyle = OxyPlot.Axes.TickStyle.None,
                //MajorGridlineStyle = LineStyle.Dash,
                //MinorGridlineStyle = LineStyle.Dash,
                MaximumPadding = 0.1,
                MinimumPadding = 0.1,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                FontSize = 0.1,
                SelectionMode = OxyPlot.SelectionMode.Multiple
            });
        }

        public void Load_Mouse_Events (Work_Model work_model, List<ScatterSeries> list_matrix_series, List<ScatterSeries> list_work_series, Series series, OxyPlot.WindowsForms.PlotView work_plot)
        {
            MouseDown += (s, e) =>
            {
                if (e.ChangedButton == OxyMouseButton.Left)
                {
                    main_form.id = index;

                    if (model_pressed == false)
                    {
                        work_model.Series.Add(list_work_series[main_form.id]);
                        model_pressed = true;
                    }

                    work_plot.Model = work_model;
                    series = work_model.Series[0];
                    series.SelectionMode = OxyPlot.SelectionMode.Multiple;

                    for (int i = 0; i < list_matrix_series[main_form.id].Points.Count; i++)
                    {
                        if (list_matrix_series[main_form.id].IsItemSelected(i))
                        {
                            list_work_series[main_form.id].SelectItem(i);
                        }
                    }
                    work_model.Background = OxyColors.White;

                    InvalidatePlot(false);
                    e.Handled = true;
                }
            };

            MouseUp += (s, e) =>
            {
                Background = OxyColors.White;

                InvalidatePlot(false);
                e.Handled = true;
            };

            MouseEnter += (s, e) =>
            {
                PlotAreaBorderColor = OxyColors.Crimson;

                InvalidatePlot(false);
                e.Handled = true;
            };

            MouseLeave += (s, e) =>
            {
                Background = OxyColors.White;
                PlotAreaBorderColor = OxyColors.Black;

                InvalidatePlot(false);
                e.Handled = true;
            };
        }
    }

    public partial class Work_Model : PlotModel
    {
        public int index;

        public Work_Model(string[] op_tit, int plot_index, string[] x_tit, string[] y_tit)
        {
            Title = op_tit[plot_index];
            index = plot_index;
            PlotAreaBorderThickness = new OxyThickness(1, 0, 0, 1);
            PlotMargins = new OxyThickness(30, 0, 0, 30);
            IsLegendVisible = false;
            SelectionColor = OxyColors.Crimson;

            Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                TickStyle = OxyPlot.Axes.TickStyle.None,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                MaximumPadding = 0,
                MinimumPadding = 0,
                Title = x_tit[plot_index]
            });
            Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TickStyle = OxyPlot.Axes.TickStyle.None,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                MaximumPadding = 0,
                MinimumPadding = 0,
                Title = y_tit[plot_index]
            });
        }

        public void Load_Mouse_Events(Work_Model work_model, List<ScatterSeries> list_work_series, List<ScatterSeries> list_matrix_series, List<Matrix_Model> list_matrix_models)
        {
            var pressed_button = "none";
            //Selection
            LineSeries l_series = null;
            PolygonAnnotation selection_annotation = null;
            work_model.MouseDown += (s, e) =>
            {
                main_form.id = work_model.index;
                //work_model.Subtitle = main_form.id.ToString(); // Id Tracking
                if (e.ChangedButton == OxyMouseButton.Left)
                {
                    pressed_button = "left";
                    selection_annotation = new PolygonAnnotation();
                    selection_annotation.Layer = AnnotationLayer.BelowSeries;

                    l_series = new LineSeries
                    {
                        Color = OxyColors.Black,
                        StrokeThickness = 1.5,
                        LineStyle = OxyPlot.LineStyle.LongDashDot,
                        MinimumSegmentLength = 0.1,
                        CanTrackerInterpolatePoints = true,
                    };
                    work_model.Series.Add(l_series);
                    work_model.InvalidatePlot(true);
                    e.Handled = true;
                }
            };

            work_model.MouseMove += (s, e) =>
            {
                if (l_series != null && l_series.XAxis != null)
                {
                    l_series.Points.Add(l_series.InverseTransform(e.Position));
                    work_model.InvalidatePlot(false);
                }
            };

            work_model.MouseUp += (s, e) =>
            {
                List<int> selected_points = new List<int>();
                switch (pressed_button)
                {
                    case "left":
                        selection_annotation.Points.AddRange(l_series.Points);
                        work_model.Annotations.Add(selection_annotation);

                        foreach (ScatterPoint scatter_point in list_work_series[main_form.id].Points)
                        {
                            bool inside = IsPointInPolygon(l_series.Points, scatter_point);
                            if (inside == true)
                            {
                                int point_index = list_work_series[main_form.id].Points.FindIndex(a => a == scatter_point);
                                if (list_work_series[main_form.id].IsItemSelected(point_index) == false)
                                {
                                    list_work_series[main_form.id].SelectItem(point_index);
                                    selected_points.Add(point_index);
                                }
                            }
                        }

                        if (selected_points.Count != 0)
                        {
                            for (int i = 0; i < list_matrix_series.Count; i++)
                            {
                                foreach (ScatterPoint s_point in list_matrix_series[i].Points)
                                {
                                    foreach (int index in selected_points)
                                    {
                                        list_matrix_series[i].SelectItem(index);
                                    }                              
                                }
                            }
                        }

                        foreach (Matrix_Model plot in list_matrix_models)
                        {
                            plot.InvalidatePlot(true);
                        }

                        work_model.Series.Remove(l_series);
                        work_model.Annotations.Remove(selection_annotation);

                        work_model.InvalidatePlot(true);
                        e.Handled = true;

                        pressed_button = "none";
                        break;

                    case "right":
                        list_work_series[main_form.id].XAxis.Pan(e.Position, e.Position);
                        list_work_series[main_form.id].YAxis.Pan(e.Position, e.Position);

                        pressed_button = "none";
                        break;
                }
            };

            work_model.MouseEnter += (s, e) =>
            {
                main_form.id = work_model.index;
                work_model.PlotAreaBorderColor = OxyColors.Red;
                work_model.InvalidatePlot(false);
                e.Handled = true;
            };

            work_model.MouseLeave += (s, e) =>
            {
                work_model.PlotAreaBorderColor = OxyColors.Black;
                work_model.InvalidatePlot(false);
                e.Handled = true;
            };
        }

        private static bool IsPointInPolygon(List<DataPoint> polygon, ScatterPoint point_to_check)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < point_to_check.Y && polygon[j].Y >= point_to_check.Y || polygon[j].Y < point_to_check.Y && polygon[i].Y >= point_to_check.Y)
                {
                    if (polygon[i].X + (point_to_check.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point_to_check.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
    #endregion
}