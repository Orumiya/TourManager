namespace DATA.Repositoriees
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    public class ReportRepository : IRepository<Report>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public ReportRepository(HappyTourDatabaseEntities entities)
        {
            this.entities = entities;
        }

        public void Create(Report dataobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Report dataobject)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Report> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        public void ThrowIfExists(Report dataobject)
        {
            throw new NotImplementedException();
        }
    }
}
