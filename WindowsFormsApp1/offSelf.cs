using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class offSelf : Form
    {
        public offSelf()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void offSelf_Load(object sender, EventArgs e)
        {

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

        ArrayList al = new ArrayList();

        private string foreachGoods(showGoods_class rt)
        {
            int goodsNum = 0;
            string all_str = "";
            al.Clear();


            foreach (var key in rt.list)
            {
                goodsNum++;

                //添加序号
                all_str += goodsNum.ToString() + ".";

                //id
                all_str += "ID:";
                all_str += key.id += "\n";
                al.Add(key.id.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));


                //标题
                all_str += "标题:";
                all_str += key.name += "\n";

                //数量
                all_str += "数量:";
                all_str += key.stock += "\n";

                //单位
                all_str += "单位:";
                all_str += key.unit + "\n";

                //价格
                all_str += "现价:";
                all_str += key.now_price + "\n";
                all_str += "市场价:";
                all_str += key.old_price + "\n";

                //上传修改日期
                all_str += "上传日期:";
                all_str += key.update_at + "\n";
                all_str += "修改日期:";
                all_str += key.create_at + "\n";

                all_str += "\n";



            }

            return all_str;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "加载中，耐心等待..........";
            Thread thread = new Thread(new ThreadStart(threadGetgoods));
            thread.Start();

        }


        private void threadGetgoods()
        {
            string url = "https://www.zhengdianzisha.com/zhibo/store/goods-list";
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //添加参数
            dic.Add("is_up", "1");
            dic.Add("page", "1");
            dic.Add("limit", "100000");
            dic.Add("jwt", jwt_str);

            //get请求
            string get_ret = Get(url, dic);
            showGoods_class rt = JsonConvert.DeserializeObject<showGoods_class>(get_ret);
            if (rt.errcode != 0)
            {
                return;
            }

            //遍历所有商品函数
            richTextBox1.Text = foreachGoods(rt);
        }



        //删除商品
        private void button2_Click(object sender, EventArgs e)
        {



            Thread thread1 = new Thread(new ThreadStart(foreachDelete));

            thread1.Start();



        }

        private void threadGet(object obj)
        {
            foreach (var key in (Dictionary<string, Dictionary<string, string>>)obj)
            {

                Get(key.Key, key.Value);

                progressBar1.Value += 1;



            }





        }

        private void foreachDelete()
        {
            string url = "https://www.zhengdianzisha.com/zhibo/store/del-goods";


            int index = al.Count;
            if (index > 0)
            {
                progressBar1.Maximum = index;
            }
            progressBar1.Value = 0;

            foreach (string val in al)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("id", val);
                dic.Add("jwt", jwt_str);

                Dictionary<string, Dictionary<string, string>> dic1 = new Dictionary<string, Dictionary<string, string>>();
                dic1.Add(url, dic);

                Thread thread2 = new Thread(new ParameterizedThreadStart(threadGet));

                thread2.Start(dic1);





            }

            //MessageBox.Show("清空成功");

        }

    }
}
