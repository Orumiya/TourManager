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
        PERSONCOUNT,
        TOTALSUM,
        ISLOYALTY,
        ISCANCELLED,
        ISPAYED
    }

    public class OrderBL : IOrderList //, ISearcheable<Order>
    {
        private readonly IRepository<Order> orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBL"/> class.
        /// creates the OrderBL
        /// </summary>
        /// <param name="orderRepository">input repository</param>
        public OrderBL(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
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

        ///// <inheritdoc />
        //public IList<Order> Search(object searchterm, object searchvalue)
        //{
        //    var orders = this.orderRepository.GetAll();
        //    Order order = new Order
        //    {
                
        //    }
        //    //if ((OrderTerms)searchterm == OrderTerms.LastName)
        //    //{

        //    //}
        //    throw new NotImplementedException();

        //}

        /// <inheritdoc />
        public void ThrowIfExists(Order order)
        {
            this.orderRepository.ThrowIfExists(order);
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
