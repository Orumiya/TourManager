using BL.Interfaces;
using DATA;
using DATA.Repositoriees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// searchterms for Tourguide entities
    /// </summary>
    public enum TourguideTerms
    {
        Taxidentification,
        LastName,
        Default,
        Language,
        IsOnHoliday,
        IsAvailable
    }

    public class TourguideBL : ISearcheable<Tourguide>, ITourguideList
    {
        private readonly TourguideRepository tourguideRepository;
        private readonly LanguageRepository languageRepository;
        private readonly OnHolidayRepository onHolidayRepository;

        /// <inheritdoc />
        public event EventHandler TourguideListChanged;

        /// <inheritdoc />
        public void Delete(Tourguide guide)
        {
            this.tourguideRepository.Delete(guide);
        }

        /// <inheritdoc />
        public void Save(Tourguide guide)
        {
            this.tourguideRepository.Create(guide);
        }

        /// <inheritdoc />
        public IList<Tourguide> Search(object searchterm, object searchvalue)
        {
            var tourguideList = this.tourguideRepository.GetAll();
            if ((TourguideTerms)searchterm == TourguideTerms.LastName)
            {
                tourguideList = tourguideList.Where(e => e.Person.LastName.Equals((string)searchvalue));
            }
            else if ((TourguideTerms)searchterm == TourguideTerms.Taxidentification)
            {
                tourguideList = tourguideList.Where(e => e.Taxidentification == (decimal)searchvalue);
            }
            else if ((TourguideTerms)searchterm == TourguideTerms.Language)
            {
                tourguideList = tourguideList.Where(e => e.Languages.Equals((string)searchvalue));
            }
            else if ((TourguideTerms)searchterm == TourguideTerms.IsOnHoliday)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                //tourguideList = tourguideList.Where(i => (i.StartDate >= interval[0] && i.StartDate <= interval[1])
                   // || i.EndDate >= interval[1] && i.EndDate <= interval[1]);
            }
            else if ((CustomerTerms)searchterm == CustomerTerms.Default)
            {
                return tourguideList.ToList<Tourguide>();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }

            return tourguideList.ToList<Tourguide>();
        }

        /// <inheritdoc />
        public void ThrowIfExists(Tourguide guide)
        {
            this.tourguideRepository.ThrowIfExists(guide);
        }

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnTourguideListChanged()
        {
            this.TourguideListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
