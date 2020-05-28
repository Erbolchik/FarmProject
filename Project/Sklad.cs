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
    public partial class Sklad : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Sklad()
        {
            InitializeComponent();
            button1.Enabled = false;
            button2.Enabled = false;
            richTextBox1.Enabled = false;
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select Серийный_номер,Название_запчасти,Дата_прибытия,Количество,Категория from Склад_Для_Запчастей ",connection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            button1.Enabled = true;
            button2.Enabled = true;
            richTextBox1.Enabled = true;
            textBox1.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                int columnindex = dataGridView1.CurrentCell.ColumnIndex;

                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Select Коментарии from Склад_Для_Запчастей where Серийный_номер =" + dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString(), connection);
                    richTextBox1.Text = sqlCommand.ExecuteScalar().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    connection.Open();

                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Сначала введите количество для вычтения");
                    }
                    int amountToSubtract = Convert.ToInt32(textBox1.Text);

                    int rowindex = dataGridView1.CurrentCell.RowIndex;
                    int columnindex = dataGridView1.CurrentCell.ColumnIndex;

                    SqlCommand sqlCommandCount = new SqlCommand("Select Количество from Склад_Для_Запчастей where Серийный_номер=" + dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString(), connection);
                    int amounNow = Convert.ToInt32(sqlCommandCount.ExecuteScalar().ToString());
                    int countToUpdate = amounNow - amountToSubtract;

                    if (countToUpdate < 0)
                    {
                        MessageBox.Show("Невозможно вычтать,не хватает количество!!!");
                    }
                    else
                    {
                        SqlCommand sqlCommand = new SqlCommand("update Склад_Для_Запчастей set Количество = +" + countToUpdate + " where Серийный_номер =" + dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString(), connection);
                        SqlCommand sqlDelete = new SqlCommand("delete from Склад_Для_Запчастей where Количество = 0");
                        sqlCommand.ExecuteNonQuery();
                        sqlDelete.ExecuteNonQuery();
                        MessageBox.Show("Данные успешно вычтины");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                var form = new AddValue();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
