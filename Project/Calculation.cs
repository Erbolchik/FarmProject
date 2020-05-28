using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Calculation : Form
    {
        public Calculation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double totalGegtrazh = Convert.ToDouble(textBox1.Text);
                double totalTonna = Convert.ToDouble(textBox2.Text);

                double result = (totalGegtrazh / totalTonna);
                MessageBox.Show("result = " + result.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                var form = new Menu();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }
        }
    }
}
