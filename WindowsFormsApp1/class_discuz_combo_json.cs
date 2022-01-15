using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ListItem1
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 有些知识
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
    }

    public class Root5
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem1> list { get; set; }
    }

}
