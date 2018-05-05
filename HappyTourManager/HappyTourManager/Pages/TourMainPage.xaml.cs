// <copyright file="TourMainPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Interaction logic for TourMainPage.xaml
    /// </summary>
    ///
    public partial class TourMainPage : Page
    {
        private IRepository<Place> placeRepo;
        private IRepository<PLTCON> pltconRepo;
        private IRepository<Program> programRepo;
        private IRepository<PRTCON> prtconRepo;
        private IRepository<Tourguide> tourguideRepo;
        private IRepository<Tour> tourRepo;
        private TourMainViewModel tourVM;
        private TourDetailsUC tourDetail;

        /// <summary>
        /// Initializes a new instance of the <see cref="TourMainPage"/> class.
        /// tour main page
        /// </summary>
        /// <param name="tourRepo">tour</param>
        /// <param name="placeRepo">place</param>
        /// <param name="pltconRepo">pltcon</param>
        /// <param name="programRepo">program</param>
        /// <param name="prtconRepo">prtcon</param>
        /// <param name="tourguideRepo">tourguide</param>
        public TourMainPage(
            IRepository<Tour> tourRepo,
            IRepository<Place> placeRepo,
            IRepository<PLTCON> pltconRepo,
            IRepository<Program> programRepo,
            IRepository<PRTCON> prtconRepo,
            IRepository<Tourguide> tourguideRepo)
        {
            this.InitializeComponent();
            this.tourRepo = tourRepo;
            this.placeRepo = placeRepo;
            this.pltconRepo = pltconRepo;
            this.programRepo = programRepo;
            this.prtconRepo = prtconRepo;
            this.tourguideRepo = tourguideRepo;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.tourVM = new TourMainViewModel(this.tourRepo, this.placeRepo, this.pltconRepo, this.programRepo, this.prtconRepo, this.tourguideRepo);

            this.searchCat.ItemsSource = this.tourVM.SearchCategories;
            this.searchCat.Visibility = Visibility.Visible;
            this.searchCat.SelectedItem = "DEFAULT";
            this.DataContext = this.tourVM;
            this.tourDetail = new TourDetailsUC();
            foreach (string item in this.tourVM.CountryList)
            {
                this.tourDetail.cboxCountry.Items.Add(item);
            }

            this.contTourDetails.Content = this.tourDetail;
            this.contTourDetails.Visibility = Visibility.Hidden;
        }

        private void SearchCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tourVM.SelectedCtegory == "TOURDATE")
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
            else if (this.tourVM.SelectedCtegory == "DEFAULT")
            {
                this.contSearch1.Content = null;
                this.contSearch2.Content = null;
                this.tourVM.SelectedValue1 = default(string);
            }
            else if (this.tourVM.SelectedCtegory == "ADULTPRICE" || this.tourVM.SelectedCtegory == "CHILDPRICE")
            {
                this.tourVM.SelectedValue1 = string.Empty;
                this.tourVM.SelectedValue2 = string.Empty;
                TextBox textbox = new TextBox();
                Binding binding = new Binding("SelectedValue1");
                textbox.SetBinding(TextBox.TextProperty, binding);
                this.contSearch1.Content = textbox;

                TextBox textbox2 = new TextBox();
                Binding binding2 = new Binding("SelectedValue2");
                textbox2.SetBinding(TextBox.TextProperty, binding2);
                this.contSearch2.Content = textbox2;
            }
            else if (this.tourVM.SelectedCtegory == "COUNTRY")
            {
                ComboBox cBox = new ComboBox();

                foreach (var item in this.tourVM.PlaceListAll)
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
            else if (this.tourVM.SelectedCtegory == "CITY")
            {
                ComboBox cBox = new ComboBox();
                foreach (var item in this.tourVM.PlaceListAll)
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
            else if (this.tourVM.SelectedCtegory == "PROGRAM")
            {
                ComboBox cBox = new ComboBox();
                foreach (var item in this.tourVM.ProgramListAll)
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

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.tourVM.GetSearchResult();
                if (this.tourVM.ResultList.Count > 0)
                {
                    this.btnEdit.Visibility = Visibility.Visible;
                    this.btnDelete.Visibility = Visibility.Visible;
                }
                else
                {
                    this.btnEdit.Visibility = Visibility.Hidden;
                    this.btnDelete.Visibility = Visibility.Hidden;
                }

                this.contTourDetails.Visibility = Visibility.Hidden;
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
            this.tourVM.IsEdit = true;
            this.tourVM.SelectedTour = new Tour()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            this.tourVM.SelectedPlace = new Place();
            this.tourVM.SelectedProgram = new Program();
            this.tourVM.GetTourPlaces();
            this.tourVM.GetTourPrograms();
            this.tourVM.AdultP = 0;
            this.tourVM.ChildP = 0;
            this.contTourDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.tourVM.Checkvalues(this.tourDetail.tcTour.SelectedIndex))
                {
                    MessageBox.Show("All values must be filled in!");
                }
                else
                {
                    this.tourVM.SaveInstance(this.tourDetail.tcTour.SelectedIndex);
                    MessageBox.Show("Data is saved!");
                    this.contTourDetails.Visibility = Visibility.Hidden;
                    this.tourVM.SelectedTour = null;
                    this.tourVM.SelectedPlace = null;
                    this.tourVM.SelectedProgram = null;
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.contTourDetails.Visibility = Visibility.Hidden;
            this.tourVM.SelectedTour = null;
            this.tourVM.SelectedPlace = null;
            this.tourVM.SelectedProgram = null;
            this.btnSave.Visibility = Visibility.Hidden;
            this.btnCancel.Visibility = Visibility.Hidden;
            this.tourVM.AdultP = 0;
            this.tourVM.ChildP = 0;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.tourVM.IsEdit = false;
            this.tourVM.SelectedPlace = null;
            this.tourVM.SelectedProgram = null;
            if (this.tourVM.ResultList.Count > 0 && this.tourVM.SelectedTour != null)
            {
                this.tourVM.GetTourPlaces();
                this.tourVM.GetTourPrograms();
                this.contTourDetails.Visibility = Visibility.Visible;
                this.btnSave.Visibility = Visibility.Visible;
                this.btnCancel.Visibility = Visibility.Visible;
            }
            else
            {
                this.contTourDetails.Visibility = Visibility.Hidden;
                this.btnSave.Visibility = Visibility.Hidden;
                this.btnCancel.Visibility = Visibility.Hidden;
                MessageBox.Show("Please select a tour!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.tourVM.SelectedTour != null)
            {
                if (MessageBox.Show("Do you want to delete tour?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        this.tourVM.DeleteInstance();
                        MessageBox.Show("Customer is deleted");
                        this.tourVM.ResultList.Remove(this.tourVM.SelectedTour);
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
            }
            else
            {
                MessageBox.Show("Please select a tour!");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tourVM.AdultP = 0;
            this.tourVM.ChildP = 0;
            if (this.tourVM.SelectedTour != null)
            {
                this.tourVM.GetTourPlaces();
                this.tourVM.GetTourPrograms();
                this.tourVM.AdultP = this.tourVM.SelectedTour.AdultPrice;
                this.tourVM.ChildP = this.tourVM.SelectedTour.ChildPrice;
            }
        }
    }
}
