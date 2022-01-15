using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class add_url : Form
    {
        public add_url()
        {
            InitializeComponent();
        }

        private void add_url_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string string_img_url = "banner_img[]=" + textBox1.Text +
                                   "&banner_img[]=" + textBox2.Text +
                                   "&banner_img[]=" + textBox3.Text +
                                   "&banner_img[]=" + textBox4.Text;

            richTextBox1.Text = string_img_url;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
