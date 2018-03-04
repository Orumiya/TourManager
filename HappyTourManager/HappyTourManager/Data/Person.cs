namespace HappyTourManager.Data
{
    using System;

    public abstract class Person
    {
        public int PersonID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public string AddressCity { get; set; }

        public string AddressZip { get; set; }

        public string AddressFree { get; set; }

        public string AddressCountry { get; set; }

        public string IDType { get; set; }

        public string IDNumber { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
