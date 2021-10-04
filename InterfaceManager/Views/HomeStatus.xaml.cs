using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using InterfaceManager.Utils;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InterfaceManager
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class HomeStatus : Page
    {
        private string LibraryDownloadLink = null;
        public HomeStatus()
        {
            InitializeComponent();
            refreshData();
        }

        public void refreshData()
        {
            #region Commit
            this.Dispatcher.Invoke(new Action(delegate
            {
                try
                {
                    var Url = "https://api.github.com/repos/liteldev/litexloader/commits";
                    string result = NetWork.HttpGet(Url);
                    JArray json = (JArray)JsonConvert.DeserializeObject(result);
                    JObject commit = (JObject)json[0];
                    CommitTime.Text = "时间: "+commit["commit"]["author"]["date"];
                    CommitDescription.Text = "描述: "+ commit["commit"]["message"];
                    CommitSender.Text = "发送者: "+ commit["commit"]["author"]["name"];
                    CommitSha.Text = "Sha: " + commit["sha"];
                }
                catch (Exception e)
                {
                    CommitTime.Text = "时间: 获取失败";
                    CommitDescription.Text = "描述: 获取失败";
                    CommitSender.Text = "发送者: 获取失败";
                    CommitSha.Foreground = Brushes.Red;
                    CommitSha.Text = "原因: " + e.Message;
                }
            }));
            #endregion


            #region Library
            this.Dispatcher.Invoke(new Action(delegate {
                try
                {

                    string token = NetWork.GetCloudToken();
                    string result = NetWork.HttpGet(token + "LXLDevHelper/server.json");
                    JObject json = (JObject)JsonConvert.DeserializeObject(result);


                    LibraryTime.Text = "更新时间: " + json["Updates"][0]["UpdateTime"].ToString();
                    LibraryDescript.Text = "更新描述: " + json["Updates"][0]["Description"][0].ToString();
                    LibraryDownload.Text = json["Updates"][0]["FileDownloadLink"].ToString();
                    LibraryVersion.Text = "版本号: " + json["Updates"][0]["Version"].ToString();
                    LibraryDownloadLink = json["Updates"][0]["FileDownloadLink"].ToString();
                }
                catch (Exception e)
                {
                    LibraryTime.Text = "获取失败";
                    LibraryDescript.Text = "原因: \n" + e.ToString();
                    LibraryDownload.Text = "";
                    LibraryVersion.Text = "";
                }
            }));
            /* void Library(Home home)
              {

              }
              Action<Home> action = Library;
              //BeginInvoke 开启异步线程
              action.BeginInvoke(this,null, null);*/
            #endregion


            #region 完成情况
            int classNum = 10;
            int functionNum = 20;
            int valueNum = 7;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Class数量",
                    Values = new ChartValues<double> { classNum },
                },
                 new ColumnSeries
                {
                    Title="Function数量",
                    Values = new ChartValues<double> { functionNum },
                }, new ColumnSeries
                {
                    Title="Value数量",
                    Values = new ChartValues<double> { valueNum },
                }
            };
            cartesianChart.DataTooltip.SetResourceReference(BackgroundProperty, "MaterialDesignPaper");

            #endregion


            #region 使用说明
            Usage.Text =
                "1.使用本软件编辑/修改接口信息\n" +
                "2.直接发起Push/Pull request到仓库\n" +
                "注意: 请确保本软件所处位置为正确的Git环境";
            #endregion


            DataContext = this;
           
        }
        public SeriesCollection SeriesCollection { get; set; }

       

        private void LibraryDownload_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (LibraryDownloadLink != null)
            {
                Process.Start(new ProcessStartInfo(LibraryDownloadLink));
            }
            else
            {
                LibraryDownload.Text = "获取失败";
            }

        }

    }

}