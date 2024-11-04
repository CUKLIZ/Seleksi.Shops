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

            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;

            textBox1.TextChanged += textBox1_TextChanged;
            this.Load += new System.EventHandler(this.Vendor_Load);
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        private void Vendor_Load(object sender, EventArgs e)
        {
            GetVendor();
        }


        void GetVendor()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Vendors ORDER BY IdVendor DESC", conn);
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

                button2.Visible = true;
                button3.Visible = true;
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

            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
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

            textBox1.Clear();
            textBox2.Clear();

            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;

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

        private bool CheckIdExists(string id)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(conn.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Vendors WHERE IdVendor = @IdVendor", tempConn))
                    {
                        cmd.Parameters.AddWithValue("@IdVendor", id);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0; // Mengembalikan true jika IdVendor ada, false jika tidak ada
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                bool idSekarang = CheckIdExists(textBox1.Text); // Memeriksa keberadaan IdVendor

                // Jika IdVendor belum ada di database, tampilkan tombol Simpan (button1), sembunyikan tombol Perbarui dan Hapus
                button1.Visible = !idSekarang;
                button2.Visible = idSekarang;
                button3.Visible = idSekarang;

                // Jika IdVendor tidak ada, kosongkan nama vendor di textBox2
                if (!idSekarang)
                {
                    textBox2.Text = "";
                }
            }
            else
            {
                // Jika textBox1 kosong, tampilkan hanya tombol Simpan dan sembunyikan tombol lainnya
                button1.Visible = true;
                button2.Visible = false;
                button3.Visible = false;
            }
        }
    }
}
