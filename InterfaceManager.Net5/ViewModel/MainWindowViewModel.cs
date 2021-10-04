using Prism.Mvvm;
using Prism.Commands;
using InterfaceManager.Net5.View;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using System.Windows;

namespace InterfaceManager.Net5.ViewModel
{
    internal class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public MainWindowViewModel()
        {
            HomeCommand = new DelegateCommand(new Action(Home));
            ManageCommand = new DelegateCommand(new Action(Manage));
            ToggleBaseThemeCommand = new DelegateCommand(new Action(ToggleBaseTheme));
            CloseCommand = new DelegateCommand(new Action(Close));
        }

        private bool isLight = true;
        /// <summary>
        /// 切换主题
        /// </summary>
        internal bool IsLight
        {
            get => isLight;
            set
            {
                isLight = value;

                var paletteHelper = new PaletteHelper();
                Color primaryColor = Colors.Purple;
                Color secondaryColor = Colors.Lime;
                IBaseTheme baseTheme = value ? Theme.Light : Theme.Dark;
                ITheme theme = Theme.Create(baseTheme, primaryColor, secondaryColor);
                paletteHelper.SetTheme(theme);

                LightorNightToolTip = value ? "浅色模式" : "深色模式";
                LightorNightIcon = new PackIcon()
                {
                    Kind = value ? PackIconKind.WeatherNight : PackIconKind.WhiteBalanceSunny,
                    Width = 30,
                    Height = 30
                };
            }
        }

        private object _MainContent = new HomeStatus();
        /// <summary>
        /// 主页内容
        /// </summary>
        public object MainContent
        {
            get => _MainContent;
            set => SetProperty(ref _MainContent, value);
        }

        private string _LightorNightToolTip = "浅色模式";
        /// <summary>
        /// ToggleBaseTheme按钮文本
        /// </summary>
        public string LightorNightToolTip
        {
            get => _LightorNightToolTip;
            set => SetProperty(ref _LightorNightToolTip, value);
        }

        private object _LightorNightIcon = new PackIcon { Kind = PackIconKind.WeatherNight, Width = 30, Height = 30 };
        /// <summary>
        /// ToggleBaseTheme按钮图标
        /// </summary>
        public object LightorNightIcon
        {
            get => _LightorNightIcon;
            set => SetProperty(ref _LightorNightIcon, value);
        }

        ////////////命令////////////
        
        /// <summary>
        /// 主页
        /// </summary>
        
        public DelegateCommand HomeCommand { get; set; }

        /// <summary>
        /// 切换主题
        /// </summary>
        public DelegateCommand ToggleBaseThemeCommand { get; set; }

        /// <summary>
        /// 编辑API页面
        /// </summary>
        public DelegateCommand ManageCommand { get; set; }

        /// <summary>
        /// 关闭程序
        /// </summary>
        public DelegateCommand CloseCommand { get; set; }

        /// <summary>
        /// HomeCommand
        /// </summary>
        private void Home()
        {
            MainContent = new HomeStatus();
        }

        /// <summary>
        /// ToggleBaseThemeCommand
        /// </summary>
        private void ToggleBaseTheme()
        {
            IsLight = !IsLight;
        }

        /// <summary>
        /// ManageCommand
        /// </summary>
        private void Manage()
        {
            MainContent = new InterfaceEditor();
        }

        /// <summary>
        /// CloseCommand
        /// </summary>
        private void Close()
        {
            Application.Current.Shutdown();
        }
    }
}
