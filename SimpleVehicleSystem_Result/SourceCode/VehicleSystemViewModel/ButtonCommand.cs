using System;
using System.Windows.Input;

namespace SimpleVehicleSystem.VehicleSystemViewModel
{
    public class ButtonCommand : ICommand
    {
        private Action<object> execution;
        private Func<object, bool> validation;

        public ButtonCommand(Action<object> execution, Func<object, bool> validation)
        {
            this.execution = execution;
            this.validation = validation;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return validation(parameter);
        }

        public void Execute(object parameter)
        {
            execution(parameter);
        }
    }
}
