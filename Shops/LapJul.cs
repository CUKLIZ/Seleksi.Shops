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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
        private object pageSize;

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

        public void exportToPdt(DataGridView dgw, string fileName)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdfPTable = new PdfPTable(dgw.Columns.Count);
            pdfPTable.DefaultCell.Padding = 3;
            pdfPTable.WidthPercentage = 100;
            pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPTable.DefaultCell.BorderWidth = 1;


            iTextSharp.text.Font text = new iTextSharp.text.Font(bf , 10, iTextSharp.text.Font.NORMAL); 
            // Add Header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfPTable.AddCell(cell);

            } 

            // Add Data Row
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    pdfPTable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.DefaultExt = ".pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdfPTable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }

        }

        private void Print_Click(object sender, EventArgs e)
        {
            exportToPdt(dataGridView1, "Laporan Penjualan");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
