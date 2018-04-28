// <copyright file="OrderBL.cs" company="PlaceholderCompany">
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
        TOTALSUM,
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
                orders = orders.Where(e => e.IsLoyalty.Equals(isloyalty));
                return orders.ToList();
            }

            // searching for orders which are payed
            // searchvalue must be a string and accepted values are 1 or 0
            else if ((OrderTerms)searchterm == OrderTerms.ISPAYED)
            {
                string ispayed = (string)searchvalue;
                orders = orders.Where(e => e.IsPayed.Equals(ispayed));
                return orders.ToList();
            }

            // searching for orders which are cancelled
            // searchvalue must be a string and accepted values are 1 or 0
            else if ((OrderTerms)searchterm == OrderTerms.ISCANCELLED)
            {
                string iscancelled = (string)searchvalue;
                orders = orders.Where(e => e.IsCancelled.Equals(iscancelled));
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
