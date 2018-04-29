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
    //using HappyTourManager.VM;
    //using Helper;
    using DATA;
    using DATA.Repositories;
    using DATA.Interfaces;
    using Pages;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// a főmenü page-t indítja
        /// </summary>
        /// 

        LoginPage loginPage;
        MainPage mainPage;

        HappyTourDatabaseEntities entities;
        public MainWindow()
        {
            this.InitializeComponent();
            this.entities = new HappyTourDatabaseEntities();
            SetPage("MainPage");

            this.DataContext = new WindowViewModel(this);
        }

        public void SetPage(string pagetype)
        {
            if (pagetype == "LoginPage")
            {
                loginPage = new LoginPage(entities, this);
                this.MainFrame.Content = loginPage;
            }
            else
            {
                mainPage = new MainPage(entities, this);
                this.MainFrame.Content = mainPage;
            }
        }
    }
}
