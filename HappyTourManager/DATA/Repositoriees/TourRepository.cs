namespace DATA.Repositoriees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DATA.Interfaces;

    class TourRepository : IRepository<Tour>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public TourRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }
    }
}
