// <copyright file="TourReport.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using DATA;
    using LiveCharts;
    using LiveCharts.Wpf;

    /// <summary>
    /// Interaction logic for TourReport.xaml
    /// </summary>
    public partial class TourReport : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TourReport"/> class.
        /// Tourreport constructor
        /// </summary>
        /// <param name="datas">data serie</param>
        public TourReport(Dictionary<Tour, decimal> datas)
        {
            this.InitializeComponent();

            ChartValues<decimal> valuelist = new ChartValues<decimal>();
            List<string> labels = new List<string>();
            if (datas != null)
            {
                foreach (var item in datas)
                {
                    valuelist.Add(item.Value);
                    labels.Add(item.Key.TravelName);
                }

                this.SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Tours",
                    Values = valuelist,
                    MaxColumnWidth = 100
                }
            };

                this.Labels = labels;
                this.Formatter = value => value.ToString("N");
                this.CreationDay = DateTime.Today;
            }

            this.DataContext = this;
        }

        /// <summary>
        /// Gets or sets data series
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; }

        /// <summary>
        /// Gets or sets labels
        /// </summary>
        public List<string> Labels { get; set; }

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
