using BL;
using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTourManager
{
    class CustomerMainViewModel : Bindable
    {
        private IRepository<Customer> custRepository;
        private CustomerBL custBL;

        private List<string> searchCategories;

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

        public IList<Customer> ResultList
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
                OnPropertyChanged(nameof(ResultList));
            }
        }

        private string selectedCtegory = "DEFAULT";
        private string selectedValue;
        private DateTime selectedDateFrom = DateTime.Today;
        private DateTime selectedDateTo = DateTime.Today;
        private IList<Customer> resultList;
        private Customer selectedCustomer;
        

        public CustomerMainViewModel(IRepository<Customer> custRepository)
        {
            this.custRepository = custRepository;
            custBL = new CustomerBL(custRepository);

            searchCategories = new List<string>();
            foreach (CustomerTerms item in Enum.GetValues(typeof(CustomerTerms)))
            {
                searchCategories.Add(item.ToString());
            }
        }

        public void GetSearchResult()
        {
            if (SelectedCtegory == "VALIDTO")
            {
                DateTime[] dt = new DateTime[1];
                dt[0] = SelectedDateFrom;
                dt[1] = SelectedDateTo;

                ResultList = custBL.Search(SelectedCtegory, dt);
            }
            else
            {
                ResultList = custBL.Search(SelectedCtegory, SelectedValue);
            }
        }



    }
}
