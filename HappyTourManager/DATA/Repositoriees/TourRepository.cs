namespace DATA.Repositoriees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DATA.Interfaces;

    public class TourRepository : IRepository<Tour>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourRepository"/> class.
        /// creates the repository
        /// </summary>
        /// <param name="entities">database</param>
        public TourRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        /// <summary>
        /// adds a Tour object to the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Create(Tour dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// removes a Program from the database
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void Delete(Tour dataobject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns all Tours from the database
        /// </summary>
        /// <returns>TourList</returns>
        public IQueryable<Tour> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tour> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throws an exception if the new Tour exists
        /// </summary>
        /// <param name="dataobject">input param</param>
        public void ThrowIfExists(Tour dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
