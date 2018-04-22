// <copyright file="CustomerBL.cs" company="PlaceholderCompany">
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBL"/> class.
        /// creates the customerBL
        /// </summary>
        /// <param name="customerRepository">input param</param>
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

            // returns customers with this last name
            // searchvalue must be a string
            if ((CustomerTerms)searchterm == CustomerTerms.LastName)
            {
                customerList = customerList.Where(e => e.Person.LastName.ToLower().Equals(((string)searchvalue).ToLower()));
            }

            // returns customers  with this city
            // searchvalue must be a string
            else if ((CustomerTerms)searchterm == CustomerTerms.AddressCity)
            {
                customerList = customerList.Where(e => e.Person.AddressCity.ToLower().Equals(((string)searchvalue).ToLower()));
            }

            // returns customers with this IDNumber
            // searchvalue must be int
            else if ((CustomerTerms)searchterm == CustomerTerms.IDNumber)
            {
                customerList = customerList.Where(e => e.Person.IDNumber == (int)searchvalue);
            }

            // LoyaltyCard is a string in DB, values can be 1 for true and 0 for false
            // searchvalue must be a string
            else if ((CustomerTerms)searchterm == CustomerTerms.LoyaltyCard)
            {
                customerList = customerList.Where(e => e.LoyaltyCard.Equals((string)searchvalue));
            }

            // returns customers with ValidTo date of their ID between these 2 dates
            // searchvalue must be a DateTime[]
            else if ((CustomerTerms)searchterm == CustomerTerms.ValidTo)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                customerList = customerList.Where(e => e.Person.ValidTo <= endInterval && e.Person.ValidTo >= startInterval);
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
