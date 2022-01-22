using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Disclaimer : Form
    {
        public Disclaimer()
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


        private void Form3_Load(object sender, EventArgs e)
        {
            this.TopMost = top_bool;
            richTextBox1.Text = "声明:使用此软件所造成的任何问题与本人无关\n\n\n引用的开源的json库。";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
