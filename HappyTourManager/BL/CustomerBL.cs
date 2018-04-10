namespace BL
{
    using BL.Interfaces;
    using DATA;
    using DATA.Repositoriees;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public enum CustomerTerms
    {
        LoyaltyCard,
        LastName,
        FirstName,
        AddressCity,
        IDNumber,
        ValidTo
    }

    public class CustomerBL : ISearcheable<Customer>
    {
        private readonly CustomerRepository customerRepository;

      public IList<Customer> Search(object searchterm, object searchvalue)
        {
            var customerList = customerRepository.GetAll();
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
                customerList = customerList.Where(e => e.Person.IDNumber == (decimal)searchvalue));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.LoyaltyCard)
            {
                customerList = customerList.Where(e => e.LoyaltyCard.Equals((string)searchvalue));
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.ValidTo)
            {
                customerList = customerList.Where(e => e.Person.ValidTo <= (DateTime)searchvalue);
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }

            return customerList.ToList<Customer>();
        }
    }
}
