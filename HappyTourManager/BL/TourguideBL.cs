using BL.Interfaces;
using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TourguideBL : ISearcheable<Tourguide>, ITourguideList
    {
        /// <inheritdoc />
        public event EventHandler TourguideListChanged;

        /// <inheritdoc />
        public void Delete(Tourguide guide)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Save(Tourguide guide)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<Tourguide> Search(object searchterm, object searchvalue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void ThrowIfExists(Tourguide guide)
        {
            throw new NotImplementedException();
        }
    }
}
