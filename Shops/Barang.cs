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
    public partial class Barang : Form
    {
        public Barang()
        {
            InitializeComponent();
            LoadVendors();
            GetBarang();

            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;

            textBox1.TextChanged += textBox1_TextChanged;

        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        // Untuk Mengisi ComboBox dengan Data Vendor
        void LoadVendors()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdVendor, namaVendor FROM Vendors", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            // Untuk Setting ComboBox
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "namaVendor";
            comboBox1.ValueMember = "IdVendor";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["IdBarang"].Value.ToString();
                textBox2.Text = row.Cells["NamaBarang"].Value.ToString();
                textBox3.Text = row.Cells["Stock"].Value.ToString();
                textBox4.Text = row.Cells["Harga"].Value.ToString();
                comboBox1.SelectedValue = row.Cells["Idven"].Value.ToString();

                //textBox1.ReadOnly = true;
                button2.Visible = true;
                button3.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd = new SqlCommand("INSERT INTO Barangs (IdBarang, NamaBarang, Stock, Harga, Idven) VALUES (@IdBarang, @NamaBarang, @Stock, @Harga, @Idven)", conn);
            SqlCommand cmd = new SqlCommand("INSERT INTO Barangs (IdBarang, NamaBarang, Stock, Harga, NamaVendor, Idven) VALUES (@IdBarang, @NamaBarang, @Stock, @Harga, @NamaVendor, @Idven)", conn);
            conn.Open();



            cmd.Parameters.AddWithValue("@IdBarang", textBox1.Text);
            cmd.Parameters.AddWithValue("@NamaBarang", textBox2.Text);
            cmd.Parameters.AddWithValue("@Stock", int.Parse(textBox3.Text));
            cmd.Parameters.AddWithValue("@Harga", int.Parse(textBox4.Text));

            // Mengambil IdVendor Dari Combo Box
            string selectedIdVendor = comboBox1.SelectedValue.ToString();
            string selectedNamaVendor = comboBox1.Text; // Nama vendor dari ComboBox

            cmd.Parameters.AddWithValue("@Idven", selectedIdVendor);
            cmd.Parameters.AddWithValue("@NamaVendor", selectedNamaVendor);

            cmd.ExecuteNonQuery();
            conn.Close();

            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;

            MessageBox.Show("Barang Berhasil Ditambah");
            GetBarang();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();


        }

        // Buat Nampilin Barang Di Dalam Panel Barang
        void GetBarang()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdBarang, NamaBarang, Stock, Harga, NamaVendor, Idven FROM Barangs ORDER BY IdBarang DESC", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Ambil IdBarang Yang Di Pilih Di Row
                string idBarang = dataGridView1.SelectedRows[0].Cells["IdBarang"].Value.ToString();


                SqlCommand cmd = new SqlCommand("UPDATE Barangs SET NamaBarang = @NamaBarang, Stock = @Stock, Harga = @Harga, NamaVendor = @NamaVendor, Idven = @Idven WHERE IdBarang = @IdBarang", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("@IdBarang", idBarang); // Di Ambil Dari Row Yang Di Pilih
                cmd.Parameters.AddWithValue("@NamaBarang", textBox2.Text);
                cmd.Parameters.AddWithValue("@Stock", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@Harga", int.Parse(textBox4.Text));

                // Mengambil IdVendor Dari Combo Box
                string selectedIdVendor = comboBox1.SelectedValue.ToString();
                string selectedNamaVendor = comboBox1.Text; // Nama vendor dari ComboBox

                cmd.Parameters.AddWithValue("@Idven", selectedIdVendor);
                cmd.Parameters.AddWithValue("@NamaVendor", selectedNamaVendor);

                cmd.ExecuteNonQuery();
                conn.Close();

                button1.Visible = true;
                button2.Visible = false;
                button3.Visible = false;

                MessageBox.Show("Data Berhasil Di Edit");
                GetBarang();

                //textBox1.Clear();
                //textBox2.Clear();
                //textBox3.Clear();
                //textBox4.Clear();
            }
            else
            {
                MessageBox.Show("Pilih baris yang ingin diedit.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE Barangs WHERE IdBarang=@IdBarang", conn);
            cmd.Parameters.AddWithValue("@IdBarang", textBox1.Text);

            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Data Berhasil Di Hapus");
            }
            else
            {
                MessageBox.Show("Data tidak ditemukan atau sudah dihapus sebelumnya.");
            }

            GetBarang();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private bool CheckIdExists(string id)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(conn.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Id = @Id", tempConn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                bool idSekarang = CheckIdExists(textBox1.Text);

                button1.Visible = !idSekarang;
                button2.Visible = idSekarang;
                button3.Visible = idSekarang;

                if (idSekarang)
                {
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
            } else
            {
                button1.Visible = true;
                button2.Visible = false;
                button3.Visible = false;
            }
        }
    }
}
