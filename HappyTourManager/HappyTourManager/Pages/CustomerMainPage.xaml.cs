namespace HappyTourManager.Pages
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
    using DATA;
    using DATA.Interfaces;
    using DATA.Repositoriees;

    /// <summary>
    /// Interaction logic for CustomerMainPage.xaml
    /// </summary>
    public partial class CustomerMainPage : Page
    {
        #region private variables
        private IRepository<Customer> custRepository;
        CustomerMainViewModel custVM;
        AddCustomerUC custDetail;
        #endregion

        #region constructor
        public CustomerMainPage(IRepository<Customer> custRepository)
        {
            this.InitializeComponent();
            this.custRepository = custRepository;

        }
        #endregion

        #region eventhandlers
        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.custVM.SelectedCtegory == "VALIDTO")
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
            else if (this.custVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                this.custVM.SelectedValue = default(string);
            }
            else if (this.custVM.SelectedCtegory == "LOYALTYCARD")
            {
                ComboBox cBox = new ComboBox();

                cBox.Items.Add("yes");
                cBox.Items.Add("no");

                Binding binding = new Binding("SelectedValue");
                cBox.SetBinding(ComboBox.SelectedItemProperty, binding);
                this.contSearch1.Content = cBox;
                this.contSearch2.Content = null;
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
            try
            {
                this.custVM.GetSearchResult();
                if (this.custVM.ResultList.Count > 0)
                {
                    this.btnEdit.Visibility = Visibility.Visible;
                    this.btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    this.btnEdit.Visibility = Visibility.Hidden;
                    this.btnDelete.Visibility = Visibility.Hidden;
                }
                this.contCustDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong :(");
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.custVM = new CustomerMainViewModel(this.custRepository);
            this.searchCat.ItemsSource = this.custVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = this.custVM;
            this.custDetail = new AddCustomerUC();
            foreach (string item in this.custVM.countryList)
            {
                this.custDetail.cboxCountry.Items.Add(item);
            }
            this.contCustDetails.Content = this.custDetail;
            this.contCustDetails.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.custVM.SelectedCustomer = new Customer()
            {
                Person = new Person()
            {
                BirthDate = DateTime.Today,
                ValidTo = DateTime.Today
            }
            };
            this.contCustDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.custVM.Checkvalues())
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    this.custVM.SaveInstance();
                    MessageBox.Show("Customer data is saved!");
                    this.contCustDetails.Visibility = Visibility.Hidden;
                    this.custVM.SelectedCustomer = null;
                    this.btnSave.Visibility = Visibility.Hidden;
                    this.btnCancel.Visibility = Visibility.Hidden;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong :(");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contCustDetails.Visibility = Visibility.Hidden;
            this.custVM.SelectedCustomer = null;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.custVM.ResultList.Count>0 && this.custVM.SelectedCustomer !=null)
            {
                this.contCustDetails.Visibility = Visibility.Visible;
                this.btnSave.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.contCustDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
                MessageBox.Show("Please select a customer!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.custVM.SelectedCustomer != null)
            {
                if (MessageBox.Show("Do you want to delete customer?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.custVM.DeleteInstance();
                    MessageBox.Show("Customer is deleted");
                    this.custVM.ResultList.Remove(this.custVM.SelectedCustomer);
                }
            }
            else
            {
                MessageBox.Show("Please select a customer!");
            }
        }
        #endregion
    }
}
