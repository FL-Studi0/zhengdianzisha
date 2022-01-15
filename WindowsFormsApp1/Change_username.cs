using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Change_username : Form
    {
        public Change_username()
        {
            InitializeComponent();
        }

        //父窗口版本号传参
        private string string1;
        public string String1
        {
            set
            {
                string1 = value;
            }
        }

        //父窗口jwt传参
        private string form2_jwt_str;
        public string Form2_jwt_str
        {
            set
            {
                form2_jwt_str = value;
            }
        }

        //父窗口用户名传参
        private string form2_username_str;
        public string Form2_username_str
        {
            set
            {
                form2_username_str = value;
            }
        }

        public void Follow_username()
        {
            label10.Text = form2_username_str;
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



        private void Form2_Load(object sender, EventArgs e)
        {
            this.TopMost = top_bool;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //更改用户名发送网络流
        private string sent_change_username(string change_username, string all_jwt)
        {
            string url = "https://www.zhengdianzisha.com/zhibo/per/set-nickname?nickname=";
            string url2 = "&jwt=";
            string allurl = url + change_username + url2 + all_jwt;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(allurl);    //创建一个请求示例
            request.UserAgent = "Dart/2.13 (dart:io)"; //添加头

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();
            return html;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(form2_jwt_str))
            {
                string change_username = textBox3.Text;
                sent_change_username(change_username, form2_jwt_str);
                label10.Text = textBox3.Text;
                MessageBox.Show("更名成功");
            }
            else
            {
                MessageBox.Show("失败,请先获取账号信息");
            }
        }
    }
}
