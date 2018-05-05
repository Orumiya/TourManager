using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace HappyTourManager
{
    /// <summary>
    /// Interaction logic for PieChartUC.xaml
    /// </summary>
    public partial class PieChartUC : UserControl
    {
        public PieChartUC(int point1, int point2)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "WithLoyalty",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(point1) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "WithoutLoyalty",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(point2) },
                    DataLabels = true
                }


            };
            CreationDay = DateTime.Today;
            DataContext = this;

        }

        public SeriesCollection SeriesCollection { get; set; }
        public DateTime CreationDay {get; set;}

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


    }
}
