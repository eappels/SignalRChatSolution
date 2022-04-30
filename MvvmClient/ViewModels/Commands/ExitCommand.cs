using System;
using System.Windows.Input;

namespace MvvmClient.ViewModels.Commands
{
    public class ExitCommand : ICommand
    {

        private readonly ShellViewModel _shellViewModel;

        public ExitCommand(ShellViewModel model)
        {
            _shellViewModel = model;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            App.Current.Shutdown();
        }
    }
}