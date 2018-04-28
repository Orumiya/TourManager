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
        LOYALTYCARD,
        LASTNAME,
        ADDRESSCITY,
        IDNUMBER,
        DEFAULT,
        VALIDTO
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
            try
            {
                this.customerRepository.Delete(customer);
            }
            finally
            {
                this.OnCustomerListChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Customer customer)
        {
            try
            {
                this.customerRepository.Create(customer);
            }
            finally
            {
                this.OnCustomerListChanged();
            }
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
            if ((CustomerTerms)searchterm == CustomerTerms.LASTNAME)
            {
                customerList = customerList.Where(e => e.Person.LastName.ToLower().Equals(((string)searchvalue).ToLower()));
            }

            // returns customers  with this city
            // searchvalue must be a string
            else if ((CustomerTerms)searchterm == CustomerTerms.ADDRESSCITY)
            {
                customerList = customerList.Where(e => e.Person.AddressCity.ToLower().Equals(((string)searchvalue).ToLower()));
            }

            // returns customers with this IDNumber
            // searchvalue must be int
            else if ((CustomerTerms)searchterm == CustomerTerms.IDNUMBER)
            {
                customerList = customerList.Where(e => e.Person.IDNumber == (int)searchvalue);
            }

            // LoyaltyCard is a string in DB, values can be 1 for true and 0 for false
            // searchvalue must be a string
            else if ((CustomerTerms)searchterm == CustomerTerms.LOYALTYCARD)
            {
                customerList = customerList.Where(e => e.LoyaltyCard.Equals((string)searchvalue));
            }

            // returns customers with ValidTo date of their ID between these 2 dates
            // searchvalue must be a DateTime[]
            else if ((CustomerTerms)searchterm == CustomerTerms.VALIDTO)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                customerList = customerList.Where(e => e.Person.ValidTo <= endInterval && e.Person.ValidTo >= startInterval);
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.DEFAULT)
            {
                return customerList.ToList<Customer>();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }

            throw new InvalidOperationException("Not found");
        }

        /// <inheritdoc />
        public void ThrowIfExists(Customer customer)
        {
            this.customerRepository.ThrowIfExists(customer);
        }

        /// <summary>
        /// updates an entry
        /// </summary>
        public void Update()
        {
            this.customerRepository.Update();
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
