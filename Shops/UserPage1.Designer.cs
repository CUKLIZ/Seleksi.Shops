namespace Shops
{
    partial class UserPage1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(84, 79);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(273, 388);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button3
            // 
            button3.BackColor = Color.Red;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(61, 273);
            button3.Name = "button3";
            button3.Size = new Size(143, 23);
            button3.TabIndex = 16;
            button3.Text = "Hapus";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(143, 244);
            button2.Name = "button2";
            button2.Size = new Size(143, 23);
            button2.TabIndex = 15;
            button2.Text = "Perbarui";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(6, 244);
            button1.Name = "button1";
            button1.Size = new Size(109, 23);
            button1.TabIndex = 14;
            button1.Text = "Simpan";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(134, 200);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(143, 23);
            textBox4.TabIndex = 12;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(134, 145);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(143, 23);
            textBox3.TabIndex = 11;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(134, 90);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(143, 23);
            textBox2.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(24, 205);
            label4.Name = "label4";
            label4.Size = new Size(69, 18);
            label4.TabIndex = 8;
            label4.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(24, 150);
            label2.Name = "label2";
            label2.Size = new Size(75, 18);
            label2.TabIndex = 7;
            label2.Text = "Username";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(24, 90);
            label1.Name = "label1";
            label1.Size = new Size(47, 18);
            label1.TabIndex = 6;
            label1.Text = "Nama";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(134, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(143, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(24, 35);
            label3.Name = "label3";
            label3.Size = new Size(22, 18);
            label3.TabIndex = 4;
            label3.Text = "Id";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(407, 75);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(306, 392);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "User";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // UserPage1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Name = "UserPage1";
            Size = new Size(772, 533);
            Load += UserPage1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button3;
        private Button button2;
        private Button button1;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label label4;
        private Label label2;
        private Label label1;
        private TextBox textBox1;
        private Label label3;
        private GroupBox groupBox1;
    }
}
