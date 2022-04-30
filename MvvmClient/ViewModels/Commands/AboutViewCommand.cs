using System;
using System.Windows.Input;

namespace MvvmClient.ViewModels.Commands
{
    public class AboutViewCommand : ICommand
    {

        private readonly ShellViewModel _shellViewModel;

        public AboutViewCommand(ShellViewModel model)
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
            return (_shellViewModel.SwitchView != "about");
        }

        public void Execute(object? parameter)
        {
            _shellViewModel.SwitchView = "about";
        }
    }
}