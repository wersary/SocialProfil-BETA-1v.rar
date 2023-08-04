using MySql.Data.MySqlClient;
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
    public partial class Form2 : Form
    {
        string imageOpen;
        public Form2()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog setImage = new OpenFileDialog();
            setImage.Filter = "Image Files(*.PNG;*JPEG;*.BMP;*.JPG)|*.PNG;*JPEG;*.BMP;*.JPG|All files (*.*)|*.*";
            if (setImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(setImage.FileName);
                    imageOpen = setImage.FileName;

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool chekUpdateServer = false;
            try
            {
                if (chekUpdateServer == false)
                {
                    if (imageOpen == null)
                    {
                        MessageBox.Show("Вы не выбрали изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MySqlConnection connection = new MySqlConnection("здесь должен был быть запрос к подключению SQL");
                        string userName = textBox1.Text;
                        string userPassword = textBox2.Text;
                        Image image = Image.FromFile(imageOpen);
                        string userNickname = textBox3.Text;
                        string userOther = richTextBox1.Text;

                        if (userOther.Length > 199 || userOther == "" || userName.Length > 13 | userName == "" || userPassword.Length > 13 || userPassword == "" || userNickname.Length > 13 || userNickname == "" || image == null)
                        {
                            MessageBox.Show("Ввы ввели недопустимое количество символов, или не выбрали изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            //string insertQuery = $"UPDATE `mydb`.`products` SET `Image` = @Image WHERE (`product_id` = {productList[i].ProductId})";
                            //MySqlCommand command = new MySqlCommand(insertQuery, connection);
                            //command.Parameters.Add("@Image", MySqlDbType.MediumBlob);
                            // command.Parameters["@Image"].Value = bytes;
                            //command.ExecuteScalar();
                            using (MemoryStream ms = new MemoryStream())
                            {
                                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                byte[] bytes = ms.ToArray();
                                MySqlCommand registration = new MySqlCommand($"insert into acaunts_table value(\"{userName}\",\"{userPassword}\",\"\",\"{userOther}\",\"{userNickname}\");", connection);
                                MySqlCommand checkName = new MySqlCommand($"SELECT count(user_name)>0 FROM acaunts_table WHERE user_name = \"{userName}\";", connection);
                                MySqlCommand checkName1 = new MySqlCommand($"SELECT count(user_name)>0 FROM acaunts_table WHERE user_nik = \"{userNickname}\";", connection);
                                MySqlCommand setImage = new MySqlCommand($"update acaunts_table set user_image = @Image where user_name = \"{userName}\";", connection);
                                setImage.Parameters.Add("@Image", MySqlDbType.MediumBlob);
                                setImage.Parameters["@Image"].Value = bytes;
                                connection.Open();
                                string result = checkName.ExecuteScalar().ToString();
                                string result1 = checkName.ExecuteScalar().ToString();
                                connection.Close();
                                if (result == "0" & result1 == "0")
                                {
                                    connection.Open();
                                    registration.ExecuteScalar();
                                    setImage.ExecuteScalar();
                                    connection.Close();
                                    this.Hide();
                                    Form1 form1 = new Form1();
                                    form1.Show();
                                }
                                else if (result == "1")
                                {
                                    MessageBox.Show("Данный логин уже занят", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (result1 == "1")
                                {
                                    MessageBox.Show("Данный никнейм уже занят", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                chekUpdateServer = true;
                MessageBox.Show("Вышла новая версия программы или сервер перестал работать", "Oшибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}