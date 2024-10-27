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

namespace Shops
{
    public partial class Penjualan : Form
    {
        public Penjualan(string idPetugas, string namaPetugas)
        {
            InitializeComponent();
            textBox5.Text = idPetugas;
            textBox4.Text = namaPetugas;
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        private void Penjualan_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string InputId = textBox1.Text;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nama FROM Login WHERE Id = @id AND Role = 'User'", conn);
                cmd.Parameters.AddWithValue("@Id", InputId);

                SqlDataReader reader = cmd.ExecuteReader();

                // Cek Ada Data Apa Tidak
                if (reader.Read())
                {
                    textBox2.Text = reader["Id"].ToString();
                    textBox3.Text = reader["Nama"].ToString();
                }
                else
                {
                    MessageBox.Show("User Tidak Di Temukan");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string InputId = textBox9.Text;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT NamaBarang, Stock, Harga FROM Barangs WHERE IdBarang = @Barang", conn);
                cmd.Parameters.AddWithValue("@Barang", InputId);

                SqlDataReader reader = cmd.ExecuteReader();

                // Cek Ada Data Apa Tidak
                if (reader.Read())
                {
                    textBox8.Text = reader["NamaBarang"].ToString();
                    //textBox7.Text = reader["Stock"].ToString();
                    label11.Text = reader["Stock"].ToString() + " /Stock";
                    textBox10.Text = reader["Harga"].ToString();
                }
                else
                {
                    MessageBox.Show("Barang Tidak Di Temukan");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idBarang = textBox9.Text;
            string namaBarang = textBox8.Text;
            int jumlah = int.Parse(textBox7.Text);
            int Harga = int.Parse(textBox10.Text);

            string stockText = label11.Text;
            int stockEndIndex = stockText.IndexOf(" ");
            string stockNumber = stockText.Substring(0, stockEndIndex);
            int stock = int.Parse(stockNumber);

            if (jumlah > stock)
            {
                MessageBox.Show("Stock Tidak Tersedia");
                return;
            }


            int TotalHarga = jumlah * Harga;

            dataGridView1.Rows.Add(idBarang, namaBarang, jumlah, Harga, TotalHarga);

            long totalKeseluruh = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    totalKeseluruh += Convert.ToInt64(row.Cells[4].Value);
                }
            }
            textBox13.Text = totalKeseluruh.ToString();

            //dataGridView1.Rows.Clear();

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool isPegawai = !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox4.Text);

            try
            {
                conn.Open();

                // Insert data ke tabel LaporanPenjualan
                SqlCommand cmd = new SqlCommand("INSERT INTO LaporanPenjualan (IdPenjualan, TGLPenjualan, IdBarang, NamaBarang, Jumlah, Harga, IdUser, NamaUser, IdPetugas, NamaPetugas) VALUES (@IdPenjualan, @TGLPenjualan, @IdBarang, @NamaBarang, @Jumlah, @Harga, @IdUser, @NamaUser, @IdPetugas, @NamaPetugas)", conn);

                cmd.Parameters.AddWithValue("@IdPenjualan", textBox6.Text);
                cmd.Parameters.AddWithValue("@TGLPenjualan", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@IdBarang", textBox9.Text);
                cmd.Parameters.AddWithValue("@NamaBarang", textBox8.Text);
                cmd.Parameters.AddWithValue("@Jumlah", textBox7.Text);
                cmd.Parameters.AddWithValue("@Harga", textBox10.Text);
                cmd.Parameters.AddWithValue("@IdUser", textBox2.Text);
                cmd.Parameters.AddWithValue("@NamaUser", textBox3.Text);

                if (isPegawai)
                {
                    cmd.Parameters.AddWithValue("@IdPetugas", textBox5.Text);
                    cmd.Parameters.AddWithValue("@NamaPetugas", textBox4.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdPetugas", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NamaPetugas", DBNull.Value);
                }

                cmd.ExecuteNonQuery();
                
                int jumlah = int.Parse(textBox7.Text); 
                SqlCommand updateStockCmd = new SqlCommand("UPDATE Barangs SET Stock = Stock - @Jumlah WHERE IdBarang = @IdBarang", conn);
                updateStockCmd.Parameters.AddWithValue("@Jumlah", jumlah);
                updateStockCmd.Parameters.AddWithValue("@IdBarang", textBox9.Text);
                updateStockCmd.ExecuteNonQuery();

                SqlCommand deleteCmd = new SqlCommand("DELETE FROM Penjualan WHERE IdPenjualan = @IdPenjualan", conn);
                deleteCmd.Parameters.AddWithValue("@IdPenjualan", textBox6.Text);
                deleteCmd.ExecuteNonQuery();

                MessageBox.Show("Data Berhasil Ditambahkan ke Laporan Penjualan dan Stok Berkurang");

                textBox6.Clear();
                textBox9.Clear();
                textBox8.Clear();
                textBox7.Clear();
                textBox10.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Clear();
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string IdPenjualan = textBox6.Text;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT IdBarang, NamaBarang, Jumlah, Harga, IdUser, NamaUser FROM Penjualan WHERE IdPenjualan = @IdPenjualan", conn);
                cmd.Parameters.AddWithValue("@IdPenjualan", IdPenjualan);
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    textBox9.Text = reader["IdBarang"].ToString();
                    textBox8.Text = reader["NamaBarang"].ToString();
                    textBox7.Text = reader["Jumlah"].ToString();
                    textBox10.Text = reader["Harga"].ToString();
                    textBox2.Text = reader["IdUser"].ToString();
                    textBox3.Text = reader["NamaUser"].ToString();

                    string idBarang = reader["IdBarang"].ToString();
                    reader.Close();

                    SqlCommand cmdStock = new SqlCommand("SELECT Stock FROM Barangs WHERE IdBarang = @IdBarang", conn);
                    cmdStock.Parameters.AddWithValue("@IdBarang", idBarang);
                    SqlDataReader stockReader = cmdStock.ExecuteReader();

                    if (stockReader.Read())
                    {
                        label11.Text = stockReader["Stock"].ToString() + " /Stock";
                    }
                    else
                    {
                        label11.Text = "Stok Tidak Tersedia";
                    }

                    stockReader.Close();

                } else
                {
                    MessageBox.Show("Data Tidak Di Temukan");
                }
                reader.Close();
            } catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            } finally
            {
                conn.Close();
            }

        }
    }
}
