namespace HappyTourManager
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Class for commands
    /// </summary>
    class RelayCommand : ICommand
    {

        private Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
