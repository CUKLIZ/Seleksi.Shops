namespace Shops
{
    partial class LapReturnPenjualan
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
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            Print = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(26, 115);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(721, 378);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(26, 17);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(721, 92);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Laporan Penjualan";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(8, 44);
            label1.Name = "label1";
            label1.Size = new Size(138, 23);
            label1.TabIndex = 3;
            label1.Text = "Cari Berdasarkan";
            // 
            // button2
            // 
            button2.Location = new Point(640, 44);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Reset";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(541, 43);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Cari";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(152, 44);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(179, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(337, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(186, 23);
            textBox1.TabIndex = 1;
            // 
            // Print
            // 
            Print.BackColor = Color.FromArgb(41, 128, 185);
            Print.FlatStyle = FlatStyle.Flat;
            Print.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Print.Location = new Point(599, 499);
            Print.Name = "Print";
            Print.Size = new Size(148, 35);
            Print.TabIndex = 9;
            Print.Text = "Print";
            Print.UseVisualStyleBackColor = false;
            Print.Click += Print_Click;
            // 
            // LapReturnPenjualan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 546);
            Controls.Add(Print);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LapReturnPenjualan";
            Text = "LapReturnPenjualan";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Button button2;
        private Button button1;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label label1;
        private Button Print;
    }
}