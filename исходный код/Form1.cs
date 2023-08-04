using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public string userNameText;
        public string userPasswordText;
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextAlignChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TabStopChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = userNameText;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            userNameText = textBox1.Text;
            textBox1.Text = "user name";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = userPasswordText;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            userPasswordText = textBox2.Text;
            textBox2.Text = "password";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 newWindow = new Form2();
            newWindow.Show();
            this.Hide();

        }

        private void enter_button_Click(object sender, EventArgs e)
        {
            bool chekUpdateServer = false;
            try
            {
                if (chekUpdateServer == false)
                {
                    MySqlConnection connection = new MySqlConnection("здесь должен был быть запрос к подключению SQL");
                    MySqlCommand getPassword = new MySqlCommand($"select user_password from acaunts_table where user_name = \"{userNameText}\";", connection);
                    connection.Open();
                    if (userNameText == "" || userPasswordText == "")
                    {
                        MessageBox.Show("Вы оставили поле(ля) пустым(и)", "Oшибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        string mySqlPassword = "";
                        bool chek = false;
                        try
                        {
                            if (chek == false)
                            {
                                mySqlPassword = getPassword.ExecuteScalar().ToString();
                            }
                        }
                        catch
                        {
                            chek = true;

                        }
                        {
                            connection.Close();
                            if (mySqlPassword == "" || mySqlPassword != userPasswordText)
                            {
                                MessageBox.Show("Вы ввели неверный логин или пароль", "Oшибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                textBox1.Text = "";
                                textBox2.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Form3 form3 = new Form3(userNameText);
                                this.Hide();
                                form3.Show();
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
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

//username 14
//usrpassword 14
//nik 14
//other 200