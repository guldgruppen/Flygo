﻿using System;
using System.Windows.Input;

namespace FlygoApp.Commons
{
    public class RelayCommand : ICommand
    {
        //Bruges til at binde metoder til knapper i GUI.
        private readonly Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {           
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}
