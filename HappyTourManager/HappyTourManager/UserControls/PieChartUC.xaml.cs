// <copyright file="PieChartUC.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Windows.Controls;
    using LiveCharts;
    using LiveCharts.Defaults;
    using LiveCharts.Wpf;

    /// <summary>
    /// Interaction logic for PieChartUC.xaml
    /// </summary>
    public partial class PieChartUC : UserControl
    {
        public PieChartUC(int point1, int point2)
        {
            this.InitializeComponent();

            this.SeriesCollection = new SeriesCollection
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
            this.CreationDay = DateTime.Today;
            this.DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public DateTime CreationDay {get; set;}

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
