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
    /// </summary>
    public class Customer : Person
    {
        public int LoyaltyCard { get; set; }
    }
}
