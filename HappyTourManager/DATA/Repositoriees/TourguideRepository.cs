namespace DATA.Repositoriees
{
    using System;
    using System.Linq;
    using DATA.Interfaces;

    public class TourguideRepository : IRepository<Tourguide>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourguideRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">input param database</param>
        public TourguideRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <inheritdoc />
        public void Create(Tourguide dataobject)
        {
            this.ThrowIfExists(dataobject);
            this.entities.People.Add(dataobject.Person);
            this.entities.Tourguides.Add(dataobject);
            this.entities.SaveChanges();
        }

        /// <inheritdoc />
        public void Delete(Tourguide dataobject)
        {
            this.entities.Tourguides.Remove(dataobject);
            this.entities.SaveChanges();
        }

        /// <inheritdoc />
        public IQueryable<Tourguide> GetAll()
        {
            return this.entities.Tourguides;
        }

        /// <summary>
        /// throws exception if Tourguide is already exists
        /// comparing FistName & Lastname & BirthDate & Taxidentification
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Tourguide dataobject)
        {
            bool exist = this.entities.Tourguides.Any(
                e => e.Person.FirstName.Equals(dataobject.Person.FirstName) &&
                e.Person.LastName.Equals(dataobject.Person.LastName) &&
                e.Person.BirthDate.Equals(dataobject.Person.BirthDate) &&
                e.Taxidentification.Equals(dataobject.Taxidentification));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }
    }
}
