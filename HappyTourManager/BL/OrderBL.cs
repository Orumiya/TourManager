﻿// <copyright file="OrderBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.Interfaces;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// searchterms for Order entities
    /// </summary>
    public enum OrderTerms
    {
        ORDERDATE,
        ISLOYALTY,
        ISCANCELLED,
        ISPAYED,
        DEFAULT
    }

    public class OrderBL : IOrderList, ISearcheable<Order>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Tour> tourRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBL"/> class.
        /// creates the OrderBL
        /// </summary>
        /// <param name="orderRepository">input repository</param>
        /// <param name="customerRepository">input customer repository</param>
        /// <param name="tourRepository">input tour repository</param>
        public OrderBL(
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Tour> tourRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.tourRepository = tourRepository;
        }

        /// <inheritdoc />
        public event EventHandler OrderListChanged;

        /// <inheritdoc />
        public void Delete(Order order)
        {
            try
            {
                this.orderRepository.Delete(order);
            }
            finally
            {
                this.OnOrderListChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Order order)
        {
            try
            {
                this.orderRepository.Create(order);
            }
            finally
            {
                this.OnOrderListChanged();
            }
        }

        /// <inheritdoc />
        public IList<Order> Search(object searchterm, object searchvalue)
        {
            var orders = this.orderRepository.GetAll();

            // searching for orders which are between 2 dates
            // searchvalue must be a DateTime[]
            if ((OrderTerms)searchterm == OrderTerms.ORDERDATE)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                orders = orders.Where(
                    i => (i.OrderDate <= endInterval) && (startInterval <= i.OrderDate));
                return orders.ToList();
            }

            // searching for orders which are ordered with loyaltycard holders
            // searchvalue must be a string and accepted values are 1 or 0
            else if ((OrderTerms)searchterm == OrderTerms.ISLOYALTY)
            {
                string isloyalty = (string)searchvalue;
                if (isloyalty == "1")
                {
                    orders = orders.Where(e => e.IsLoyalty.Equals(isloyalty));
                }
                else
                {
                    orders = orders.Where(e => !e.IsLoyalty.Equals("1"));
                }

                return orders.ToList();
            }

            // searching for orders which are payed
            // searchvalue must be a string and accepted values are 1 or 0
            else if ((OrderTerms)searchterm == OrderTerms.ISPAYED)
            {
                string ispayed = (string)searchvalue;
                if (ispayed == "1")
                {
                    orders = orders.Where(e => e.IsPayed.Equals(ispayed));
                }
                else
                {
                    orders = orders.Where(e => !e.IsPayed.Equals("1"));
                }

                return orders.ToList();
            }

            // searching for orders which are cancelled
            // searchvalue must be a string and accepted values are 1 or 0
            else if ((OrderTerms)searchterm == OrderTerms.ISCANCELLED)
            {
                string iscancelled = (string)searchvalue;
                if (iscancelled == "1")
                {
                    orders = orders.Where(e => e.IsCancelled.Equals(iscancelled));
                }
                else
                {
                    orders = orders.Where(e => !e.IsCancelled.Equals("1"));
                }

                return orders.ToList();
            }

            // searches for all Orders
            else if ((OrderTerms)searchterm == OrderTerms.DEFAULT)
            {
                return orders.ToList();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
        }

        /// <inheritdoc />
        public void ThrowIfExists(Order order)
        {
            this.orderRepository.ThrowIfExists(order);
        }

        /// <summary>
        /// calculates the order sum before the loyaltyCard is taken into account
        /// </summary>
        /// <param name="adultCount">adult</param>
        /// <param name="adultPrice">price per adult of the tour</param>
        /// <param name="childCount">child</param>
        /// <param name="childPrice">price per child of the tour</param>
        /// <returns>totel sum of the order</returns>
        public int CalculateOrderPriceBeforeLoyaltyCounted(int adultCount, int adultPrice, int childCount, int childPrice)
        {
            if (adultCount < 0 || childCount < 0)
            {
                throw new InvalidOperationException("Can't be negative.");
            }
            else
            {
                return (adultCount * adultPrice) + (childCount * childPrice);
            }
        }

        /// <summary>
        /// calculates the order sum, when loyaltyCard is taken into account
        /// </summary>
        /// <param name="sumPrice">raw sumPrice</param>
        /// <param name="isLoyalty">true, if a customer has a loyaltyCard</param>
        /// <returns>total sum of the order</returns>
        public int CalculateOrderPriceWithLoyaltyCounted(int sumPrice, bool isLoyalty)
        {
            if (isLoyalty)
            {
                sumPrice = (int)(sumPrice * 0.95);
                return sumPrice;
            }
            else
            {
                return sumPrice;
            }
        }

        /// <summary>
        /// determines if a Customer has a LoyaltyCard or not
        /// </summary>
        /// <param name="cust">customer</param>
        /// <returns>returns true, if has a loyaltycard</returns>
        public bool DoesHaveACustomerLoyaltyCard(Customer cust)
        {
            if (cust.LoyaltyCard.Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime DetermineTheOrderDate()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// counts the booked seats to a selected tour
        /// </summary>
        /// <param name="tour">selected tour</param>
        /// <returns>ordered seats</returns>
        public int BookedTourActualPersonCount(Tour tour)
        {
            var orders = tour.Orders.AsQueryable();
            int actualPersoncount = 0;
            foreach (var item in orders)
            {
                actualPersoncount += (int)item.PersonCount;
            }

            return actualPersoncount;
        }

        /// <summary>
        /// Checks if there are enough seats for the new order
        /// </summary>
        /// <param name="tour">selected tour</param>
        /// <param name="newOrderPersonCount">people in the order</param>
        /// <returns>true, if there are enough seats</returns>
        public bool MaxAllowedSeatCheck(Tour tour, int newOrderPersonCount)
        {
            int actualPersonCount = this.BookedTourActualPersonCount(tour);
            if (actualPersonCount + newOrderPersonCount <= tour.MaxNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// decides, whether the min number of travellers is reached for a tour
        /// </summary>
        /// <param name="tour">selected tour</param>
        /// <returns>true, if min number reached</returns>
        public bool MinTourNumberReachedCheck(Tour tour)
        {
            int actualPersonCount = this.BookedTourActualPersonCount(tour);
            if (actualPersonCount >= tour.MinNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks whether a Customer's passport is valid till the end of
        /// the travel + another 6 months
        /// </summary>
        /// <param name="order">current order</param>
        /// <returns>true, if the passport/ID is valid</returns>
        public bool PassportValidityCheck(Order order)
        {
            if (order.Customer.Person.ValidTo >= order.Tour.EndDate + new TimeSpan(180, 0, 0, 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc />
        public void Update()
        {
            this.orderRepository.Update();
        }

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnOrderListChanged()
        {
            this.OrderListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
