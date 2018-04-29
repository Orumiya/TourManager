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

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value;
                OnPropertyChanged(nameof(this.SelectedOrder));
            }
        }

        private Order newOrder;

        public Order NewOrder
        {
            get { return newOrder; }
            set { newOrder = value;
                OnPropertyChanged(nameof(this.NewOrder));
            }
        }

        private int totalSum;

        //public int TotalSum
        //{
        //    get { return totalSum; }
        //    set { totalSum = orderBL.CalculateOrderPriceBeforeLoyaltyCounted()
        //    OnPropertyChanged(nameof(this.NewOrder));
        //    }
        //}

        private int adultCountNew;

        public int AdultCountNew
        {
            get { return adultCountNew; }
            set { adultCountNew = value;
                OnPropertyChanged(nameof(this.AdultCountNew));
            }
        }

        private int childCountNew;

        public int ChildCountNew
        {
            get { return childCountNew; }
            set { childCountNew = value;
                OnPropertyChanged(nameof(this.ChildCountNew));
            }
        }

        private Tour selectedTour;

        public Tour SelectedTour
        {
            get { return selectedTour; }
            set { selectedTour = value;
                OnPropertyChanged(nameof(this.SelectedTour));
            }
        }

        private Customer selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value; }
        }




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
        }

        public void RefreshOrderList()
        {
            OrderList = orderBL.Search(OrderTerms.DEFAULT, null);
            OnPropertyChanged(nameof(OrderList));
            
        }

        public IList<Order> OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }

        private IList<Customer> customerList;

        public IList<Customer> CustomerList
        {
            get { return customerList; }
            set { customerList = GetAllCustomers();
                OnPropertyChanged(nameof(CustomerList));
            }
        }

        private IList<Tour> tourList;

        public IList<Tour> TourList
        {
            get { return tourList; }
            set { tourList = GetAllTours();
                OnPropertyChanged(nameof(TourList));
            }
        }

        //private Order CreateNewOrder()
        //{

        //}

        private IList<Tour> GetAllTours()
        {
            tourBL = new TourBL(tourRepository,programRepository,placeRepository,pltconRepository,prtconRepository);
            IList<Tour> tglist = tourBL.Search(TourTerms.DEFAULT, null);

            return tglist;
        }

        private IList<Customer> GetAllCustomers()
        {
            customerBL = new CustomerBL(customerRepository);
            IList<Customer> custList = customerBL.Search(CustomerTerms.DEFAULT, null);
            return custList;
        }


    }
}
