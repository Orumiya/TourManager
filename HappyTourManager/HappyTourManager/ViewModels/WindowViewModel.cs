namespace HappyTourManager
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// View Model for the basic window
    /// </summary>
    class WindowViewModel : Bindable
    {
        private Window window;

        private int outerMarginsize = 10;
        private int windowRadius = 10;

        public string Test { get; set; } = "My string";

        public int ResizeBorder { get; set; } = 5;

        public Thickness ResizeBorderThickness { get { return new Thickness(this.ResizeBorder + this.OuterMarginSize); } }

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

        public Thickness OuterMarginThickness { get { return new Thickness(this.OuterMarginSize); } }

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

        public CornerRadius WCornerRadius { get { return new CornerRadius(this.WindowRadius); } }

        public int TitleHeight { get; set; } = 40;

        public GridLength TitleHeightGL { get { return new GridLength(this.TitleHeight + this.ResizeBorder); } }

        public string ActualPage { get; set; } = "LoginPage";

        /// <summary>
        /// Construnctor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            this.window = window;
            window.MaxHeight = SystemParameters.WorkArea.Height;
            window.StateChanged += this.Window_StateChanged;

            this.MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            this.MaximizeCommand = new RelayCommand(() => window.WindowState = WindowState.Maximized);
            this.CloseCommand = new RelayCommand(() => window.Close());

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged("ResizeBorderThickness");
            this.OnPropertyChanged("OuterMarginSize");
            this.OnPropertyChanged("OuterMarginThickness");
            this.OnPropertyChanged("WindowRadius");
            this.OnPropertyChanged("WCornerRadius");
        }

        // Commands

        /// <summary>
        /// Minimize Window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Maximize Window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Close Window
        /// </summary>
        public ICommand CloseCommand { get; set; }

    }
}
