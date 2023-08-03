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

namespace Kütüphane_otamasyonu
{
    public partial class sonuçlar : Form
    {
        public sonuçlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");

        private void sonuçlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select adsoyad,okukitapsayisi from Uye ",baglanti);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                chart1.Series["Okunnan Kitap sayisi"].Points.AddXY(reader["adsoyad"].ToString(), reader["okukitapsayisi"].ToString());
            }
            baglanti.Close();
           


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
