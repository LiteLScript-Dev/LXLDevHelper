using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceManager.Net5.Layout
{
    //建一个PropertyNodeItem类
    internal class PropertyNodeItem
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }


        public List<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {
            Children = new List<PropertyNodeItem>();
        }
    }
}
