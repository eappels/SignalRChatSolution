using System;
using System.Windows.Input;

namespace MvvmClient.ViewModels.Commands
{
    public class ConnectCommand : ICommand
    {

        private readonly ShellViewModel _shellViewModel;

        public ConnectCommand(ShellViewModel model)
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
            return  _shellViewModel.Connection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Disconnected;
        }

        public void Execute(object? parameter)
        {
            _shellViewModel.ConnectToChatServer();
        }
    }
}