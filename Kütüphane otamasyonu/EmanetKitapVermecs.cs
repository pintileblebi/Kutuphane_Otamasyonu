using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane_otamasyonu
{
    public partial class EmanetKitapVermecs : Form
    {
        public EmanetKitapVermecs()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");
        DataSet daset = new DataSet();


        private void txtTelefon_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kitap where barkodno like'" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {

                txtKitapAdı.Text = reader["kitapadi"].ToString();
                txtYazar.Text = reader["yazar"].ToString();
                txtYayinevi.Text = reader["yayinevi"].ToString();
                txtSayfaSayisi.Text = reader["sayfasayisi"].ToString();


            }
            baglanti.Close();
            if (txtBarkodNo.Text=="")
            {
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtKitapSayisi)
                    {
                        item.Text = "";
                    }



                }

            }
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {
            if (lblKitapsayisi.Text!="")
            {
                if (lblKayitliKitapSayisi.Text == "" && int.Parse(lblKitapsayisi.Text)<= 3 || lblKayitliKitapSayisi.Text !="" && int.Parse(lblKayitliKitapSayisi.Text) + int.Parse(lblKitapsayisi.Text) <= 3)
                {
                    if (txtTcAra.Text != "" && txtAdSoyad.Text != "" && txtYas.Text != "" && txtTelefon.Text != "")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            baglanti.Open();
                            SqlCommand sqlCommand = new SqlCommand("insert into EmanetKitaplar(tc,adsoyad,yas,telefon,barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@tc,@adsoyad,@yas,@telefon,@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
                            sqlCommand.Parameters.AddWithValue("@tc",txtTcAra.Text);
                            sqlCommand.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            sqlCommand.Parameters.AddWithValue("@yas", txtYas.Text);
                            sqlCommand.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            sqlCommand.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@sayfasayisi", int.Parse(dataGridView1.Rows[i].Cells["sayfasayisi"].Value.ToString()));
                            sqlCommand.Parameters.AddWithValue("@kitapsayisi", dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            sqlCommand.Parameters.AddWithValue("@iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            sqlCommand.ExecuteNonQuery();
                            SqlCommand komut2 = new SqlCommand("update Uye set okukitapsayisi=okukitapsayisi+'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "'where tc='" + txtTcAra.Text + "'", baglanti);
                            komut2.ExecuteNonQuery();
                            SqlCommand komut3 = new SqlCommand("update Kitap set stoksayisi= stoksayisi-'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "'where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                            komut3.ExecuteNonQuery();
                            baglanti.Close();

                        }
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("delete from sepet", baglanti);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("kitaplar emanet edildi");
                        daset.Tables["sepet"].Clear();
                        sepetlistele();
                        txtTcAra.Text = "";
                        lblKitapsayisi.Text = "";
                        Kitapsayisi();
                        lblKayitliKitapSayisi.Text = "";
                        //komut satırları
                    }
                    else
                    {
                        MessageBox.Show("Önce üye ismi seçmeniz gerekir lütfen üye seçin", "Uyarı !");

                    }

                }
                else
                {
                    MessageBox.Show("Emanet kitap sayısı 3 ten fazla olamaz","Uyarı !");
                    
                }


            }
            else
            {
                MessageBox.Show("Önce sepete Kitap Ekleyiniz","Uyarı!");
            }
            
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Sepet", baglanti);
            adtr.Fill(daset, "Sepet");
            dataGridView1.DataSource = daset.Tables["Sepet"];
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Sepet(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadi", txtKitapAdı.Text);
            komut.Parameters.AddWithValue("@yazari", txtYazar.Text);
            komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(txtKitapSayisi.Text));
            komut.Parameters.AddWithValue("@teslimtarihi", TeslimTarihi.Text);
            komut.Parameters.AddWithValue("@iadetarihi", İadeTarihi.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["Sepet"].Clear();
            sepetlistele();
            MessageBox.Show("Kitaplar sepete eklendi");
            lblKitapsayisi.Text = "";
            Kitapsayisi();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtKitapSayisi)
                    {
                        item.Text = "";
                    }



                }

            }


        }
        private void Kitapsayisi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapsayisi) from Sepet", baglanti);
            lblKitapsayisi.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();
        }

        private void EmanetKitapVermecs_Load(object sender, EventArgs e)
        {
            sepetlistele();
            Kitapsayisi();

        }

        private void txtTcAra_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kitap kaydını silmek istiyormusunuz?", "sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {

                SqlCommand komut = new SqlCommand("delete from Sepet where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() +"'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                daset.Tables["Sepet"].Clear();
                sepetlistele();
                lblKitapsayisi.Text = "";
                Kitapsayisi();

            }
        }

        private void txtTcAra_Leave(object sender, EventArgs e)
        {
           

        }

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Uye where tc like'" +  txtTcAra.Text + "'", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                txtAdSoyad.Text = reader["adsoyad"].ToString();
                txtYas.Text = reader["yas"].ToString();
                txtTelefon.Text = reader["telefon"].ToString();


            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(kitapsayisi) from Emanetkitaplar", baglanti);
            lblKayitliKitapSayisi.Text = komut2.ExecuteScalar().ToString();
            baglanti.Close();
            if (txtTcAra.Text == "")
            {
                foreach (Control item in groupBox1.Controls)
                {
                    item.Text = "";
                    lblKayitliKitapSayisi.Text = "";

                }
            }
            lblKayitliKitapSayisi.Text = "";
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
