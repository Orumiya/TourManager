// <copyright file="OrderMainPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderMainPage"/> class.
        /// Order page
        /// </summary>
        /// <param name="orderRepository">orders</param>
        /// <param name="customerRepository">customers</param>
        /// <param name="tourRepository">tour</param>
        /// <param name="programRepository">program</param>
        /// <param name="placeRepository">place</param>
        /// <param name="pltconRepository">plt</param>
        /// <param name="prtconRepository">prt</param>
        public OrderMainPage(
            IRepository<Order> orderRepository,
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.orderVM = new OrderMainViewModel(this.orderRepository, this.customerRepository, this.tourRepository, this.programRepository, this.placeRepository, this.pltconRepository, this.prtconRepository);

            this.searchCat.ItemsSource = this.orderVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = this.orderVM;
            this.orderDetail = new OrderDetailsUC();
            this.contOrderDetails.Content = this.orderDetail;
            this.contOrderDetails.Visibility = Visibility.Hidden;
        }

        private void SearchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.orderVM.GetSearchResult();
                if (this.orderVM.ResultList.Count > 0)
                {
                    this.btnEdit.Visibility = Visibility.Visible;
                    this.btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    this.btnEdit.Visibility = Visibility.Hidden;
                    this.btnDelete.Visibility = Visibility.Hidden;
                }

                this.contOrderDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
        }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Missing data");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Wrong data type");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.orderVM.SelectedOrder = new Order()
            {
                OrderDate = DateTime.Today
            };
            this.orderVM.TotalPrice = 0;
            this.orderVM.AdultCountNew = 0;
            this.orderVM.SelectedCustomer = null;
            this.orderVM.SelectedTour = null;
            this.contOrderDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.orderVM.Checkvalues())
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    this.orderVM.SaveInstance();
                    MessageBox.Show("Data is saved!");
                    this.contOrderDetails.Visibility = Visibility.Hidden;
                    this.orderVM.SelectedOrder = null;
                    this.orderVM.SelectedCustomer = null;
                    this.orderVM.SelectedTour = null;
                    this.orderVM.AdultCountNew = 0;
                    this.orderVM.TotalPrice = 0;
                    this.btnSave.Visibility = Visibility.Hidden;
                    this.btnCancel.Visibility = Visibility.Hidden;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Missing data");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Wrong data type");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contOrderDetails.Visibility = Visibility.Hidden;
            this.orderVM.SelectedOrder = null;
            this.orderVM.SelectedCustomer = null;
            this.orderVM.SelectedTour = null;
            this.orderVM.TotalPrice = 0;
            this.orderVM.AdultCountNew = 0;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderVM.ResultList.Count > 0 && this.orderVM.SelectedOrder != null)
            {
                this.orderVM.SelectedCustomer = this.orderVM.SelectedOrder.Customer;
                this.orderVM.SelectedTour = this.orderVM.SelectedOrder.Tour;
                this.orderVM.TotalPrice = this.orderVM.SelectedOrder.TotalSum;
                this.orderVM.AdultCountNew = (int)this.orderVM.SelectedOrder.PersonCount;
                this.contOrderDetails.Visibility = Visibility.Visible;
                this.btnSave.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.contOrderDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
                this.orderVM.SelectedCustomer = null;
                this.orderVM.SelectedTour = null;
                this.orderVM.TotalPrice = 0;
                this.orderVM.AdultCountNew = 0;
                MessageBox.Show("Please select an order!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderVM.SelectedOrder != null)
            {
                if (MessageBox.Show("Do you want to delete order?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        this.orderVM.DeleteInstance();
                        MessageBox.Show("Order is deleted");
                        this.orderVM.ResultList.Remove(this.orderVM.SelectedOrder);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Missing data");
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Wrong data type");
                    }
                    finally
                    {
                        this.orderVM.SelectedCustomer = null;
                        this.orderVM.SelectedTour = null;
                        this.orderVM.TotalPrice = 0;
                        this.orderVM.AdultCountNew = 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a tour!");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.orderVM.SelectedOrder != null)
            {
                this.orderVM.SelectedCustomer = this.orderVM.SelectedOrder.Customer;
                this.orderVM.SelectedTour = this.orderVM.SelectedOrder.Tour;
                this.orderVM.TotalPrice = this.orderVM.SelectedOrder.TotalSum;
                this.orderVM.AdultCountNew = (int)this.orderVM.SelectedOrder.PersonCount;
            }
        }
    }
}
