using System.Collections.Generic;

namespace GFCA.APT.WEB.Models
{
    public class MenuItem
    {
        public string text { get; set; }
        public string url { get; set; }
        public string iconCss { get; set; }
        public List<object> items { get; set; }
    }
}