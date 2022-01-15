using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Shipping_warehouse : Form
    {
        public Shipping_warehouse()
        {
            InitializeComponent();
        }






        private string string1;
        public string String1
        {
            set
            {
                string1 = value;
            }
        }

        public void Follow_Version()
        {
            label13.Text = string1;
        }


        private bool top_bool;

        public bool Top_bool
        {
            set
            {
                top_bool = value;
            }
        }

        //父窗口jwt传参
        private string jwt_str;
        public string Form2_jwt_str
        {
            set
            {
                jwt_str = value;
            }
        }

        //定义post函数
        public static string Post(string url, string content)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Dart/2.13 (dart:io)"; //添加头
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }







        private void Shipping_warehouse_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url1 = "https://www.zhengdianzisha.com/zhibo/store/create-house?jwt=" + jwt_str;


            //发送的请求体
            string post_str = "";




            Post(url1, post_str);

        }
    }
}
