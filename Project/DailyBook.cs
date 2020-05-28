using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project
{
    public partial class DailyBook : Form
    {
        public DailyBook()
        {
            InitializeComponent();
            richTextBox1.Enabled = false;
            button4.Enabled = false;
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
                var form = new CommentForWorker();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(String.Format("Select Фамилия,Имя,Агрегат,Серийный_номер_агрегата,Дата_События from Ежедневник where Дата_События='{0}'", dateTimePicker1.Value.Date), connection);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    if (dataGridView1.Rows.Count == 1)
                    {
                        MessageBox.Show("Нету данных в данной дате");
                    }
                    else
                    {
                        button4.Enabled = true;
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
            try
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                int columnindex = dataGridView1.CurrentCell.ColumnIndex;

                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-O3QKGQU\SQLEXPRESS;Initial Catalog=Farm;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    connection.Open();
                    SqlCommand commandSelect = new SqlCommand(String.Format("Select Комментарии from Ежедневник where Фамилия ='{0}'", dataGridView1.Rows[rowindex].Cells[columnindex].Value.ToString()), connection);
                    richTextBox1.Text = commandSelect.ExecuteScalar().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
