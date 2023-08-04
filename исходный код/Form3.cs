using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form3 : Form
    {
        public Form3(string userName)
        {
            InitializeComponent();
            MySqlConnection connection = new MySqlConnection("здесь должен был быть запрос к подключению SQL");
            MySqlCommand getImage = new MySqlCommand($"SELECT user_image FROM acaunts_table where user_name = \"{userName}\";",connection);
            MySqlCommand getNik = new MySqlCommand($"SELECT user_nik FROM acaunts_table where user_name = \"{userName}\";", connection);
            MySqlCommand getOther = new MySqlCommand($"SELECT user_other FROM acaunts_table where user_name = \"{userName}\";", connection);
            System.Drawing.Image image;
            connection.Open();
            byte[] bytes = (byte[])getImage.ExecuteScalar();
            connection.Close();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(stream );
            }
            pictureBox1.Image = image;
            connection.Open();
            label2.Text = getNik.ExecuteScalar().ToString();
            label3.Text = userName;
            label1.Text = getOther.ExecuteScalar().ToString();
            connection.Close();

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
