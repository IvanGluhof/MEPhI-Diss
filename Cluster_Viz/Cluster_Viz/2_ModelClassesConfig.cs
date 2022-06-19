using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//для OxyPlot
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace Cluster_Viz
{
    //Класс графика, который выводится выводится в общую матрицу графиков
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

        public void Load_Mouse_Events(Work_Model work_model, List<ScatterSeries> list_matrix_series, List<ScatterSeries> list_work_series, Series series, OxyPlot.WindowsForms.PlotView work_plot)
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
    
    //Класс графика, который выводится выводится в рабочую область
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
                Title = x_tit[plot_index],
                SelectionMode = OxyPlot.SelectionMode.Multiple
            });
            Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                TickStyle = OxyPlot.Axes.TickStyle.None,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dash,
                MaximumPadding = 0,
                MinimumPadding = 0,
                Title = y_tit[plot_index],
                SelectionMode = OxyPlot.SelectionMode.Multiple
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
                //DEBUG:Id Tracking
                //work_model.Subtitle = main_form.id.ToString(); 
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

        //Алгоритм поиска точек внутри многоугольника (во время выделения области)
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
}
