// <copyright file="ICustomerList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL.Interfaces
{
    using System;
    using DATA;

    public interface ICustomerList
    {
        /// <summary>
        /// Is fired when a delete or save happens.
        /// </summary>
        event EventHandler CustomerListChanged;

        /// <summary>
        /// Deletes Customer from the CustomerList list.
        /// </summary>
        /// <param name="customer">input param</param>
        void Delete(Customer customer);

        /// <summary>
        /// Saves customer regardless of it is already in the list
        /// </summary>
        /// <param name="customer">input param</param>
        void Save(Customer customer);

        /// <summary>
        /// Checks whether a Customer exists in the list.
        /// </summary>
        /// <param name="customer">input param</param>
        void ThrowIfExists(Customer customer);
    }
}
