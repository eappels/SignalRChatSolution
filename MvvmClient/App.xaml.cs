using Microsoft.Extensions.DependencyInjection;
using MvvmClient.ViewModels;
using MvvmClient.Views;
using System;
using System.Windows;

namespace MvvmClient
{
    public partial class App : Application
    {

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {            
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var win = new ShellView();
            win.Show();
            
            base.OnStartup(e);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<ShellViewModel>();
            return services.BuildServiceProvider();
        }
    }
}