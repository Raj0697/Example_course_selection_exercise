using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exercise_course_selection
{
    public partial class Form1 : Form
    {
        SqlConnection con,con2;
        SqlCommand cmd,cmd2;
        SqlDataReader read,data;
        int temp;
        int i = 5;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = "5";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=RAJ;Initial Catalog=Raj;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("select COUNT(course) from student where course='"+comboBox1.Text+"'",con);
            read = cmd.ExecuteReader();
            if (read.Read())
            {
                int count = int.Parse(read.GetValue(0).ToString());
                temp = 5 - count;
              //  MessageBox.Show(""+temp);
                textBox4.Text = "" + temp;
                if(textBox4.Text == "0")
                {
                    MessageBox.Show("no seats are available");
                    MessageBox.Show("you cannot apply to this course!!!");
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                }
                else
                {
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                }
            }
            else
            {
                MessageBox.Show("no data found");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox4.Text == "0")
            {
                MessageBox.Show("no seats are available");
                
            }
            else
            {
                con = new SqlConnection(@"Data Source=RAJ;Initial Catalog=Raj;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("insert into student values(@reg,@name,@course)", con);
                cmd.Parameters.AddWithValue("@reg", textBox1.Text);
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@course", comboBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                DialogResult dr = MessageBox.Show("success","message",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                if(dr == DialogResult.OK)
                {
                    con2 = new SqlConnection(@"Data Source=RAJ;Initial Catalog=Raj;Integrated Security=True");
                    con2.Open();
                    cmd2 = new SqlCommand("", con2);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Text = "" + 5;
                    textBox4.Clear();
                    comboBox1.Text = "";
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Text = "" + 5;
                    textBox4.Clear();
                    comboBox1.Text = "";
                }
            }
            
        }
    }
}
