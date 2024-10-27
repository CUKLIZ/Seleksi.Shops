﻿using System;
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
    public partial class Petugas : Form
    {
        public Petugas()
        {
            InitializeComponent();
            LoadDataPetugas();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        private void Petugas_Load(object sender, EventArgs e)
        {

        }

        void LoadDataPetugas()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nama FROM Login WHERE Role = 'Petugas'", conn);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eror" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string selectedPetugasId = row.Cells["Id"].Value.ToString();

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Username, Password FROM Login WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", selectedPetugasId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox1.Text = selectedPetugasId;
                        textBox2.Text = row.Cells["Nama"].Value.ToString();
                        textBox3.Text = reader["Username"].ToString();
                        textBox4.Text = reader["Password"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Login (Id,Nama, Username, Password, Role) VALUES (@Id, @Nama, @Username, @Password, 'Petugas') ", conn);

                    cmd.Parameters.AddWithValue("@Id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Nama", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Username", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox4.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Di Tambah");

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";

                    conn.Close();
                    LoadDataPetugas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string IdUser = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();

                SqlCommand cmd = new SqlCommand("UPDATE Login SET Nama = @Nama, Username = @Username, Password = @Password WHERE Id= @Id", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", IdUser);
                cmd.Parameters.AddWithValue("@Nama", textBox2.Text);
                cmd.Parameters.AddWithValue("@Username", textBox3.Text);
                cmd.Parameters.AddWithValue("@Password", textBox4.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Data Berhasil Di Edit");
                LoadDataPetugas();
            }
            else
            {
                MessageBox.Show("Pilih Baris Yang Mau Di Edit");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE Login WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", textBox1.Text);

            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Data Berhasil Di Hapus");
            } else
            {
                MessageBox.Show("Data Tidak Di Temukan");
            }

            LoadDataPetugas();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
