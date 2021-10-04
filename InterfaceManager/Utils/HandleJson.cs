using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceManager.Utils
{
    class HandleJson
    {
        public static string GetClassName(string json) {
            JObject data = (JObject)JsonConvert.DeserializeObject(json);
            return data["ClassName"].ToString();
        }
    }
}
