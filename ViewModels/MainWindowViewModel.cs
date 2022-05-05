using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using TouringCarHMI.Common;
using TouringCarHMI.Extensions;
using TouringCarHMI.Models;
using TouringCarHMI.ViewModels;
namespace TouringCarHMI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private void QuitSendMsg(bool Flag)
        {
            EventAggregatorRepository.GetInstance().eventAggregator.GetEvent<GetQuitMessage>().Publish(Flag);
        }
        private readonly IRegionManager regionManager;
        private ObservableCollection<MenuBar> menuBars;
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        //public DelegateCommand CloseCommand { get; }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set
            {
                menuBars = value;
                this.RaisePropertyChanged();
            }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
          
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            //CloseCommand = new DelegateCommand(()=> App.Current.Shutdown());
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;
            if (obj.NameSpace == "quit")
            {
                this.QuitSendMsg(true);
                App.Current.Shutdown();
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace);
           
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Tag = "\ue625", Title = "首页",  NameSpace = "FirstView" });
            MenuBars.Add(new MenuBar() { Tag = "\ue97a", Title = "开关", NameSpace = "SwitchView" });
            MenuBars.Add(new MenuBar() { Tag = "\ue63f", Title = "运行配置", NameSpace = "ConfigView" });
            MenuBars.Add(new MenuBar() { Tag = "\ue601", Title = "系统设置", NameSpace = "SettingsView" });
            MenuBars.Add(new MenuBar() { Tag = "\ue892", Title = "退出",  NameSpace ="quit" });
        }

      
    }
}
