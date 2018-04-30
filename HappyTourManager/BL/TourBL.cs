// <copyright file="TourBL.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// enums as searching terms
    /// </summary>
    public enum TourTerms
    {
        COUNTRY,
        CITY,
        ADULTPRICE,
        CHILDPRICE,
        TOURDATE,
        PROGRAM,
        DEFAULT
    }

    public class TourBL : ITourList, ISearcheable<Tour>
    {
        private readonly IRepository<Tour> tourRepository;
        private readonly IRepository<Program> programRepository;
        private readonly IRepository<Place> placeRepository;
        private readonly IRepository<PLTCON> pltconRepository;
        private readonly IRepository<PRTCON> prtconRepository;

        public TourBL(
            IRepository<Tour> tourRepository,
            IRepository<Program> programRepository,
            IRepository<Place> placeRepository,
            IRepository<PLTCON> pltconRepository,
            IRepository<PRTCON> prtconRepository)
        {
            this.tourRepository = tourRepository;
            this.programRepository = programRepository;
            this.placeRepository = placeRepository;
            this.pltconRepository = pltconRepository;
            this.prtconRepository = prtconRepository;
        }

        /// <inheritdoc />
        public event EventHandler TourListChanged;

        /// <inheritdoc />
        public void Delete(Tour tour)
        {
            try
            {
                this.tourRepository.Delete(tour);
            }
            finally
            {
                this.OnTourListChanged();
            }
        }

        /// <inheritdoc />
        public void Save(Tour tour)
        {
            try
            {
            this.tourRepository.Create(tour);
            }
            finally
            {
                this.OnTourListChanged();
            }
        }

        /// <summary>
        /// Calculates the AdultPrice of the Tour
        /// </summary>
        /// <param name="startDate">startDate of the Tour</param>
        /// <param name="endDate">endDate of the Tour</param>
        /// <param name="priceProNight">price per one night</param>
        /// <returns>the calculated sum of AdultPrice</returns>
        public int AdultPriceCalculator(DateTime startDate, DateTime endDate, int priceProNight)
        {
            TimeSpan timeSpan = startDate - endDate;
            int days = timeSpan.Days;
            return priceProNight * days;
        }

        /// <summary>
        /// calculates the ChildPrice which is the 70 % of the adultPrice
        /// </summary>
        /// <param name="adultPrice">adultPrice</param>
        /// <returns>childPrice</returns>
        public int ChildPriceCalculator(int adultPrice)
        {
            int childPrice = (int)(adultPrice * 0.7);
            return childPrice;
        }

        /// <inheritdoc />
        public IList<Tour> Search(object searchterm, object searchvalue)
        {
            var tourList = this.tourRepository.GetAll();

            // returns the tours to the searched country
            // searchvalue must be a string
            if ((TourTerms)searchterm == TourTerms.COUNTRY)
            {
                var countries = this.placeRepository.GetAll();
                string searchedCountry = ((string)searchvalue).ToLower();
                tourList = tourList.Where(e => e.PLTCONs.Select(s => s.Place).Select(r => r.Country.ToLower()).Contains(searchedCountry));
                IList<Tour> tlist = new List<Tour>();
                if (tourList.Count() != 0)
                {
                    foreach (var item in tourList)
                    {
                        if (!tlist.Contains(item))
                        {
                            tlist.Add(item);
                        }
                    }
                }

                return tlist;
            }

            // returns the tours to the searched city
            // searchvalue must be a string
            else if ((TourTerms)searchterm == TourTerms.CITY)
            {
                var cities = this.placeRepository.GetAll();
                string searchedCity = ((string)searchvalue).ToLower();
                tourList = tourList.Where(e => e.PLTCONs.Select(s => s.Place).Select(r => r.City.ToLower()).Contains(searchedCity));
                IList<Tour> tlist = new List<Tour>();
                if (tourList.Count() != 0)
                {
                    foreach (var item in tourList)
                    {
                        if (!tlist.Contains(item))
                        {
                            tlist.Add(item);
                        }
                    }
                }

                return tlist;
            }

            // returns tours with adultprice in the search range
            // searchvalue must be int[]
            else if ((TourTerms)searchterm == TourTerms.ADULTPRICE)
            {
                int[] priceRange = (int[])searchvalue;
                int minValue = priceRange[0];
                int maxValue = priceRange[1];
                var tours = this.tourRepository.GetAll();
                tours = tours.Where(e => e.AdultPrice >= minValue && e.AdultPrice <= maxValue);
                return tours.ToList();
            }

            // returns tours with childprice in the search range
            // searchvalue must be int[]
            else if ((TourTerms)searchterm == TourTerms.CHILDPRICE)
            {
                int[] priceRange = (int[])searchvalue;
                int minValue = priceRange[0];
                int maxValue = priceRange[1];
                var tours = this.tourRepository.GetAll();
                tours = tours.Where(e => e.ChildPrice >= minValue && e.ChildPrice <= maxValue);
                return tours.ToList();
            }

            // searching for tours which are between 2 dates
            // searchvalue must be a DateTime[]
            else if ((TourTerms)searchterm == TourTerms.TOURDATE)
            {
                DateTime[] interval = (DateTime[])searchvalue;
                DateTime startInterval = interval[0];
                DateTime endInterval = interval[1];
                tourList = tourList.Where(
                    i => (i.StartDate <= endInterval) && (startInterval <= i.EndDate));
                return tourList.ToList();
            }

            // returns the tours to the searched country
            // searchvalue must be a string
            else if ((TourTerms)searchterm == TourTerms.PROGRAM)
            {
                var programs = this.programRepository.GetAll();
                string searchedProgram = ((string)searchvalue).ToLower();

                // programs = programs.Where(r => r.ProgramType.ToLower().Contains(searchedProgram));
                tourList = tourList.Where(e => e.PRTCONs.Select(s => s.Program).Select(r => r.ProgramType.ToLower()).Contains(searchedProgram));
                IList<Tour> tlist = new List<Tour>();
                if (tourList.Count() != 0)
                {
                    foreach (var item in tourList)
                    {
                        if (!tlist.Contains(item))
                        {
                            tlist.Add(item);
                        }
                    }
                }

                return tlist;
            }

            // searches for all Tours
            else if ((TourTerms)searchterm == TourTerms.DEFAULT)
            {
                return tourList.ToList<Tour>();
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
        }

        /// <inheritdoc />
        public void ThrowIfExists(Tour tour)
        {
            this.tourRepository.ThrowIfExists(tour);
        }

        /// <summary>
        /// updates an entry
        /// </summary>
        public void Update()
        {
            this.tourRepository.Update();
        }

        /// <summary>
        /// notifies the outside about any collection change manually
        /// </summary>
        private void OnTourListChanged()
        {
            this.TourListChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
