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
using System.Windows.Forms;

namespace Shops
{
    public partial class HomePage : Form
    {
        private string role;
        private string userId;
        private string userName;
        private System.Windows.Forms.Timer timer;

        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");
        public HomePage(string userRole, string userId, string namaUser)
        {
            InitializeComponent();
            this.Petugas.Click += new System.EventHandler(this.Petugas_Click);

            label3.Text = $"Welcome, {namaUser}";            

            label5.Text = "Home Page";

            this.role = userRole;
            this.userId = userId;
            this.userName = namaUser;

            PelanganPesan pelanganPesanControl = new PelanganPesan(userId, namaUser);
            panel3.Controls.Clear();
            panel3.Controls.Add(pelanganPesanControl);

            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer(); 
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick; 
            timer.Start(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("HH:mm:ss"); 
        }

        public void loadForm(object Form)
        {
            if (this.panel3.Controls.Count > 0)
                this.panel3.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(f);
            this.panel3.Tag = f;
            f.Show();
        }


        private void HomePage_Load(object sender, EventArgs e)
        {
            if (role == "Admin")
            {
                string nama = userName;

                label3.Text = $"Welcome {userName}";              
                Pesan.Hide();
            }
            else if (role == "User")
            {
                string nama = userName;

                label3.Text = $"Welcome {userName}";
                Master.Hide();
                User.Hide();
                Petugas.Hide();
                Vendor.Hide();
                Barang.Hide();
                Aktifitas.Hide();
                TerimaPesanan.Hide();
                Laporan.Hide();
                Penjualan.Hide();
                ReturnPenjualan.Hide();
            }
            else if (role == "Petugas")
            {
                string nama = userName;

                label3.Text = $"Welcome  {userName}";
                Pesan.Hide();
                Petugas.Hide();

            }

            //Home.Visible = true;
            ShowHomeControl();


        }

        private void ShowHomeControl()
        {
            label5.Text = "Home Page"; 
            Home homeControl = new Home();
            panel3.Controls.Clear();
            homeControl.Dock = DockStyle.Fill;
            panel3.Controls.Add(homeControl);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "Home Page";
            Home homeControl = new Home();
            panel3.Controls.Clear();
            panel3.Controls.Add(homeControl);


        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //if (role == "Admin")
            //{
            //    label3.Text = "Welcome Admin";
            //}
            //else if (role == "User")
            //{
            //    label3.Text = "Welcome User";
            //}
        }

        private void Laporan_Click(object sender, EventArgs e)
        {

        }

        private void TerimaPesanan_Click(object sender, EventArgs e)
        {
            label5.Text = "Terima Pesanan Page";
            loadForm(new TerimaPesan());
        }

        private void Pesan_Click(object sender, EventArgs e)
        {   
            label5.Text = "Pesan Page";
            PelanganPesan pelanganPesanControl = new PelanganPesan(userId, userName);
            panel3.Controls.Clear();
            panel3.Controls.Add(pelanganPesanControl);
        }

        private void User_Click(object sender, EventArgs e)
        {
            label5.Text = "User Page";
            UserPage1 UserPage1Control = new UserPage1();
            panel3.Controls.Clear();
            UserPage1Control.Dock = DockStyle.Fill;
            panel3.Controls.Add(UserPage1Control);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("T");
        }

        private void Petugas_Click(object sender, EventArgs e)
        {
            label5.Text = "Petugas Page";
            loadForm(new Petugas());
        }

        private void Vendor_Click(object sender, EventArgs e)
        {
            label5.Text = "Vendor Page";
            loadForm(new Vendor());
        }

        private void Barang_Click(object sender, EventArgs e)
        {
            label5.Text = "Barang Page";
            Barang barangForm = new Barang();
            loadForm(barangForm);
            //barangForm.GetBarang();
        }

        private void Penjualan_Click(object sender, EventArgs e)
        {
            label5.Text = "Penjualan Page";

            if (role == "Petugas")
            {
                Penjualan penjualanForm = new Penjualan(userId, userName);
                loadForm(penjualanForm);
            }
            else if (role == "Admin")
            {
                Penjualan penjualanForm = new Penjualan("", ""); // Biarkan kosong untuk Admin
                loadForm(penjualanForm);
            }
        }

        private void ReturnPenjualan_Click(object sender, EventArgs e)
        {
            label5.Text = "Return Penjualan Page";
            loadForm(new ReturnPenjualan());
        }

        private void LapJul_Click(object sender, EventArgs e)
        {
            label5.Text = "Laporan Jualan Page";
            loadForm(new LapJul(userId, role));
        }

        private void LapReturnJul_Click(object sender, EventArgs e)
        {
            label5.Text = "Laporan Return Jualan Page";
            loadForm(new LapReturnPenjualan(userId, role));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CloseBTN homePage = new CloseBTN();
            homePage.Show();
            this.Hide();
        }
    }
}
