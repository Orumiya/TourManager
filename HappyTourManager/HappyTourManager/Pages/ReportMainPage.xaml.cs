namespace HappyTourManager.Pages
{
    using BL;
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

    /// <summary>
    /// Interaction logic for ReportMainPage.xaml
    /// </summary>
    public partial class ReportMainPage : Page
    {
        private IRepository<Report> reportRepository;
        private IRepository<Order> orderRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<Tourguide> tourguideRepository;
        private IRepository<Language> languageRepository;
        private IRepository<OnHoliday> onHolidayRepository;
        private IRepository<Program> programRepository;
        private IRepository<Place> placeRepository;
        private IRepository<PLTCON> pltconRepository;
        private IRepository<PRTCON> prtconRepository;

        private ReportMainViewModel reportVM;


        public ReportMainPage(
            IRepository<Report> reportRepository,
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Tour> tourRepository,
            IRepository<Tourguide> tourguideRepository,
            IRepository<Language> languageRepository,
            IRepository<OnHoliday> onHolidayRepository,
            IRepository<Program> programRepository,
            IRepository<Place> placeRepository,
            IRepository<PLTCON> pltconRepository,
            IRepository<PRTCON> prtconRepository
            )
        {
            this.reportRepository = reportRepository;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.tourguideRepository = tourguideRepository;
            this.tourRepository = tourRepository;
            this.languageRepository = languageRepository;
            this.onHolidayRepository = onHolidayRepository;
            this.programRepository = programRepository;
            this.placeRepository = placeRepository;
            this.pltconRepository = pltconRepository;
            this.prtconRepository = prtconRepository;

            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.reportVM = new ReportMainViewModel(reportRepository, orderRepository, customerRepository, tourRepository, 
                                                    tourguideRepository, languageRepository, onHolidayRepository, programRepository,
                                                    placeRepository, pltconRepository, prtconRepository);

            //this.searchCat.ItemsSource = this.reportVM.SearchCategories;
            //this.searchCat.Visibility = Visibility.Visible;
            //this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = this.reportVM;

           

            


        }

        //private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (this.reportVM.SelectedCtegory == "REPORTDATE")
        //    {
        //        DatePicker datePicker = new DatePicker();

        //        Binding binding = new Binding("SelectedDateFrom");
        //        datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);
        //        this.contSearch1.Content = datePicker;

        //        DatePicker datePicker2 = new DatePicker();
        //        Binding binding2 = new Binding("SelectedDateTo");
        //        datePicker2.SetBinding(DatePicker.SelectedDateProperty, binding2);
        //        this.contSearch2.Content = datePicker2;

        //    }
        //    else if (this.reportVM.SelectedCtegory == "DEFAULT")
        //    {
        //        this.contSearch1.Content = null;
        //        this.contSearch2.Content = null;
        //        this.reportVM.SelectedValue = default(string);
        //    }
        //    else
        //    {
        //        TextBox textbox = new TextBox();
        //        Binding binding = new Binding("SelectedValue");
        //        textbox.SetBinding(TextBox.TextProperty, binding);
        //        this.contSearch1.Content = textbox;
        //        this.contSearch2.Content = null;
        //    }
        //}

        //private void btnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        this.reportVM.GetSearchResult();
        //        if (this.reportVM.ResultList.Count > 0)
        //        {
        //            this.btnEdit.Visibility = Visibility.Visible;
        //            this.btnDelete.Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            this.btnEdit.Visibility = Visibility.Hidden;
        //            this.btnDelete.Visibility = Visibility.Hidden;
        //        }
        //        //this.contCustDetails.Visibility = Visibility.Hidden;
        //        this.btnSave.Visibility = Visibility.Hidden;
        //        this.btnCancel.Visibility = Visibility.Hidden;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    //try
        //    //{
        //    //    if (this.custVM.Checkvalues())
        //    //    {
        //    //        MessageBox.Show("All values must be filled in!");
        //    //    }
        //    //    else
        //    //    {
        //    //        this.custVM.SaveInstance();
        //    //        MessageBox.Show("Customer data is saved!");
        //    //        this.contCustDetails.Visibility = Visibility.Hidden;
        //    //        this.custVM.SelectedCustomer = null;
        //    //        this.btnSave.Visibility = Visibility.Hidden;
        //    //        this.btnCancel.Visibility = Visibility.Hidden;
        //    //    }
        //    //}
        //    //catch (InvalidOperationException ex)
        //    //{
        //    //    MessageBox.Show(ex.Message);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show("Something went wrong :(");
        //    //}

        //}

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.contCustDetails.Visibility = Visibility.Hidden;
        //    //this.custVM.SelectedCustomer = null;
        //    //this.btnSave.Visibility = Visibility.Hidden;
        //    //this.btnCancel.Visibility = Visibility.Hidden;
        //}

        //private void btnEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (this.custVM.ResultList.Count > 0 && this.custVM.SelectedCustomer != null)
        //    //{
        //    //    this.contCustDetails.Visibility = Visibility.Visible;
        //    //    this.btnSave.Visibility = Visibility.Visible;
        //    //    this.btnCancel.Visibility = Visibility.Visible;
        //    //}
        //    //else
        //    //{
        //    //    this.contCustDetails.Visibility = Visibility.Hidden;
        //    //    this.btnSave.Visibility = Visibility.Hidden;
        //    //    this.btnCancel.Visibility = Visibility.Hidden;
        //    //    MessageBox.Show("Please select a customer!");
        //    //}
        //}

        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (this.custVM.SelectedCustomer != null)
        //    //{
        //    //    if (MessageBox.Show("Do you want to delete customer?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    //    {
        //    //        this.custVM.DeleteInstance();
        //    //        MessageBox.Show("Customer is deleted");
        //    //        this.custVM.ResultList.Remove(this.custVM.SelectedCustomer);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Please select a customer!");
        //    //}
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (reportVM.SelectedType != null)
            {
                try
                {
                    this.reportVM.GenerateReport();
                }
                catch (Exception)
                {

                    MessageBox.Show("Report is not implemented!");
                    this.contReportDetails.Content = null;
                }
                
                if (reportVM.SelectedType == "CUSTOMERREPORT")
                {

                    this.contReportDetails.Content = new PieChartUC(reportVM.Point1, reportVM.Point2);
                }
                else if (reportVM.SelectedType == "ORDERREPORT")
                {
                    this.contReportDetails.Content = new OrderReportUC(reportVM.Point1, reportVM.Point2, reportVM.Point3);
                }
            }
            else
            {
                MessageBox.Show("Please select reporttype!");
            }
            
            
            
        }
    }
}
