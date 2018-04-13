﻿// <copyright file="CustomerBL.cs" company="PlaceholderCompany">
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
    using DATA.Repositoriees;

    /// <summary>
    /// searchterms for Customer entities
    /// </summary>
    public enum CustomerTerms
    {
        LoyaltyCard,
        LastName,
        AddressCity,
        IDNumber,
        Default,
        ValidTo
    }

    public class CustomerBL : ISearcheable<Customer>, ICustomerList
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerBL(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        /// <inheritdoc />
        public event EventHandler CustomerListChanged;

        /// <inheritdoc />
        public void Delete(Customer customer)
        {
            this.customerRepository.Delete(customer);
        }

        /// <inheritdoc />
        public void Save(Customer customer)
        {
            this.customerRepository.Create(customer);
        }

        /// <summary>
        /// implementing the searches in the CustomerList
        /// </summary>
        /// <param name="searchterm">searching param</param>
        /// <param name="searchvalue">searching value</param>
        /// <returns>returns a List of Customers</returns>
        public IList<Customer> Search(object searchterm, object searchvalue)
        {
            var customerList = this.customerRepository.GetAll();

            if ((CustomerTerms)searchterm == CustomerTerms.LastName)
            {
                customerList = customerList.Where(e => e.Person.LastName.ToLower().Equals(((string)searchvalue).ToLower()));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.AddressCity)
            {
                customerList = customerList.Where(e => e.Person.AddressCity.ToLower().Equals(((string)searchvalue).ToLower()));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.IDNumber)
            {
                customerList = customerList.Where(e => e.Person.IDNumber == (int)searchvalue);
            }

            // LoyaltyCard is a char in DB, values can be 1 for true and 0 for false
            else if ((CustomerTerms)searchterm == CustomerTerms.LoyaltyCard)
            {
                customerList = customerList.Where(e => e.LoyaltyCard.Equals((char)searchvalue));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.ValidTo)
            {
                customerList = customerList.Where(e => e.Person.ValidTo <= (DateTime)searchvalue);
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.Default)
            {
                return customerList.ToList<Customer>();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }

            return customerList.ToList<Customer>();
        }

        /// <inheritdoc />
        public void ThrowIfExists(Customer customer)
        {
            this.customerRepository.ThrowIfExists(customer);
        }

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnCustomerListChanged()
        {
            this.CustomerListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
