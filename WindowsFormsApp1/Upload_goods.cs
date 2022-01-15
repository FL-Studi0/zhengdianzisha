using Newtonsoft.Json;
using RestSharp;
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



    public partial class Form5 : Form
    {
        public Form5()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }



        //封装获取商品类别的函数
        private Root2 goods_list_class(string get_url, int id)
        {
            //拼接请求url
            string all_url = get_url + "?id=" + id.ToString();

            //发送请求
            string post_ret = Post(all_url, "");

            //返回的请求转化class类
            Root2 rt = JsonConvert.DeserializeObject<Root2>(post_ret);

            //返回class
            return rt;
        }




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


        private bool TextBox_FULL()
        {
            bool FULL = false;

            if (textBox1.Text == "" || textBox2.Text == "" || shop_list == -1 ||
                textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" ||
                textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" ||
                textBox10.Text == "" || textBox11.Text == "")
            {
                FULL = false;

            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    if (textBox13.Text == "")
                    {
                        return false;
                    }

                }
                FULL = true;
            }




            return FULL;
        }

        private void threadPost(object url)
        {

            //强转arraylist
            foreach(var key in (Dictionary<string,string>)url)
            {

                Post(key.Key, key.Value);
            }

        }

        private void threadFunc(object obj)
        {
            //设置pro maximux属性为数据最大值
            progressBar1.Maximum = (int)obj;
            progressBar1.Value = 0;

            string textShow = "";





            string url1 = "https://www.zhengdianzisha.com/zhibo/store/create-goods?jwt=";
            string jwt = form5_jwt_str;

            string PostUrl = url1 + jwt;

            int is_limit = 0;
            if (checkBox1.Checked == true)
            {
                is_limit = 1;

            }
            else
            {
                is_limit = 0;
            }


            string PostData =
                "name=" + textBox1.Text +
                "&unit=" + textBox2.Text +
                "&category_id=" + shop_list.ToString() +
                "&stock=" + textBox4.Text +
                "&now_price=" + textBox5.Text +
                "&old_price=" + textBox6.Text +
                "&freight_id=" + textBox7.Text +
                "&post_id=" + textBox8.Text +
                "&house_id=" + textBox9.Text +
                "&thumb=" + textBox10.Text +
                 textBox11.Text +
                 textBox12.Text +
                "&is_limit=" + is_limit.ToString() +
                "&limit=" + textBox13.Text;
            //MessageBox.Show(PostData);




            for (int i = 0; i < (int)obj; i++)
            {
                //更新ui
                progressBar1.Value = i + 1;
                textShow = (i + 1).ToString() + "/" + obj.ToString();
                label17.Text = textShow;


                //组合array
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add(PostUrl, PostData);

                //创建线程
                Thread thread = new Thread(new ParameterizedThreadStart(threadPost));
                thread.Start(dic);


            }


            //if (ret_goods_str != "0")
            //{
            //    Root1 rt = JsonConvert.DeserializeObject<Root1>(ret_goods_str);
            //    if (rt.errcode == 0)
            //    {
            //        MessageBox.Show("提交成功!\n" + "平台总商品个数" + rt.goods_id.ToString() + "\n" +
            //            "需要支付的保证为:" + rt.msg.ToString() + "\n" + "提示:" + rt.tips.ToString());
            //    }
            //    else
            //    {
            //        MessageBox.Show("提交失败:" + rt.errmsg.ToString());

            //    }
            //}




        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox3.Text == "")
            {
                textBox3.Text = "1";
            }
            //上架次数
            int num1 = int.Parse(textBox3.Text);

            bool ret_FULL = TextBox_FULL();

            //判断格子是否填满
            if (ret_FULL == false)
            {
                MessageBox.Show("咦，居然还有空没有填！");

            }


            Thread thread = new Thread(new ParameterizedThreadStart(threadFunc));
            thread.Start(num1);


        }

        //JObject Data_Json_jo = (JObject)JsonConvert.DeserializeObject(Login_Json);
        //string Login_Data_Json = Data_Json_jo["data"].ToString();


        //父窗口版本号传参
        private string string1;

        public string String1
        {
            set
            {
                string1 = value;
            }
        }


        private bool top_bool;

        public bool Top_bool
        {
            set
            {
                top_bool = value;
            }
        }

        public void Follow_Version()
        {
            label13.Text = string1;
        }

        private string form5_jwt_str;
        public string Form5_jwt_str
        {
            set
            {
                form5_jwt_str = value;
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //设置窗口属性
            this.TopMost = top_bool;












        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox13.Text = "";
                textBox13.Visible = true;
            }
            else
            {
                textBox13.Text = "";
                textBox13.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            //add_url form6 = new add_url();
            //form6.ShowDialog();


            //TextBox box = new TextBox();
            //this.Controls.Add(box);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "请选择";
        }

        static string goods_list_url = "https://www.zhengdianzisha.com/zhibo/store/goods-type";
        //当combo1下拉框点按后
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            //清空combo1
            comboBox1.Items.Clear();

            //定义常量

            //获取商品1列表
            //参数2为0，表示总类
            Root2 rt = goods_list_class(goods_list_url, 0);

            //当返回结果正确时
            if (rt.errcode == 0)
            {
                //遍历rt.list
                foreach (var num in rt.list)
                {

                    comboBox1.Items.Add(num.name);
                }
            }
            else
            {
                return;
            }

        }



        private void comboBox2_DropDown(object sender, EventArgs e)
        {

            //获取combo1的index
            int cat_id = comboBox1.SelectedIndex;
            //判断是否未选择父菜单
            if (cat_id == -1)
            {
                return;
            }

            //清空combo2的items
            comboBox2.Items.Clear();

            Root2 rt = goods_list_class(goods_list_url, 0);

            //获取到二级combo的id
            int cat_id2 = rt.list[cat_id].id;

            //访问二级combo的id
            Root2 rt2 = goods_list_class(goods_list_url, cat_id2);

            //遍历二级菜单数值
            foreach (var name_str in rt2.list)
            {
                comboBox2.Items.Add(name_str.name);
            }

        }






        int shop_list = -1;

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取combo1的index
            int cat_id = comboBox1.SelectedIndex;
            int cat_id2 = comboBox2.SelectedIndex;

            Root2 rt = goods_list_class(goods_list_url, 0);

            //获取到二级combo的id
            int combo_id = rt.list[cat_id].id;

            //访问二级combo的id
            Root2 rt2 = goods_list_class(goods_list_url, combo_id);

            shop_list = rt2.list[cat_id2].id;





        }


        string public_ossurl;
        string public_imgurl;

        //获取发送文件用的token
        private Dictionary<string, string> get_uploadtoken()
        {
            //创建存储返回值的字典
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //url
            string get_token_url = "https://www.zhengdianzisha.com/zhibo/img/uploadtoken?type=image&suffix=jpg" + "&jwt=" + form5_jwt_str;

            //发送请求获取token
            string token_ret = Post(get_token_url, "");

            //用类解析返回的json
            uploadtoken rt = JsonConvert.DeserializeObject<uploadtoken>(token_ret);

            //
            public_ossurl = rt.data.url;
            string public_key = rt.data.dir + rt.data.filename;

            public_imgurl = public_ossurl + "/" + public_key;


            if (rt.errcode == 0)
            {
                dic.Add("OSSAccessKeyId", rt.data.accessid);
                dic.Add("policy", rt.data.policy);
                dic.Add("Signature", rt.data.signature);
                dic.Add("key", rt.data.dir + rt.data.filename);
                dic.Add("success_action_status", "200");
                dic.Add("callback", rt.data.callback);
            }


            return dic;
        }


        //上传文件接口
        private string upload_file_oss(Dictionary<string, string> dic, string file_name)
        {


            var client = new RestClient(public_ossurl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            //遍历字典中的值到requests
            foreach (KeyValuePair<string, string> key in dic)
            {
                request.AddParameter(key.Key, key.Value);
            }

            //添加文件
            request.AddFile("file", file_name);
            IRestResponse response = client.Execute(request);

            //上传后的返回值
            string upload_ret = response.Content;


            //调用封装类
            oss_class rt = JsonConvert.DeserializeObject<oss_class>(upload_ret);

            if (rt.Status == "Ok")
            {
                return public_imgurl;
            }
            else
            {
                return "上传失败";
            }
        }


        //获取文件路径接口
        private string[] get_filename(bool is_true)
        {
            //存储文件路径
            //string all_filename = "";

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "图像文件(*.jpg;*.png;*.bmp;*.gif)|*.jpg;*.png;*.bmp;*.gif|所有文件-测试(*.*)|*.*";
            dlg.Multiselect = is_true;//等于true表示可以选择多个文件
            dlg.Title = "请选择文件";

            if (dlg.ShowDialog() == DialogResult.OK)
            {


                return dlg.FileNames;

            }
            else
            {
                return null;
            }



        }


        //封装上传总函数，方便调用
        //返回上传成功后的utl
        //参数1 是否多选
        private string[] all_upload(bool is_true)
        {



            //选择文件，返回文件路径
            string[] file_name = get_filename(is_true);
            if (file_name == null)
            {
                return null;
            }

            string[] oss_ret_files = new string[file_name.Length];

            for (int i = 0; i < file_name.Length; i++)
            {
                //获取上传密钥
                Dictionary<string, string> dic = get_uploadtoken();
                //oss文件上传
                oss_ret_files[i] = upload_file_oss(dic, file_name[i]);

            }

            return oss_ret_files;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string[] file_string = all_upload(false);
            if (file_string != null)
            {
                foreach (string file_name in file_string)
                {
                    textBox10.Text = file_name;
                }
            }



        }





        private void button3_Click_1(object sender, EventArgs e)
        {

            string[] file_string = all_upload(true);
            if (file_string != null)
            {
                //定义副图发送字符串
                string side_picture = "";
                foreach (string file_name in file_string)
                {
                    side_picture += "&banner_img[]=";
                    side_picture += file_name;
                }
                textBox11.Text = side_picture;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] file_string = all_upload(true);
            if (file_string != null)
            {
                //定义副图发送字符串
                string side_picture = "";
                foreach (string file_name in file_string)
                {
                    side_picture += "&detail_img[]=";
                    side_picture += file_name;
                }
                textBox12.Text = side_picture;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
