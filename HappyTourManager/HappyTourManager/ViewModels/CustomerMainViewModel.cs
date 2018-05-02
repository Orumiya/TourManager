using BL;
using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HappyTourManager
{
    class CustomerMainViewModel : Bindable
    {

        #region private variables
        private IRepository<Customer> custRepository;
        private CustomerBL custBL;

        private string selectedCtegory = "DEFAULT";
        private string selectedValue;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private IList<Customer> rL;
        private ObservableCollection<Customer> resultList;
        private Customer selectedCustomer;

        private List<string> searchCategories;

        public IEnumerable<string> countryList;
        #endregion

        #region Parameters
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

        public ObservableCollection<Customer> ResultList
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

        public Customer SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }

            set
            {
                selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
        #endregion


        #region constructor
        public CustomerMainViewModel(IRepository<Customer> custRepository)
        {
            this.custRepository = custRepository;
            custBL = new CustomerBL(custRepository);
            CreateCountryList();

            searchCategories = new List<string>();
            foreach (CustomerTerms item in Enum.GetValues(typeof(CustomerTerms)))
            {
                searchCategories.Add(item.ToString());
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Get the search result list
        /// </summary>
        public void GetSearchResult()
        {
            if (SelectedCtegory == "VALIDTO")
            {
                DateTime[] dt = new DateTime[2];
                dt[0] = SelectedDateFrom;
                dt[1] = SelectedDateTo;

                rL = custBL.Search(Enum.Parse(typeof(CustomerTerms), SelectedCtegory), dt);
            }
            else
            {
                rL = custBL.Search(Enum.Parse(typeof(CustomerTerms), SelectedCtegory), SelectedValue);
            }
            ResultList = new ObservableCollection<Customer>(rL);
        }

        /// <summary>
        /// Save a new customer or update an existing one
        /// </summary>
        public void SaveCustomer()
        {
            if (ResultList != null && ResultList.Contains(SelectedCustomer))
            {
                custBL.Update();
            }
            else
            {
                custBL.Save(SelectedCustomer);
            }
        }

        /// <summary>
        /// Delete selected customer
        /// </summary>
        public void DeleteCustomer()
        {
            custBL.Delete(SelectedCustomer);
        }

        /// <summary>
        /// Check if all mandatory value is filled in
        /// </summary>
        /// <returns></returns>
        public bool Checkvalues()
        {
            bool isNull = false;

            foreach (var item in SelectedCustomer.Person.GetType().GetProperties())
            {
                string s = item.Name;
                if (item.Name != "BirthDate" && item.Name != "ValidTo" && item.Name != "PersonID"
                    && item.Name != "Customer" && item.Name != "Tourguide")
                {
                    Decimal parsedValue;

                    if (item.GetValue(SelectedCustomer.Person) == null)
                    {
                        isNull = true;
                    }
                    else if (Decimal.TryParse(item.GetValue(SelectedCustomer.Person).ToString(), out parsedValue))
                    {
                        isNull = parsedValue == 0;
                    }
                }

            }
            return isNull;
        }
        #endregion

        #region private methods
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
