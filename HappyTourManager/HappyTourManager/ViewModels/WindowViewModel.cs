using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HappyTourManager
{
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

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public int OuterMarginSize
        {
            get
            {
                return window.WindowState == WindowState.Maximized ? 0 : outerMarginsize;
            }

            set
            {
                outerMarginsize = value;
            }
        }

        public Thickness OuterMarginThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return window.WindowState == WindowState.Maximized ? 0 : windowRadius;
            }

            set
            {
                windowRadius = value;
            }
        }

        public CornerRadius WCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 40;

        public GridLength TitleHeightGL { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public string ActualPage { get; set; } = "LoginPage";

        /// <summary>
        /// Construnctor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            this.window = window;

            window.StateChanged += Window_StateChanged;

            MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => window.Close());

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("ResizeBorderThickness");
            OnPropertyChanged("OuterMarginSize");
            OnPropertyChanged("OuterMarginThickness");
            OnPropertyChanged("WindowRadius");
            OnPropertyChanged("WCornerRadius");
        }

        //Commands

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
