using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_otamasyonu
{
    public partial class EmanetKitapListelefrm : Form
    {
        public EmanetKitapListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");
        DataSet daset = new DataSet();

        private void EmanetKitapListelefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
            comboBox1.SelectedIndex = 0;


        }

        private void EmanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from EmanetKitaplar", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["EmanetKitaplar"].Clear();
            if (comboBox1.SelectedIndex == 0)
            {
                EmanetListele();
            }
            else if (comboBox1.Text == "Geciken Kitaplar")
            {
                /*
               baglanti.Open();

               SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM EmanetKitaplar WHERE '" + DateTime.Now.ToShortDateString() + "' >= iadetarihi ORDER BY iadetarihi ASC", baglanti);
               adtr.Fill(daset, "EmanetKitaplar");
               dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
               baglanti.Close();
                 */



                baglanti.Open();

                string sqlQuery = "SELECT * FROM EmanetKitaplar WHERE CONVERT(DATE, iadetarihi, 23) < @Today ORDER BY iadetarihi ASC";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, baglanti);
                sqlCommand.Parameters.AddWithValue("@Today", DateTime.Today);

                SqlDataAdapter adtr = new SqlDataAdapter(sqlCommand);
                DataSet daset = new DataSet();
                adtr.Fill(daset, "EmanetKitaplar");

                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];

                baglanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                baglanti.Open();

                string sqlQuery = "SELECT * FROM EmanetKitaplar WHERE CONVERT(DATE, iadetarihi, 23) >= @Today ORDER BY iadetarihi ASC";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, baglanti);
                sqlCommand.Parameters.AddWithValue("@Today", DateTime.Today);

                SqlDataAdapter adtr = new SqlDataAdapter(sqlCommand);
                DataSet daset = new DataSet();
                adtr.Fill(daset, "EmanetKitaplar");

                dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];

                baglanti.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDıs_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "Excel Dosyaları";
            save.DefaultExt = "xlsx";
            save.Filter = "xlsx Dosyaları (*.xlsx)|*.xlsx|Tüm Dosyalar(*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Sayfa1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Excel Dışa Aktarım";
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                workbook.SaveAs(save.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }
        }
    }
}
