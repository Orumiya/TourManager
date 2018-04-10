namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.Interfaces;
    using DATA;
    using DATA.Repositoriees;

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
        private readonly OnholidayRepository onHolidayRepository;

        public TourguideBL(TourguideRepository tourguideRepository, LanguageRepository languageRepository, OnholidayRepository onHolidayRepository)
        {
            this.tourguideRepository = tourguideRepository;
            this.languageRepository = languageRepository;
            this.onHolidayRepository = onHolidayRepository;
        }

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
            foreach (var item in tourguideList)
            {
                Console.WriteLine(item.Person.LastName + " " + item.OnHolidays.ToString());
            }
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
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                var onholidayList = this.onHolidayRepository.GetAll();
                onholidayList = onholidayList.Where(
                    i => (i.StartDate >= startInterval && i.StartDate <= endInterval)
                    || (i.EndDate >= startInterval && i.EndDate <= endInterval));
                foreach (var item in onholidayList)
                {
                    Console.WriteLine(item.StartDate + " " + item.EndDate);
                }
                var query = tourguideList.Where(e => onholidayList.Select(s => s.TourguideID).Contains(e.PersonID));

                return query.ToList();

            }
            else if ((TourguideTerms)searchterm == TourguideTerms.Default)
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

        //public IList<Tourguide> GetHolidays()
        //{
        //    //var holidaylist = this.tourguideRepository.GetAll()
        //    //    .Join(
        //    //    this.onHolidayRepository.GetAll(),
        //    //    persID => persID.PersonID,
        //    //    tourgID => tourgID.TourguideID,
        //    //    (persID, tourgID) => new { Tourguide = persID, OnHoliday = tourgID });
        //    var tourguideList = this.tourguideRepository.GetAll();
        //    tourguideList = tourguideList.Where();
        //    //IQueryable<Tourguide> tourgList = (IQueryable<Tourguide>)holidaylist;
        //    //return tourgList.ToList<Tourguide>();
        //}
    }
}
