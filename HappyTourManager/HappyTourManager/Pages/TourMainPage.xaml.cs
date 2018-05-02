﻿using DATA;
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
        private IRepository<Place> placeRepo;
        private IRepository<PLTCON> pltconRepo;
        private IRepository<Program> programRepo;
        private IRepository<PRTCON> prtconRepo;
        private IRepository<Tourguide> tourguideRepo;
        private IRepository<Tour> tourRepo;

        TourMainViewModel tourVM;
        TourDetailsUC tourDetail;

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
            tourVM.SelectedTour = new Tour()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            tourVM.SelectedPlace = new Place();
            tourVM.SelectedProgram = new Program();
            this.contTourDetails.Visibility = Visibility.Visible;
            this.btnSave.Visibility = Visibility.Visible;
            this.btnCancel.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (custVM.Checkvalues())
            //    {
            //        MessageBox.Show("All values must be filled in!");
            //    }
            //    else
            //    {
            //        custVM.SaveCustomer();
            //        MessageBox.Show("Customer data is saved!");
            //        this.contCustDetails.Visibility = Visibility.Hidden;
            //        custVM.SelectedCustomer = null;
            //        this.btnSave.Visibility = Visibility.Hidden;
            //        this.btnCancel.Visibility = Visibility.Hidden;
            //    }
            //}
            //catch (InvalidOperationException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Something went wrong :(");
            //}


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.contCustDetails.Visibility = Visibility.Hidden;
            //custVM.SelectedCustomer = null;
            //this.btnSave.Visibility = Visibility.Hidden;
            //this.btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //if (custVM.ResultList.Count > 0 && custVM.SelectedCustomer != null)
            //{
            //    this.contCustDetails.Visibility = Visibility.Visible;
            //    this.btnSave.Visibility = Visibility.Visible;
            //    this.btnCancel.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    this.contCustDetails.Visibility = Visibility.Hidden;
            //    this.btnSave.Visibility = Visibility.Hidden;
            //    this.btnCancel.Visibility = Visibility.Hidden;
            //}
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //if (custVM.SelectedCustomer != null)
            //{
            //    if (MessageBox.Show("Do you want to delete customer?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        custVM.DeleteCustomer();
            //        MessageBox.Show("Customer is deleted");
            //        custVM.ResultList.Remove(custVM.SelectedCustomer);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please select a customer!");
            //}
        }
    }
}
