namespace DATA.Interfaces
{
    using System.Linq;

    public interface IRepository<T>
    {
        /// <summary>
        /// returns all data objects
        /// </summary>
        /// <returns>output</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// create new dataobject
        /// </summary>
        /// <param name="dataobject">input param</param>
        void Create(T dataobject);

        /// <summary>
        /// Delete dataobject (generic method)
        /// </summary>
        /// <param name="dataobject">input param</param>
        void Delete(T dataobject);

        /// <summary>
        /// throws exception, if same object exists
        /// </summary>
        /// <param name="dataobject">input param</param>
        void ThrowIfExists(T dataobject);

        /// <summary>
        /// updates a repository
        /// </summary>
        void Update();
    }
}
