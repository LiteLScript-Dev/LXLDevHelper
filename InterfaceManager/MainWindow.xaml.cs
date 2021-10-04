using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterfaceManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isLight = true;
        private ApiPage apiPage = null;
        public MainWindow()
        {
           InitializeComponent();
            Home home = new Home();
            MainPage.Content = home;
            home.refreshData();
        }

        // 窗体移动
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        //退出点击
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //暗色浅色切换点击
        private void Day_Click(object sender, RoutedEventArgs e)
        {
            if (isLight)
            {
                var paletteHelper = new PaletteHelper();
                Color primaryColor = Colors.Purple;
                Color secondaryColor = Colors.Lime;
                IBaseTheme baseTheme = Theme.Dark;
                ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
                LightorNight.Content = new PackIcon { Kind = PackIconKind.WhiteBalanceSunny, Width = 30, Height = 30 };
                LightorNight.ToolTip = "浅色模式";
                isLight = false;
                paletteHelper.SetTheme(theme);
            }
            else
            {
                var paletteHelper = new PaletteHelper();
                Color primaryColor = Colors.Purple;
                Color secondaryColor = Colors.Lime;
                IBaseTheme baseTheme = Theme.Light;
                ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
                LightorNight.Content = new PackIcon { Kind = PackIconKind.WeatherNight, Width=30,Height=30};
                LightorNight.ToolTip = "深色模式";
                isLight = true;
                paletteHelper.SetTheme(theme);
            }
        }
        //Home Page
        private void Home_Click(object sender,RoutedEventArgs e)
        {
            Home home = new Home();
            MainPage.Content = home;
            home.refreshData();
        }
        //Manage Page
        private void Manage_Click(object sender, RoutedEventArgs e)
        {
            if (apiPage == null)
            {
                ApiPage apiPage = new ApiPage();
                this.apiPage = apiPage;
                MainPage.Content = apiPage;
            }
            else
            {
                MainPage.Content = apiPage;
            }
        }

    }
}
