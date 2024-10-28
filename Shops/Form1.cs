using System.Data.SqlClient;
using System.Drawing;
using System;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace Shops
{
    public partial class CloseBTN : Form
    {
        public CloseBTN()
        {
            InitializeComponent();
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);    
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {
            String username, password, role;

            username = UsernameBox.Text;
            password = PasswordBox.Text;

            try
            {
                // Query untuk mengambil Role berdasarkan Username dan Password
                //string query = "SELECT Role FROM Login WHERE Username = @username AND Password = @password";
                string query = "SELECT Id, Role FROM Login WHERE Username = @username AND Password = @password";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                sda.SelectCommand.Parameters.AddWithValue("@username", username);
                sda.SelectCommand.Parameters.AddWithValue("@password", password);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                // Cek apakah query mengembalikan hasil
                if (dtable.Rows.Count > 0)
                {
                    role = dtable.Rows[0]["Role"].ToString();
                    string idUser = dtable.Rows[0]["Id"].ToString(); 
                    string namaUser = username;

                    // Cek role dan buka halaman yang sesuai
                    if (role == "Admin" || role == "User" || role == "Petugas")
                    {
                        // Kirim role, idUser, dan namaUser ke HomePage
                        //HomePage homePage = new HomePage(role, idUser, namaUser);
                        //homePage.Show();
                        //this.Hide();

                        LoadingScreen loadingScreen = new LoadingScreen(role, idUser, namaUser);
                        loadingScreen.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Salah Password atau Username");
                    UsernameBox.Clear();
                    PasswordBox.Clear();
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

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                PasswordBox.PasswordChar = '\0';
            } else
            {
                PasswordBox.PasswordChar = '*';
            }
        }
    }
}
