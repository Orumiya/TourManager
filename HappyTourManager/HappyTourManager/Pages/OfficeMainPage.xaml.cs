namespace HappyTourManager.Pages
{
    using DATA;
    using DATA.Interfaces;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for OfficeMainPage.xaml
    /// </summary>
    public partial class OfficeMainPage : Page
    {
        private OfficeMainViewModel officeVM;
        private IRepository<Office> officeRepository;

        public OfficeMainPage(IRepository<Office> officeRepository)
        {
            this.officeRepository = officeRepository;
            this.InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.officeVM.CurrentOffice = new DATA.Office();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.officeVM = new OfficeMainViewModel(officeRepository);
            this.officeVM.CurrentOffice = new DATA.Office();
            this.DataContext = this.officeVM;
        }
    }
}
