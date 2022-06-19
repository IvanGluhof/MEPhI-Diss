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
    //Класс для кластера, который отображается в матрице графиков
    public partial class Matrix_Cluster : ScatterSeries
    {
        public int index;
        public string cluster_name;

        public Matrix_Cluster()
        {
            MarkerType = MarkerType.Diamond;
            MarkerSize = 3;
        }
    }

    //Класс для кластера, который отображается в рабочей области
    public partial class Work_Cluster : ScatterSeries
    {
        public int index;
        public string cluster_name;

        public Work_Cluster()
        {
            MarkerType = MarkerType.Diamond;
            MarkerSize = 3;
        }
    }
}
