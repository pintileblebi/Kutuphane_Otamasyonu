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
    }
}
