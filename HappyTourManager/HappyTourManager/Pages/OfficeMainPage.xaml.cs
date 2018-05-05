// <copyright file="OfficeMainPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using DATA;
    using DATA.Interfaces;

    /// <summary>
    /// Interaction logic for OfficeMainPage.xaml
    /// </summary>
    public partial class OfficeMainPage : Page
    {
        private OfficeMainViewModel officeVM;
        private IRepository<Office> officeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeMainPage"/> class.
        /// Office page
        /// </summary>
        /// <param name="officeRepository">office</param>
        public OfficeMainPage(IRepository<Office> officeRepository)
        {
            this.officeRepository = officeRepository;
            this.InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.officeVM.SaveInstance();
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("Under Construction");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.officeVM.CurrentOffice = new DATA.Office();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.officeVM = new OfficeMainViewModel(this.officeRepository);
            this.officeVM.CurrentOffice = new DATA.Office();
            this.DataContext = this.officeVM;
        }
    }
}
