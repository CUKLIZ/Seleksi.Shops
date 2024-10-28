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
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Admin
                SqlCommand cmdAdmin = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Role = 'Admin' ", conn);
                int JumAdmin = (int)cmdAdmin.ExecuteScalar();
                label3.Text = $"{JumAdmin}";

                // Petugas
                SqlCommand cmdPetugas = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Role = 'Petugas' ", conn);
                int JumPetugas = (int)cmdPetugas.ExecuteScalar();
                label4.Text = $"{JumPetugas}";

                // User
                SqlCommand cmdUser = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Role = 'User' ", conn);
                int JumUser = (int)cmdUser.ExecuteScalar();
                label7.Text = $"{JumUser}";

                // Produk 
                SqlCommand cmdProduk = new SqlCommand("SELECT COUNT(*) FROM Barangs", conn);
                int JumProduk = (int)cmdProduk.ExecuteScalar();
                label11.Text = $"{JumProduk}";

            } catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            } finally
            {
                conn.Close();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
