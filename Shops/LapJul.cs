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
using System.Buffers;

namespace Shops
{
    public partial class LapJul : Form
    {

        private string userId;
        private string role;

        public LapJul(string userId, string role)
        {
            InitializeComponent();
            this.userId = userId;
            this.role = role;
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        void GetLapJul()    
        {
            SqlCommand cmd;

            if (role == "User")
            {
                cmd = new SqlCommand("SELECT IdPenjualan, TGLPenjualan, IdBarang, NamaBarang, Jumlah, Harga, NamaUser FROM LaporanPenjualan WHERE IdUser = @UserId", conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
            }
            else
            {
                cmd = new SqlCommand("SELECT IdPenjualan, TGLPenjualan, IdBarang, NamaBarang, Jumlah, Harga, NamaUser FROM LaporanPenjualan", conn);
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LapJul_Load(object sender, EventArgs e)
        {
            GetLapJul();

            comboBox1.Items.Add("IdPenjualan");
            comboBox1.Items.Add("IdBarang");
            comboBox1.Items.Add("NamaBarang");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = comboBox1.Text;
            string Cari = textBox1.Text;

            SqlCommand cmd;

            try
            {
                conn.Open();

                if (input == "IdBarang")
                {
                    cmd = new SqlCommand("SELECT IdPenjualan, IdBarang, NamaBarang, Jumlah, Harga, (Jumlah * Harga) AS TotalHarga FROM LaporanPenjualan WHERE IdBarang LIKE @Cari", conn);
                }
                else if (input == "NamaBarang")
                {
                    cmd = new SqlCommand("SELECT IdPenjualan, IdBarang, NamaBarang, Jumlah, Harga, (Jumlah * Harga) AS TotalHarga FROM LaporanPenjualan WHERE NamaBarang LIKE @Cari", conn);
                }
                else if (input == "IdPenjualan")
                {
                    cmd = new SqlCommand("SELECT IdPenjualan, IdBarang, NamaBarang, Jumlah, Harga, (Jumlah * Harga) AS TotalHarga FROM LaporanPenjualan WHERE IdPenjualan LIKE @Cari", conn);
                }
                else
                {
                    MessageBox.Show("Error");
                    return;
                }

                cmd.Parameters.AddWithValue("@Cari", "%" + Cari + "%");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string Input = comboBox1.Text;
            string Value = textBox1.Text;

            SqlCommand cmd;

            try
            {
                conn.Open();

                if (Input == "IdBarang")
                {
                    cmd = new SqlCommand("DELETE FROM LaporanPenjualan WHERE IdBarang = @Value", conn);
                    MessageBox.Show("Data Berhasil Di Hapus");
                }
                else if (Input == "NamaBarang")
                {
                    cmd = new SqlCommand("DELETE FROM LaporanPenjualan WHERE NamaBarang = @Value", conn);
                    MessageBox.Show("Data Berhasil Di Hapus");
                }
                else
                {
                    MessageBox.Show("Error");
                    return;
                }

                cmd.Parameters.AddWithValue("@Value", Value);
                cmd.ExecuteNonQuery();

                GetLapJul();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetLapJul();
            textBox1.Clear();
        }
    }
}
