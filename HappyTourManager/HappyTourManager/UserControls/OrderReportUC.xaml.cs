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
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderReportUC"/> class.
        /// Order report
        /// </summary>
        /// <param name="point1">data1</param>
        /// <param name="point2">data2</param>
        /// <param name="point3">data3</param>
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

        /// <summary>
        /// Gets or sets data series
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; }

        /// <summary>
        /// Gets or sets labels
        /// </summary>
        public string[] Labels { get; set; }

        /// <summary>
        /// Gets or sets formats
        /// </summary>
        public Func<int, string> Formatter { get; set; }

        /// <summary>
        /// Gets or sets creation day
        /// </summary>
        public DateTime CreationDay { get; set; }
    }
}
