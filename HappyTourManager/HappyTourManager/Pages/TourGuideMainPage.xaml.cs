using DATA;
using DATA.Interfaces;
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
    /// Interaction logic for TourGuideMainPage.xaml
    /// </summary>
    public partial class TourGuideMainPage : Page
    {
        #region private variables
        private IRepository<Tourguide> tourGuideRepo;
        private IRepository<Language> languageRepo;
        private IRepository<OnHoliday> holidayRepo;
        private IRepository<Tour> tourRepo;
        private TourGuideMainViewModel tgVM;
        private TGDetailsUC tgDetails;
        #endregion

        #region constructor
        public TourGuideMainPage(
                IRepository<Tourguide> tourGuideRepo,
                IRepository<Language> languageRepo,
                IRepository<OnHoliday> holidayRepo,
                IRepository<Tour> tourRepo
            )
        {
            this.tourGuideRepo = tourGuideRepo;
            this.languageRepo = languageRepo;
            this.holidayRepo = holidayRepo;
            this.tourRepo = tourRepo;
            InitializeComponent();

            
            
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tgVM = new TourGuideMainViewModel(tourGuideRepo,languageRepo,holidayRepo,tourRepo);
            this.DataContext = tgVM;
            this.searchCat.ItemsSource = tgVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            tgDetails = new TGDetailsUC();
            foreach (string item in tgVM.countryList)
            {
                tgDetails.cboxCountry.Items.Add(item);
            }
            this.contTGDetails.Content = tgDetails;
            this.contTGDetails.Visibility = Visibility.Hidden;
        }

        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (custVM.SelectedCtegory == "VALIDTO")
            //{
            //    DatePicker datePicker = new DatePicker();

            //    Binding binding = new Binding("SelectedDateFrom");
            //    datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);
            //    this.contSearch1.Content = datePicker;

            //    DatePicker datePicker2 = new DatePicker();
            //    Binding binding2 = new Binding("SelectedDateTo");
            //    datePicker2.SetBinding(DatePicker.SelectedDateProperty, binding2);
            //    this.contSearch2.Content = datePicker2;

            //}
            //else if (custVM.SelectedCtegory == "DEFAULT")
            //{
            //    this.contSearch1.Content = null;
            //    this.contSearch2.Content = null;
            //    custVM.SelectedValue = default(string);
            //}
            //else
            //{
            //    TextBox textbox = new TextBox();
            //    Binding binding = new Binding("SelectedValue");
            //    textbox.SetBinding(TextBox.TextProperty, binding);
            //    this.contSearch1.Content = textbox;
            //    this.contSearch2.Content = null;
            //}

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    custVM.GetSearchResult();
            //    if (custVM.ResultList.Count > 0)
            //    {
            //        btnEdit.Visibility = Visibility.Visible;
            //        btnDelete.Visibility = Visibility.Visible;
            //    }
            //    else
            //    {
            //        btnEdit.Visibility = Visibility.Hidden;
            //        btnDelete.Visibility = Visibility.Hidden;
            //    }
            //    this.contCustDetails.Visibility = Visibility.Hidden;
            //    this.btnSave.Visibility = Visibility.Hidden;
            //    this.btnCancel.Visibility = Visibility.Hidden;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //custVM.SelectedCustomer = new Customer()
            //{
            //    Person = new Person()
            //    {
            //        BirthDate = DateTime.Today,
            //        ValidTo = DateTime.Today
            //    }
            //};
            //this.contCustDetails.Visibility = Visibility.Visible;
            //this.btnSave.Visibility = Visibility.Visible;
            //this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (custVM.Checkvalues())
            //    {
            //        MessageBox.Show("All values must be filled in!");
            //    }
            //    else
            //    {
            //        custVM.SaveCustomer();
            //        MessageBox.Show("Customer data is saved!");
            //        this.contCustDetails.Visibility = Visibility.Hidden;
            //        custVM.SelectedCustomer = null;
            //        this.btnSave.Visibility = Visibility.Hidden;
            //        this.btnCancel.Visibility = Visibility.Hidden;
            //    }
            //}
            //catch (InvalidOperationException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Something went wrong :(");
            //}


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.contCustDetails.Visibility = Visibility.Hidden;
            //custVM.SelectedCustomer = null;
            //this.btnSave.Visibility = Visibility.Hidden;
            //this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //if (custVM.ResultList.Count > 0 && custVM.SelectedCustomer != null)
            //{
            //    this.contCustDetails.Visibility = Visibility.Visible;
            //    this.btnSave.Visibility = Visibility.Visible;
            //    this.btnCancel.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    this.contCustDetails.Visibility = Visibility.Hidden;
            //    this.btnSave.Visibility = Visibility.Hidden;
            //    this.btnCancel.Visibility = Visibility.Hidden;
            //}
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //if (custVM.SelectedCustomer != null)
            //{
            //    if (MessageBox.Show("Do you want to delete customer?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        custVM.DeleteCustomer();
            //        MessageBox.Show("Customer is deleted");
            //        custVM.ResultList.Remove(custVM.SelectedCustomer);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please select a customer!");
            //}
        }
    }
}
