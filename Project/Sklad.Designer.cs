namespace Project
{
    partial class Sklad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(756, 491);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold);
            this.buttonSelect.Location = new System.Drawing.Point(37, 581);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(177, 78);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Вывести информацию из таблицы";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(815, 56);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(335, 491);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(43, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Склад";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(810, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Комментарии по запчасти";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(831, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 78);
            this.button1.TabIndex = 5;
            this.button1.Text = "Вывести комментарии по запчасти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(1224, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 78);
            this.button2.TabIndex = 6;
            this.button2.Text = "Вычитать ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(1203, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Количество для вычитания";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1224, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(191, 22);
            this.textBox1.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(262, 581);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 78);
            this.button3.TabIndex = 9;
            this.button3.Text = "Добавить новые данные";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(1224, 246);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(177, 78);
            this.button4.TabIndex = 10;
            this.button4.Text = "Вернутся в главное меню";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1224, 425);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(191, 24);
            this.comboBox1.TabIndex = 11;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(1238, 486);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(177, 78);
            this.button5.TabIndex = 12;
            this.button5.Text = "Сортировать";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(215, 15);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(463, 22);
            this.textBox2.TabIndex = 13;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Sklad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 690);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Sklad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Склад З/П";
            this.Load += new System.EventHandler(this.Sklad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox2;
    }
}