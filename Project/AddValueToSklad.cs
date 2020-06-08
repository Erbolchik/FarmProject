using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Project
{
    public partial class AddValueToSklad : Form
    {

        public AddValueToSklad()
        {
            InitializeComponent();
            comboBox1.Validating += skladNumberBox_Validating;
            comboBox2.Validating += nameBox_Validating;
            textBox1.Validating += countBox_Validating;
            textBox3.Validating += namePostavkaBox_Validating;
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
                if (textBox1.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
                {
                    MessageBox.Show("Сначала заполните все поля!!");
                }
                else
                {
                    try
                    {
                        int count = Convert.ToInt32(textBox1.Text);
                        string name = comboBox2.SelectedItem.ToString();
                        string namePostavka = textBox3.Text;
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";
                        DateTime date = dateTimePicker1.Value;
                        int skladNumber = Convert.ToInt32(comboBox1.SelectedItem.ToString());

                        string query = String.Format("Insert into Склады (Еденица_на_складе,Название_культуры,ФИО_Механизатора,Номер_Склада,Дата_поставки) Values ('{0}','{1}','{2}','{3}','{4}')", count, name, namePostavka, skladNumber, date);
                        int maxCapacity = 0;
                        int? countInSklad = 0;
                        string queryCapacity = "Select Вместимость from Виды_Складов where id_Склада=" + skladNumber;
                        string queryCountInSklad = "Select sum(Еденица_на_складе) from Склады where Номер_Склада=" + skladNumber;
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
                                SqlCommand commandCapacity = new SqlCommand(queryCapacity, connection);
                                SqlCommand commandCountInSklad = new SqlCommand(queryCountInSklad, connection);
                                maxCapacity = Convert.ToInt32(commandCapacity.ExecuteScalar().ToString());
                                if (Convert.IsDBNull(commandCountInSklad.ExecuteScalar()) == true)
                                {
                                    countInSklad = 0;
                                }
                                else
                                {
                                    countInSklad = Convert.ToInt32(commandCountInSklad.ExecuteScalar().ToString());
                                }
                                connection.Close();

                                if (countInSklad + count > maxCapacity)
                                {
                                    MessageBox.Show("Невозможно добавить,склад переполнен");
                                }
                                else
                                {
                                    try
                                    {

                                        connection.Open();
                                        SqlCommand command = new SqlCommand(query, connection);
                                        command.ExecuteNonQuery();
                                        MessageBox.Show("Данные успешны добавлены");
                                        textBox1.Text = "";
                                        textBox3.Text = "";
                                        comboBox1.Text = "";
                                        comboBox2.Text = "";

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Произошла ошибка,повторите попытку!");
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }



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
                var form = new Razdel_2();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
        }

        private void AddValueToSklad_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                string[] listName = { "Пшеница", "Пшеница твердого сорта", "Лён", "Рапс", "Чечевица" };
                foreach (var i in listName)
                    comboBox2.Items.Add(i);

                SqlCommand command = new SqlCommand("Select distinct id_Склада from Виды_Складов", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0].ToString());
                    }
                }
            }
        }

        Regex onlyNumber = new Regex(@"^[0-9]+$");
        Regex regex = new Regex("^[a-zA-Z]+$");
        private void namePostavkaBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Не указано ФИО механизатора!");
            }
            else if (textBox3.Text.Length < 2)
            {
                errorProvider1.SetError(textBox3, "Слишком короткое ФИО!");
            }
            else if (!regex.IsMatch(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Пишите только буквы,без цифр!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void nameBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox2.Text))
            {
                errorProvider1.SetError(comboBox2, "Не указано название культуры!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void countBox_Validating(object sender, CancelEventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Не указано количество !");
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


        private void skladNumberBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                errorProvider1.SetError(comboBox1, "Не указано номер склада !");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
