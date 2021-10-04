using InterfaceManager.Layout;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// ApiPage.xaml 的交互逻辑
    /// </summary>
    public partial class ApiPage : Page
    {

        public ApiPage()
        {
            InitializeComponent();
            refdata();
        }
        private void refdata()
        {

            DirectoryInfo TheFolder = new DirectoryInfo("C:\\Users\\36015\\source\\repos\\LXLDevHelper\\DataSource");
            if (!TheFolder.Exists)
                return;
            List<PropertyNodeItem> itemList = new List<PropertyNodeItem>();
            foreach (DirectoryInfo NextDir in TheFolder.GetDirectories())
            {
                if (NextDir != null)
                {
                    List<PropertyNodeItem> filesList = new List<PropertyNodeItem>();
                    DirectoryInfo folder = new DirectoryInfo(NextDir.FullName);
                    foreach (FileInfo NextFile in folder.GetFiles())
                    {
                        if (NextFile != null)
                        {
                            PropertyNodeItem files = new PropertyNodeItem();
                            files.DisplayName = NextFile.Name;
                            files.Url = NextFile.FullName;
                            files.Name = "类名";
                            filesList.Add(files);
                        }
                    }
                    PropertyNodeItem directory = new PropertyNodeItem();
                    directory.DisplayName = NextDir.Name;
                    directory.Url = NextDir.FullName;
                    directory.Name = "文件夹";
                    directory.Children = filesList;
                    itemList.Add(directory);
                }
            }
            treeView.ItemsSource = itemList;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            refdata();
        }

    }
}
