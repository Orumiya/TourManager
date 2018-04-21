// <copyright file="TourBL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BL
{
    using System;
    using System.Collections.Generic;
    using BL.Interfaces;
    using DATA;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
        STARTDATE,
        ENDDATE,
        PROGRAM

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
            this.tourRepository.Delete(tour);
        }

        /// <inheritdoc />
        public void Save(Tour tour)
        {
            this.tourRepository.Create(tour);
        }

        /// <inheritdoc />
        public IList<Tour> Search(object searchterm, object searchvalue)
        {
            var tourList = this.tourRepository.GetAll();
            if ((TourTerms)searchterm == TourTerms.COUNTRY)
            {
                var countries = this.placeRepository.GetAll();
                string searchedCountry = (string)searchvalue;
                tourList = tourList.Where(e => e.PLTCONs.Select(s => s.Place).Select(r => r.Country).Contains(searchedCountry));
                IList<Tour> tlist = new List<Tour>();
                if (tourList.Count() != 0)
                {
                    foreach (var item in tourList)
                    {
                        if (!tlist.Contains(item))
                        {
                            tlist.Add(item);
                            Console.WriteLine(item.TravelName);
                        }
                    }
                }

                return tlist;
            }
            else if ((TourTerms)searchterm == TourTerms.CITY)
            {
                //var places = this.entities.Places.Where(e => e.City.Equals((string)searchvalue));
                //return places;
            }
            else
            {
                throw new InvalidOperationException("Not found");
            }
            throw new InvalidOperationException("Not found");
        }

        /// <inheritdoc />
        public void ThrowIfExists(Tour tour)
        {
            this.tourRepository.ThrowIfExists(tour);
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
