using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class CommentForWorker : Form
    {
        public CommentForWorker()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox2.Enabled = false;
            comboBox2.Enabled = false;
            button4.Enabled = false;
            dateTimePicker1.Enabled = false;
            richTextBox1.Enabled = false;
            richTextBox1.Validating += commentBox_Validating;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите сотрудника!");
            }
            else
            {
                button4.Enabled = true;
                dateTimePicker1.Enabled = true;
                richTextBox1.Enabled = true;
                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        connection.Open();
                        string lastname = comboBox1.SelectedItem.ToString();
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
                            textBox3.Text = cells[11].ToString();
                            comboBox2.Text = cells[10].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void CommentForWorker_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select Фамилия from Сотрудники", connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0].ToString());
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || 
                comboBox1.Text == "" || comboBox2.Text == "" || richTextBox1.Text == "")
            {
                MessageBox.Show("Сначала заполните все поля!!");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {

                    try
                    {
                        connection.Open();
                        SqlCommand sqlCommandId = new SqlCommand("Select id_Сотрудника from Сотрудники where Фамилия = '" + comboBox1.SelectedItem.ToString() + "'", connection);

                        int idWorker = Convert.ToInt32((sqlCommandId.ExecuteScalar().ToString()));
                        string name = textBox1.Text;
                        string lastname = textBox2.Text;
                        string ogregat = comboBox2.Text.ToString();
                        int numberOgregat = Convert.ToInt32(textBox3.Text);
                        DateTime dateEvent = dateTimePicker1.Value.Date;
                        string comment = richTextBox1.Text;

                        string quearyAdd = String.Format("Insert into Ежедневник(id_Работника,Фамилия,Имя,Агрегат,Серийный_номер_агрегата,Дата_События,Комментарии)" +
                            "Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", idWorker, name, lastname, ogregat, numberOgregat, dateEvent, comment);

                        SqlCommand sqlCommandInsert = new SqlCommand(quearyAdd, connection);
                        sqlCommandInsert.ExecuteNonQuery();
                        MessageBox.Show("Данные успешны записаны");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        richTextBox1.Text = "";
                        comboBox2.Text = "";
                        comboBox1.Text = "";
                        button4.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        richTextBox1.Enabled = false;

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
                var form = new DailyBook();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
        }

        private void commentBox_Validating(object sender, CancelEventArgs e)
        {

            if (String.IsNullOrEmpty(richTextBox1.Text))
            {
                errorProvider1.SetError(richTextBox1, "Не указан комментарий!");
            }
            else if (richTextBox1.Text.Length < 1)
            {
                errorProvider1.SetError(richTextBox1, "Слишком короткий комментарий!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
