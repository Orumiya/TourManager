namespace HappyTourManager.Data
{
    using HappyTourManager.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Tourguide class is to handle the data of the tourguides of the travel agency.
    /// Tourguide class has inherited properties from the base class "Person"
    /// </summary>
    public class Tourguide : Person
    {
        public int TaxID { get; set; }

        public int DailyAllowance { get; set; }
    }
}
