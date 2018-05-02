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
    /// Interaction logic for TourDetailsUC.xaml
    /// </summary>
    public partial class TourDetailsUC : UserControl
    {
        public TourDetailsUC()
        {
            InitializeComponent();

            cbTrans.Items.Add("bus");
            cbTrans.Items.Add("flight");
            cbTrans.Items.Add("cruise");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
