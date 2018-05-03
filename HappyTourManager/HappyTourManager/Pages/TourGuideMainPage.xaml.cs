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


        #region event handlers
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tgVM = new TourGuideMainViewModel(tourGuideRepo,languageRepo,holidayRepo,tourRepo);
            this.DataContext = tgVM;
            this.searchCat.ItemsSource = tgVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            tgDetails = new TGDetailsUC();
            foreach (string item in tgVM.countryList)
            {
                tgDetails.cboxCountry.Items.Add(item);
            }
            this.contTGDetails.Content = tgDetails;
            foreach (string item in tgVM.languageList)
            {
                tgDetails.cboxLanguage.Items.Add(item);
            }
            this.contTGDetails.Visibility = Visibility.Hidden;
        }

        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tgVM.SelectedCtegory == "ISONHOLIDAY" || tgVM.SelectedCtegory == "ISAVAILABLE")
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
            else if (tgVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                tgVM.SelectedValue = default(string);
            }
            else if (tgVM.SelectedCtegory == "LANGUAGE")
            {
                ComboBox cBox = new ComboBox();

                foreach (var item in tgVM.languageList)
                {
                    cBox.Items.Add(item);
                }
                Binding binding = new Binding("SelectedValue");
                cBox.SetBinding(ComboBox.SelectedItemProperty, binding);
                this.contSearch1.Content = cBox;
                this.contSearch2.Content = null;
            }
            else
            {
                tgVM.SelectedValue = default(string);
                TextBox textbox = new TextBox();
                Binding binding = new Binding("SelectedValue");
                textbox.SetBinding(TextBox.TextProperty, binding);
                this.contSearch1.Content = textbox;
                this.contSearch2.Content = null;
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tgVM.GetSearchResult();
                if (tgVM.ResultList.Count > 0)
                {
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    btnEdit.Visibility = Visibility.Hidden;
                    btnDelete.Visibility = Visibility.Hidden;
                }
                this.contTGDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("The searchvalue has an incorrect datatype!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tgVM.SelectedTG = new Tourguide()
            {
                Person = new Person()
                {
                    BirthDate = DateTime.Today,
                    ValidTo = DateTime.Today
                }
            };
            tgVM.SelectedLanguage = null;
            tgVM.SelectedHolidayFrom = default(DateTime);
            tgVM.SelectedHolidayTill = default(DateTime);
            this.contTGDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tgVM.Checkvalues())
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    tgVM.SaveTG();
                    MessageBox.Show("Tour Guide data is saved!");
                    this.contTGDetails.Visibility = Visibility.Hidden;
                    tgVM.SelectedTG = null;
                    tgVM.SelectedHolidayTill = default(DateTime);
                    tgVM.SelectedHolidayFrom = default(DateTime);
                    tgVM.SelectedLanguage = null;
                    this.btnSave.Visibility = Visibility.Hidden;
                    this.btnCancel.Visibility = Visibility.Hidden;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong :(");
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contTGDetails.Visibility = Visibility.Hidden;
            tgVM.SelectedTG = null;
            tgVM.SelectedHolidayTill = default(DateTime);
            tgVM.SelectedHolidayFrom = default(DateTime);
            tgVM.SelectedLanguage = null;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            tgVM.SelectedHolidayTill = default(DateTime);
            tgVM.SelectedHolidayFrom = default(DateTime);
            tgVM.SelectedLanguage = null;
            if (tgVM.ResultList.Count > 0 && tgVM.SelectedTG != null)
            {
                this.contTGDetails.Visibility = Visibility.Visible;
                this.btnSave.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.contTGDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
                MessageBox.Show("Please select a Tour guide!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tgVM.SelectedTG != null)
            {
                if (MessageBox.Show("Do you want to delete the selected Tour guide?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        tgVM.DeleteTG();
                        MessageBox.Show("Tour Guide is deleted");
                        tgVM.ResultList.Remove(tgVM.SelectedTG);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Tour Guide cannot be deleted!");
                    }
                    finally
                    {
                        tgVM.SelectedTG = null;
                        tgVM.SelectedHolidayTill = default(DateTime);
                        tgVM.SelectedHolidayFrom = default(DateTime);
                        tgVM.SelectedLanguage = null;
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Please select a Tour guide!");
            }
        }
        #endregion
    }
}

