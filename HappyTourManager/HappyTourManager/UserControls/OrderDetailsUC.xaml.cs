namespace HappyTourManager
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for OrderDetailsUC.xaml
    /// </summary>
    public partial class OrderDetailsUC : UserControl
    {
        public OrderDetailsUC()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = -1; i < 10; i++)
            {
                this.cbAdults.Items.Add(i + 1);
            }
        }
    }
}
