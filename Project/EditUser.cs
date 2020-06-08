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
    public partial class EditUser : Form
    {
        public EditUser()
        {
            InitializeComponent();
            string[] jobWhere = { "Машдвор", "Ц/Ток", "М/Эливатор", "Автопарк", "Мтс", "Бригада", "Контора" };
            string[] ogregat = { "К700", "МТЗ", "ДТ-75", "Камаз", "ЗИЛ", "Нива Москвич", "Уаз", "ДонМар", "MagDon", "Challenge", "John Deere" };
            foreach (var i in jobWhere)
                comboBox1.Items.Add(i);
            foreach (var i in ogregat)
                comboBox2.Items.Add(i);
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select Фамилия from Сотрудники", connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader[0].ToString());
                    }
                }
            }
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            dateTimePicker1.Enabled = false;

            textBox1.Validating += nameBox_Validating;
            textBox2.Validating += lastnameBox_Validating;
            textBox4.Validating += middlenameBox_Validating;
            textBox8.Validating += phoneeBox_Validating;
            textBox5.Validating += addressBox_Validating;
            textBox7.Validating += positionBox_Validating;
            textBox6.Validating += salaryBox_Validating;
            textBox3.Validating += numberogregatBox_Validating;
            comboBox1.Validating += jobBox_Validating;
            comboBox2.Validating += ogregatBox_Validating;

        }




        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите сотрудника!");
            }
            else
            {
                textBox1.Enabled = true;
                textBox3.Enabled = true;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                dateTimePicker1.Enabled = true;
                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        connection.Open();
                        string lastname = comboBox3.SelectedItem.ToString();
                        SqlDataAdapter adapter = new SqlDataAdapter("Select * from Сотрудники where Фамилия ='" + lastname + "'", connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DataRow dataRow = dt.NewRow();
                        foreach (DataRow i in dt.Rows)
                        {
                            var cells = i.ItemArray;
                            textBox1.Text = cells[1].ToString();
                            textBox2.Text = cells[2].ToString();
                            textBox4.Text = cells[3].ToString();
                            textBox8.Text = cells[4].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(cells[5].ToString());
                            textBox5.Text = cells[6].ToString();
                            textBox7.Text = cells[7].ToString();
                            textBox6.Text = cells[8].ToString();
                            comboBox1.Text = cells[9].ToString();
                            comboBox2.Text = cells[10].ToString();
                            textBox3.Text = cells[11].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "Вы точно хотите изменить?",
              "Сообщение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    try
                    {
                        connection.Open();
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
                        DateTime today = DateTime.Now;


                        string queryUpdate = String.Format("update Сотрудники " +
                            "set Фамилия='{0}'" +
                            ", Имя='{1}'" +
                            ", Отчество='{2}'" +
                            ", Номер_Телефона='{3}'" +
                            ", Дата_Рождения='{4}'" +
                            ", Адрес_Проживания='{5}'" +
                            ", Должность='{6}'" +
                            ", Оклад='{7}'" +
                            ", Место_Работы='{8}'" +
                            ", Огрегаты='{9}'" +
                            ", Серийный_Номер_огрегата='{10}'" +
                            "where Фамилия = '{11}' ", name, lastname, middlename, phone, birthday, address, position, salary, jobWhere, ogregat, numberOgregat, comboBox3.SelectedItem.ToString());

                        if (birthday > today)
                        {
                            MessageBox.Show("Ошибка даты,нельзя добавить будущую дату");
                        }
                        else if (today.Year - birthday.Year > 80)
                        {
                            MessageBox.Show("Ошибка даты,нельзя слишком старую дату");
                        }
                        else
                        {
                            SqlCommand sqlCommandUpdate = new SqlCommand(queryUpdate, connection);
                            sqlCommandUpdate.ExecuteNonQuery();
                            MessageBox.Show("Данные успешно обновлены");
                            comboBox3.Items.Clear();
                            SqlCommand sqlCommand = new SqlCommand("Select Фамилия from Сотрудники", connection);
                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    comboBox3.Items.Add(reader[0].ToString());
                                }
                            }

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
                            comboBox3.Text = "";

                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            textBox3.Enabled = false;
                            textBox4.Enabled = false;
                            textBox5.Enabled = false;
                            textBox6.Enabled = false;
                            textBox7.Enabled = false;
                            textBox8.Enabled = false;
                            comboBox1.Enabled = false;
                            comboBox2.Enabled = false;
                            button2.Enabled = false;
                            button4.Enabled = false;
                            dateTimePicker1.Enabled = false;
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
              "Вы точно хотите удалить?",
              "Сообщение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand sqlCommandId = new SqlCommand("Select id_Сотрудника from Сотрудники where Фамилия = '" + comboBox3.SelectedItem.ToString() + "'", connection);
                        int id = Convert.ToInt32((sqlCommandId.ExecuteScalar().ToString()));

                        SqlCommand sqlCommandDelete = new SqlCommand("Delete Сотрудники where id_Сотрудника =" + id, connection);
                        sqlCommandDelete.ExecuteNonQuery();

                        MessageBox.Show("Сотрудник успешно удален");
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
                        comboBox3.Items.Clear();
                        SqlCommand sqlCommand = new SqlCommand("Select Фамилия from Сотрудники", connection);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox3.Items.Add(reader[0].ToString());
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

        private void button3_Click(object sender, EventArgs e)
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
                var form = new Razdel_3();
                form.Closed += (s, args) => this.Close();
                form.Show();
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
            else if (!onlyNumber.IsMatch(textBox6.Text))
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
