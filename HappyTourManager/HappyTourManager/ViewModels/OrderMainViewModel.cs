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
        #region private variables

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
        private ObservableCollection<Order> resultList;
        private decimal totalPrice;
        #endregion

        #region parameters
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

        public IList<Order> OrderList
        {
            get { return this.orderList; }
            set { this.orderList = value; }
        }

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

        public decimal TotalPrice
        {
            get
            {
                return totalPrice;
            }

            set
            {
                totalPrice = CalculateTotalPrice();
                this.OnPropertyChanged(nameof(this.TotalPrice));
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
        #endregion

        #region public methods

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
                    
                    
                }
                rL = orderBL.Search(Enum.Parse(typeof(OrderTerms), this.SelectedCtegory), "0");
            }
            else
            {
                rL = orderBL.Search(Enum.Parse(typeof(OrderTerms), this.SelectedCtegory), this.SelectedValue);
            }
            ResultList = new ObservableCollection<Order>(rL);
        }

        public void GetAllTours()
        {
            var tglist = this.tourBL.GetAllTours();

            this.TourList = tglist;
        }

        public void GetAllCustomers()
        {
            IList<Customer> custList = this.customerBL.GetAllCustomers();

            this.CustomerList = custList;
        }

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

        public void DeleteInstance()
        {
            if (this.SelectedOrder != null)
            {
                this.orderBL.Delete(this.SelectedOrder);
            }
        }

        #endregion

        #region private methods
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
        #endregion
    }
}
