using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
//<------------------Для Excel--------------------->
using ExcelObj = Microsoft.Office.Interop.Excel;
//<-----------------Для OxyPlot-------------------->
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace ConverterLibrary
{
    public class Data_Converter
    {
        private List<ScatterSeries> list_matrix_series = new List<ScatterSeries>();
        private List<ScatterSeries> list_work_series = new List<ScatterSeries>();

        private string[] x_axis;
        private string[] y_axis;
        private string[] plot_title;

        string[] columnNames_r;

        public List<ScatterSeries> list_m_s { get { return list_matrix_series; } }
        public List<ScatterSeries> list_w_s { get { return list_work_series; } }

        public string[] x_a { get { return x_axis; } }
        public string[] y_a { get { return y_axis; } }
        public string[] plot_t { get { return plot_title; } }

        public string[] column_r { get { return columnNames_r; } }

        public Data_Converter()
        {

        }

        /// <summary>
        /// Перевод данных Excel в набор точек для графиков
        /// Что - то ещё тут мудрить наверное не стоит
        /// </summary>
        public void Convert_Excel_to_Series(DataTable excel_table_data)
        {
            string[] column_array_to_X; // Массив для хранения столбца из Excel, который будет использоваться в качестве точек оси X
            string[] column_array_to_Y; // Массив для хранения столбца из Excel, который будет использоваться в качестве точек оси Y

            int matrix_columns = excel_table_data.Columns.Count - 1;
            int size = excel_table_data.Columns.Count * matrix_columns; // Размерность - для массивов x_axis и y_axis
            int index = 0; // Индексация для наборов точек

            x_axis = new string[size]; // Массив - содержит точки для оси X
            y_axis = new string[size]; // Массив - содержит точки для оси Y
            plot_title = new string[size]; // Название графика - берется из сложения названий столбцов из Excel

            string[] columnNames = excel_table_data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray(); // Сами названия

            columnNames_r = excel_table_data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray(); // Header для R

            for (int j = 0; j <= matrix_columns; j++)
            {
                column_array_to_X = excel_table_data.AsEnumerable().Select(r => r[j].ToString()).ToArray();
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
                            column_array_to_Y = excel_table_data.AsEnumerable().Select(r => r[i].ToString()).ToArray();
                            x_axis[index] = columnNames[j].ToString();
                            y_axis[index] = columnNames[i].ToString();
                            plot_title[index] = columnNames[j].ToString() + " " + columnNames[i].ToString();
                            //Ось X = "Ось " + columnNames[j];
                            //Ось Y = "Ось " + columnNames[i];
                        }
                    }
                    else
                    {
                        column_array_to_Y = excel_table_data.AsEnumerable().Select(r => r[i].ToString()).ToArray();
                        x_axis[index] = columnNames[j].ToString();
                        y_axis[index] = columnNames[i].ToString();
                        plot_title[index] = columnNames[j].ToString() + " " + columnNames[i].ToString();
                        //Ось X = "Ось " + columnNames[j];
                        //Ось Y = "Ось " + columnNames[i];
                    }

                    // Конфигурация наборов точек для матричного и рабочего графиков (можно вынести в отдельный классы)
                    ScatterSeries matrix_points = new ScatterSeries
                    {
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 1.5,
                        MarkerFill = OxyColors.DarkGreen,
                        SelectionMode = OxyPlot.SelectionMode.Multiple,
                    };
                    ScatterSeries work_points = new ScatterSeries
                    {
                        MarkerType = MarkerType.Circle,
                        MarkerSize = 1.5,
                        MarkerFill = OxyColors.DarkGreen,
                        SelectionMode = OxyPlot.SelectionMode.Multiple,
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
    }
}
