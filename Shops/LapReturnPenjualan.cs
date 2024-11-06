using iTextSharp.text;
using iTextSharp.text.pdf;
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

namespace Shops
{
    public partial class LapReturnPenjualan : Form
    {

        private string userId;
        private string role;
        public LapReturnPenjualan(string userId, string role)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.LapReturnPenjualan_Load);
            this.userId = userId;
            this.role = role;
        }
        SqlConnection conn = new SqlConnection(@"Data Source=Tamara-Desktop\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;");


        void GetLapReturn()
        {
            SqlCommand cmd;

            if (role == "User")
            {
                cmd = new SqlCommand("SELECT * FROM ReturnPenjualan WHERE IdUser = @IdUser", conn);
                cmd.Parameters.AddWithValue("@IdUser", userId);
            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM ReturnPenjualan", conn);
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void LapReturnPenjualan_Load(object sender, EventArgs e)
        {
            GetLapReturn();

            comboBox1.Items.Add("IdReturn");
            comboBox1.Items.Add("NamaBarang");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Input = comboBox1.Text;
            string Cari = textBox1.Text;

            SqlCommand cmd;

            try
            {
                conn.Open();

                if (Input == "IdReturn")
                {
                    cmd = new SqlCommand("SELECT IdReturn, IdBarang, NamaBarang, Jumlah, Harga, (CAST(Jumlah AS BIGINT) * Harga) AS TotalHarga FROM ReturnPenjualan WHERE IdReturn LIKE @Cari OR NamaBarang LIKE @Cari", conn);
                }
                else if (Input == "NamaBarang")
                {
                    cmd = new SqlCommand("SELECT IdReturn, IdBarang, NamaBarang, Jumlah, Harga,  (CAST(Jumlah AS BIGINT) * Harga)AS TotalHarga FROM ReturnPenjualan WHERE IdReturn LIKE @Cari OR NamaBarang LIKE @Cari", conn);
                }
                else
                {
                    MessageBox.Show("Error CUY");
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
                MessageBox.Show("Eror" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetLapReturn();

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

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            // Add Header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                pdfPTable.AddCell(cell);
            }

            // Add Data Row
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdfPTable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.DefaultExt = ".pdf";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
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
            exportToPdt(dataGridView1, "Laporan Return Penjualan");
        }
    }
}
