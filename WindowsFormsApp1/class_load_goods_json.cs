using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class Root2
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
    }
    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_show { get; set; }
        /// <summary>
        ///   紫砂雕塑  
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_del { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string create_at { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string update_at { get; set; }
    }



}
