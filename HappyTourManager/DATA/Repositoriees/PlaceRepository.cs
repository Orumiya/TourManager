namespace DATA.Repositories
{
    using DATA.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// enums as searching terms
    /// </summary>
    public enum PlaceTerms
    {
        COUNTRY,
        CITY
    }

    public class PlaceRepository : IRepository<Place>
    {
        /// <summary>
        /// field to Database
        /// </summary>
        private HappyTourDatabaseEntities entities;

        /// <summary>
        /// creates the repository
        /// </summary>
        /// <param name="entities"></param>
        public PlaceRepository()
        {
            this.entities = new HappyTourDatabaseEntities();
        }

        /// <summary>
        /// adds a new Place object to the database
        /// </summary>
        /// <param name="dataobject"></param>
        public void Create(Place dataobject)
        {
            ThrowIfExists(dataobject);
            entities.Places.Add(dataobject);
            entities.SaveChanges();
        }

        /// <summary>
        /// removes a Place from the database
        /// </summary>
        /// <param name="dataobject"></param>
        public void Delete(Place dataobject)
        {
            entities.Places.Remove(dataobject);
            entities.SaveChanges();
        }

        /// <summary>
        /// returns all Places from the database
        /// </summary>
        /// <returns></returns>
        public IQueryable<Place> GetAll()
        {
            return entities.Places;
        }

        /// <summary>
        /// searches for an object with attribut and value pair
        /// </summary>
        /// <param name="searchterm"></param>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public IQueryable<Place> Search(object searchterm, object searchvalue)
        {
            if ((PlaceTerms)searchterm == PlaceTerms.COUNTRY)
            {
                var places = entities.Places.Where(e => e.Country.Equals((string)searchvalue));
                return places;
            }
            else if ((PlaceTerms)searchterm == PlaceTerms.CITY)
            {
                var places = entities.Places.Where(e => e.City.Equals((string)searchvalue));
                return places;
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
            
        }

        /// <summary>
        /// throws an exception if the new Place exists
        /// </summary>
        /// <param name="dataobject"></param>
        public void ThrowIfExists(Place dataobject)
        {
            
            bool exist = entities.Places.Any(
                e => e.City.Equals(dataobject.City) &&
                e.Country.Equals(dataobject.Country));
            if (exist)
            {
                throw new InvalidOperationException("Already exists!");
            }
        }

        /// <summary>
        /// updates an entry in the database
        /// </summary>
        public void Update()
        {
            entities.SaveChanges();
        }

    }
}
