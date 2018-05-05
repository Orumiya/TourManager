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
        /// <summary>
        /// Initializes a new instance of the <see cref="PieChartUC"/> class.
        /// Piechart
        /// </summary>
        /// <param name="point1">data1</param>
        /// <param name="point2">data2</param>
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

        /// <summary>
        /// Gets or sets dataseries
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; }

        /// <summary>
        /// Gets or sets today
        /// </summary>
        public DateTime CreationDay { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (PieChart)chartpoint.ChartView;

            // clear selected slice.
            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
