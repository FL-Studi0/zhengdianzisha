namespace WindowsFormsApp1
{

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }


    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jwt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
    }






    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shangji { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shangjilive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int accountstatus { get; set; }
        /// <summary>
        /// 小标师
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wxqrcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int golds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int coin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int achievement { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_live { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_store { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_master { get; set; }
        /// <summary>
        /// 紫砂小镖师
        /// </summary>
        public string m_store_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string m_store_desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string m_store_logo { get; set; }
        /// <summary>
        /// 紫砂小镖师
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 吃粗茶淡饭、喝一壶淡茶
        /// </summary>
        public string store_desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string store_headimg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string store_img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kf_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kf_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kf_password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_open { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int fans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string stream_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int createtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int updatetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xcx_openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gzh_openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string app_openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_close { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int close_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apple_code { get; set; }
    }



}
