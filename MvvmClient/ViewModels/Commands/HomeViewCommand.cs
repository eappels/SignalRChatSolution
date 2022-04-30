using System;
using System.Windows.Input;

namespace MvvmClient.ViewModels.Commands
{
    public class HomeViewCommand : ICommand
    {

        private readonly ShellViewModel _shellViewModel;

        public HomeViewCommand(ShellViewModel model)
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
            return (_shellViewModel.SwitchView != "home");
        }

        public void Execute(object? parameter)
        {
            _shellViewModel.SwitchView = "home";
        }
    }
}