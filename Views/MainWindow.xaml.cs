using Prism.Ioc;
using Prism.Regions;
using System.Windows;
using System.Windows.Input;

namespace TouringCarHMI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager,IContainerExtension container)
        {  
            InitializeComponent();
            regionManager.RegisterViewWithRegion("MainViewRegion", typeof(FirstView));
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
        }
    }
}
