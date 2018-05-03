using BL;
using DATA;
using DATA.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTourManager
{
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
                return searchCategories;
            }

            set
            {
                searchCategories = value;
                OnPropertyChanged(nameof(SearchCategories));
            }
        }

        public string SelectedCtegory
        {
            get
            {
                return selectedCtegory;
            }

            set
            {
                selectedCtegory = value;
                OnPropertyChanged(nameof(SelectedCtegory));
            }
        }

        public string SelectedValue1
        {
            get
            {
                return selectedValue1;
            }

            set
            {
                selectedValue1 = value;
                OnPropertyChanged(nameof(SelectedValue1));
            }
        }

        public string SelectedValue2
        {
            get
            {
                return selectedValue2;
            }

            set
            {
                selectedValue2 = value;
                OnPropertyChanged(nameof(SelectedValue2));
            }
        }

        public DateTime SelectedDateFrom
        {
            get
            {
                return selectedDateFrom;
            }

            set
            {
                selectedDateFrom = value;
                OnPropertyChanged(nameof(SelectedDateFrom));
            }
        }

        public DateTime SelectedDateTo
        {
            get
            {
                return selectedDateTo;
            }

            set
            {
                selectedDateTo = value;
                OnPropertyChanged(nameof(SelectedDateTo));
            }
        }

        public ObservableCollection<Tour> ResultList
        {
            get
            {
                return resultList;
            }

            set
            {
                resultList = value;
                OnPropertyChanged(nameof(ResultList));
            }
        }

        public Tour SelectedTour
        {
            get
            {
                return selectedTour;
            }

            set
            {
                selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public ObservableCollection<Place> PlaceListAll
        {
            get
            {
                return placeListAll;
            }

            set
            {
                placeListAll = value;
                OnPropertyChanged(nameof(PlaceListAll));
            }
        }

        public ObservableCollection<Place> TourPlaceList
        {
            get
            {
                return tourPlaceList;
            }

            set
            {
                tourPlaceList = value;
                OnPropertyChanged(nameof(TourPlaceList));
            }
        }

        public Place SelectedPlace
        {
            get
            {
                return selectedPlace;
            }

            set
            {
                selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }

        public ObservableCollection<Program> ProgramListAll
        {
            get
            {
                return programListAll;
            }

            set
            {
                programListAll = value;
                OnPropertyChanged(nameof(ProgramListAll));
            }
        }

        public ObservableCollection<Program> TourProgramList
        {
            get
            {
                return tourProgramList;
            }

            set
            {
                tourProgramList = value;
                OnPropertyChanged(nameof(TourProgramList));
            }
        }

        public Program SelectedProgram
        {
            get
            {
                return selectedProgram;
            }

            set
            {
                selectedProgram = value;
                OnPropertyChanged(nameof(SelectedProgram));
            }
        }

        public int PricePerNight
        {
            get
            {
                return pricePerNight;
            }

            set
            {
                pricePerNight = value;
                CalculatePrices();
                OnPropertyChanged(nameof(PricePerNight));
            }
        }

        public ObservableCollection<Tourguide> TourGuideList
        {
            get
            {
                return tourGuideList;
            }

            set
            {
                tourGuideList = value;
                OnPropertyChanged(nameof(TourGuideList));
            }
        }

        public Tourguide SelectedTourGuide
        {
            get
            {
                return selectedTourGuide;
            }

            set
            {
                selectedTourGuide = value;
                OnPropertyChanged(nameof(SelectedTourGuide));
            }
        }

        public bool IsEdit
        {
            get
            {
                return isEdit;
            }

            set
            {
                isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
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
            CreateCountryList();
            tourBL = new TourBL(tourRepo, programRepo, placeRepo, pltconRepo, prtconRepo);

            searchCategories = new List<string>();
            foreach (TourTerms item in Enum.GetValues(typeof(TourTerms)))
            {
                searchCategories.Add(item.ToString());
            }

            PlaceListAll = new ObservableCollection<Place>();
            programListAll = new ObservableCollection<Program>();
            TourGuideList = new ObservableCollection<Tourguide>();
            TourPlaceList = new ObservableCollection<Place>();
            TourProgramList = new ObservableCollection<Program>();
            GetAllPlaces();
            GetAllPrograms();
            GetAllTourGuides();
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            IList<Tour> rL;
            if (SelectedCtegory == "TOURDATE")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = SelectedDateFrom;
                dt[1] = SelectedDateTo;

                rL = tourBL.Search(Enum.Parse(typeof(TourTerms), SelectedCtegory), dt);
            }
            else if (SelectedCtegory == "ADULTPRICE" || SelectedCtegory == "CHILDPRICE")
            {
                int[] arr = new int[2];
                arr[0] = Int32.Parse(SelectedValue1);
                arr[1] = Int32.Parse(SelectedValue2);

                rL = tourBL.Search(Enum.Parse(typeof(TourTerms), SelectedCtegory), arr);
            }
            else
            {
                rL = tourBL.Search(Enum.Parse(typeof(TourTerms), SelectedCtegory), SelectedValue1);
            }
            ResultList = new ObservableCollection<Tour>(rL);
        }

        /// <summary>
        /// Get Places for selected tour
        /// </summary>
        public void GetTourPlaces()
        {
            IQueryable<PLTCON> places = pltconRepo.GetAll();
            foreach (var item in places)
            {
                if (item != null && item.TourID == SelectedTour.TourID)
                {
                    if (!TourPlaceList.Contains(item.Place))
                    {
                        TourPlaceList.Add(item.Place);
                    }
                    
                }
                
            }

        }

        /// <summary>
        /// Get Programs for selected tour
        /// </summary>
        public void GetTourPrograms()
        {
            IQueryable<PRTCON> programs = prtconRepo.GetAll();
            foreach (var item in programs)
            {
                if (item != null && item.TourID == SelectedTour.TourID)
                {
                    if (!TourProgramList.Contains(item.Program))
                    {
                        TourProgramList.Add(item.Program);
                    }

                    
                }

                
            }

        }

        public bool Checkvalues(int tab)
        {
            switch (tab)
            {
                case 0:
                    if (SelectedTour.TravelName == null) return true;
                    if (SelectedTour.Transport == null) return true;
                    break;
                case 1:
                    if (SelectedPlace.Country == null) return true;
                    if (SelectedPlace.City == null) return true;
                    break;
                case 2:
                    if (SelectedProgram.ProgramType == null) return true;
                    break;
                default:
                    break;

                    
            }
            return false;
        }

        public void SaveTour(int tab)
        {
            switch (tab)
            {
                case 0:
                    if (ResultList != null && ResultList.Contains(SelectedTour))
                    {
                        if (SelectedPlace != null)
                        {
                            pltconRepo.Create(new PLTCON() { TourID = SelectedTour.TourID, PlaceID = SelectedPlace.PlaceID });
                        }
                        if (SelectedProgram != null)
                        {
                            prtconRepo.Create(new PRTCON() { TourID = SelectedTour.TourID, ProgramID = SelectedProgram.ProgramID });
                        }
                        tourBL.Update();
                    }
                    else
                    {
                        tourBL.Save(SelectedTour);
                        if (SelectedPlace != null)
                        {
                            pltconRepo.Create(new PLTCON() { TourID = SelectedTour.TourID, PlaceID = SelectedPlace.PlaceID });
                        }
                        if (SelectedProgram != null)
                        {
                            prtconRepo.Create(new PRTCON() { TourID = SelectedTour.TourID, ProgramID = SelectedProgram.ProgramID });
                        }
                    }
                    break;
                case 1:
                    if (PlaceListAll.Contains(SelectedPlace))
                    {
                        placeRepo.Update();
                    }
                    else
                    {
                        placeRepo.Create(SelectedPlace);
                        PlaceListAll.Add(SelectedPlace);
                    }
                    break;
                case 2:
                    if (ProgramListAll.Contains(SelectedProgram))
                    {
                        programRepo.Update();
                    }
                    else
                    {
                        programRepo.Create(SelectedProgram);
                        ProgramListAll.Add(SelectedProgram);
                    }
                    break;
                default:
                    break;


            }
        }

        public void DeleteTour()
        {
            IQueryable<PLTCON> plts = pltconRepo.GetAll();
            List<PLTCON> pltList = new List<PLTCON>();
            foreach (var item in plts)
            {
                if (item.TourID == SelectedTour.TourID)
                {
                    pltList.Add(item);
                }
            }
            foreach (var item in pltList)
            {
                try
                {
                    pltconRepo.Delete(item);
                }
                finally { }
            }
            IQueryable<PRTCON> prts = prtconRepo.GetAll();
            List<PRTCON> prtList = new List<PRTCON>();
            foreach (var item in prts)
            {
                if (item.TourID == SelectedTour.TourID)
                {
                    prtList.Add(item);
                }
                
            }
            foreach (var item in prtList)
            {
                try
                {
                    prtconRepo.Delete(item);
                }
                finally { }
            }
            tourBL.Delete(SelectedTour);
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

            countryList = countryNames.OrderBy(names => names).Distinct();
        }

        private void GetAllPlaces()
        {
            IQueryable<Place> places = placeRepo.GetAll();
            foreach (var item in places)
            {
                PlaceListAll.Add(item);
            }
        }

        private void GetAllPrograms()
        {
            IQueryable<Program> programs = programRepo.GetAll();
            foreach (var item in programs)
            {
                ProgramListAll.Add(item);
            }
        }



        private void GetAllTourGuides()
        {

            IQueryable<Tourguide> tg = tourguideRepo.GetAll();
            if (tg !=null)
            {
                foreach (var item in tg)
                {
                    TourGuideList.Add(item);
                }
            }
            
        }

        private void CalculatePrices()
        {
            if (SelectedTour != null)
            {
                SelectedTour.AdultPrice = tourBL.AdultPriceCalculator(SelectedTour.EndDate, SelectedTour.StartDate, pricePerNight);
                SelectedTour.ChildPrice = tourBL.ChildPriceCalculator((int)SelectedTour.AdultPrice);
            }
        }
        #endregion

    }
}
