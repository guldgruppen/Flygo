using System;
using System.Windows.Input;

namespace FlygoApp.Commons
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            return canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
