namespace HappyTourManager.Data
{
    using HappyTourManager.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Customer class has inherited properties from the base class "Person",
    /// and implements the ICrudFunctions interface.
    /// </summary>
    public class Customer : Person, ICrudFunctions
    {
        public int LoyaltyCard { get; set; }

        public void CreateEntry(object obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntry(object obj)
        {
            throw new NotImplementedException();
        }

        public void EditEntry(object obj)
        {
            throw new NotImplementedException();
        }

        public List<object> ListAllEntry()
        {
            throw new NotImplementedException();
        }

        public object SearchEntry(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
