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
            this.contOrderDetails.Content = orderDetail;
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
            try
            {
                orderVM.GetSearchResult();
                if (orderVM.ResultList.Count > 0)
                {
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    btnEdit.Visibility = Visibility.Hidden;
                    btnDelete.Visibility = Visibility.Hidden;
                }
                this.contOrderDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            orderVM.SelectedOrder = new Order()
            {
                OrderDate = DateTime.Today
            };
            orderVM.TotalPrice = 0;
            orderVM.AdultCountNew = 0;
            orderVM.SelectedCustomer = null;
            orderVM.SelectedTour = null;
            this.contOrderDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (orderVM.Checkvalues())
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    orderVM.SaveInstance();
                    MessageBox.Show("Data is saved!");
                    this.contOrderDetails.Visibility = Visibility.Hidden;
                    orderVM.SelectedOrder = null;
                    orderVM.SelectedCustomer = null;
                    orderVM.SelectedTour = null;
                    orderVM.AdultCountNew = 0;
                    orderVM.TotalPrice = 0;
                    this.btnSave.Visibility = Visibility.Hidden;
                    this.btnCancel.Visibility = Visibility.Hidden;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong :(");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contOrderDetails.Visibility = Visibility.Hidden;
            orderVM.SelectedOrder = null;
            orderVM.SelectedCustomer = null;
            orderVM.SelectedTour = null;
            orderVM.TotalPrice = 0;
            orderVM.AdultCountNew = 0;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (orderVM.ResultList.Count > 0 && orderVM.SelectedOrder != null)
            {
                orderVM.SelectedCustomer = orderVM.SelectedOrder.Customer;
                orderVM.SelectedTour = orderVM.SelectedOrder.Tour;
                orderVM.TotalPrice = orderVM.SelectedOrder.TotalSum;
                orderVM.AdultCountNew = (int)orderVM.SelectedOrder.PersonCount;
                this.contOrderDetails.Visibility = Visibility.Visible;
                this.btnSave.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.contOrderDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
                orderVM.SelectedCustomer = null;
                orderVM.SelectedTour = null;
                orderVM.TotalPrice = 0;
                orderVM.AdultCountNew = 0;
                MessageBox.Show("Please select an order!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (orderVM.SelectedOrder != null)
            {
                if (MessageBox.Show("Do you want to delete order?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        orderVM.DeleteInstance();
                        MessageBox.Show("Order is deleted");
                        orderVM.ResultList.Remove(orderVM.SelectedOrder);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Order cannot be deleted!");
                    }
                    finally
                    {
                        orderVM.SelectedCustomer = null;
                        orderVM.SelectedTour = null;
                        orderVM.TotalPrice = 0;
                        orderVM.AdultCountNew = 0;
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a tour!");
            }
        }
        #endregion

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderVM.SelectedOrder != null)
            {
                orderVM.SelectedCustomer = orderVM.SelectedOrder.Customer;
                orderVM.SelectedTour = orderVM.SelectedOrder.Tour;
                orderVM.TotalPrice = orderVM.SelectedOrder.TotalSum;
                orderVM.AdultCountNew = (int)orderVM.SelectedOrder.PersonCount;

                
            }
        }
    }
}
