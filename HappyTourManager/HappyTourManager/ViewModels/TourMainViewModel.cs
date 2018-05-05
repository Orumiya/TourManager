// <copyright file="TourMainViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using BL;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Tour view model
    /// </summary>
    internal class TourMainViewModel : Bindable
    {
        private TourBL tourBL;
        private TourguideBL tourguideBL;
        private string selectedCtegory = "DEFAULT";
        private string selectedValue1;
        private string selectedValue2;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private ObservableCollection<Tour> resultList;
        private Tour selectedTour;
        private ObservableCollection<Place> placeListAll;
        private ObservableCollection<Place> tourPlaceList;
        private Place selectedPlace;
        private Tourguide selectedTourGuide;
        private ObservableCollection<Program> programListAll;
        private ObservableCollection<Program> tourProgramList;
        private Program selectedProgram;
        private int pricePerNight;
        private ObservableCollection<Tourguide> tourGuideList;
        private List<string> searchCategories;
        private bool isEdit;
        private decimal adultP;
        private decimal childP;
        private IEnumerable<string> countryList;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourMainViewModel"/> class.
        /// constructor for tour viewmodel
        /// </summary>
        /// <param name="tourRepo"> tourrepository</param>
        /// <param name="placeRepo">place repository</param>
        /// <param name="pltconRepo">pltcon repository</param>
        /// <param name="programRepo">propgram repository</param>
        /// <param name="prtconRepo">prtcon repository</param>
        /// <param name="tourguideRepo">tourguide repository</param>
        public TourMainViewModel(
            IRepository<Tour> tourRepo,
            IRepository<Place> placeRepo,
            IRepository<PLTCON> pltconRepo,
            IRepository<Program> programRepo,
            IRepository<PRTCON> prtconRepo,
            IRepository<Tourguide> tourguideRepo)
        {
            this.CreateCountryList();
            this.tourBL = new TourBL(tourRepo, programRepo, placeRepo, pltconRepo, prtconRepo);
            this.tourguideBL = new TourguideBL(tourguideRepo);

            this.searchCategories = new List<string>();
            foreach (TourTerms item in Enum.GetValues(typeof(TourTerms)))
            {
                this.searchCategories.Add(item.ToString());
            }

            this.PlaceListAll = new ObservableCollection<Place>();
            this.programListAll = new ObservableCollection<Program>();
            this.TourGuideList = new ObservableCollection<Tourguide>();
            this.TourPlaceList = new ObservableCollection<Place>();
            this.TourProgramList = new ObservableCollection<Program>();
            this.GetAllPlaces();
            this.GetAllPrograms();
            this.GetAllTourGuides();
        }

        /// <summary>
        /// Gets or sets list of selectable search categories
        /// </summary>
        public List<string> SearchCategories
        {
            get
            {
                return this.searchCategories;
            }

            set
            {
                this.searchCategories = value;
                this.OnPropertyChanged(nameof(this.SearchCategories));
            }
        }

        /// <summary>
        /// Gets or sets selected search category
        /// </summary>
        public string SelectedCtegory
        {
            get
            {
                return this.selectedCtegory;
            }

            set
            {
                this.selectedCtegory = value;
                this.OnPropertyChanged(nameof(this.SelectedCtegory));
            }
        }

        /// <summary>
        /// Gets or sets selected string value
        /// </summary>
        public string SelectedValue1
        {
            get
            {
                return this.selectedValue1;
            }

            set
            {
                this.selectedValue1 = value;
                this.OnPropertyChanged(nameof(this.SelectedValue1));
            }
        }

        /// <summary>
        /// Gets or sets selected string value
        /// </summary>
        public string SelectedValue2
        {
            get
            {
                return this.selectedValue2;
            }

            set
            {
                this.selectedValue2 = value;
                this.OnPropertyChanged(nameof(this.SelectedValue2));
            }
        }

        /// <summary>
        /// Gets or sets selected date value
        /// </summary>
        public DateTime SelectedDateFrom
        {
            get
            {
                return this.selectedDateFrom;
            }

            set
            {
                this.selectedDateFrom = value;
                this.OnPropertyChanged(nameof(this.SelectedDateFrom));
            }
        }

        /// <summary>
        /// Gets or sets selected date value
        /// </summary>
        public DateTime SelectedDateTo
        {
            get
            {
                return this.selectedDateTo;
            }

            set
            {
                this.selectedDateTo = value;
                this.OnPropertyChanged(nameof(this.SelectedDateTo));
            }
        }

        /// <summary>
        /// Gets or sets list of search result
        /// </summary>
        public ObservableCollection<Tour> ResultList
        {
            get
            {
                return this.resultList;
            }

            set
            {
                this.resultList = value;
                this.OnPropertyChanged(nameof(this.ResultList));
            }
        }

        /// <summary>
        /// Gets or sets selected tour
        /// </summary>
        public Tour SelectedTour
        {
            get
            {
                return this.selectedTour;
            }

            set
            {
                this.selectedTour = value;
                this.OnPropertyChanged(nameof(this.SelectedTour));
            }
        }

        /// <summary>
        /// Gets or sets list of all places
        /// </summary>
        public ObservableCollection<Place> PlaceListAll
        {
            get
            {
                return this.placeListAll;
            }

            set
            {
                this.placeListAll = value;
                this.OnPropertyChanged(nameof(this.PlaceListAll));
            }
        }

        /// <summary>
        /// Gets or sets list of places of a selected tour
        /// </summary>
        public ObservableCollection<Place> TourPlaceList
        {
            get
            {
                return this.tourPlaceList;
            }

            set
            {
                this.tourPlaceList = value;
                this.OnPropertyChanged(nameof(this.TourPlaceList));
            }
        }

        /// <summary>
        /// Gets or sets selected place
        /// </summary>
        public Place SelectedPlace
        {
            get
            {
                return this.selectedPlace;
            }

            set
            {
                this.selectedPlace = value;
                this.OnPropertyChanged(nameof(this.SelectedPlace));
            }
        }

        /// <summary>
        /// Gets or sets list of all programs
        /// </summary>
        public ObservableCollection<Program> ProgramListAll
        {
            get
            {
                return this.programListAll;
            }

            set
            {
                this.programListAll = value;
                this.OnPropertyChanged(nameof(this.ProgramListAll));
            }
        }

        /// <summary>
        /// Gets or sets list of programs of a selected tour
        /// </summary>
        public ObservableCollection<Program> TourProgramList
        {
            get
            {
                return this.tourProgramList;
            }

            set
            {
                this.tourProgramList = value;
                this.OnPropertyChanged(nameof(this.TourProgramList));
            }
        }

        /// <summary>
        /// Gets or sets selectedprogram
        /// </summary>
        public Program SelectedProgram
        {
            get
            {
                return this.selectedProgram;
            }

            set
            {
                this.selectedProgram = value;
                this.OnPropertyChanged(nameof(this.SelectedProgram));
            }
        }

        /// <summary>
        /// Gets or sets prie per night
        /// </summary>
        public int PricePerNight
        {
            get
            {
                return this.pricePerNight;
            }

            set
            {
                this.pricePerNight = value;
                this.CalculatePrices();
                this.OnPropertyChanged(nameof(this.PricePerNight));
            }
        }

        /// <summary>
        /// Gets or sets list of tourguides
        /// </summary>
        public ObservableCollection<Tourguide> TourGuideList
        {
            get
            {
                return this.tourGuideList;
            }

            set
            {
                this.tourGuideList = value;
                this.OnPropertyChanged(nameof(this.TourGuideList));
            }
        }

        /// <summary>
        /// Gets or sets selected tour guide
        /// </summary>
        public Tourguide SelectedTourGuide
        {
            get
            {
                return this.selectedTourGuide;
            }

            set
            {
                this.selectedTourGuide = value;
                this.OnPropertyChanged(nameof(this.SelectedTourGuide));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether edit mode
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.isEdit;
            }

            set
            {
                this.isEdit = value;
                this.OnPropertyChanged(nameof(this.IsEdit));
            }
        }

        /// <summary>
        /// Gets or sets adult price
        /// </summary>
        public decimal AdultP
        {
            get
            {
                return this.adultP;
            }

            set
            {
                this.adultP = value;
                this.OnPropertyChanged(nameof(this.AdultP));
            }
        }

        /// <summary>
        /// Gets or sets childprice
        /// </summary>
        public decimal ChildP
        {
            get
            {
                return this.childP;
            }

            set
            {
                this.childP = value;
                this.OnPropertyChanged(nameof(this.ChildP));
            }
        }

        /// <summary>
        /// Gets or sets property
        /// </summary>
        public IEnumerable<string> CountryList
        {
            get
            {
                return this.countryList;
            }

            set
            {
                this.countryList = value;
            }
        }

        /// <summary>
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            IList<Tour> rL;
            if (this.SelectedCtegory == "TOURDATE")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = this.SelectedDateFrom;
                dt[1] = this.SelectedDateTo;

                rL = this.tourBL.Search(Enum.Parse(typeof(TourTerms), this.SelectedCtegory), dt);
            }
            else if (this.SelectedCtegory == "ADULTPRICE" || this.SelectedCtegory == "CHILDPRICE")
            {
                int[] arr = new int[2];
                arr[0] = int.Parse(this.SelectedValue1);
                arr[1] = int.Parse(this.SelectedValue2);

                rL = this.tourBL.Search(Enum.Parse(typeof(TourTerms), this.SelectedCtegory), arr);
            }
            else
            {
                rL = this.tourBL.Search(Enum.Parse(typeof(TourTerms), this.SelectedCtegory), this.SelectedValue1);
            }

            this.ResultList = new ObservableCollection<Tour>(rL);
        }

        /// <summary>
        /// Get Places for selected tour
        /// </summary>
        public void GetTourPlaces()
        {
            IList<PLTCON> places = this.tourBL.GetAllPLTCONs();
            this.TourPlaceList = new ObservableCollection<Place>();
            foreach (var item in places)
            {
                if (item != null && item.TourID == this.SelectedTour.TourID)
                {
                    if (!this.TourPlaceList.Contains(item.Place))
                    {
                        this.TourPlaceList.Add(item.Place);
                    }
                }
            }
        }

        /// <summary>
        /// Get Programs for selected tour
        /// </summary>
        public void GetTourPrograms()
        {
            IList<PRTCON> programs = this.tourBL.GetAllPRTCONs();
            this.TourProgramList = new ObservableCollection<Program>();
            foreach (var item in programs)
            {
                if (item != null && item.TourID == this.SelectedTour.TourID)
                {
                    if (!this.TourProgramList.Contains(item.Program))
                    {
                        this.TourProgramList.Add(item.Program);
                    }
                }
            }
        }

        /// <summary>
        /// check if form is correctly filled in
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns> true if values are ok</returns>
        public bool Checkvalues(int tab)
        {
            switch (tab)
            {
                case 0:
                    if (this.SelectedTour.TravelName == null)
                    {
                        return true;
                    }

                    if (this.SelectedTour.Transport == null)
                    {
                        return true;
                    }

                    break;
                case 1:
                    if (this.SelectedPlace.Country == null)
                    {
                        return true;
                    }

                    if (this.SelectedPlace.City == null)
                    {
                        return true;
                    }

                    break;
                case 2:
                    if (this.SelectedProgram.ProgramType == null)
                    {
                        return true;
                    }

                    break;
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// save item
        /// </summary>
        /// <param name="tab"> tab</param>
        public void SaveInstance(int tab)
        {
            if (this.SelectedTour != null)
            {
                this.SelectedTour.AdultPrice = this.AdultP;
                this.SelectedTour.ChildPrice = this.ChildP;
            }

            switch (tab)
            {
                case 0:
                    if (this.ResultList != null && this.ResultList.Contains(this.SelectedTour))
                    {
                        if (this.SelectedPlace != null)
                        {
                            this.tourBL.CreatePLTCON(new PLTCON() { TourID = this.SelectedTour.TourID, PlaceID = this.SelectedPlace.PlaceID });
                        }

                        if (this.SelectedProgram != null)
                        {
                            this.tourBL.CreatePRTCON(new PRTCON() { TourID = this.SelectedTour.TourID, ProgramID = this.SelectedProgram.ProgramID });
                        }

                        this.tourBL.Update();
                    }
                    else
                    {
                        this.tourBL.Save(this.SelectedTour);
                        if (this.SelectedPlace != null)
                        {
                            this.tourBL.CreatePLTCON(new PLTCON() { TourID = this.SelectedTour.TourID, PlaceID = this.SelectedPlace.PlaceID });
                        }

                        if (this.SelectedProgram != null)
                        {
                            this.tourBL.CreatePRTCON(new PRTCON() { TourID = this.SelectedTour.TourID, ProgramID = this.SelectedProgram.ProgramID });
                        }
                    }

                    break;
                case 1:
                    if (this.PlaceListAll.Contains(this.SelectedPlace))
                    {
                        this.tourBL.PlaceRepoUpdate();
                    }
                    else
                    {
                        this.tourBL.CreatePlace(this.SelectedPlace);
                        this.PlaceListAll.Add(this.SelectedPlace);
                    }

                    break;
                case 2:
                    if (this.ProgramListAll.Contains(this.SelectedProgram))
                    {
                        this.tourBL.ProgramRepoUpdate();
                    }
                    else
                    {
                        this.tourBL.CreateProgram(this.SelectedProgram);
                        this.ProgramListAll.Add(this.SelectedProgram);
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// delete item
        /// </summary>
        public void DeleteInstance()
        {
            IList<PLTCON> plts = this.tourBL.GetAllPLTCONs();
            List<PLTCON> pltList = new List<PLTCON>();
            foreach (var item in plts)
            {
                if (item.TourID == this.SelectedTour.TourID)
                {
                    pltList.Add(item);
                }
            }

            foreach (var item in pltList)
            {
                try
                {
                    this.tourBL.DeletePLTCON(item);
                }
                finally
                {
                }
            }

            IList<PRTCON> prts = this.tourBL.GetAllPRTCONs();
            List<PRTCON> prtList = new List<PRTCON>();
            foreach (var item in prts)
            {
                if (item.TourID == this.SelectedTour.TourID)
                {
                    prtList.Add(item);
                }
            }

            foreach (var item in prtList)
            {
                try
                {
                    this.tourBL.DeletePRTCON(item);
                }
                finally
                {
                }
            }

            this.tourBL.Delete(this.SelectedTour);
        }

        private void CreateCountryList()
        {
            RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
            List<string> countryNames = new List<string>();
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);

                countryNames.Add(country.DisplayName.ToString());
            }

            this.CountryList = countryNames.OrderBy(names => names).Distinct();
        }

        /// <summary>
        /// returns all available places and puts them in the list for FE
        /// </summary>
        private void GetAllPlaces()
        {
            IList<Place> places = this.tourBL.GetAllPlaces();
            foreach (var item in places)
            {
                this.PlaceListAll.Add(item);
            }
        }

        /// <summary>
        /// returns all available programs and puts them in the list for FE
        /// </summary>
        private void GetAllPrograms()
        {
            IList<Program> programs = this.tourBL.GetAllPrograms();
            if (programs != null)
            {
                foreach (var item in programs)
                {
                    this.ProgramListAll.Add(item);
                }
            }
        }

        /// <summary>
        /// returns all tourguides and puts them in the list for FE
        /// </summary>
        private void GetAllTourGuides()
        {
            IList<Tourguide> tg = this.tourguideBL.GetAllTourguides();
            if (tg != null)
            {
                foreach (var item in tg)
                {
                    this.TourGuideList.Add(item);
                }
            }
        }

        private void CalculatePrices()
        {
            if (this.SelectedTour != null)
            {
                this.AdultP = this.tourBL.AdultPriceCalculator(this.SelectedTour.EndDate, this.SelectedTour.StartDate, this.pricePerNight);
                this.ChildP = this.tourBL.ChildPriceCalculator((int)this.AdultP);
            }
        }
    }
}
