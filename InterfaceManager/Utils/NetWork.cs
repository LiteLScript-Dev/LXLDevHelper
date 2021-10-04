using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceManager.Utils
{
    class NetWork
    {
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
            // request.Timeout = Timeout;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            ////获得Response的
            Stream myResponseStream = response.GetResponseStream();
            //读取流数据
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string result = myStreamReader.ReadToEnd();
            //读取完成  关闭数据流
            myStreamReader.Close();
            myResponseStream.Close();
            return result;
        }
        public static string GetCloudToken()
        {
                string ServerUrl = "https://cdn.jsdelivr.net/gh/LiteLDev-LXL/Cloud/";
                string CloudUrl = "https://lxl-cloud.amd.rocks/id.json";
                JObject json = (JObject)JsonConvert.DeserializeObject(HttpGet(CloudUrl));
                return ServerUrl + json["token"] + "/";
        }
    }
}
