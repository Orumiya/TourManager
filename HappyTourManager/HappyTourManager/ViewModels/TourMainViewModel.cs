namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BL;
    using DATA;
    using DATA.Interfaces;

    class TourMainViewModel : Bindable
    {
        #region private variables
        private IRepository<Place> placeRepo;
        private IRepository<PLTCON> pltconRepo;
        private IRepository<Program> programRepo;
        private IRepository<PRTCON> prtconRepo;
        private IRepository<Tourguide> tourguideRepo;
        private IRepository<Tour> tourRepo;
        private TourBL tourBL;

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
        int pricePerNight;
        ObservableCollection<Tourguide> tourGuideList;

        private List<string> searchCategories;
        private bool isEdit;

        public IEnumerable<string> countryList;
        #endregion

        #region parameters
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

        #endregion

        #region constructor
        public TourMainViewModel(IRepository<Tour> tourRepo,
            IRepository<Place> placeRepo,
            IRepository<PLTCON> pltconRepo,
            IRepository<Program> programRepo,
            IRepository<PRTCON> prtconRepo,
            IRepository<Tourguide> tourguideRepo)
        {
            this.tourRepo = tourRepo;
            this.placeRepo = placeRepo;
            this.pltconRepo = pltconRepo;
            this.programRepo = programRepo;
            this.prtconRepo = prtconRepo;
            this.tourguideRepo = tourguideRepo;
            this.CreateCountryList();
            this.tourBL = new TourBL(tourRepo, programRepo, placeRepo, pltconRepo, prtconRepo);

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

        #endregion

        #region Public methods
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
                arr[0] = Int32.Parse(this.SelectedValue1);
                arr[1] = Int32.Parse(this.SelectedValue2);

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
            IQueryable<PLTCON> places = this.pltconRepo.GetAll();
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
            IQueryable<PRTCON> programs = this.prtconRepo.GetAll();
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

        public bool Checkvalues(int tab)
        {
            switch (tab)
            {
                case 0:
                    if (this.SelectedTour.TravelName == null) return true;
                    if (this.SelectedTour.Transport == null) return true;
                    break;
                case 1:
                    if (this.SelectedPlace.Country == null) return true;
                    if (this.SelectedPlace.City == null) return true;
                    break;
                case 2:
                    if (this.SelectedProgram.ProgramType == null) return true;
                    break;
                default:
                    break;

            }
            return false;
        }

        public void SaveInstance(int tab)
        {
            switch (tab)
            {
                case 0:
                    if (this.ResultList != null && this.ResultList.Contains(this.SelectedTour))
                    {
                        if (this.SelectedPlace != null)
                        {
                            this.pltconRepo.Create(new PLTCON() { TourID = this.SelectedTour.TourID, PlaceID = this.SelectedPlace.PlaceID });
                        }
                        if (this.SelectedProgram != null)
                        {
                            this.prtconRepo.Create(new PRTCON() { TourID = this.SelectedTour.TourID, ProgramID = this.SelectedProgram.ProgramID });
                        }
                        this.tourBL.Update();
                    }
                    else
                    {
                        this.tourBL.Save(this.SelectedTour);
                        if (this.SelectedPlace != null)
                        {
                            this.pltconRepo.Create(new PLTCON() { TourID = this.SelectedTour.TourID, PlaceID = this.SelectedPlace.PlaceID });
                        }
                        if (this.SelectedProgram != null)
                        {
                            this.prtconRepo.Create(new PRTCON() { TourID = this.SelectedTour.TourID, ProgramID = this.SelectedProgram.ProgramID });
                        }
                    }
                    break;
                case 1:
                    if (this.PlaceListAll.Contains(this.SelectedPlace))
                    {
                        this.placeRepo.Update();
                    }
                    else
                    {
                        this.placeRepo.Create(this.SelectedPlace);
                        this.PlaceListAll.Add(this.SelectedPlace);
                    }
                    break;
                case 2:
                    if (this.ProgramListAll.Contains(this.SelectedProgram))
                    {
                        this.programRepo.Update();
                    }
                    else
                    {
                        this.programRepo.Create(this.SelectedProgram);
                        this.ProgramListAll.Add(this.SelectedProgram);
                    }
                    break;
                default:
                    break;

            }
        }

        public void DeleteInstance()
        {
            IQueryable<PLTCON> plts = this.pltconRepo.GetAll();
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
                    this.pltconRepo.Delete(item);
                }
                finally { }
            }
            IQueryable<PRTCON> prts = this.prtconRepo.GetAll();
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
                    this.prtconRepo.Delete(item);
                }
                finally { }
            }
            this.tourBL.Delete(this.SelectedTour);
        }
        #endregion

        #region Private methods
        private void CreateCountryList()
        {
            RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
            List<string> countryNames = new List<string>();
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);

                countryNames.Add(country.DisplayName.ToString());
            }

            this.countryList = countryNames.OrderBy(names => names).Distinct();
        }

        private void GetAllPlaces()
        {
            IQueryable<Place> places = this.placeRepo.GetAll();
            foreach (var item in places)
            {
                this.PlaceListAll.Add(item);
            }
        }

        private void GetAllPrograms()
        {
            IQueryable<Program> programs = this.programRepo.GetAll();
            foreach (var item in programs)
            {
                this.ProgramListAll.Add(item);
            }
        }

        private void GetAllTourGuides()
        {

            IQueryable<Tourguide> tg = this.tourguideRepo.GetAll();
            if (tg !=null)
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
                this.SelectedTour.AdultPrice = this.tourBL.AdultPriceCalculator(this.SelectedTour.EndDate, this.SelectedTour.StartDate, this.pricePerNight);
                this.SelectedTour.ChildPrice = this.tourBL.ChildPriceCalculator((int)this.SelectedTour.AdultPrice);
            }
        }

        #endregion

    }
}
