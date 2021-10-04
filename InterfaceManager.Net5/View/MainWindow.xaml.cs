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

namespace InterfaceManager.Net5.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.MainWindowViewModel();
        }

        // 窗体移动
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs arg)
        {
            if (arg.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
