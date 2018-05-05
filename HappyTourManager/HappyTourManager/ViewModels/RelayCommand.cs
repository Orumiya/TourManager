// <copyright file="RelayCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Class for commands
    /// </summary>
    internal class RelayCommand : ICommand
    {
        private Action action;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// Relay command
        /// </summary>
        /// <param name="action"> action</param>
        public RelayCommand(Action action)
        {
            this.action = action;
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// check if executable
        /// </summary>
        /// <param name="parameter">object</param>
        /// <returns>returns true</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="parameter"> object</param>
        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
