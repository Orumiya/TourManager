using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
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

namespace HappyTourManager.Pages
{
    /// <summary>
    /// Interaction logic for CustomerMainPage.xaml
    /// </summary>
    public partial class CustomerMainPage : Page
    {
        private IRepository<Customer> custRepository;
        CustomerMainViewModel custVM;
        AddCustomerUC custDetail;

        public CustomerMainPage(IRepository<Customer> custRepository)
        {
            InitializeComponent();
            this.custRepository = custRepository;

        }

        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (custVM.SelectedCtegory == "VALIDTO")
            {
                DatePicker datePicker = new DatePicker();
                
                Binding binding = new Binding("SelectedDateFrom");
                datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);
                this.contSearch1.Content = datePicker;

                DatePicker datePicker2 = new DatePicker();
                Binding binding2 = new Binding("SelectedDateTo");
                datePicker2.SetBinding(DatePicker.SelectedDateProperty, binding2);
                this.contSearch2.Content = datePicker2;

            }
            else if (custVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                custVM.SelectedValue = default(string);
            }
            else
            {
                TextBox textbox = new TextBox();
                Binding binding = new Binding("SelectedValue");
                textbox.SetBinding(TextBox.TextProperty, binding);
                this.contSearch1.Content = textbox;
                this.contSearch2.Content = null;
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(custVM.SelectedValue);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            custVM = new CustomerMainViewModel(custRepository);
            this.searchCat.ItemsSource = custVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = custVM;
            custDetail = new AddCustomerUC();
            foreach (string item in custVM.countryList)
            {
                custDetail.cboxCountry.Items.Add(item);
            }
            this.contCustDetails.Content = custDetail;
            this.contCustDetails.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.contCustDetails.Visibility = Visibility.Visible;
        }
    }
}
