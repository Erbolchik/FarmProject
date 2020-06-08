using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Project
{
    public partial class AddValue : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public AddValue()
        {
            InitializeComponent();
            comboBox1.Items.Add("Камаз");
            comboBox1.Items.Add("К-700");
            comboBox1.Items.Add("Challenge");
            comboBox1.Items.Add("MacDon");
            comboBox1.Items.Add("John Deere");
            comboBox1.Items.Add("Комбайн");
            comboBox1.Items.Add("Трактор");
            textBox1.Validating += seriaNumberBox_Validating;
            textBox2.Validating += nameBox_Validating;
            textBox3.Validating += countBox_Validating;
            comboBox1.Validating += categoryBox_Validating;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "Вы точно хотите перейти?",
              "Сообщение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                var form = new Sklad();
                form.Closed += (s, args) => this.Close();
                form.Show();
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
                if (textBox1.Text == "" || textBox2.Text == "" | textBox3.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Сначала заполните все поля!!");
                }
                else
                {
                    try
                    {
                        int seriaNumber = Convert.ToInt32(textBox1.Text);
                        string name = textBox2.Text;
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";
                        DateTime date = dateTimePicker1.Value;
                        int count = Convert.ToInt32(textBox3.Text);
                        string category = comboBox1.SelectedItem.ToString();
                        string comentary = richTextBox1.Text;
                        string query = String.Format("Insert into Склад_Для_Запчастей (Серийный_номер,Название_запчасти,Дата_прибытия,Количество,Категория,Коментарии) Values ('{0}','{1}','{2}','{3}','{4}','{5}')", seriaNumber, name, date, count, category, comentary);
                        DateTime today = DateTime.Now;

                        using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                        {
                            if (date > today)
                            {
                                MessageBox.Show("Ошибка даты,нельзя добавить будущую дату");
                            }
                            else if (today.Year - date.Year > 80)
                            {
                                MessageBox.Show("Ошибка даты,нельзя слишком старую дату");
                            }
                            else
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(query, connection);
                                command.ExecuteNonQuery();
                                MessageBox.Show("Успешно доблавено");

                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                richTextBox1.Text = "";
                                comboBox1.Text = "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка,повторите попытку!");
                        MessageBox.Show(ex.Message);

                    }
                }

            }

        }
        Regex onlyNumber = new Regex(@"^[0-9]+$");
        private void seriaNumberBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Не указан серийный номер!");
            }
            else if (!onlyNumber.IsMatch(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Только цифры без букв!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void nameBox_Validating(object sender, CancelEventArgs e)
        {

            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Не указано название!");
            }
            else if (textBox2.Text.Length < 1)
            {
                errorProvider1.SetError(textBox2, "Слишком короткое название!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void countBox_Validating(object sender, CancelEventArgs e)
        {

            if (String.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Не указано количество !");
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
        private void categoryBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Не указана категория !");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
