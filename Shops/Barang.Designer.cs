namespace Shops
{
    partial class Barang
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
            groupBox1 = new GroupBox();
            comboBox1 = new ComboBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            button4 = new Button();
            button3 = new Button();
            textBox2 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(454, 70);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(306, 331);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            groupBox1.Text = "Barang";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Inforce", "Ecuda" });
            comboBox1.Location = new Point(134, 172);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(143, 23);
            comboBox1.TabIndex = 23;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(134, 140);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(143, 23);
            textBox4.TabIndex = 22;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(134, 104);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(143, 23);
            textBox3.TabIndex = 21;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(24, 140);
            label5.Name = "label5";
            label5.Size = new Size(47, 18);
            label5.TabIndex = 20;
            label5.Text = "Harga";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(24, 177);
            label4.Name = "label4";
            label4.Size = new Size(54, 18);
            label4.TabIndex = 19;
            label4.Text = "Vendor";
            label4.Click += label4_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(24, 104);
            label2.Name = "label2";
            label2.Size = new Size(43, 18);
            label2.TabIndex = 18;
            label2.Text = "Stock";
            // 
            // button4
            // 
            button4.Location = new Point(7, 289);
            button4.Name = "button4";
            button4.Size = new Size(121, 23);
            button4.TabIndex = 17;
            button4.Text = "RESET";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Red;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(134, 289);
            button3.Name = "button3";
            button3.Size = new Size(143, 23);
            button3.TabIndex = 16;
            button3.Text = "Hapus";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(134, 69);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(143, 23);
            textBox2.TabIndex = 10;
            // 
            // button2
            // 
            button2.Location = new Point(134, 260);
            button2.Name = "button2";
            button2.Size = new Size(143, 23);
            button2.TabIndex = 15;
            button2.Text = "Perbarui";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(134, 231);
            button1.Name = "button1";
            button1.Size = new Size(143, 23);
            button1.TabIndex = 14;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(24, 69);
            label1.Name = "label1";
            label1.Size = new Size(98, 18);
            label1.TabIndex = 6;
            label1.Text = "Nama Barang";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(24, 35);
            label3.Name = "label3";
            label3.Size = new Size(75, 18);
            label3.TabIndex = 4;
            label3.Text = "ID Barang";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(134, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(143, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(436, 388);
            dataGridView1.TabIndex = 23;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Barang
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 533);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Barang";
            Text = "Barang";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button4;
        private Button button3;
        private TextBox textBox2;
        private Button button2;
        private Button button1;
        private Label label1;
        private Label label3;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private Label label2;
        private Label label5;
        private Label label4;
        private TextBox textBox4;
        private TextBox textBox3;
        private ComboBox comboBox1;
    }
}