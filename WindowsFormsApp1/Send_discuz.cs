using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Send_discuz : Form
    {
        public Send_discuz()
        {
            InitializeComponent();
        }



        private bool top_bool;

        public bool Top_bool
        {
            set
            {
                top_bool = value;
            }
        }




        private void Send_discuz_Load(object sender, EventArgs e)
        {
            this.TopMost = top_bool;


            richTextBox1.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
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


        //父窗口jwt传参
        private string jwt_str;
        public string Form2_jwt_str
        {
            set
            {
                jwt_str = value;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


            if (richTextBox1.Text == "")
            {
                label4.Visible = true;
            }
            else
            {
                label4.Visible = false;
            }

            //字数统计
            int str_len = Len(richTextBox1.Text);

            string out_str_len = str_len.ToString() + "/1000";

            label8.Text = out_str_len;

        }

        //字符统计
        int Len(string text)
        {
            int len = 0;


            //
            int iAllChr = 0; //字符总数：不计字符'\n'和'\r'

            //int iChineseChr = 0; //中文字符计数
            //int iChinesePnct = 0;//中文标点计数
            //int iEnglishChr = 0; //英文字符计数
            //int iEnglishPnct = 0;//中文标点计数
            //int iNumber = 0;  //数字字符：0-9

            foreach (char ch in text)
            {
                //if (ch != '\n' && ch != '\r')
                // {
                iAllChr++;
                // }

                //其他字符统计
                //if ("～！＠＃￥％…＆（）—＋－＝".IndexOf(ch) != -1 ||
                // "｛｝【】：“”；‘'《》，。、？｜＼".IndexOf(ch) != -1) iChinesePnct++;
                //if (ch >= 0x4e00 && ch <= 0x9fbb) iChineseChr++;
                //if ("`~!@#$%^&*()_+-={}[]:\";'<>,.?/\\|".IndexOf(ch) != -1) iEnglishPnct++;
                //if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')) iEnglishChr++;
                //if (ch >= '0' && ch <= '9') iNumber++;
            }


            len = iAllChr;
            return len;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label5.Visible = true;
            }
            else
            {
                label5.Visible = false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

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


        //get
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(jwt_str))
            {
                MessageBox.Show("请先登录账号");
                return;
            }

            if (!(richTextBox1.Text != "" && textBox1.Text != "" && comboBox1.Text != "请选择"))
            {
                MessageBox.Show("失败，请把内容填写完整后再尝试提交");
            }


            string url = "https://www.zhengdianzisha.com/zhibo/discuz/save" + "?jwt=" + jwt_str;
            int cat_id, is_show;


            //combo下标方式获取对应元素键值
            //cat_id = comboBox1.SelectedIndex + 1;

            //方法2
            dic_di.TryGetValue(comboBox1.Text, out cat_id);

            if (richTextBox1.Enabled)
            {
                is_show = 1;
            }
            else
            {
                is_show = 0;
            }
            
            string PostData =
                "cat_id=" + cat_id.ToString() +
                "&content=" + richTextBox1.Text +
                "&address=" + textBox1.Text +
                "&is_show=" + is_show.ToString() +
                textBox2.Text;
            Post(url, PostData);
            MessageBox.Show("发布成功");
            this.Close();


        }

        //获取发送文件用的token
        private Dictionary<string, string> get_uploadtoken()
        {
            //创建存储返回值的字典
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //url
            string get_token_url = "https://www.zhengdianzisha.com/zhibo/img/uploadtoken?type=image&suffix=jpg" + "&jwt=" + jwt_str;

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


        string public_ossurl;
        string public_imgurl;
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


        private void button2_Click(object sender, EventArgs e)
        {
            string[] file_string = all_upload(true);

            if (file_string != null)
            {
                //定义副图发送字符串
                string side_picture = "";
                foreach (string file_name in file_string)
                {
                    side_picture += "&img[]=";
                    side_picture += file_name;
                }
                textBox2.Text = side_picture;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        //全局combo词典
        Dictionary<string, int> dic_di = new Dictionary<string, int>();
        private void get_di_combo()
        {
            string url = "https://www.zhengdianzisha.com/zhibo/discuz/getCategory";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("jwt", jwt_str);
            string ret = Get(url, dic);

            Root5 rt = JsonConvert.DeserializeObject<Root5>(ret);

            if (rt.errcode != 0)
            {
                MessageBox.Show("combo载入未知错误");
                return;
            }


            //置空
            dic_di.Clear();
            comboBox1.Items.Clear();

            foreach (var val in rt.list)
            {
                //存储对应标
                dic_di.Add(val.name, int.Parse(val.id));
                comboBox1.Items.Add(val.name);
            }



        }


        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            get_di_combo();


        }
    }
}
