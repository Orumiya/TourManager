// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    using BL;
    //using HappyTourManager.VM;
    //using Helper;
    using DATA;
    using DATA.Interfaces;
    using DATA.Repositoriees;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// a főmenü page-t indítja
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            //Menu page1 = new Menu(this.mainFrame);
            //this.mainFrame.Content = page1;
            HappyTourDatabaseEntities entities = new HappyTourDatabaseEntities();
            TourguideRepository tourguideRepository = new TourguideRepository(entities);
            LanguageRepository languageRepository = new LanguageRepository(entities);
            OnholidayRepository onHolidayRepository = new OnholidayRepository(entities);
            TourguideBL bl = new TourguideBL(tourguideRepository, languageRepository, onHolidayRepository);
            //var lista = bl.Search(TourguideTerms.Default, null);
            var lista = bl.Search(TourguideTerms.IsOnHoliday, new DateTime[] { new DateTime(2000,07,22), new DateTime(2000,07,30)});
            foreach (var item in lista)
            {
                Console.WriteLine("LastName "+item.Person.LastName + " ");
            }
        }
    }
}
