using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System.Transactions;
using System.DirectoryServices.ActiveDirectory;

namespace Shops
{
    public partial class PelanganPesan : UserControl
    {
        private string userId;
        private string userName;
        public PelanganPesan(string userId, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.userName = userName;

            Console.WriteLine($"Pelanggan: {this.userId}, userName: {this.userName}");

            LoadVendors();
            GetPesan();
        }

        public PelanganPesan()
        {
            InitializeComponent();
            LoadVendors();
            GetPesan();
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

        private void button1_Click(object sender, EventArgs e)
        {

            // Validasi input fields
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            try
            {
                conn.Open();
                GetPesan(); // Cek stock
                SqlCommand stockCmd = new SqlCommand("SELECT Stock FROM Barangs WHERE IdBarang = @IdBarang", conn);
                stockCmd.Parameters.AddWithValue("@IdBarang", textBox1.Text);

                object result = stockCmd.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show("Barang tidak ditemukan!");
                    return;
                }

                int stockSekarang = Convert.ToInt32(result);
                int order = int.Parse(textBox3.Text);
                int stockBaru = stockSekarang - order;

                // Jika Stock 0
                if (stockSekarang <= 0)
                {
                    MessageBox.Show("Stock Habis");
                    return;
                }

                // Jika User Melebihi Stock
                if (stockBaru < 0)
                {
                    MessageBox.Show("Stock Tidak Cukup");
                    return;
                }


                // Menghitung Diskon
                int harga = int.Parse(textBox4.Text);
                double diskon = 0;

                if (harga > 100000)
                {
                    diskon = 0.10; // 10% diskon
                }

                double hargaSetelahDiskon = harga - (harga * diskon);

                textBox4.Text = hargaSetelahDiskon.ToString(); // Harga setelah diskon
                textBox5.Text = (diskon * 100).ToString() + "%"; // Persentase diskon

                // Tampilkan nilai userId dan userName
                Console.WriteLine($"userId: {userId}, userName: {userName}");

                // Insert pesanan
                string insertQuery = @"INSERT INTO PelanganPesan (IdBarang, NamaBarang, Stock, Harga, NamaVendor, Idven, Id, NamaUser) VALUES (@IdBarang, @NamaBarang, @Stock, @Harga, @NamaVendor, @Idven, @Id, @NamaUser)";

                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);

                insertCmd.Parameters.Clear();
                insertCmd.Parameters.AddWithValue("@IdBarang", textBox1.Text);
                insertCmd.Parameters.AddWithValue("@NamaBarang", textBox2.Text);
                insertCmd.Parameters.AddWithValue("@Stock", order);
                insertCmd.Parameters.AddWithValue("@Harga", int.Parse(textBox4.Text));
                insertCmd.Parameters.AddWithValue("@NamaVendor", comboBox1.Text);
                insertCmd.Parameters.AddWithValue("@Idven", comboBox1.SelectedValue.ToString());
                insertCmd.Parameters.AddWithValue("@Id", userId);
                insertCmd.Parameters.AddWithValue("@NamaUser", userName);

                foreach (SqlParameter param in insertCmd.Parameters)
                {
                    Console.WriteLine($"Parameter {param.ParameterName}: {param.Value}");
                }

                int rowsAffected = insertCmd.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");

                // Update stock
                //SqlCommand updateCmd = new SqlCommand(
                //    "UPDATE Barangs SET Stock = @NewStock WHERE IdBarang = @IdBarang",
                //    conn,
                //    transaction);
                //updateCmd.Parameters.AddWithValue("@NewStock", stockBaru);
                //updateCmd.Parameters.AddWithValue("@IdBarang", textBox1.Text);
                //updateCmd.ExecuteNonQuery();
                MessageBox.Show("Pesanan berhasil!");

                // Clear form
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }
        void GetPesan()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdBarang, NamaBarang, Stock, Harga, NamaVendor, Idven FROM Barangs", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private int StockAda = 0;

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

                StockAda = Convert.ToInt32(row.Cells["Stock"].Value.ToString());

                // Menghitung total harga
                int harga = int.Parse(textBox4.Text);

                try
                {
                    int Stock = Convert.ToInt32(textBox3.Text);
                    int totalHarga = harga * Stock;
                    textBox6.Text = totalHarga.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                // Menghitung Diskon
                //int harga = int.Parse(textBox4.Text);
                //double diskon = 0;

                //if (harga > 100000)
                //{
                //    diskon = 0.10; // Diskon 10% untuk harga di atas 100000
                //}

                //double hargaSetelahDiskon = harga - (harga * diskon);
                //textBox4.Text = hargaSetelahDiskon.ToString(); // Update harga setelah diskon
                //textBox5.Text = (diskon * 100).ToString() + "%";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void UpdateTotalHarga()
        {
            try
            {
                int harga = int.Parse(textBox4.Text);
                int jumlah = int.Parse(textBox3.Text);
                int totalHarga = harga * jumlah;

                // Menghitung Diskon
                double diskon = 0;

                if (totalHarga > 100000)
                {
                    diskon = 0.10; //10 %
                }
                double HargaDis = totalHarga - (totalHarga * diskon);

                if (diskon > 0)
                {
                    textBox5.Text = (diskon * 100).ToString() + "%";
                    textBox6.Text = HargaDis.ToString();
                } else
                {
                    textBox5.Text = "0%";
                    textBox6.Text = totalHarga.ToString();
                }
            } catch (FormatException)
            {
                MessageBox.Show("Masukan Angka");
                textBox6.Text = "0";
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }

            //if (e.KeyChar == (char)Keys.Enter)
            //{
            //    UpdateTotalHarga();
            //    e.Handled = true;
            //}

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (int.TryParse(textBox3.Text, out int jumlah) && jumlah > StockAda)
                {
                    MessageBox.Show($"Stock Tidak Cukup, Stock Hanya Ada {StockAda}");
                    textBox3.Clear();
                    textBox6.Text = "";
                } else
                {
                    UpdateTotalHarga();
                }
                e.Handled = true;
            }

        }
    }
}
