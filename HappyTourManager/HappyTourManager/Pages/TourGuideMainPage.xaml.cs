namespace HappyTourManager.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Interaction logic for TourGuideMainPage.xaml
    /// </summary>
    public partial class TourGuideMainPage : Page
    {

        private IRepository<Tourguide> tourGuideRepo;
        private IRepository<Language> languageRepo;
        private IRepository<OnHoliday> holidayRepo;
        private TourGuideMainViewModel tgVM;
        private TGDetailsUC tgDetails;



        public TourGuideMainPage(
                IRepository<Tourguide> tourGuideRepo,
                IRepository<Language> languageRepo,
                IRepository<OnHoliday> holidayRepo
            )
        {
            this.tourGuideRepo = tourGuideRepo;
            this.languageRepo = languageRepo;
            this.holidayRepo = holidayRepo;
            this.InitializeComponent();

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.tgVM = new TourGuideMainViewModel(this.tourGuideRepo,this.languageRepo,this.holidayRepo);
            this.DataContext = this.tgVM;
            this.searchCat.ItemsSource = this.tgVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.tgDetails = new TGDetailsUC();
            foreach (string item in this.tgVM.countryList)
            {
                this.tgDetails.cboxCountry.Items.Add(item);
            }
            this.contTGDetails.Content = this.tgDetails;
            foreach (string item in this.tgVM.languageList)
            {
                this.tgDetails.cboxLanguage.Items.Add(item);
            }
            this.contTGDetails.Visibility = Visibility.Hidden;
        }

        private void searchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tgVM.SelectedCtegory == "ISONHOLIDAY" || this.tgVM.SelectedCtegory == "ISAVAILABLE")
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
            else if (this.tgVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                this.tgVM.SelectedValue = default(string);
            }
            else if (this.tgVM.SelectedCtegory == "LANGUAGE")
            {
                ComboBox cBox = new ComboBox();

                foreach (var item in this.tgVM.languageList)
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
                this.tgVM.SelectedValue = default(string);
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
                this.tgVM.GetSearchResult();
                if (this.tgVM.ResultList.Count > 0)
                {
                    this.btnEdit.Visibility = Visibility.Visible;
                    this.btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    this.btnEdit.Visibility = Visibility.Hidden;
                    this.btnDelete.Visibility = Visibility.Hidden;
                }
                this.contTGDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Missing data");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Wrong data type");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.tgVM.SelectedTG = new Tourguide()
            {
                Person = new Person()
                {
                    BirthDate = DateTime.Today,
                    ValidTo = DateTime.Today
                }
            };
            this.tgVM.SelectedLanguage = null;
            this.tgVM.SelectedHolidayFrom = default(DateTime);
            this.tgVM.SelectedHolidayTill = default(DateTime);
            this.contTGDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.tgVM.Checkvalues())
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    this.tgVM.SaveInstance();
                    MessageBox.Show("Tour Guide data is saved!");
                    this.contTGDetails.Visibility = Visibility.Hidden;
                    this.tgVM.SelectedTG = null;
                    this.tgVM.SelectedHolidayTill = default(DateTime);
                    this.tgVM.SelectedHolidayFrom = default(DateTime);
                    this.tgVM.SelectedLanguage = null;
                    this.btnSave.Visibility = Visibility.Hidden;
                    this.btnCancel.Visibility = Visibility.Hidden;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Missing data");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Wrong data type");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contTGDetails.Visibility = Visibility.Hidden;
            this.tgVM.SelectedTG = null;
            this.tgVM.SelectedHolidayTill = default(DateTime);
            this.tgVM.SelectedHolidayFrom = default(DateTime);
            this.tgVM.SelectedLanguage = null;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.tgVM.SelectedHolidayTill = default(DateTime);
            this.tgVM.SelectedHolidayFrom = default(DateTime);
            this.tgVM.SelectedLanguage = null;
            if (this.tgVM.ResultList.Count > 0 && this.tgVM.SelectedTG != null)
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
            if (this.tgVM.SelectedTG != null)
            {
                if (MessageBox.Show("Do you want to delete the selected Tour guide?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        this.tgVM.DeleteInstance();
                        MessageBox.Show("Tour Guide is deleted");
                        this.tgVM.ResultList.Remove(this.tgVM.SelectedTG);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Missing data");
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Wrong data type");
                    }
                    finally
                    {
                        this.tgVM.SelectedTG = null;
                        this.tgVM.SelectedHolidayTill = default(DateTime);
                        this.tgVM.SelectedHolidayFrom = default(DateTime);
                        this.tgVM.SelectedLanguage = null;
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a Tour guide!");
            }
        }
    }
}

