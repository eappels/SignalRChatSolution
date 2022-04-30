using System;
using System.Windows.Input;

namespace MvvmClient.ViewModels.Commands
{
    internal class ChatViewCommand : ICommand
    {

        private readonly ShellViewModel _shellViewModel;

        public ChatViewCommand(ShellViewModel model)
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
            return (_shellViewModel.SwitchView != "chat");
        }

        public void Execute(object? parameter)
        {
            _shellViewModel.SwitchView = "chat";
        }
    }
}
