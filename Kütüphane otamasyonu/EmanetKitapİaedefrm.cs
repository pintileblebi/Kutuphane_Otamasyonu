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
    public partial class EmanetKitapİaedefrm : Form
    {
        public EmanetKitapİaedefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");
        DataSet daset = new DataSet();
        private void EmanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from EmanetKitaplar", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");
            dataGridView1.DataSource = daset.Tables["EmanetKitaplar"];
            baglanti.Close();
        }
        private void EmanetKitapİaedefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["EmanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from EmanetKitaplar where tc like'%"+txtTcAra.Text +"%'",baglanti);
            adtr.Fill(daset,"EmanetKitaplar");
            baglanti.Close();
            if (txtTcAra.Text=="")
            {
                daset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }

        }

        private void txtBarkodAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["EmanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from EmanetKitaplar where barkodno like'%" + txtBarkodAra.Text + "%'", baglanti);
            adtr.Fill(daset, "EmanetKitaplar");
            baglanti.Close();
            if (txtBarkodAra.Text == "")
            {
                daset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM EmanetKitaplar WHERE tc=@tc AND barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut.ExecuteNonQuery();


            baglanti.Close() ;

            string sqlSorgusu = "UPDATE Kitap SET stoksayisi = stoksayisi + @kitapSayisi WHERE barkodno = @barkodno";

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand(sqlSorgusu, baglanti);

            komut2.Parameters.AddWithValue("@kitapSayisi", Convert.ToInt32(dataGridView1.CurrentRow.Cells["kitapsayisi"].Value));
            komut2.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());

            komut2.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kitaplar iade edildi", "Bilgi");

            daset.Tables["EmanetKitaplar"].Clear();
            EmanetListele();


        }
    }
}
