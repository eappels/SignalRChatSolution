using ControlzEx.Theming;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmClient.Helpers;
using MvvmClient.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmClient.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        
        private string _switchView;
        public ICommand HomeViewCommand { get; }
        public ICommand ChatViewCommand { get; }
        public ICommand AboutViewCommand { get; }
        public ICommand ChangeThemeCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand SendMessage { get; }
        public ICommand ExitCommand { get; }

        public HubConnection Connection { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public bool IsConnected { get; set; }
        public string MessageInput { get; set; }

        public ShellViewModel()
        {
            Messages = new ObservableCollection<string>();
            SwitchView = "home";
            HomeViewCommand = new HomeViewCommand(this);
            ChatViewCommand = new ChatViewCommand(this);
            AboutViewCommand = new AboutViewCommand(this);
            ChangeThemeCommand = new ChangeThemeCommand(this);
            ConnectCommand = new ConnectCommand(this);
            SendMessage = new SendMessage(this);
            ExitCommand = new ExitCommand(this);

            Connection = new HubConnectionBuilder().WithUrl("https://localhost:7121/chathub").WithAutomaticReconnect().Build();
            IsConnected = true;

            Connection.Reconnecting += (sender) =>
            {
                IsConnected = true;
                var newmessage = "Reconnecting....";
                Messages.Add(newmessage);
                return Task.CompletedTask;
            };

            Connection.Reconnected += (sender) =>
            {
                IsConnected = true;
                Messages.Clear();
                var newmessage = "Reconnected.";
                Messages.Add(newmessage);
                return Task.CompletedTask;
            };

            Connection.Closed += (sender) =>
            {
                IsConnected = false;
                Messages.Clear();
                var newmessage = "Closed connection.";
                Messages.Add(newmessage);
                return Task.CompletedTask;
            };

            Connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Debug.WriteLine(user + " " + message);
                Messages.Add(user + " says: " + message);
            });
        }

        public string SwitchView
        {
            get => _switchView;
            set => SetProperty(ref _switchView, value);
        }

        public void ChangeAppTheme(string color = "Default")
        {
            switch (color)
            {
                case "blue":
                    ThemeManager.Current.ChangeTheme(App.Current, "Dark.Blue");
                    break;
                case "red":
                    ThemeManager.Current.ChangeTheme(App.Current, "Dark.Red");
                    break;
                case "purple":
                    ThemeManager.Current.ChangeTheme(App.Current, "Dark.Purple");
                    break;
                case "green":
                    ThemeManager.Current.ChangeTheme(App.Current, "Dark.Green");
                    break;
            }
        }

        public async Task ConnectToChatServer()
        {
            try
            {
                Connection.StartAsync();
                Messages.Add("Connecting...");
            }
            catch (Exception ex)
            {
                Messages.Add(ex.Message);
                Debug.WriteLine(ex);
            }
        }

        public async Task SendMessageToServer(string message)
        {
            try
            {
                await Connection.InvokeAsync("SendMessage", "Wpf Client", message);
            }
            catch (Exception ex)
            {
                Messages.Add(ex.Message);
            }
        }
    }
}