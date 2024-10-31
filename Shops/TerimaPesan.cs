using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shops
{
    public partial class TerimaPesan : Form
    {
        public TerimaPesan()
        {
            InitializeComponent();
            getPesan();
            TungguProses();
            getLapJul();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        void getPesan()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdBarang, NamaBarang, Stock, Harga, NamaVendor, Idven FROM PelanganPesan", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void TungguProses()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdPenjualan ,IdBarang, NamaBarang, Jumlah, Harga, NamaVendor, Idven FROM Penjualan", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        void getLapJul()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdPenjualan FROM LaporanPenjualan ORDER BY IdPenjualan DESC", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Ambil IdBarang dari baris yang dipilih
                string idBarang = dataGridView1.Rows[e.RowIndex].Cells["IdBarang"].Value.ToString();

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT IdBarang, Id, NamaUser, Harga, Stock FROM PelanganPesan WHERE IdBarang = @IdBarang", conn);
                    cmd.Parameters.AddWithValue("@IdBarang", idBarang);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        idBarang = reader["IdBarang"].ToString();
                        string idUser = reader["Id"].ToString();
                        string namaUser = reader["NamaUser"].ToString();
                        decimal harga = Convert.ToDecimal(reader["Harga"]);
                        int jumlah = Convert.ToInt32(reader["Stock"]);

                        decimal totalHarga = harga * jumlah;

                        textBox2.Text = idUser;
                        textBox3.Text = namaUser;
                        textBox5.Text = totalHarga.ToString("N2"); // Format ke 2 desimal                        
                    }
                    reader.Close();
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Semua data harus diisi dan baris harus dipilih.");
                return;
            }

            string idPenjualan = textBox1.Text;
            DateTime tglPenjualan = dateTimePicker1.Value;
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            
            if (selectedRow.Cells["NamaBarang"] == null ||
                selectedRow.Cells["Stock"] == null ||
                selectedRow.Cells["Harga"] == null ||
                selectedRow.Cells["NamaVendor"] == null) 
            {
                MessageBox.Show("Stock, Harga, atau Nama Vendor tidak valid.");
                return;
            }

            string namaBarang = selectedRow.Cells["NamaBarang"].Value?.ToString();
            int jumlah = Convert.ToInt32(selectedRow.Cells["Stock"].Value);
            decimal harga = Convert.ToDecimal(selectedRow.Cells["Harga"].Value);
            string idBarang = selectedRow.Cells["IdBarang"].Value?.ToString();
            string idUser = textBox2.Text;
            string namaUser = textBox3.Text;
            string namaVendor = selectedRow.Cells["NamaVendor"].Value?.ToString(); 

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Penjualan (IdPenjualan, TGLPenjualan, NamaBarang, Jumlah, Harga, IdUser, NamaUser, IdBarang, IdVen, NamaVendor) VALUES (@IdPenjualan, @TGLPenjualan, @NamaBarang, @Jumlah, @Harga, @IdUser, @NamaUser, @IdBarang, @IdVen, @NamaVendor)", conn);

                cmd.Parameters.AddWithValue("@IdPenjualan", idPenjualan);
                cmd.Parameters.AddWithValue("@TGLPenjualan", tglPenjualan);
                cmd.Parameters.AddWithValue("@NamaBarang", namaBarang);
                cmd.Parameters.AddWithValue("@Jumlah", jumlah);
                cmd.Parameters.AddWithValue("@Harga", harga);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
                cmd.Parameters.AddWithValue("@NamaUser", namaUser);
                cmd.Parameters.AddWithValue("@IdBarang", idBarang);
                cmd.Parameters.AddWithValue("@IdVen", selectedRow.Cells["IdVen"].Value?.ToString());
                cmd.Parameters.AddWithValue("@NamaVendor", namaVendor); 

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    SqlCommand Delete = new SqlCommand("DELETE FROM PelanganPesan WHERE IdBarang = @IdBarang", conn);
                    Delete.Parameters.AddWithValue("@IdBarang", idBarang);
                    Delete.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil disimpan.");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox5.Clear();

                    getPesan();
                    TungguProses();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan data.");
                }
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
