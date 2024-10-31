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
    public partial class ReturnPenjualan : Form
    {
        public ReturnPenjualan()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ReturnPenjualan_Load);
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        private void ReturnPenjualan_Load(object sender, EventArgs e)
        {
            Proses();
            GetLapReturnId();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string InputId = textBox1.Text;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdPenjualan, IdUser, NamaUser, IdBarang, NamaBarang, Jumlah, Harga FROM Penjualan WHERE IdPenjualan = @IdPenjualan", conn);
                cmd.Parameters.AddWithValue("@IdPenjualan", InputId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox2.Text = reader["IdUser"].ToString();
                    textBox3.Text = reader["IdUser"].ToString();
                    textBox14.Text = reader["NamaUser"].ToString();
                    textBox9.Text = reader["IdBarang"].ToString();
                    textBox8.Text = reader["NamaBarang"].ToString();
                    textBox7.Text = reader["Jumlah"].ToString();
                    textBox10.Text = reader["Harga"].ToString();
                }
                else
                {
                    MessageBox.Show("Penjualan Tidak Di Temukan");
                }
                reader.Close();

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string InputId = textBox9.Text;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT NamaBarang, Jumlah, Harga, IdPenjualan, IdUser, NamaUser FROM Penjualan WHERE IdBarang = @IdBarang", conn);
                cmd.Parameters.AddWithValue("@IdBarang", InputId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox8.Text = reader["NamaBarang"].ToString();
                    textBox7.Text = reader["Jumlah"].ToString();
                    textBox10.Text = reader["Harga"].ToString();
                    //textBox1.Text = reader["IdPenjualan"].ToString();
                    //textBox2.Text = reader["IdPenjualan"].ToString();
                    //textBox3.Text = reader["IdUser"].ToString();
                    //textBox14.Text = reader["NamaUser"].ToString();
                }
                else
                {
                    MessageBox.Show("Penjualan Tidak Di Temukan");
                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IdBarang = textBox9.Text;
            string NamaBarang = textBox8.Text;
            int Jumlah = int.Parse(textBox7.Text);
            int Harga = int.Parse(textBox10.Text);

            int TotalHarga = Jumlah * Harga;
            dataGridView1.Rows.Add(IdBarang, NamaBarang, Jumlah, Harga, TotalHarga);

            long totalKeseluruhan = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    totalKeseluruhan += Convert.ToInt64(row.Cells[4].Value);
                }
            }
            textBox13.Text = totalKeseluruhan.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string IdPenjualan = textBox1.Text;
            string IdReturn = textBox6.Text;
            string IdUser = textBox3.Text;
            string NamaUser = textBox14.Text;

            try
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {                    
                    if (row.Cells[0].Value != null) // Cek Nak Row Tidak Kosong
                    {
                        string IdBarang = row.Cells[0].Value.ToString();
                        string NamaBarang = row.Cells[1].Value.ToString();
                        int Jumlah = Convert.ToInt32(row.Cells[3].Value);
                        int Harga = Convert.ToInt32(row.Cells[4].Value);

                        SqlCommand cmd = new SqlCommand("INSERT INTO ReturnPenjualan (IdReturn, IdPenjualan, IdBarang, NamaBarang, Jumlah, Harga, IdUser, NamaUser) " +
                            "VALUES (@IdReturn, @IdPenjualan, @IdBarang, @NamaBarang, @Jumlah, @Harga, @IdUser, @NamaUser) ", conn);

                        cmd.Parameters.AddWithValue("@IdReturn", IdReturn);
                        cmd.Parameters.AddWithValue("@IdPenjualan", IdPenjualan);
                        cmd.Parameters.AddWithValue("@IdBarang", IdBarang);
                        cmd.Parameters.AddWithValue("@NamaBarang", NamaBarang);
                        cmd.Parameters.AddWithValue("@Jumlah", Jumlah);
                        cmd.Parameters.AddWithValue("@Harga", Harga);
                        cmd.Parameters.AddWithValue("@IdUser", IdUser);
                        cmd.Parameters.AddWithValue("@NamaUser", NamaUser);

                        cmd.ExecuteNonQuery();
                    }
                }

                SqlCommand Delete = new SqlCommand("DELETE FROM Penjualan WHERE IdPenjualan = @IdPenjualan", conn);
                Delete.Parameters.AddWithValue("@IdPenjualan", IdPenjualan);
                Delete.ExecuteNonQuery();

                MessageBox.Show("Barang Berhasil Di Return");

                textBox1.Clear();
                textBox6.Clear();
                dataGridView1.Rows.Clear();
                GetLapReturnId();
                Proses();
            }
            catch 
            {
                MessageBox.Show("Error");
            } finally
            {
                conn.Close();
            }            
        }
        void Proses()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdPenjualan ,IdBarang, NamaBarang, Jumlah, Harga, NamaVendor, Idven FROM Penjualan", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        void GetLapReturnId()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdReturn FROM ReturnPenjualan ORDER BY IdReturn DESC", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd); 
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView3.DataSource = dt;
        }
    }
}
