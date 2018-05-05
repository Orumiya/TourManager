// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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

    class TourGuideMainViewModel: Bindable, IContentPage
    {
        #region private variables
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

        public string SelectedValue
        {
            get
            {
                return this.selectedValue;
            }

            set
            {
                this.selectedValue = value;
                this.OnPropertyChanged(nameof(this.SelectedValue));
            }
        }

        public ObservableCollection<Tourguide> ResultList
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

        public Tourguide SelectedTG
        {
            get
            {
                return this.selectedTG;
            }

            set
            {
                this.selectedTG = value;
                this.OnPropertyChanged(nameof(this.SelectedTG));
            }
        }

        public string SelectedLanguage
        {
            get
            {
                return this.selectedLanguage;
            }

            set
            {
                this.selectedLanguage = value;
                this.OnPropertyChanged(nameof(this.SelectedLanguage));
            }
        }

        public DateTime SelectedHolidayFrom
        {
            get
            {
                return this.selectedHolidayFrom;
            }

            set
            {
                this.selectedHolidayFrom = value;
                this.OnPropertyChanged(nameof(this.SelectedHolidayFrom));
            }
        }

        public DateTime SelectedHolidayTill
        {
            get
            {
                return this.selectedHolidayTill;
            }

            set
            {
                this.selectedHolidayTill = value;
                this.OnPropertyChanged(nameof(this.SelectedHolidayTill));
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
            this.tgBL = new TourguideBL(tourGuideRepo, languageRepo, holidayRepo);
            this.CreateCountryList();

            this.languageList = new List<string>();
            this.languageList.Add("english");
            this.languageList.Add("german");
            this.languageList.Add("french");
            this.languageList.Add("spanish");
            this.languageList.Add("italian");
            this.languageList.Add("dutch");
            this.languageList.Add("chinese");
            this.languageList.Add("japanese");

            this.searchCategories = new List<string>();
            foreach (TourguideTerms item in Enum.GetValues(typeof(TourguideTerms)))
            {
                this.searchCategories.Add(item.ToString());
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
            if (this.SelectedCtegory == "ISONHOLIDAY" || this.SelectedCtegory == "ISAVAILABLE")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = this.SelectedDateFrom;
                dt[1] = this.SelectedDateTo;

                rL = this.tgBL.Search(Enum.Parse(typeof(TourguideTerms), this.SelectedCtegory), dt);
            }
            else if (this.SelectedCtegory == "TAXIDENTIFICATION")
            {
                rL = this.tgBL.Search(Enum.Parse(typeof(TourguideTerms), this.SelectedCtegory), Int32.Parse(this.SelectedValue));
            }
            else
            {
                rL = this.tgBL.Search(Enum.Parse(typeof(TourguideTerms), this.SelectedCtegory), this.SelectedValue);
            }
            this.ResultList = new ObservableCollection<Tourguide>(rL);
        }

        public bool Checkvalues()
        {
            bool isNull = false;

            foreach (var item in this.SelectedTG.Person.GetType().GetProperties())
            {
                string s = item.Name;
                if (item.Name != "BirthDate" && item.Name != "ValidTo" && item.Name != "PersonID"
                    && item.Name != "Customer" && item.Name != "Tourguide")
                {
                    Decimal parsedValue;

                    if (item.GetValue(this.SelectedTG.Person) == null)
                    {
                        isNull = true;
                    }
                    else if (Decimal.TryParse(item.GetValue(this.SelectedTG.Person).ToString(), out parsedValue))
                    {
                        if (parsedValue == 0)
                        {
                            isNull = true;
                        }

                    }
                }

            }
            if (this.SelectedTG.Taxidentification == 0)
            {
                isNull = true;
            }
            if (this.SelectedTG.Dailyallowance == 0)
            {
                isNull = true;
            }
            return isNull;
        }

        public void SaveInstance()
        {

            if (this.ResultList != null && this.ResultList.Contains(this.SelectedTG))
            {
                if (this.SelectedLanguage != null)
                {
                    this.tgBL.CreateLanguage(new Language() { TourguideID = this.SelectedTG.PersonID, Language1 = this.SelectedLanguage });
                 }
                 if (this.SelectedHolidayFrom != default(DateTime) && this.SelectedHolidayTill != default(DateTime))
                 {
                    this.tgBL.CreateHoliday(new OnHoliday() { StartDate = this.SelectedHolidayFrom, EndDate = this.SelectedHolidayTill, TourguideID = this.SelectedTG.PersonID });
                 }
                 this.tgBL.Update();
            }
            else
            {
                this.tgBL.Save(this.SelectedTG);
                if (this.SelectedLanguage != null)
                {
                    this.tgBL.CreateLanguage(new Language() { TourguideID = this.SelectedTG.PersonID, Language1 = this.SelectedLanguage });
                }
                if (this.SelectedHolidayFrom != default(DateTime) && this.SelectedHolidayTill != default(DateTime))
                {
                    this.tgBL.CreateHoliday(new OnHoliday() { StartDate = this.SelectedHolidayFrom, EndDate = this.SelectedHolidayTill, TourguideID = this.SelectedTG.PersonID });
                }
            }

        }

        public void DeleteInstance()
        {
            IList<Language> languages = this.tgBL.GetAllLanguages();
            List<Language> lList = new List<Language>();
            foreach (var item in languages)
            {
                if (item.TourguideID == this.SelectedTG.PersonID)
                {
                    lList.Add(item);
                }
            }
            foreach (var item in lList)
            {
                try
                {
                    this.tgBL.DeleteLanguage(item);
                }
                finally { }
            }
            IList<OnHoliday> holidays = this.tgBL.GetAllHolidays();
            List<OnHoliday> hList = new List<OnHoliday>();
            foreach (var item in holidays)
            {
                if (item.TourguideID == this.SelectedTG.PersonID)
                {
                    hList.Add(item);
                }
            }
            foreach (var item in hList)
            {
                try
                {
                    this.tgBL.DeleteHoliday(item);
                }
                finally { }
            }
            this.tgBL.Delete(this.SelectedTG);
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

            this.countryList = countryNames.OrderBy(names => names).Distinct();
        }
        #endregion
    }
}
