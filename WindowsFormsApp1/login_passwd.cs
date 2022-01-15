using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class login_passwd : Form
    {
        public login_passwd()
        {
            InitializeComponent();
        }

        string nowtime;//yyyyMMDD
        string nowtime_md5;//

        //全局验证
        bool is_true = false;

        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public int[] calc_passwd_array()
        {
            Random rd = new Random();
            int[] calc_result = new int[3];

            int num1 = rd.Next(1, 50);
            int num2 = rd.Next(1, 50);

            calc_result[0] = num1;
            calc_result[1] = num2;
            calc_result[2] = num1 + num2;

            return calc_result;
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

        public void Follow_Version()
        {
            label6.Text = string1;
        }

        private void login_passwd_Load(object sender, EventArgs e)
        {

        }

        //加强验证
        public bool verify_true()
        {
            return is_true;
        }
        int error_num = 0;

        private void button1_Click(object sender, EventArgs e)
        {

            error_num++;
            if (error_num == 3)
            {
                MessageBox.Show("失败次数超限，感谢使用，再见。");
                is_true = false;
                Process.GetCurrentProcess().Kill();
            }
            if (textBox1.Text == nowtime && textBox2.Text == "123456" && textBox3.Text == calc_result_all.ToString())
            {
                MessageBox.Show("验证通过，请使用");
                is_true = true;
                this.Close();
            }
            else
            {
                is_true = false;
                MessageBox.Show("验证失败");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nowtime = DateTime.Now.ToString("yyyyMMddhhmm");
            nowtime_md5 = GenerateMD5(nowtime);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        int calc_result_all;
        private void button2_Click(object sender, EventArgs e)
        {
            int[] ret_calc = calc_passwd_array();
            string show_calc = ret_calc[0].ToString() + "+" + ret_calc[1].ToString();
            label4.Text = show_calc;
            calc_result_all = ret_calc[2];
        }

        private void login_passwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_true == false)
            {
                Process.GetCurrentProcess().Kill();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rd = new Random();
            int i = rd.Next(1, 20000000);
            string nowtime1_md5 = GenerateMD5(i.ToString());
            label9.Text = nowtime1_md5;
        }
    }
}
