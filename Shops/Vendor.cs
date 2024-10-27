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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Shops
{
    public partial class Vendor : Form
    {
        public Vendor()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Vendor_Load);
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        private void Vendor_Load(object sender, EventArgs e)
        {
            GetVendor();
        }


        void GetVendor()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Vendors", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["IdVendor"].Value.ToString();
                textBox2.Text = row.Cells["namaVendor"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Vendors VALUES (@IdVendor, @namaVendor) ", conn);
            cmd.Parameters.AddWithValue("@IdVendor", textBox1.Text);
            cmd.Parameters.AddWithValue("@namaVendor", textBox2.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data Berhasil Di Tambah");
            GetVendor();

            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Vendors SET namaVendor=@namaVendor WHERE IdVendor=@IdVendor", conn);
            cmd.Parameters.AddWithValue("@IdVendor", textBox1.Text);
            cmd.Parameters.AddWithValue("@namaVendor", textBox2.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data Berhasil Di Edit");

            GetVendor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE Vendors WHERE IdVendor=@IdVendor", conn);
            cmd.Parameters.AddWithValue("@IdVendor", textBox1.Text);
            cmd.ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("Data Berhasil Di Hapus");
            GetVendor();
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
