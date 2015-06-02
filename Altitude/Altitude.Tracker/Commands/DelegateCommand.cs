using System;
using System.Windows.Input;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand([NotNull] Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true; // YES WE CAN!
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}