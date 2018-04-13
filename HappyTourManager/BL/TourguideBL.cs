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
        Taxidentification,
        LastName,
        Default,
        Language,
        IsOnHoliday,
        IsAvailable
    }

    public class TourguideBL : ISearcheable<Tourguide>, ITourguideList
    {
        private readonly IRepository<Tourguide> tourguideRepository;
        private readonly IRepository<Language> languageRepository;
        private readonly IRepository<OnHoliday> onHolidayRepository;

        public TourguideBL(IRepository<Tourguide> tourguideRepository, IRepository<Language> languageRepository, IRepository<OnHoliday> onHolidayRepository)
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
            else if ((TourguideTerms)searchterm == TourguideTerms.IsOnHoliday)
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
            else if ((TourguideTerms)searchterm == TourguideTerms.IsAvailable)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                var onholidayList = this.onHolidayRepository.GetAll();
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
    }
}
