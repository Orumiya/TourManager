using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.Interfaces;

namespace DATA.Repositoriees
{
    class TourguideRepository : IRepository<Tourguide>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public TourguideRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }
    }
}
