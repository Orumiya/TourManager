namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.Interfaces;
    using DATA;
    using DATA.Repositoriees;

    /// <summary>
    /// searchterms for Customer entities
    /// </summary>
    public enum CustomerTerms
    {
        LoyaltyCard,
        LastName,
        FirstName,
        AddressCity,
        IDNumber,
        Default,
        ValidTo
    }

    public class CustomerBL : ISearcheable<Customer>, ICustomerList
    {
        private readonly CustomerRepository customerRepository;

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
            if ((CustomerTerms)searchterm == CustomerTerms.FirstName)
            {
                customerList = customerList.Where(e => e.Person.FirstName.Equals((string)searchvalue));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.LastName)
            {
                customerList = customerList.Where(e => e.Person.LastName.Equals((string)searchvalue));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.AddressCity)
            {
                customerList = customerList.Where(e => e.Person.AddressCity.Equals((string)searchvalue));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.IDNumber)
            {
                customerList = customerList.Where(e => e.Person.IDNumber == (decimal)searchvalue);
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.LoyaltyCard)
            {
                customerList = customerList.Where(e => e.LoyaltyCard.Equals((string)searchvalue));
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
