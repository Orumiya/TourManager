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
    /// Interaction logic for OrderMainPage.xaml
    /// </summary>
    public partial class OrderMainPage : Page
    {
        OrderMainViewModel vm;
        private IRepository<Order> orderRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<Program> programRepository;
        private IRepository<Place> placeRepository;
        private IRepository<PLTCON> pltconRepository;
        private IRepository<PRTCON> prtconRepository;

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
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new OrderMainViewModel(orderRepository,customerRepository, tourRepository, programRepository,placeRepository,pltconRepository, prtconRepository);
            this.DataContext = vm;
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTGuide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOffice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newOrder_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            if (orderDate.SelectedDate != null)
            {
                date = (DateTime)orderDate.SelectedDate;
            }
            else
            {
                vm.orderBL.DetermineTheOrderDate();
            }

            
            
        }
    }
}
