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
    public partial class Razdel_2 : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public Razdel_2()
        {
            InitializeComponent();
            label2.Enabled = false;
            textBox1.Enabled = false;
            button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Склады ", connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Razdel_2_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select distinct Номер_Склада from Склады", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0].ToString());
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
                var form = new AddValueToSklad();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Сначала выберите склад для фильтрации!");
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                    {
                        connection.Open();

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Склады where Номер_Склада=" + comboBox1.SelectedItem.ToString(), connection);
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

                        SqlCommand commandName = new SqlCommand("Select Название_склада from  Виды_Складов where id_Склада=" + comboBox1.SelectedItem.ToString(), connection);
                        label3.Text = commandName.ExecuteScalar().ToString();
                        SqlCommand commandCapacity = new SqlCommand("Select Вместимость from  Виды_Складов where id_Склада=" + comboBox1.SelectedItem.ToString(), connection);
                        SqlCommand commandCount = new SqlCommand("Select sum(Еденица_на_складе) from Склады where Номер_Склада=" + comboBox1.SelectedItem.ToString(), connection);


                        label4.Text = commandCount.ExecuteScalar().ToString() + "/" + commandCapacity.ExecuteScalar().ToString();
                        double procent = Math.Round((Convert.ToDouble(commandCount.ExecuteScalar().ToString()) / Convert.ToDouble(commandCapacity.ExecuteScalar().ToString())) * 100, 1);
                        label6.Text = procent + "% Занято";


                        label2.Enabled = true;
                        textBox1.Enabled = true;
                        button1.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
                var form = new Menu();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;
            int amountToSubtract = Convert.ToInt32(textBox1.Text);

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {

                    connection.Open();
                    SqlCommand sqlCommandCount = new SqlCommand("Select Еденица_на_складе from Склады where id_культуры=" + dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString(), connection);
                    int amounNow = Convert.ToInt32(sqlCommandCount.ExecuteScalar().ToString());
                    int countToUpdate = amounNow - amountToSubtract;

                    if (countToUpdate < 0)
                    {
                        MessageBox.Show("Невозможно вычтать,не хватает количество!!!");
                    }
                    else
                    {
                        SqlCommand sqlCommand = new SqlCommand("update Склады set Еденица_на_складе = +" + countToUpdate + " where id_культуры =" + dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString(), connection);
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно вычтины");
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
