using Microsoft.Extensions.DependencyInjection;
using MvvmClient.ViewModels;

namespace MvvmClient.Views
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<ShellViewModel>();
        }
    }
}