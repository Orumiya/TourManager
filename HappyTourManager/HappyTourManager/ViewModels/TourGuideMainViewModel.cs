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
        private string selectedLanguage;
        private DateTime selectedHolidayFrom;
        private DateTime selectedHolidayTill;

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

        public string SelectedLanguage
        {
            get
            {
                return selectedLanguage;
            }

            set
            {
                selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }

        public DateTime SelectedHolidayFrom
        {
            get
            {
                return selectedHolidayFrom;
            }

            set
            {
                selectedHolidayFrom = value;
                OnPropertyChanged(nameof(SelectedHolidayFrom));
            }
        }

        public DateTime SelectedHolidayTill
        {
            get
            {
                return selectedHolidayTill;
            }

            set
            {
                selectedHolidayTill = value;
                OnPropertyChanged(nameof(SelectedHolidayTill));
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
            languageList.Add("dutch");
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
        /// <summary>
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            IList<Tourguide> rL;
            if (SelectedCtegory == "ISONHOLIDAY" || SelectedCtegory == "ISAVAILABLE")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = SelectedDateFrom;
                dt[1] = SelectedDateTo;

                rL = tgBL.Search(Enum.Parse(typeof(TourguideTerms), SelectedCtegory), dt);
            }
            else if (SelectedCtegory == "TAXIDENTIFICATION")
            {
                rL = tgBL.Search(Enum.Parse(typeof(TourguideTerms), SelectedCtegory), Int32.Parse(SelectedValue));
            }
            else
            {
                rL = tgBL.Search(Enum.Parse(typeof(TourguideTerms), SelectedCtegory), SelectedValue);
            }
            ResultList = new ObservableCollection<Tourguide>(rL);
        }

        public bool Checkvalues()
        {
            bool isNull = false;

            foreach (var item in SelectedTG.Person.GetType().GetProperties())
            {
                string s = item.Name;
                if (item.Name != "BirthDate" && item.Name != "ValidTo" && item.Name != "PersonID"
                    && item.Name != "Customer" && item.Name != "Tourguide")
                {
                    Decimal parsedValue;

                    if (item.GetValue(SelectedTG.Person) == null)
                    {
                        isNull = true;
                    }
                    else if (Decimal.TryParse(item.GetValue(SelectedTG.Person).ToString(), out parsedValue))
                    {
                        if (parsedValue == 0)
                        {
                            isNull = true;
                        }
                        
                    }
                }

            }
            if (SelectedTG.Taxidentification == 0)
            {
                isNull = true;
            }
            if (SelectedTG.Dailyallowance == 0)
            {
                isNull = true;
            }
            return isNull;
        }

        public void SaveTG()
        {

            if (ResultList != null && ResultList.Contains(SelectedTG))
            {
                if (SelectedLanguage != null)
                {
                    languageRepo.Create(new Language() { TourguideID = SelectedTG.PersonID, Language1 = SelectedLanguage });
                 }
                 if (SelectedHolidayFrom != default(DateTime) && SelectedHolidayTill != default(DateTime))
                 {
                    holidayRepo.Create(new OnHoliday() { StartDate = SelectedHolidayFrom, EndDate = SelectedHolidayTill, TourguideID = SelectedTG.PersonID });
                 }
                 tgBL.Update();
            }
            else
            {
                tgBL.Save(SelectedTG);
                if (SelectedLanguage != null)
                {
                    languageRepo.Create(new Language() { TourguideID = SelectedTG.PersonID, Language1 = SelectedLanguage });
                }
                if (SelectedHolidayFrom != default(DateTime) && SelectedHolidayTill != default(DateTime))
                {
                    holidayRepo.Create(new OnHoliday() { StartDate = SelectedHolidayFrom, EndDate = SelectedHolidayTill, TourguideID = SelectedTG.PersonID });
                }
            }
                    

        }

        public void DeleteTG()
        {
            IQueryable<Language> languages = languageRepo.GetAll();
            List<Language> lList = new List<Language>();
            foreach (var item in languages)
            {
                if (item.TourguideID == SelectedTG.PersonID)
                {
                    lList.Add(item);
                }
            }
            foreach (var item in lList)
            {
                try
                {
                    languageRepo.Delete(item);
                }
                finally { }
            }
            IQueryable<OnHoliday> holidays = holidayRepo.GetAll();
            List<OnHoliday> hList = new List<OnHoliday>();
            foreach (var item in holidays)
            {
                if (item.TourguideID == SelectedTG.PersonID)
                {
                    hList.Add(item);
                }
            }
            foreach (var item in hList)
            {
                try
                {
                    holidayRepo.Delete(item);
                }
                finally { }
            }
            tgBL.Delete(SelectedTG);
        }


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
