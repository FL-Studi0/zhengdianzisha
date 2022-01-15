using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Search_page : Form
    {
        public Search_page()
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


        public static string Get(string url, Dictionary<string, string> dic)
        {
            string result = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            req.UserAgent = "Dart/2.13 (dart:io)"; //添加头
            //添加参数
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }



        //获取热搜词json
        private Root4 get_hot_search_json(int linit)
        {
            string url = "https://www.zhengdianzisha.com/zhibo/index/get-user-keywords";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("type", "0");
            dic.Add("limit", linit.ToString());
            dic.Add("jwt", jwt_str);
            string get_ret = Get(url, dic);
            Root4 rt = JsonConvert.DeserializeObject<Root4>(get_ret);
            return rt;
        }


        //解析热搜词返回string
        private string get_hot_search(Root4 rt)
        {
            string hot_search = "";
            int counter = 0;
            foreach (var num in rt.keywords)
            {
                counter++;
                hot_search += counter.ToString() + ".";
                hot_search += num.keywords += "\n";
            }
            return hot_search;
        }


        //解析json是否获取成功
        private bool check_ret_json(Root4 rt)
        {

            if (rt.errcode != 0)
            {
                return false;
            }
            return true;
        }

        private bool check_ret_json(Root3 rt)
        {

            if (rt.errcode != 0)
            {
                return false;
            }
            return true;
        }




        //载入窗体时
        private void Search_page_Load(object sender, EventArgs e)
        {
            //获取热搜词的数量
            int limit = 10;
            //获取热搜词
            Root4 rt = get_hot_search_json(limit);
            //循环遍历
            if (check_ret_json(rt))
            {
                label3.Text = get_hot_search(rt);
            }
            else
            {
                label13.Text = "获取失败";
            }


        }

        int radiotype = -1;





        //获取2商品类函数
        private Root3 get_commodity_json(string url, Dictionary<string, string> dic)
        {
            string get_ret = Get(url, dic);
            Root3 rt = JsonConvert.DeserializeObject<Root3>(get_ret);
            return rt;
        }

        private void forearch_commodity(Root3 rt)
        {
            //计数器
            int list_num = 0;
            //存储获取到的文字
            string all_str = "";
            //遍历
            foreach (var num in rt.lists)
            {
                list_num++;
                all_str += list_num.ToString();
                all_str += ".";
                all_str += "商品名称：";
                string tmp = "";
                int max_length = 27;
                if (num.name.Length > max_length)
                {
                    tmp = num.name.Substring(0, max_length);
                    tmp += "...";
                }
                else
                {
                    tmp = num.name;
                }
                all_str += tmp += "\n";
                all_str += "价格：";
                all_str += num.now_price;
                all_str += "\n\n";
            }
            //给richtextbox赋值
            richTextBox1.Text = all_str;

            //数据统计输出
            label2.Text = list_num.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //先判断是否填写完整
            if (radiotype == -1 || textBox1.Text == "")
            {
                MessageBox.Show("请先填写完整");
                return;
            }

            //搜索发送请求的url
            string search_url = "https://www.zhengdianzisha.com/zhibo/index/k-search";

            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            //添加请求体
            dic1.Add("keywords", textBox1.Text);
            dic1.Add("type", radiotype.ToString());
            dic1.Add("limit", "6000");
            dic1.Add("page", "1");
            dic1.Add("jwt", jwt_str);



            switch (radiotype)
            {
                //直播
                case (1): break;

                //商品
                case (2):
                    //获取商品
                    Root3 rt = get_commodity_json(search_url, dic1);
                    if (check_ret_json(rt))
                    {
                        forearch_commodity(rt);
                    }
                    break;

                //短视频
                case (3): break;

                //用户
                case (4): break;

                //店铺
                case (5): break;
            }




        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radiotype = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radiotype = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radiotype = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            radiotype = 4;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            radiotype = 5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Root4 rt = get_hot_search_json(1000);
            if (check_ret_json(rt))
            {
                MessageBox.Show(get_hot_search(rt));
            }
            else
            {
                MessageBox.Show("内部错误，获取失败");
            }



        }
    }
}
