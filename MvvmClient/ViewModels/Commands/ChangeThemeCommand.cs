using System;
using System.Windows.Input;

namespace MvvmClient.ViewModels.Commands
{
    internal class ChangeThemeCommand : ICommand
    {

        private readonly ShellViewModel _shellViewModel;

        public ChangeThemeCommand(ShellViewModel model)
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
            _shellViewModel.ChangeAppTheme((string)parameter);
        }
    }
}