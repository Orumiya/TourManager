// <copyright file="OrderReportUC.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Windows.Controls;
    using LiveCharts;
    using LiveCharts.Wpf;

    /// <summary>
    /// Interaction logic for OrderReportUC.xaml
    /// </summary>
    public partial class OrderReportUC : UserControl
    {
        public OrderReportUC(int point1, int point2, int point3)
        {
            this.InitializeComponent();

            this.SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Orders",
                    Values = new ChartValues<int> { point1, point2, point3 },
                    MaxColumnWidth = 100
                }
            };

            this.Labels = new[] { "Payed Orders", "Cancelled Orders", "Pending Orders" };
            this.Formatter = value => value.ToString("N");
            this.CreationDay = DateTime.Today;

            this.DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        public DateTime CreationDay { get; set; }
    }
}
