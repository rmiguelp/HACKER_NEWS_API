using System.Collections.Generic;

namespace HACKER__NEWS_API
{
    public class DataItems
    {
        public List<DataItem> _DataItems { get; set; }      

        public DataItems()
        { _DataItems = new List<DataItem>(); }        
    }

    public class DataItem
    {
        public int id { get; set; }        
        public bool deleted { get; set; }
        public string type { get; set; }
        public string by { get; set; }
        public long time { get; set; }
        public string text { get; set; }
        public bool dead { get; set; }
        public string parent { get; set; }
        public string poll { get; set; }
        public List<int> Kids { get; set; }
        public string url { get; set; }
        public int score { get; set; }
        public string title { get; set; }
        public List<string> parts { get; set; }
        public string descendants { get; set; }
    }
}
