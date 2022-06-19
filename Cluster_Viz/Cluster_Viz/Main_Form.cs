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
        bool excel_loaded = false; //Определяет, загрузился ли файл Excel
        bool coordinates_shown = false; //Определяет, показаны ли координаты

        DataTable excel_data = new DataTable(); //Данные Excel в виде таблицы

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
            //Загрузка меню
            Menu_Toolbar test = new Menu_Toolbar();



            InitializeComponent();
            Load_Menu();
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {

        }
        #endregion   

        #region Matrix Building
        private void button_build_matrix_Click(object sender, EventArgs e) //Построение матрицы графиков
        {
            if (excel_loaded == true)
            {
                this.tab_control.SelectedTab = tab_page_2D; //Доделать на автоматически
                tab_page_2D.Text = "Графики 2D. Таблица: " + excel_data.TableName;
                Draw_MatrixPlots();
            }
            else
            {
                DialogResult _result = MessageBox.Show("Нет данных для анализа", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Draw_MatrixPlots()
        {
            work_area.Controls.Add(work_plot = new OxyPlot.WindowsForms.PlotView());
            work_plot.Height = work_area.Height;
            work_plot.Width = work_area.Width;

            int _columns = excel_data.Columns.Count;
            int _rows = excel_data.Columns.Count;

            int _height = panel_2D.Height / _columns;
            int _width = panel_2D.Width / _columns;

            int x_poz = 0;
            int y_poz = 0;
            int plot_index = 0;

            //Создаёт графики, равное количеству столбцов-1
            _columns = excel_data.Columns.Count - 1;
            Covert_Excel_to_Series();

            for (int j = 0; j < _rows; j++)
            {
                x_poz = 0;
                for (int i = 0; i < _columns; i++)
                {
                    panel_2D.Controls.Add(plot_2D = new OxyPlot.WindowsForms.PlotView());
                    plot_2D.Height = _height;
                    plot_2D.Width = _width;
                    plot_2D.Location = new System.Drawing.Point(x_poz, y_poz);

                    Create_Model(plot_index);

                    x_poz = x_poz + _width;
                    plot_index = plot_index + 1;
                }
                y_poz = y_poz + _height + 1;
            }
        }

        private void Covert_Excel_to_Series() //Перевод данных Excel в графики
        {
            string[] column_array_to_X;
            string[] column_array_to_Y;

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
        #endregion

        #region Work_Model_Buttons
        private void button_add_cluster_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list_matrix_models.Count; i++)
            {
                Matrix_Cluster matrix_cluster = new Matrix_Cluster();
                Work_Cluster work_cluster = new Work_Cluster();

                foreach (ScatterPoint point in list_matrix_series[i].Points)
                {
                    int index = list_matrix_series[i].Points.FindIndex(a => a == point);

                    if (list_matrix_series[i].IsItemSelected(index))
                    {
                        matrix_cluster.Points.Add(point);
                        work_cluster.Points.Add(point);
                    }
                }
                list_matrix_models[i].Series.Add(matrix_cluster);
                list_work_models[i].Series.Add(work_cluster);

                list_matrix_models[i].Series[0].Selectable = false;
                list_matrix_models[i].Series[1].Selectable = false;
                list_work_models[i].Series[0].Selectable = false;
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

        private void button_show_coords_Click(object sender, EventArgs e)
        {
            if (coordinates_shown == false)
            {
                for (int i = 0; i < list_work_series.Count; i++)
                {
                    list_work_series[i].LabelFormatString = "{0}, {1}";
                }
                list_work_models[id].InvalidatePlot(true);
                coordinates_shown = true;
            }
            else
            {
                for (int i = 0; i < list_work_series.Count; i++)
                {
                    list_work_series[i].LabelFormatString = "";
                }
                list_work_models[id].InvalidatePlot(true);
                coordinates_shown = false;
            }
        }

        //Снятие выделения с графиков
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
        #endregion     
    }
}