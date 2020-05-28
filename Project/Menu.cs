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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
                this.TopMost = true;
                this.Hide();
                var form = new Sklad();
                form.Closed += (s, args) => this.Close();
                form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
                this.Hide();
                var form = new Razdel_2();
                form.Closed += (s, args) => this.Close();
                form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                this.Hide();
                var form = new Razdel_3();
                form.Closed += (s, args) => this.Close();
                form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                this.Hide();
                var form = new Calculation();
                form.Closed += (s, args) => this.Close();
                form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
                this.Hide();
                var form = new AddUser();
                form.Closed += (s, args) => this.Close();
                form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
                this.Hide();
                var form = new AddValueToSklad();
                form.Closed += (s, args) => this.Close();
                form.Show();
        }
    }
}
