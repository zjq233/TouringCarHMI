using Prism.Ioc;
using System.Windows;
using TouringCarHMI.ViewModels;
using TouringCarHMI.Views;

namespace TouringCarHMI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {   
            
            containerRegistry.RegisterForNavigation<FirstView, FirstViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<SwitchView, SwitchViewModel>();
            containerRegistry.RegisterForNavigation<ConfigView, ConfigViewModel>();
            containerRegistry.RegisterSingleton
        }
    }
}
