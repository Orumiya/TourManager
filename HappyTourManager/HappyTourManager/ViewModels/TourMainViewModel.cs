using BL;
using DATA;
using DATA.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Program> programListAll;
        private ObservableCollection<Program> tourProgramList;
        private Program selectedProgram;

        private List<string> searchCategories;

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
                return TourPlaceList1;
            }

            set
            {
                TourPlaceList1 = value;
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
        }
        #endregion

        #region Public methods

        #endregion


        #region Private methods

        #endregion

    }
}
