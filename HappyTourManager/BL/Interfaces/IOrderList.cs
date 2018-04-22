// <copyright file="IOrderList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System;
    using DATA;

    public interface IOrderList
    {
        /// <summary>
        /// Is fired when a delete or save happens.
        /// </summary>
        event EventHandler OrderListChanged;

        /// <summary>
        /// Deletes  Order from the  OrderList list.
        /// </summary>
        /// <param name="order">input param</param>
        void Delete(Order order);

        /// <summary>
        /// Saves  Order regardless of it is already in the list
        /// </summary>
        /// <param name="order">input param</param>
        void Save(Order order);

        /// <summary>
        /// Checks whether a Order exists in the list.
        /// </summary>
        /// <param name="order">input param</param>
        void ThrowIfExists(Order order);
    }
}
