using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Main_window : Form
    {
        //全局版本号
        public const string Version = "1.2.11  Update date:2021/12/8";

        //全局语言
        bool is_english = false;

        //全局置顶参数
        bool is_top = false;

        public Main_window()
        {
            InitializeComponent();
        }


        //号码识别检测
        public static bool IsPhoneNo(string str_handset)
        {
            return Regex.IsMatch(str_handset, "^(0\\d{2,3}-?\\d{7,8}(-\\d{3,5}){0,1})|(((13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$");
        }

        //jwt
        string jwt_public;
        string username_public;

        //添加系统时间函数(在传入的字符串前添加时间),hh:mm:ss+stirng，参数二（1开0关）
        private string PrintStr_addtime(string Str, bool use_time)
        {
            string allstr;
            if (use_time == true)
            {
                allstr = DateTime.Now.ToString("HH:mm:ss") + "  " + Str + "\n";
            }
            else
            {
                allstr = Str + "\n";
            }
            return allstr;
        }

        //解析手机号验证码并返回Login json
        private string Sentsms(string Phonenum, string PassWord)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string url = "https://www.zhengdianzisha.com/zhibo/login/login?phone=";
            string url2 = "&code=";
            string allurl = url + Phonenum + url2 + PassWord;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(allurl);    //创建一个请求示例

            request.UserAgent = "Dart/2.13 (dart:io)"; //添加头
            request.Timeout = 2000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();
            return html;
        }

        //当点击登陆后的一系列响应
        private void button1_Click(object sender, EventArgs e)
        {
            //登陆前先刷新上次的内容
            //防止登录失败后内容的残留
            label13.Text = "未获取";
            label14.Text = "未获取";
            label15.Text = "未获取";
            label16.Text = "未获取";
            pictureBox1.Image = null;
            jwt_public = "";

            //判断是否输入为空
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入手机号");
                return;
            }

            if (!IsPhoneNo(textBox1.Text))
            {
                MessageBox.Show("手机号非法");
                return;
            }
            else
            {
                //传入手机号和验证码，获取登录返回的json字符串
                //string Login_Json = Sentsms(textBox1.Text, textBox2.Text);
                string Login_Json;

                try
                {
                    if (textBox2.ReadOnly == true)
                    {
                        Login_Json = Sentsms(textBox1.Text, "12345678");
                    }
                    else
                    {
                        Login_Json = Sentsms(textBox1.Text, textBox2.Text);
                    }
                }
                catch
                {

                    MessageBox.Show("网络连接超时");
                    return;



                }


                //判断json字符串中的errcode
                //通过实体类请求
                Root rt = JsonConvert.DeserializeObject<Root>(Login_Json);


                //JObject Login_Errcode_Json = (JObject)JsonConvert.DeserializeObject();
                int Login_Errcode = rt.errcode;

                //响应返回值
                label5.Text = Login_Errcode.ToString();
                //表示一切正常
                if (Login_Errcode == 0)
                {

                    //解析json_data字符串
                    //JObject Data_Json_jo = (JObject)JsonConvert.DeserializeObject(Login_Json);
                    //string Login_Data_Json = Data_Json_jo["data"].ToString();

                    //获取用户名
                    //string Nickname_Str = Login_User_Json_jo["nickname"].ToString();


                    //获取用户名
                    string Nickname_Str = rt.data.user.nickname;
                    //获取头像
                    string Headimgurl_Str = rt.data.user.headimgurl;
                    //获取开店状态
                    int is_store = rt.data.user.is_master;
                    //获取jwt验证
                    string jwt_str = rt.data.jwt.ToString();
                    //获取后端版本
                    string version_server = rt.data.version.ToString();


                    //答应解析到的用户名
                    //richTextBox1.Text += PrintStr_addtime("用户名:" + Nickname_Str, false);
                    //答应解析到的头像
                    //richTextBox1.Text += PrintStr_addtime("头像url:" + Headimgurl_Str, false);


                    label13.Text = Nickname_Str;

                    //判断是否开店
                    if (is_store == 1)
                    {
                        label14.Text = ("已开店");
                    }
                    else
                    {
                        label14.Text = ("未开店");
                    }

                    //richTextBox1.Text += PrintStr_addtime("jwt验证:\n" + jwt_str, false);
                    label15.Text = jwt_str;

                    //richTextBox1.Text += PrintStr_addtime("后端版本:" + version_server, false);
                    label16.Text = version_server;

                    pictureBox1.LoadAsync(Headimgurl_Str);

                    //jwt到全局变量
                    jwt_public = jwt_str;
                    //username到全局变量
                    username_public = Nickname_Str;
                }
                //errcode 10004
                else if (Login_Errcode == 10004)
                {
                    MessageBox.Show("验证码错误");
                }
                //errcode 10001
                else if (Login_Errcode == 10001)
                {
                    MessageBox.Show("请输入验证码");
                }
                //errcode els
                else
                {
                    MessageBox.Show("未识别的错误请反馈");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        ////即时刷新richtextbox控件
        //private void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        //{
        //    richTextBox1.SelectionStart = richTextBox1.Text.Length;
        //    richTextBox1.ScrollToCaret();
        //}



        //清空button响应的操作
        private void button2_Click(object sender, EventArgs e)
        {
            label13.Text = label14.Text = label15.Text = label16.Text = "           ";
            pictureBox1.Image = null;
            jwt_public = "";

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_verify()
        {
            //加密验证

            login_passwd form6 = new login_passwd();
            form6.String1 = Version;
            form6.Follow_Version();

            //加强检测，通过返回值判定是否通过验证
            if (form6.verify_true())
            {
                Process.GetCurrentProcess().Kill();
            }
            form6.ShowDialog();
            //加强检测，通过返回值判定是否通过验证
            if (!form6.verify_true())
            {
                Process.GetCurrentProcess().Kill();
            }
            //加密验证结束
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            label2.TextAlign = ContentAlignment.MiddleRight;
            label8.TextAlign = ContentAlignment.MiddleRight;
            label9.TextAlign = ContentAlignment.MiddleRight;
            label10.TextAlign = ContentAlignment.MiddleRight;
            label11.TextAlign = ContentAlignment.MiddleRight;
            label12.TextAlign = ContentAlignment.MiddleRight;

            label6.Text = Version;

            //加密验证
            //login_verify();


            timer1.Enabled = true;
        }



        int check_num = 0;
        private void label2_Click(object sender, EventArgs e)
        {
            check_num++;

            if (check_num == 3)
            {
                label2.ForeColor = System.Drawing.Color.Red;
                label2.Text += "\n(实验功能)";
                textBox2.ReadOnly = true;
                button5.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            label2.Text = "测试软件，不可商用";
            label2.ForeColor = System.Drawing.Color.Black;
            textBox2.ReadOnly = false;
            textBox2.Text = "";
            check_num = 0;
        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void 打开窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("打开");
        }

        private void 关闭窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关闭");
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {

        }


        //关于软件
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            Disclaimer form3 = new Disclaimer();//实例化一个Form2窗口
            form3.String1 = Version;
            form3.Follow_Version();

            form3.Top_bool = is_top;
            form3.ShowDialog();
        }


        //更改用户名
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }



        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {


        }


        //论坛发帖
        private void 论坛发帖ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void Main_window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }



        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            开启ToolStripMenuItem.Checked = true;
            关闭ToolStripMenuItem.Checked = false;
            is_top = true;
            this.TopMost = is_top;
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            关闭ToolStripMenuItem.Checked = true;
            开启ToolStripMenuItem.Checked = false;

            is_top = false;
            this.TopMost = is_top;
        }

        private void Main_window_Paint(object sender, PaintEventArgs e)
        {
            //Rectangle tang = this.ClientRectangle;					//获取窗口矩形 为了下面得到窗口的宽高
            //Graphics g3 = e.Graphics;								//新建一个画布
            //Color c3 = Color.FromArgb(46, 204, 113);				//声明一个 颜色
            //Pen p3 = new Pen(c3);									//新建一支画笔
            //g3.SmoothingMode = SmoothingMode.HighQuality;                 //抗锯齿 使得线条变柔顺  在画斜线或者曲线的时候使用
            //g3.InterpolationMode = InterpolationMode.HighQualityBicubic;    //使得画出来的效果高质量
            //g3.CompositingQuality = CompositingQuality.HighQuality;           //高质量画图
            //g3.DrawLine(p3, 0, 0, 0, tang.Height - 1);				//在（0，0）和（tang.Width - 1, 0）这两点间画一条直线
            //g3.DrawLine(p3, 0, tang.Height - 1, tang.Width - 1, tang.Height - 1);	//注意必须减1 不然显示不出来  因为 如果假设窗口的高度是3像素 我们知道（0，0）位置代表 窗口最左上角的像素点  那么最左下角的像素点应该是（0，2） 而不是（0，3） 因为0，1，2 已经三个像素点了
            //g3.DrawLine(p3, tang.Width - 1, tang.Height - 1, tang.Width - 1, 0);
            //g3.DrawLine(p3, tang.Width - 1, 0, 0, 0);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("文本复制成功");

            if (label15.Text != "")
            {
                Clipboard.SetDataObject(label15.Text);
            }

        }

        string p_title = "测试软件，不可商用  ";

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = p_title + DateTime.Now.ToString();
        }

        private void 商品上架ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(jwt_public))
            {
                Form5 form5 = new Form5();
                form5.String1 = Version;
                form5.Follow_Version();
                form5.Form5_jwt_str = jwt_public;
                //传递置顶消息
                form5.Top_bool = is_top;


                form5.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登陆账号");


            }

        }

        private void 连接服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("尚未连接到服务器");
        }

        private void 运费模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Freight_template form6 = new Freight_template();
            form6.String1 = Version;
            form6.Follow_Version();
            form6.Form2_jwt_str = jwt_public;
            //传递置顶消息
            form6.Top_bool = is_top;


            form6.ShowDialog();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void 满包邮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Free_shipping form7 = new Free_shipping();
            form7.String1 = Version;
            form7.Follow_Version();
            form7.Form2_jwt_str = jwt_public;
            //传递置顶消息
            form7.Top_bool = is_top;


            form7.ShowDialog();

        }

        private void 发货仓ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shipping_warehouse form8 = new Shipping_warehouse();
            form8.String1 = Version;
            form8.Follow_Version();
            form8.Form2_jwt_str = jwt_public;
            //传递置顶消息
            form8.Top_bool = is_top;


            form8.ShowDialog();
        }

        private void 搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search_page form9 = new Search_page();
            form9.String1 = Version;
            form9.Follow_Version();
            form9.Form2_jwt_str = jwt_public;
            //传递置顶消息
            form9.Top_bool = is_top;


            form9.ShowDialog();
        }

        private void 资料修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(jwt_public))
            {
                Change_username lForm = new Change_username();//实例化一个Form2窗口

                //传递version->value
                lForm.String1 = Version;
                //设置Form2中version的
                lForm.Follow_Version();
                //传递jwt
                lForm.Form2_jwt_str = jwt_public;
                //传递当前用户名
                lForm.Form2_username_str = username_public;
                //设置Form2中的用户名
                lForm.Follow_username();

                lForm.Top_bool = is_top;
                //模态对话框
                lForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登陆账号");


            }
        }

        private void 商品下架ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(jwt_public))
            {
                offSelf form10 = new offSelf();//实例化一个窗口

                //传递version->value
                form10.String1 = Version;
                //设置Form2中version的
                form10.Follow_Version();
                //传递jwt
                form10.Form2_jwt_str = jwt_public;


                //模态对话框
                form10.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登陆账号");


            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            is_english = true;
            简体中文ToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = true;
            groupBox1.Text = "Login area";
            label1.Text = "Phone:";
            label3.Text = "code:";
            button1.Text = "Login";
            button2.Text = "Sign out";

            groupBox2.Text = "information area";
            label8.Text = "Username:";
            label9.Text = "Store status";
            label11.Text = "Server version:";
            label12.Text = "Head img:";

            button5.Text = "close";


            功能ToolStripMenuItem.Text = "Tools";
            设置ToolStripMenuItem.Text = "Setting";
            关于ToolStripMenuItem.Text = "Help";

            toolStripMenuItem4.Text = "Account management";
            资料修改ToolStripMenuItem.Text = "Account data editing";
            注销账号ToolStripMenuItem.Text = "Logout account";

            店铺设置ToolStripMenuItem.Text = "Store setting";

            商品管理ToolStripMenuItem.Text = "Goods management";
            商品删除ToolStripMenuItem.Text = "Goods delete";
            商品下架ToolStripMenuItem.Text = "Goods off the shelf";

            搜索ToolStripMenuItem.Text = "Search";


            label2.Text = "Test mode";
            p_title = "Test software, not for commercial use  ";

        }

        private void 简体中文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            is_english=false;
            简体中文ToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;

            groupBox1.Text = "登录区域";
            label1.Text = "手机号:";
            label3.Text = "验证码:";
            button1.Text = "登录";
            button2.Text = "登出";

            groupBox2.Text = "信息区域";
            label8.Text = "用户名:";
            label9.Text = "店铺状态";
            label11.Text = "后端版本:";
            label12.Text = "头像:";

            button5.Text= "关闭";


            功能ToolStripMenuItem.Text = "功能";
            设置ToolStripMenuItem.Text = "设置";
            关于ToolStripMenuItem.Text = "帮助";


            label2.Text = "测试软件，不可商用";
            p_title = "测试软件，不可商用  ";

        }

        private void 论坛发帖ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(jwt_public))
            {
                Send_discuz form6 = new Send_discuz();

                //传递version->value
                form6.String1 = Version;
                //设置Form2中version的
                form6.Follow_Version();
                //传递jwt
                form6.Form2_jwt_str = jwt_public;

                //传递置顶消息
                form6.Top_bool = is_top;

                form6.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登陆账号");
            }
        }
    }
}
