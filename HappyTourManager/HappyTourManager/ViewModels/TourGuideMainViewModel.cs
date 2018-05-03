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
    class TourGuideMainViewModel: Bindable
    {
        #region private variables
        private IRepository<Tourguide> tourGuideRepo;
        private IRepository<Language> languageRepo;
        private IRepository<OnHoliday> holidayRepo;
        private IRepository<Tour> tourRepo;

        private TourguideBL tgBL;
        private string selectedCtegory = "DEFAULT";
        private string selectedValue;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private ObservableCollection<Tourguide> resultList;
        private Tourguide selectedTG;

        private List<string> searchCategories;

        public IEnumerable<string> countryList;
        public List<string> languageList;
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

        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }

            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        public ObservableCollection<Tourguide> ResultList
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

        public Tourguide SelectedTG
        {
            get
            {
                return selectedTG;
            }

            set
            {
                selectedTG = value;
                OnPropertyChanged(nameof(SelectedTG));
            }
        }
        #endregion


        #region constructor
        public TourGuideMainViewModel(
            IRepository<Tourguide> tourGuideRepo,
                IRepository<Language> languageRepo,
                IRepository<OnHoliday> holidayRepo,
                IRepository<Tour> tourRepo
            )
        {
            this.tourGuideRepo = tourGuideRepo;
            this.languageRepo = languageRepo;
            this.holidayRepo = holidayRepo;
            this.tourRepo = tourRepo;
            tgBL = new TourguideBL(tourGuideRepo, languageRepo, holidayRepo);
            CreateCountryList();

            languageList = new List<string>();
            languageList.Add("english");
            languageList.Add("german");
            languageList.Add("french");
            languageList.Add("spanish");
            languageList.Add("italian");
            languageList.Add("chinese");
            languageList.Add("japanese");

            searchCategories = new List<string>();
            foreach (TourguideTerms item in Enum.GetValues(typeof(TourguideTerms)))
            {
                searchCategories.Add(item.ToString());
            }
        }

        #endregion


        #region public method
        

        #endregion


        #region private method
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
        #endregion
    }
}
