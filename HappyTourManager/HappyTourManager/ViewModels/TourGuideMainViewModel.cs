// <copyright file="TourGuideMainViewModel.cs" company="PlaceholderCompany">
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
    /// Tourguide viewmodel
    /// </summary>
    internal class TourGuideMainViewModel : Bindable, IContentPage
    {
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
        private IEnumerable<string> countryList;
        private List<string> languageList;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourGuideMainViewModel"/> class.
        /// constructor for Tourguide view model
        /// </summary>
        /// <param name="tourGuideRepo">tourGuideRepo</param>
        /// <param name="languageRepo">languageRepo</param>
        /// <param name="holidayRepo">holidayRepo</param>
        public TourGuideMainViewModel(
            IRepository<Tourguide> tourGuideRepo,
                IRepository<Language> languageRepo,
                IRepository<OnHoliday> holidayRepo)
        {
            this.tgBL = new TourguideBL(tourGuideRepo, languageRepo, holidayRepo);
            this.CreateCountryList();

            this.LanguageList = new List<string>();
            this.LanguageList.Add("english");
            this.LanguageList.Add("german");
            this.LanguageList.Add("french");
            this.LanguageList.Add("spanish");
            this.LanguageList.Add("italian");
            this.LanguageList.Add("dutch");
            this.LanguageList.Add("chinese");
            this.LanguageList.Add("japanese");

            this.searchCategories = new List<string>();
            foreach (TourguideTerms item in Enum.GetValues(typeof(TourguideTerms)))
            {
                this.searchCategories.Add(item.ToString());
            }
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
        /// Gets or sets date search value
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
        /// Gets or sets date search value
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
        /// Gets or sets string search value
        /// </summary>
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

        /// <summary>
        /// Gets or sets list of search result
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected tourguide
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected language
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected holiday
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected holiday
        /// </summary>
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

        /// <summary>
        /// Gets or sets propertx
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
        /// Gets or sets property
        /// </summary>
        public List<string> LanguageList
        {
            get
            {
                return this.languageList;
            }

            set
            {
                this.languageList = value;
            }
        }

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
                rL = this.tgBL.Search(Enum.Parse(typeof(TourguideTerms), this.SelectedCtegory), int.Parse(this.SelectedValue));
            }
            else
            {
                rL = this.tgBL.Search(Enum.Parse(typeof(TourguideTerms), this.SelectedCtegory), this.SelectedValue);
            }

            this.ResultList = new ObservableCollection<Tourguide>(rL);
        }

        /// <summary>
        /// check if form is filled in correctly
        /// </summary>
        /// <returns> true if values ok</returns>
        public bool Checkvalues()
        {
            bool isNull = false;

            foreach (var item in this.SelectedTG.Person.GetType().GetProperties())
            {
                if (item.Name != "BirthDate" && item.Name != "ValidTo" && item.Name != "PersonID"
                    && item.Name != "Customer" && item.Name != "Tourguide")
                {
                    decimal parsedValue;

                    if (item.GetValue(this.SelectedTG.Person) == null)
                    {
                        isNull = true;
                    }
                    else if (decimal.TryParse(item.GetValue(this.SelectedTG.Person).ToString(), out parsedValue))
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

        /// <summary>
        /// save item or update if it exists
        /// </summary>
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

        /// <summary>
        /// delete item
        /// </summary>
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
                finally
                {
                }
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
                finally
                {
                }
            }

            this.tgBL.Delete(this.SelectedTG);
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
    }
}
