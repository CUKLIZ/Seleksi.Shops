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

            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;            

            textBox1.TextChanged += textBox1_TextChanged;
        }

        // Cek apakah Id sudah ada di dalam database atau belum
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
                MessageBox.Show("Error ", ex.Message);
                return false;
            }
        }

        void LoadDataUser()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nama, Username FROM Login WHERE Role = 'User' ORDER BY Id DESC", conn);

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
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells["Id"].Value != null)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string selectedUserId = row.Cells["Id"].Value.ToString();

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Username, Password FROM Login WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", selectedUserId);

                    //SqlDataReader reader = cmd.ExecuteReader();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = selectedUserId;
                            textBox2.Text = row.Cells["Nama"].Value.ToString();
                            textBox3.Text = reader["Username"].ToString();
                            textBox4.Text = reader["Password"].ToString();
                        }

                        //reader.Close();
                        button2.Visible = true;
                        button3.Visible = true;

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

                    button1.Visible = true; // Menampilkan Button
                    button2.Visible = false; // Menghilangan Button
                    button3.Visible = false; // Menghilangan Button

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

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

                button1.Visible = true;
                button2.Visible = false;
                button3.Visible = false;


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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {                      
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                bool IdSekarang = CheckIdExists(textBox1.Text);

                button1.Visible = !IdSekarang; // Hide submit button if ID exists
                button2.Visible = IdSekarang;
                button3.Visible = IdSekarang;

                // Jika Id di Text box tidak ada
                if (!IdSekarang)
                {
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
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
