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
    public partial class KitapEklefrmcs : Form
    {
        public KitapEklefrmcs()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void KitapEklefrmcs_Load(object sender, EventArgs e)
        {

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu sayfayı kapatmak istiyormusunuz", "Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
                

            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kitabı kayıt etmek istiyormusunuz?", "Kayıt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {



                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Kitap(barkodno,kitapadi,yazar,yayinevi,sayfasayisi,turu,stoksayisi,rafno,aciklama,kayittarihi)values(@barkodno,@kitapadi,@yazar,@yayinevi,@sayfasayisi,@turu,@stoksayisi,@rafno,@aciklama,@kayittarihi)", baglanti);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kitapadi", txtKitapAdı.Text);
                komut.Parameters.AddWithValue("@yazar", txtYazar.Text);
                komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
                komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
                komut.Parameters.AddWithValue("@turu", comboTur.Text);
                komut.Parameters.AddWithValue("@stoksayisi", txtStok.Text);
                komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
                komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                komut.Parameters.AddWithValue("@kayittarihi", DateTime.Now.ToShortDateString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("kitap kaydı yapıldı");
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                       



                    }

                }
            }
        }
    }
}
