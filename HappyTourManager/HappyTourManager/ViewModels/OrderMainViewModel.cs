// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using BL;
    using DATA;
    using DATA.Interfaces;
    using System.Collections.ObjectModel;

    public class OrderMainViewModel : Bindable, IContentPage
    {

        private IList<Order> orderList;
        private OrderBL orderBL;
        private TourBL tourBL;
        private CustomerBL customerBL;
        private string selectedCtegory = "DEFAULT";
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private string selectedValue;
        private Order selectedOrder;
        private int adultCountNew;
        private int childCountNew;
        private Tour selectedTour;
        private Customer selectedCustomer;
        private IList<Customer> customerList;
        private IList<Tour> tourList;
        private List<string> searchCategories;
        private ObservableCollection<Order> resultList;
        private decimal totalPrice;

        /// <summary>
        /// List for search results
        /// </summary>
        public ObservableCollection<Order> ResultList
        {
            get
            {
                return this.resultList;
            }

            set
            {
                this.resultList = value;
                this.OnPropertyChanged(nameof(this.ResultList));
            }
        }

        /// <summary>
        /// contains selected order
        /// </summary>
        public Order SelectedOrder
        {
            get
            {
                return this.selectedOrder;
            }

            set
            {
                this.selectedOrder = value;
                this.OnPropertyChanged(nameof(this.SelectedOrder));
            }
        }

        /// <summary>
        /// Contains selected adult peron count
        /// </summary>
        public int AdultCountNew
        {
            get
            {
                return this.adultCountNew;
            }

            set
            {
                this.adultCountNew = value;
                this.TotalPrice = CalculateTotalPrice();
                this.OnPropertyChanged(nameof(this.AdultCountNew));
            }
        }

        /// <summary>
        /// Contains selected adult person count
        /// </summary>
        public int ChildCountNew
        {
            get
            {
                return this.childCountNew;
            }

            set
            {
                this.childCountNew = value;
                this.TotalPrice = CalculateTotalPrice();
                this.OnPropertyChanged(nameof(this.ChildCountNew));
            }
        }

        /// <summary>
        /// contains selected tour
        /// </summary>
        public Tour SelectedTour
        {
            get
            {
                return this.selectedTour;
            }

            set
            {
                this.selectedTour = value;
                this.OnPropertyChanged(nameof(this.SelectedTour));
            }
        }

        /// <summary>
        /// contains selected customer
        /// </summary>
        public Customer SelectedCustomer
        {
            get
            {
                return this.selectedCustomer;
            }

            set
            {
                this.selectedCustomer = value;
                this.OnPropertyChanged(nameof(this.SelectedCustomer));
            }
        }

        /// <summary>
        /// contains selected search category
        /// </summary>
        public string SelectedCtegory
        {
            get
            {
                return this.selectedCtegory;
            }

            set
            {
                this.selectedCtegory = value;
                this.OnPropertyChanged(nameof(this.SelectedCtegory));
            }
        }

        /// <summary>
        /// contains orderlist
        /// </summary>
        public IList<Order> OrderList
        {
            get { return this.orderList; }
            set { this.orderList = value; }
        }

        /// <summary>
        /// contains customerlist
        /// </summary>
        public IList<Customer> CustomerList
        {
            get
            {
                return this.customerList;
            }

            set
            {
                this.customerList = value;
                this.OnPropertyChanged(nameof(this.CustomerList));
            }
        }

        /// <summary>
        /// contains tourlist
        /// </summary>
        public IList<Tour> TourList
        {
            get
            {
                return this.tourList;
            }

            set
            {
                this.tourList = value;
                this.OnPropertyChanged(nameof(this.TourList));
            }
        }

        /// <summary>
        /// contains date format search value
        /// </summary>
        public DateTime SelectedDateFrom
        {
            get
            {
                return this.selectedDateFrom;
            }

            set
            {
                this.selectedDateFrom = value;
                this.OnPropertyChanged(nameof(this.SelectedDateFrom));
            }
        }

        /// <summary>
        /// contains date format search value
        /// </summary>
        public DateTime SelectedDateTo
        {
            get
            {
                return this.selectedDateTo;
            }

            set
            {
                this.selectedDateTo = value;
                this.OnPropertyChanged(nameof(this.SelectedDateTo));
            }
        }

        /// <summary>
        /// contains all selectable search categories
        /// </summary>
        public List<string> SearchCategories
        {
            get
            {
                return this.searchCategories;
            }

            set
            {
                this.searchCategories = value;
                this.OnPropertyChanged(nameof(this.SearchCategories));
            }
        }

        /// <summary>
        /// contains string format search value
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return this.selectedValue;
            }

            set
            {
                this.selectedValue = value;
                this.OnPropertyChanged(nameof(this.SelectedValue));
            }
        }

        /// <summary>
        /// containscalculated total price
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return totalPrice;
            }

            set
            {
                totalPrice = value;
                totalPrice = CalculateTotalPrice();
                this.OnPropertyChanged(nameof(this.TotalPrice));
            }
        }

        /// <summary>
        /// Constructor for order iew model
        /// </summary>
        /// <param name="orderRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="tourRepository"></param>
        /// <param name="programRepository"></param>
        /// <param name="placeRepository"></param>
        /// <param name="pltconRepository"></param>
        /// <param name="prtconRepository"></param>
        public OrderMainViewModel(IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Tour> tourRepository,
            IRepository<Program> programRepository,
            IRepository<Place> placeRepository,
            IRepository<PLTCON> pltconRepository,
            IRepository<PRTCON> prtconRepository)
        {
            this.orderBL = new OrderBL(orderRepository, customerRepository, tourRepository);
            this.tourBL = new TourBL(tourRepository, programRepository, placeRepository, pltconRepository, prtconRepository);
            this.customerBL = new CustomerBL(customerRepository);
            this.searchCategories = new List<string>();
            foreach (OrderTerms item in Enum.GetValues(typeof(OrderTerms)))
            {
                this.searchCategories.Add(item.ToString());
            }
            GetAllCustomers();
            GetAllTours();
        }


        /// <summary>
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            IList<Order> rL;
            if (this.SelectedCtegory == "ORDERDATE")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = this.SelectedDateFrom;
                dt[1] = this.SelectedDateTo;

                rL = orderBL.Search(Enum.Parse(typeof(OrderTerms), this.SelectedCtegory), dt);
            }
            else if (this.SelectedCtegory == "ISLOYALTY" || this.SelectedCtegory == "ISCANCELLED" || this.SelectedCtegory == "ISPAYED")
            {
                if (this.SelectedValue == "yes")
                {
                    rL = orderBL.Search(Enum.Parse(typeof(OrderTerms), this.SelectedCtegory), "1");
                }
                else
                {
                    rL = orderBL.Search(Enum.Parse(typeof(OrderTerms), this.SelectedCtegory), "0");

                }
                
            }
            else
            {
                rL = orderBL.Search(Enum.Parse(typeof(OrderTerms), this.SelectedCtegory), this.SelectedValue);
            }
            ResultList = new ObservableCollection<Order>(rL);
        }

        /// <summary>
        /// Get all tours
        /// </summary>
        public void GetAllTours()
        {
            var tglist = this.tourBL.GetAllTours();

            this.TourList = tglist;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetAllCustomers()
        {
            IList<Customer> custList = this.customerBL.GetAllCustomers();

            this.CustomerList = custList;
        }

        /// <summary>
        /// Check if form is filled in correctly
        /// </summary>
        /// <returns></returns>
        public bool Checkvalues()
        {
            if (this.SelectedCustomer == null) return true;
            if (this.SelectedTour == null) return true;
            if (this.SelectedOrder != null)
            {
                this.SelectedOrder.CustomerID = this.SelectedCustomer.PersonID;
                this.SelectedOrder.TourID = this.SelectedTour.TourID;
                this.SelectedOrder.PersonCount = this.AdultCountNew;
                this.selectedOrder.TotalSum = this.TotalPrice;

                if (this.SelectedOrder.PersonCount == 0) return true;
                if (this.SelectedOrder.TotalSum == 0) return true;
            }



            return false;
        }

        /// <summary>
        /// Save item or update, if exists
        /// </summary>
        public void SaveInstance()
        {
            if (this.ResultList != null && this.ResultList.Contains(this.SelectedOrder))
            {
                this.orderBL.Update();
            }
            else
            {
                this.orderBL.Save(this.SelectedOrder);
            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        public void DeleteInstance()
        {
            if (this.SelectedOrder != null)
            {
                this.orderBL.Delete(this.SelectedOrder);
            }
        }

        /// <summary>
        /// Calculate total price
        /// </summary>
        /// <returns></returns>
        private decimal CalculateTotalPrice()
        {
            if (this.SelectedTour != null && this.SelectedCustomer != null && this.SelectedOrder != null)
            {
                int sum = this.orderBL.CalculateOrderPriceBeforeLoyaltyCounted(this.AdultCountNew, Decimal.ToInt32(this.SelectedTour.AdultPrice),
                                                        0, Decimal.ToInt32(this.SelectedTour.ChildPrice));
                if (SelectedOrder.IsLoyalty == "1")
                {
                    return (decimal)this.orderBL.CalculateOrderPriceWithLoyaltyCounted(sum, true);
                }
                return (decimal)sum;
                
            }
            return 0;

        }
    }
}
