using DATA;
using DATA.Interfaces;
using DATA.Repositoriees;
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
    /// Interaction logic for TourMainPage.xaml
    /// </summary>
    /// 
    public partial class TourMainPage : Page
    {
        #region private variables
        private IRepository<Place> placeRepo;
        private IRepository<PLTCON> pltconRepo;
        private IRepository<Program> programRepo;
        private IRepository<PRTCON> prtconRepo;
        private IRepository<Tourguide> tourguideRepo;
        private IRepository<Tour> tourRepo;

        private TourMainViewModel tourVM;
        private TourDetailsUC tourDetail;
        #endregion

        #region constructor
        public TourMainPage(IRepository<Tour> tourRepo,
            IRepository<Place> placeRepo,
            IRepository<PLTCON> pltconRepo,
            IRepository<Program> programRepo,
            IRepository<PRTCON> prtconRepo,
            IRepository<Tourguide> tourguideRepo)
        {
            InitializeComponent();
            this.tourRepo = tourRepo;
            this.placeRepo = placeRepo;
            this.pltconRepo = pltconRepo;
            this.programRepo = programRepo;
            this.prtconRepo = prtconRepo;
            this.tourguideRepo = tourguideRepo;
        }
        #endregion


        #region event handlers
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tourVM = new TourMainViewModel(tourRepo,placeRepo,pltconRepo,programRepo,prtconRepo,tourguideRepo);

            this.searchCat.ItemsSource = tourVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = tourVM;
            tourDetail = new TourDetailsUC();
            foreach (string item in tourVM.countryList)
            {
                tourDetail.cboxCountry.Items.Add(item);
            }
            this.contTourDetails.Content = tourDetail;
            this.contTourDetails.Visibility = Visibility.Hidden;
            
        }

        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tourVM.SelectedCtegory == "TOURDATE")
            {
                DatePicker datePicker = new DatePicker();

                Binding binding = new Binding("SelectedDateFrom");
                datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);
                this.contSearch1.Content = datePicker;

                DatePicker datePicker2 = new DatePicker();
                Binding binding2 = new Binding("SelectedDateTo");
                datePicker2.SetBinding(DatePicker.SelectedDateProperty, binding2);
                this.contSearch2.Content = datePicker2;

            }
            else if (tourVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                tourVM.SelectedValue1 = default(string);
            }
            else if (tourVM.SelectedCtegory == "ADULTPRICE" || tourVM.SelectedCtegory == "CHILDPRICE")
            {
                tourVM.SelectedValue1 = "";
                tourVM.SelectedValue2 = "";
                TextBox textbox = new TextBox();
                Binding binding = new Binding("SelectedValue1");
                textbox.SetBinding(TextBox.TextProperty, binding);
                this.contSearch1.Content = textbox;

                TextBox textbox2 = new TextBox();
                Binding binding2 = new Binding("SelectedValue2");
                textbox2.SetBinding(TextBox.TextProperty, binding2);
                this.contSearch2.Content = textbox2;
            }
            else if (tourVM.SelectedCtegory == "COUNTRY")
            {
                ComboBox cBox = new ComboBox();

                foreach (var item in tourVM.PlaceListAll)
                {
                    if (!cBox.Items.Contains(item.Country))
                    {
                        cBox.Items.Add(item.Country);
                    }
                }
                Binding binding = new Binding("SelectedValue1");
                cBox.SetBinding(ComboBox.SelectedItemProperty, binding);
                this.contSearch1.Content = cBox;
                this.contSearch2.Content = null;
            }
            else if (tourVM.SelectedCtegory == "CITY")
            {
                ComboBox cBox = new ComboBox();
                foreach (var item in tourVM.PlaceListAll)
                {
                    if (!cBox.Items.Contains(item.City))
                    {
                        cBox.Items.Add(item.City);
                    }
                }
                Binding binding = new Binding("SelectedValue1");
                cBox.SetBinding(ComboBox.SelectedItemProperty, binding);
                this.contSearch1.Content = cBox;
                this.contSearch2.Content = null;
            }
            else if (tourVM.SelectedCtegory == "PROGRAM")
            {
                ComboBox cBox = new ComboBox();
                foreach (var item in tourVM.ProgramListAll)
                {
                    if (!cBox.Items.Contains(item.ProgramType))
                    {
                        cBox.Items.Add(item.ProgramType);
                    }
                }
                Binding binding = new Binding("SelectedValue1");
                cBox.SetBinding(ComboBox.SelectedItemProperty, binding);
                this.contSearch1.Content = cBox;
                this.contSearch2.Content = null;
            }
            else
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tourVM.GetSearchResult();
                if (tourVM.ResultList.Count > 0)
                {
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    btnEdit.Visibility = Visibility.Hidden;
                    btnDelete.Visibility = Visibility.Hidden;
                }
                this.contTourDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tourVM.IsEdit = true;
            tourVM.SelectedTour = new Tour()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            tourVM.SelectedPlace = new Place();
            tourVM.SelectedProgram = new Program();
            tourVM.GetTourPlaces();
            tourVM.GetTourPrograms();
            this.contTourDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tourVM.Checkvalues(tourDetail.tcTour.SelectedIndex))
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    tourVM.SaveTour(tourDetail.tcTour.SelectedIndex);
                    MessageBox.Show("Data is saved!");
                    this.contTourDetails.Visibility = Visibility.Hidden;
                    tourVM.SelectedTour = null;
                    tourVM.SelectedPlace = null;
                    tourVM.SelectedProgram = null;
                    this.btnSave.Visibility = Visibility.Hidden;
                    this.btnCancel.Visibility = Visibility.Hidden;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong :(");
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contTourDetails.Visibility = Visibility.Hidden;
            tourVM.SelectedTour = null;
            tourVM.SelectedPlace = null;
            tourVM.SelectedProgram = null;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            tourVM.IsEdit = false;
            tourVM.SelectedPlace = null;
            tourVM.SelectedProgram = null;
            if (tourVM.ResultList.Count > 0 && tourVM.SelectedTour != null)
            {
                tourVM.GetTourPlaces();
                tourVM.GetTourPrograms();
                this.contTourDetails.Visibility = Visibility.Visible;
                this.btnSave.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.contTourDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tourVM.SelectedTour != null)
            {
                if (MessageBox.Show("Do you want to delete tour?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        tourVM.DeleteTour();
                        MessageBox.Show("Customer is deleted");
                        tourVM.ResultList.Remove(tourVM.SelectedTour);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Tour cannot be deleted!");
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a customer!");
            }
        }
        #endregion
    }
}
