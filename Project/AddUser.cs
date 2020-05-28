using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AddUser : Form
    {

        public AddUser()
        {
            InitializeComponent();
            string[] jobWhere = { "Машдвор", "Ц/Ток", "М/Эливатор", "Автопарк", "Мтс", "Бригада", "Контора" };
            string[] ogregat = { "К700", "МТЗ", "ДТ-75", "Камаз", "ЗИЛ", "Нива Москвич", "Уаз", "ДонМар", "MagDon", "Challenge", "John Deere" };
            foreach (var i in jobWhere)
                comboBox1.Items.Add(i);
            foreach (var i in ogregat)
                comboBox2.Items.Add(i);
            textBox1.Validating += nameBox_Validating;
            textBox2.Validating += lastnameBox_Validating;
            textBox4.Validating += middlenameBox_Validating;
            textBox8.Validating+=phoneeBox_Validating;
            textBox5.Validating+=addressBox_Validating;
            textBox7.Validating+=positionBox_Validating;
            textBox6.Validating+=salaryBox_Validating;
            textBox3.Validating+=numberogregatBox_Validating;
            comboBox1.Validating += jobBox_Validating;
            comboBox2.Validating += ogregatBox_Validating;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
             "Вы точно хотите перейти?",
             "Сообщение",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Information,
             MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var from = new Razdel_3();
                from.Closed += (s, args) => this.Close();
                from.Show();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Вы точно хотите добавить?",
               "Сообщение",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {


                if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox8.Text == "" ||
                    textBox5.Text == "" || textBox7.Text == "" || textBox3.Text == "" || textBox6.Text == "" ||
                    comboBox1.Text == "" || comboBox2.Text == "")
                {
                    MessageBox.Show("Сначала заполните все поля!!");
                }
                else
                {
                    try
                    {
                        string name = textBox1.Text;
                        string lastname = textBox2.Text;
                        string middlename = textBox4.Text;
                        string phone = textBox8.Text;
                        DateTime birthday = dateTimePicker1.Value.Date;
                        string address = textBox5.Text;
                        string position = textBox7.Text;
                        int salary = Convert.ToInt32(textBox6.Text);
                        string jobWhere = comboBox1.SelectedItem.ToString();
                        string ogregat = comboBox2.SelectedItem.ToString();
                        int numberOgregat = Convert.ToInt32(textBox3.Text);


                        string quearyAdd = String.Format("Insert into Сотрудники (Фамилия,Имя,Отчество,Номер_Телефона,Дата_Рождения,Адрес_Проживания,Должность,Оклад,Место_Работы,Огрегаты,Серийный_номер_огрегата)" +
                            "Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", name, lastname, middlename, phone, birthday, address, position, salary, jobWhere, ogregat, numberOgregat);


                        using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                        {
                            connection.Open();
                            SqlCommand commandAddUsers = new SqlCommand(quearyAdd, connection);
                            commandAddUsers.ExecuteNonQuery();
                            MessageBox.Show("Данные успешны добавлены");

                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox8.Text = "";
                            comboBox1.Text = "";
                            comboBox2.Text = "";

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        Regex regex = new Regex("^[a-zA-Z]+$");
        Regex phoneNumpattern = new Regex(@"\+[0-9]{11}");
        Regex onlyNumber = new Regex(@"^[0-9]+$");
        private void nameBox_Validating(object sender, CancelEventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Не указано фамилия!");
            }
            else if (textBox1.Text.Length < 1)
            {
                errorProvider1.SetError(textBox1, "Слишком короткое фамилия!");
            }
            else if (!regex.IsMatch(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Пишите только буквы,без цифр!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void lastnameBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Не указано имя!");
            }
            else if (textBox2.Text.Length < 2)
            {
                errorProvider1.SetError(textBox2, "Слишком короткое имя!");
            }
            else if (!regex.IsMatch(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Пишите только буквы,без цифр!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void middlenameBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Не указано отчество!");
            }
            else if (textBox4.Text.Length < 2)
            {
                errorProvider1.SetError(textBox4, "Слишком короткое отчество!");
            }
            else if (!regex.IsMatch(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Пишите только буквы,без цифр!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        private void phoneeBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox8.Text))
            {
                errorProvider1.SetError(textBox8, "Не указан телефон!");
            }
            else if (!onlyNumber.IsMatch(textBox8.Text))
            {
                errorProvider1.SetError(textBox8, "Пишите только цифры,без букв!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void addressBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox5.Text))
            {
                errorProvider1.SetError(textBox5, "Не указан адрес!");
            }
            else if (textBox5.Text.Length < 1)
            {
                errorProvider1.SetError(textBox5, "Слишком короткое имя адреса!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void positionBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox7.Text))
            {
                errorProvider1.SetError(textBox7, "Не указана должность !");
            }
            else if (textBox7.Text.Length < 2)
            {
                errorProvider1.SetError(textBox7, "Слишком короткое имя должности!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void salaryBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox6.Text))
            {
                errorProvider1.SetError(textBox6, "Не указано ЗП!");
            }
            else if(!onlyNumber.IsMatch(textBox6.Text))
            {
                errorProvider1.SetError(textBox6, "Только цифры без букв!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void numberogregatBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Не указано имя!");
            }
            else if (!onlyNumber.IsMatch(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Только цифры без букв!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void jobBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Не указано место работы !");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void ogregatBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "Не указан огрегат !");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
