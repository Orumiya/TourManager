using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyTourManager
{
    /// <summary>
    /// Interaction logic for OrderReportUC.xaml
    /// </summary>
    public partial class OrderReportUC : UserControl
    {
        public OrderReportUC(int point1, int point2, int point3)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Orders",
                    Values = new ChartValues<int> { point1, point2, point3 }
                }
            };

            Labels = new[] { "Payed Orders", "Cancelled Orders", "Pending Orders" };
            Formatter = value => value.ToString("N");
            CreationDay = DateTime.Today;

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }
        public DateTime CreationDay { get; set; }
    }
}
