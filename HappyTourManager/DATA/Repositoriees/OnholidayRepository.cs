namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    class OnholidayRepository : IRepository<OnHoliday>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public OnholidayRepository(HappyTourDatabaseEntities entities)
        {
           this.entities = entities;
        }
        public void Create(OnHoliday dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(OnHoliday dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OnHoliday> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<OnHoliday> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(OnHoliday dataobject)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
