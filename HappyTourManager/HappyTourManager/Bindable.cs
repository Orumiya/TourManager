namespace HappyTourManager
{
    using System.ComponentModel;

    /// <summary>
    /// For data binding
    /// </summary>
    public class Bindable : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
