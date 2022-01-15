using System.Collections.Generic;

namespace WindowsFormsApp1
{
    //如果好用，请收藏地址，帮忙分享。
    public class KeywordsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string keywords { get; set; }
    }

    public class Root4
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KeywordsItem> keywords { get; set; }
    }

}
