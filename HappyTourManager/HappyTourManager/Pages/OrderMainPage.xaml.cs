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

    /// <summary>
    /// Interaction logic for OrderMainPage.xaml
    /// </summary>
    public partial class OrderMainPage : Page
    {
        private IRepository<Order> orderRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<Program> programRepository;
        private IRepository<Place> placeRepository;
        private IRepository<PLTCON> pltconRepository;
        private IRepository<PRTCON> prtconRepository;

        private OrderMainViewModel orderVM;
        private OrderDetailsUC orderDetail;

        IList<Tour> tourList = new List<Tour>();
        IList<Customer> customerList = new List<Customer>();

        public OrderMainPage(IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Tour> tourRepository,
            IRepository<Program> programRepository,
            IRepository<Place> placeRepository,
            IRepository<PLTCON> pltconRepository,
            IRepository<PRTCON> prtconRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.tourRepository = tourRepository;
            this.programRepository = programRepository;
            this.placeRepository = placeRepository;
            this.pltconRepository = pltconRepository;
            this.prtconRepository = prtconRepository;

            this.InitializeComponent();
        }

        #region event handlers
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.orderVM = new OrderMainViewModel(this.orderRepository, this.customerRepository, this.tourRepository, this.programRepository, this.placeRepository, this.pltconRepository, this.prtconRepository);

            this.searchCat.ItemsSource = this.orderVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = this.orderVM;
            this.orderDetail = new OrderDetailsUC();

            this.contOrderDetails.Visibility = Visibility.Hidden;

        }

        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.orderVM.SelectedCtegory == "ORDERDATE")
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
            else if (this.orderVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                this.orderVM.SelectedValue = default(string);
            }
            else if (this.orderVM.SelectedCtegory == "ISLOYALTY" || this.orderVM.SelectedCtegory == "ISCANCELLED" || this.orderVM.SelectedCtegory == "ISPAYED")
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
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // try
            // {
            //    tourVM.GetSearchResult();
            //    if (tourVM.ResultList.Count > 0)
            //    {
            //        btnEdit.Visibility = Visibility.Visible;
            //        btnDelete.Visibility = Visibility.Visible;
            //    }
            //    else
            //    {
            //        btnEdit.Visibility = Visibility.Hidden;
            //        btnDelete.Visibility = Visibility.Hidden;
            //    }
            //    this.contTourDetails.Visibility = Visibility.Hidden;
            //    this.btnSave.Visibility = Visibility.Hidden;
            //    this.btnCancel.Visibility = Visibility.Hidden;
            // }
            // catch (Exception ex)
            // {
            //    MessageBox.Show(ex.Message);
            // }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // tourVM.IsEdit = true;
            // tourVM.SelectedTour = new Tour()
            // {
            //    StartDate = DateTime.Today,
            //    EndDate = DateTime.Today
            // };
            // tourVM.SelectedPlace = new Place();
            // tourVM.SelectedProgram = new Program();
            // tourVM.GetTourPlaces();
            // tourVM.GetTourPrograms();
            // this.contTourDetails.Visibility = Visibility.Visible;
            // this.btnSave.Visibility = Visibility.Visible;
            // this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // try
            // {
            //    if (tourVM.Checkvalues(tourDetail.tcTour.SelectedIndex))
            //    {
            //        MessageBox.Show("All values must be filled in!");
            //    }
            //    else
            //    {
            //        tourVM.SaveTour(tourDetail.tcTour.SelectedIndex);
            //        MessageBox.Show("Data is saved!");
            //        this.contTourDetails.Visibility = Visibility.Hidden;
            //        tourVM.SelectedTour = null;
            //        tourVM.SelectedPlace = null;
            //        tourVM.SelectedProgram = null;
            //        this.btnSave.Visibility = Visibility.Hidden;
            //        this.btnCancel.Visibility = Visibility.Hidden;
            //    }
            // }
            // catch (InvalidOperationException ex)
            // {
            //    MessageBox.Show(ex.Message);
            // }
            // catch (Exception)
            // {
            //    MessageBox.Show("Something went wrong :(");
            // }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // this.contTourDetails.Visibility = Visibility.Hidden;
            // tourVM.SelectedTour = null;
            // tourVM.SelectedPlace = null;
            // tourVM.SelectedProgram = null;
            // this.btnSave.Visibility = Visibility.Hidden;
            // this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // tourVM.IsEdit = false;
            // tourVM.SelectedPlace = null;
            // tourVM.SelectedProgram = null;
            // if (tourVM.ResultList.Count > 0 && tourVM.SelectedTour != null)
            // {
            //    tourVM.GetTourPlaces();
            //    tourVM.GetTourPrograms();
            //    this.contTourDetails.Visibility = Visibility.Visible;
            //    this.btnSave.Visibility = Visibility.Visible;
            //    this.btnCancel.Visibility = Visibility.Visible;
            // }
            // else
            // {
            //    this.contTourDetails.Visibility = Visibility.Hidden;
            //    this.btnSave.Visibility = Visibility.Hidden;
            //    this.btnCancel.Visibility = Visibility.Hidden;
            //    MessageBox.Show("Please select a tour!");
            // }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // if (tourVM.SelectedTour != null)
            // {
            //    if (MessageBox.Show("Do you want to delete tour?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        try
            //        {
            //            tourVM.DeleteTour();
            //            MessageBox.Show("Customer is deleted");
            //            tourVM.ResultList.Remove(tourVM.SelectedTour);
            //        }
            //        catch (Exception)
            //        {

            // MessageBox.Show("Tour cannot be deleted!");
            //        }

            // }
            // }
            // else
            // {
            //    MessageBox.Show("Please select a tour!");
            // }
        }
        #endregion

    }
}
