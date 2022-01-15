using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Freight_template : Form
    {
        public Freight_template()
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
            label6.Text = string1;
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



        private void Freight_template_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "北京市/天津市/河北省/山西省/内蒙古自治区/辽宁省/吉林省/黑龙江省/上海市/江苏省/浙江省/安徽省/福建省/江西省/山东省/河南省/湖北省/湖南省/广东省/广西壮族自治区/海南省/重庆市/四川省/贵州省/云南省/西藏自治区/陕西省/甘肃省/青海省/宁夏回族自治区/新疆维吾尔自治区/台湾省/香港特别行政区/澳门特别行政区";

        }



        private void button1_Click(object sender, EventArgs e)
        {
            string post_url = "https://www.zhengdianzisha.com/zhibo/store/create-template" + "?jwt=" + jwt_str;

            //定义post内容
            string post_str = "name=" + textBox1.Text +
                            "&province=" + richTextBox1.Text;

            //响应请求
            string post_ret = Post(post_url, post_str);

            MessageBox.Show(post_ret);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
