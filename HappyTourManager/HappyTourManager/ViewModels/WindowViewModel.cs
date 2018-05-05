// <copyright file="WindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// View Model for the basic window
    /// </summary>
    internal class WindowViewModel : Bindable
    {
        private Window window;

        private int outerMarginsize = 10;
        private int windowRadius = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowViewModel"/> class.
        /// Construnctor
        /// </summary>
        /// <param name="window"> window</param>
        public WindowViewModel(Window window)
        {
            this.window = window;
            window.MaxHeight = SystemParameters.WorkArea.Height;
            window.StateChanged += this.Window_StateChanged;

            this.MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            this.MaximizeCommand = new RelayCommand(() => window.WindowState = WindowState.Maximized);
            this.CloseCommand = new RelayCommand(() => window.Close());
        }

        /// <summary>
        /// Gets or sets resizeBorder property
        /// </summary>
        public int ResizeBorder { get; set; } = 5;

        /// <summary>
        /// Gets gets or sets ResizeBorderThickness property
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get { return new Thickness(this.ResizeBorder + this.OuterMarginSize); }
        }

        /// <summary>
        ///  Gets or sets Outer property
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return this.window.WindowState == WindowState.Maximized ? 0 : this.outerMarginsize;
            }

            set
            {
                this.outerMarginsize = value;
            }
        }

        /// <summary>
        /// Gets gets or sets
        /// </summary>
        public Thickness OuterMarginThickness
        {
            get { return new Thickness(this.OuterMarginSize); }
        }

        /// <summary>
        /// Gets or sets property
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return this.window.WindowState == WindowState.Maximized ? 0 : this.windowRadius;
            }

            set
            {
                this.windowRadius = value;
            }
        }

        /// <summary>
        /// Gets gets or sets property
        /// </summary>
        public CornerRadius WCornerRadius
        {
            get { return new CornerRadius(this.WindowRadius); }
        }

        /// <summary>
        /// Gets or sets property
        /// </summary>
        public int TitleHeight { get; set; } = 40;

        /// <summary>
        /// Gets gets or sets property
        /// </summary>
        public GridLength TitleHeightGL
        {
            get { return new GridLength(this.TitleHeight + this.ResizeBorder); }
        }

        /// <summary>
        /// Gets or sets property
        /// </summary>
        public string ActualPage { get; set; } = "LoginPage";

        // Commands

        /// <summary>
        /// Gets or sets minimize Window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Gets or sets maximize Window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Gets or sets close Window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged("ResizeBorderThickness");
            this.OnPropertyChanged("OuterMarginSize");
            this.OnPropertyChanged("OuterMarginThickness");
            this.OnPropertyChanged("WindowRadius");
            this.OnPropertyChanged("WCornerRadius");
        }
    }
}
