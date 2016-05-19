using System;
using System.Windows.Input;

namespace FlygoApp.Commons
{
    public class RelayCommandWithParameter : ICommand
    {
        //Bruges til at binde metoder til knapper i GUI, hvor der er en parameter der skal sendes med
        private Action<object> _action;

        public RelayCommandWithParameter(Action<object> action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
