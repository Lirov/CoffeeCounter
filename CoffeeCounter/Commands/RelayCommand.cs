using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeeCounter.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute { get; set; }
        private Predicate<object> _canExecute { get; set; }

        public RelayCommand(Action<object> executeAction)
        {
            _execute = executeAction;
            _canExecute = null;
        }

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteMethod)
        {
            _execute = executeAction;
            _canExecute = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value;}
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
