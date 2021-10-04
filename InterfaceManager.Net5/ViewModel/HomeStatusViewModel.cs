using Prism.Mvvm;
using Prism.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using LiveCharts.Wpf;

namespace InterfaceManager.Net5.ViewModel
{
    internal class HomeStatusViewModel : BindableBase
    {
        public HomeStatusViewModel()
        {
            RefreshDataCommand = new DelegateCommand(new Action(RefreshData));
            RefreshData();
        }

        private string LibraryDownloadLink { get; set; } = null;

        #region Commit
        private string _CommitTime;

        public string CommitTime
        {
            get => _CommitTime;
            set
            {
                string Text = $"时间：{value}";
                SetProperty(ref _CommitTime, Text);
            }
        }

        private string _CommitDescription;

        public string CommitDescription
        {
            get => _CommitDescription;
            set
            {
                string Text = $"描述：{value}";
                SetProperty(ref _CommitDescription, Text);
            }
        }

        private string _CommitSender;

        public string CommitSender
        {
            get => _CommitSender;
            set
            {
                string Text = $"发送者：{value}";
                SetProperty(ref _CommitSender, Text);
            }
        }

        private string _CommitSha;

        public string CommitSha
        {
            get => _CommitSha;
            set
            {
                string Text = $"Sha：{value}";
                SetProperty(ref _CommitSha, Text);
            }
        }

        private Brush _CommitShaForeground = Brushes.White;

        public Brush CommitShaForeground
        {
            get => _CommitShaForeground;
            set => SetProperty(ref _CommitShaForeground, value);
        }

        #endregion

        #region Library
        private string _LibraryTime;

        public string LibraryTime
        {
            get => _LibraryTime;
            set
            {
                string Text = $"更新时间：{value}";
                SetProperty(ref _LibraryTime, Text);
            }
        }

        private string _LibraryVersion;

        public string LibraryVersion
        {
            get => _LibraryVersion;
            set
            {
                string Text = $"版本号：{value}";
                SetProperty(ref _LibraryVersion, Text);
            }
        }

        private string _LibraryDescription;

        public string LibraryDescription
        {
            get => _LibraryDescription;
            set
            {
                string Text = $"更新描述：{value}";
                SetProperty(ref _LibraryDescription, Text);
            }
        }

        private string _LibraryDownload;

        public string LibraryDownload
        {
            get => _LibraryDownload;
            set
            {
                string Text = $"下载地址：{value}";
                SetProperty(ref _LibraryDownload, Text);
            }
        }
        #endregion

        #region 完成情况
        private SeriesCollection _SeriesCollection;

        public SeriesCollection SeriesCollection
        {
            get => _SeriesCollection;
            set => SetProperty(ref _SeriesCollection, value);
        }
        #endregion

        #region 使用说明
        private string _Usage = "1.使用本软件编辑/修改接口信息\n" +
            "2.直接发起Push/Pull request到仓库\n" +
            "注意: 请确保本软件所处位置为正确的Git环境";

        public string Usage
        {
            get => _Usage;
            private set => SetProperty(ref _Usage, value);
        }

        #endregion

        public DelegateCommand RefreshDataCommand { get; set; }
        private void RefreshData()
        {
            Task.Run(() =>
            {
                try
                {
                    var Url = "https://api.github.com/repos/liteldev/litexloader/commits";
                    string result = Utils.NetWork.HttpGet(Url);
                    JArray json = (JArray)JsonConvert.DeserializeObject(result);
                    JObject commit = (JObject)json[0];
                    CommitTime = commit["commit"]["author"]["date"].ToString();
                    CommitDescription = commit["commit"]["message"].ToString();
                    CommitSender = commit["commit"]["author"]["name"].ToString();
                    CommitSha = commit["sha"].ToString();
                }
                catch (Exception e)
                {
                    CommitTime = "获取失败";
                    CommitDescription = "获取失败";
                    CommitSender = "获取失败";
                    CommitShaForeground = Brushes.Red;
                    CommitSha = e.Message;
                }
            });
            Task.Run(() =>
            {
                try
                {
                    string token = Utils.NetWork.GetCloudToken();
                    string result = Utils.NetWork.HttpGet(token + "LXLDevHelper/server.json");
                    JObject json = (JObject)JsonConvert.DeserializeObject(result);


                    LibraryTime = json["Updates"][0]["UpdateTime"].ToString();
                    LibraryDescription = json["Updates"][0]["Description"][0].ToString();
                    LibraryDownload = json["Updates"][0]["FileDownloadLink"].ToString();
                    LibraryVersion = json["Updates"][0]["Version"].ToString();
                    LibraryDownloadLink = json["Updates"][0]["FileDownloadLink"].ToString();
                }
                catch (Exception e)
                {
                    LibraryTime = "获取失败";
                    LibraryDescription = "原因: \n" + e.ToString();
                    LibraryDownload = "";
                    LibraryVersion = "";
                }
            });
            Task.Run(() =>
            {
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
            });
        }
    }
}
