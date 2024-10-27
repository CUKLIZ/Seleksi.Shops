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
    public partial class UserPage1 : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");
        public UserPage1()
        {
            InitializeComponent();
            LoadDataUser(); // Untuk Me Load Users
        }

        void LoadDataUser()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nama FROM Login WHERE Role = 'User'", conn);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt); // Isi DataTable dengan hasil query

                // Tampilkan hasil di DataGridView
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    string selectedUserId = row.Cells["Id"].Value.ToString();

                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT Username, Password FROM Login WHERE Id = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", selectedUserId);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            textBox1.Text = selectedUserId;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserPage1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Login(Id, Nama, Username, Password, Role) VALUES (@Id, @Nama, @Username, @Password, 'User')", conn);

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
                    LoadDataUser();


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
            else
            {
                MessageBox.Show("Data Tidak Boleh Ada Yang Kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Ambil IdUserYang Di Pilih Di Row
                string IdUser = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();


                SqlCommand cmd = new SqlCommand("UPDATE Login SET Nama= @Nama, Username= @Username, Password= @Password WHERE Id= @Id", conn);
                conn.Open();

                cmd.Parameters.AddWithValue("@Id", IdUser); // Di Ambil Dari Row Yang Di Pilih
                cmd.Parameters.AddWithValue("@Nama", textBox2.Text);
                cmd.Parameters.AddWithValue("@Username", textBox3.Text);
                cmd.Parameters.AddWithValue("@Password", textBox4.Text);
                //cmd.Parameters.AddWithValue("@Role", "User");

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Data Berhasil Di Edit");
                LoadDataUser();

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
            }
            else
            {
                MessageBox.Show("Data Tidak Di Temukan");
            }

            LoadDataUser();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
