namespace HappyTourManager
{
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

    /// <summary>
    /// Interaction logic for TourDetailsUC.xaml
    /// </summary>
    public partial class TourDetailsUC : UserControl
    {
        public TourDetailsUC()
        {
            this.InitializeComponent();

            this.cbTrans.Items.Add("bus");
            this.cbTrans.Items.Add("flight");
            this.cbTrans.Items.Add("cruise");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
