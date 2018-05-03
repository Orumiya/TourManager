using DATA;
using DATA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyTourManager.Pages
{
    /// <summary>
    /// Interaction logic for TourGuideMainPage.xaml
    /// </summary>
    public partial class TourGuideMainPage : Page
    {
        #region private variables
        private IRepository<Tourguide> tourGuideRepo;
        private IRepository<Language> languageRepo;
        private IRepository<OnHoliday> holidayRepo;
        private IRepository<Tour> tourRepo;
        private TourGuideMainViewModel tgVM;
        private TGDetailsUC tgDetails;
        #endregion

        #region constructor
        public TourGuideMainPage(
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
            InitializeComponent();

            
            
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tgVM = new TourGuideMainViewModel();
            this.DataContext = tgVM;
            //this.searchCat.ItemsSource = tgVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            tgDetails = new TGDetailsUC();
            //foreach (string item in tgVM.countryList)
            //{
            //    tgDetails.cboxCountry.Items.Add(item);
            //}
            this.contTGDetails.Content = tgDetails;
            this.contTGDetails.Visibility = Visibility.Hidden;
        }
    }
}
