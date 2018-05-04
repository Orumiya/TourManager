// <copyright file="TourguideBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BL.Interfaces;
    using DATA;
    using DATA.Interfaces;
    using DATA.Repositoriees;

    /// <summary>
    /// searchterms for Tourguide entities
    /// </summary>
    public enum TourguideTerms
    {
        TAXIDENTIFICATION,
        LASTNAME,
        DEFAULT,
        LANGUAGE,
        ISONHOLIDAY,
        ISAVAILABLE
    }

    public class TourguideBL : ISearcheable<Tourguide>, ITourguideList
    {
        private readonly IRepository<Tourguide> tourguideRepository;
        private readonly IRepository<Language> languageRepository;
        private readonly IRepository<OnHoliday> onHolidayRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourguideBL"/> class.
        /// creates a TourguideBL
        /// </summary>
        /// <param name="tourguideRepository">input param</param>
        /// <param name="languageRepository">input param - lang repo</param>
        /// <param name="onHolidayRepository">input param - onHoliday repo</param>
        public TourguideBL(IRepository<Tourguide> tourguideRepository, IRepository<Language> languageRepository, IRepository<OnHoliday> onHolidayRepository)
        {
            this.tourguideRepository = tourguideRepository;
            this.languageRepository = languageRepository;
            this.onHolidayRepository = onHolidayRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TourguideBL"/> class.
        /// </summary>
        /// <param name="tourguideRepository">input repo</param>
        public TourguideBL(IRepository<Tourguide> tourguideRepository)
        {
            this.tourguideRepository = tourguideRepository;
        }

        /// <inheritdoc />
        public event EventHandler TourguideListChanged;

        /// <inheritdoc />
        public void Delete(Tourguide guide)
        {
            try
            {
                this.tourguideRepository.Delete(guide);
            }
            finally
            {
                this.OnTourguideListChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Tourguide guide)
        {
            try
            {
                this.tourguideRepository.Create(guide);
            }
            finally
            {
                this.OnTourguideListChanged();
            }
        }

        /// <inheritdoc />
        public IList<Tourguide> Search(object searchterm, object searchvalue)
        {
            var tourguideList = this.tourguideRepository.GetAll();

            // returns tourguides with this last name
            // searchvalue must be a string
            if ((TourguideTerms)searchterm == TourguideTerms.LASTNAME)
            {
                tourguideList = tourguideList.Where(e => e.Person.LastName.ToLower().Equals(((string)searchvalue).ToLower()));
                return tourguideList.ToList<Tourguide>();
            }

            // returns tourguides with this taxID
            // searchvalue must be int
            else if ((TourguideTerms)searchterm == TourguideTerms.TAXIDENTIFICATION)
            {
                tourguideList = tourguideList.Where(e => e.Taxidentification == (int)searchvalue);
                return tourguideList.ToList<Tourguide>();
            }

            // returns tourguides who speaks this language
            // searchvalue must be a string
            else if ((TourguideTerms)searchterm == TourguideTerms.LANGUAGE)
            {
                var languages = this.languageRepository.GetAll();
                languages = languages.Where(i => i.Language1 == (string)searchvalue);
                IList<Tourguide> tglist = new List<Tourguide>();
                foreach (var item in languages)
                {
                    if (!tglist.Contains(item.Tourguide))
                    {
                        tglist.Add(item.Tourguide);
                    }
                }

                return tglist;
            }

            // searching for tourguides who are on holiday between 2 dates
            // searchvalue must be a DateTime[]
            else if ((TourguideTerms)searchterm == TourguideTerms.ISONHOLIDAY)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                var onholidayList = this.onHolidayRepository.GetAll();
                onholidayList = onholidayList.Where(
                    i => (i.StartDate <= endInterval) && (startInterval <= i.EndDate));
                IList<Tourguide> tglist = new List<Tourguide>();
                foreach (var item in onholidayList)
                {
                    if (!tglist.Contains(item.Tourguide))
                    {
                        tglist.Add(item.Tourguide);
                    }
                }

                return tglist;
            }

            // searching for tourguides who are not on holiday between 2 dates
            // searchvalue must be a DateTime[]
            else if ((TourguideTerms)searchterm == TourguideTerms.ISAVAILABLE)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                var onholidayList = this.onHolidayRepository.GetAll();
                List<OnHoliday> l = new List<OnHoliday>();
                foreach (var item in onholidayList)
                {
                    l.Add(item);
                }

                onholidayList = onholidayList.Where(
                    i => !((i.StartDate <= endInterval) && (startInterval <= i.EndDate)));
                IList<Tourguide> tglist = new List<Tourguide>();

                foreach (var item in onholidayList)
                {
                    if (!tglist.Contains(item.Tourguide))
                    {
                        tglist.Add(item.Tourguide);
                    }
                }

                return tglist;
            }
            else if ((TourguideTerms)searchterm == TourguideTerms.DEFAULT)
            {
                return tourguideList.ToList<Tourguide>();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
        }

        /// <inheritdoc />
        public void ThrowIfExists(Tourguide guide)
        {
            this.tourguideRepository.ThrowIfExists(guide);
        }

        /// <summary>
        /// returns all tourguides
        /// </summary>
        /// <returns>tglist</returns>
        public IList<Tourguide> GetAllTourguides()
        {
            var tg = this.tourguideRepository.GetAll();
            return tg.ToList();
        }

        /// <summary>
        /// updates an entry
        /// </summary>
        public void Update()
        {
            this.tourguideRepository.Update();
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
