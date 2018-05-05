namespace HappyTourManager
{
    using System.Windows;
    using System.Windows.Controls;

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
