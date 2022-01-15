namespace WindowsFormsApp1
{


    public class Data1
    {
        /// <summary>
        /// 
        /// </summary>
        public string accessid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string policy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expire { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string callback { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dir { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string callbackUrl { get; set; }
    }

    public class uploadtoken
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data1 data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filename { get; set; }
    }



}
