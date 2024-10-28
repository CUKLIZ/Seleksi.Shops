using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shops
{
    public partial class LoadingScreen : Form
    {

        private string role;
        private string userId;
        private string userName;

        public LoadingScreen(string userRole, string idUser, string namaUser)
        {
            InitializeComponent();
            this.role = userRole;
            this.userId = idUser;
            this.userName = namaUser;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;

            if (panel2.Width >= 800)
            {
                timer1.Stop();
                HomePage homePage = new HomePage(role, userId, userName);
                homePage.Show();
                this.Hide();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
