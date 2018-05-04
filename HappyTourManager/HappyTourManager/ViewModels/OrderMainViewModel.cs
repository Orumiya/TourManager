// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using BL;
    using DATA;
    using DATA.Interfaces;
    using System;
    using System.Collections.Generic;

    public class OrderMainViewModel : Bindable
    {
        #region private variables
        private IRepository<Order> orderRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Tour> tourRepository;
        private IRepository<Program> programRepository;
        private IRepository<Place> placeRepository;
        private IRepository<PLTCON> pltconRepository;
        private IRepository<PRTCON> prtconRepository;

        private IList<Order> orderList;
        public OrderBL orderBL;
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
        #endregion

        #region parameters
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value;
                OnPropertyChanged(nameof(this.SelectedOrder));
            }
        }        

        public int AdultCountNew
        {
            get { return adultCountNew; }
            set { adultCountNew = value;
                OnPropertyChanged(nameof(this.AdultCountNew));
            }
        }

        public int ChildCountNew
        {
            get { return childCountNew; }
            set { childCountNew = value;
                OnPropertyChanged(nameof(this.ChildCountNew));
            }
        }
       

        public Tour SelectedTour
        {
            get { return selectedTour; }
            set { selectedTour = value;
                OnPropertyChanged(nameof(this.SelectedTour));
            }
        }
               

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value;
                OnPropertyChanged(nameof(this.SelectedCustomer));
            }
        }

        public string SelectedCtegory
        {
            get
            {
                return selectedCtegory;
            }

            set
            {
                selectedCtegory = value;
                OnPropertyChanged(nameof(SelectedCtegory));
            }
        }

        public IList<Order> OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }

        public IList<Customer> CustomerList
        {
            get { return customerList; }
            set
            {
                customerList = value;
                OnPropertyChanged(nameof(CustomerList));
            }
        }

        public IList<Tour> TourList
        {
            get { return tourList; }
            set
            {
                tourList = value;
                OnPropertyChanged(nameof(TourList));
            }
        }

        public DateTime SelectedDateFrom
        {
            get
            {
                return selectedDateFrom;
            }

            set
            {
                selectedDateFrom = value;
                OnPropertyChanged(nameof(SelectedDateFrom));
            }
        }

        public DateTime SelectedDateTo
        {
            get
            {
                return selectedDateTo;
            }

            set
            {
                selectedDateTo = value;
                OnPropertyChanged(nameof(SelectedDateTo));
            }
        }

        public List<string> SearchCategories
        {
            get
            {
                return searchCategories;
            }

            set
            {
                searchCategories = value;
                OnPropertyChanged(nameof(SearchCategories));
            }
        }

        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }

            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }
        #endregion

        #region constructor
        public OrderMainViewModel(IRepository<Order> orderRepository, 
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
            this.orderBL = new OrderBL(orderRepository, customerRepository, tourRepository);
            this.RefreshOrderList();

            searchCategories = new List<string>();
            foreach (OrderTerms item in Enum.GetValues(typeof(OrderTerms)))
            {
                searchCategories.Add(item.ToString());
            }
        }
        #endregion


        #region public methods



        public void RefreshOrderList()
        {
            OrderList = orderBL.Search(OrderTerms.DEFAULT, null);
            OnPropertyChanged(nameof(OrderList));
            
        }

        

        

        



        //private Order CreateNewOrder()
        //{

        //}

        public IList<Tour> GetAllTours()
        {
            tourBL = new TourBL(tourRepository,programRepository,placeRepository,pltconRepository,prtconRepository);
            IList<Tour> tglist = tourBL.Search(TourTerms.DEFAULT, null);

            return tglist;
        }

        public IList<Customer> GetAllCustomers()
        {
            customerBL = new CustomerBL(customerRepository);
            IList<Customer> custList = customerBL.Search(CustomerTerms.DEFAULT, null);
            
            return custList;
        }

        #endregion

        #region private methods

        #endregion
    }
}
