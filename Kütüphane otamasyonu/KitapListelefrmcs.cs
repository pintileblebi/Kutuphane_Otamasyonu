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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Kütüphane_otamasyonu
{
    public partial class KitapListelefrm : Form
    {
        DataSet daset = new DataSet(); 
        public KitapListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");
        private void kitaplistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Kitap", baglanti);
            adtr.Fill(daset, "Kitap");
            dataGridView1.DataSource = daset.Tables["Kitap"];
            baglanti.Close();
        }

        private void KitapListelefrm_Load(object sender, EventArgs e)
        {
            kitaplistele();

        }
        
       
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kitap kaydını güncellemek istediğinize emin misiniz?", "Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update Kitap set kitapadi=@kitapadi,yazar=@yazar,yayinevi=@yayinevi,sayfasayisi=@sayfasayisi,turu=@turu,stoksayisi=@stoksayisi,rafno=@rafno,aciklama=@aciklama where barkodno =@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@kitapadi", txtKitapAdı.Text);
                komut.Parameters.AddWithValue("@yayinevi", txtYayinevi.Text);
                komut.Parameters.AddWithValue("@yazar", txtYazar.Text);
                komut.Parameters.AddWithValue("@turu", comboTur.Text);
                komut.Parameters.AddWithValue("@sayfasayisi", txtSayfaSayisi.Text);
                komut.Parameters.AddWithValue("@stoksayisi", txtSayfaSayisi.Text);
                komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
                komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
    
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi gerçekleşti");

                daset.Tables["Kitap"].Clear();
                kitaplistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                        
                    }

                }
                baglanti.Close();

            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kitap kaydını silmek istiyormusunuz?", "sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Kitap where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("silme işlemi gerçekleşti");
                daset.Tables["Kitap"].Clear();
                kitaplistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }

                }
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        
        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
           
            string alinen_deger = comboBox1.Text;
            if (alinen_deger == "Kitap Barkod NO ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where barkodno like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Kitap Adı ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where kitapadi like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Yazar Adı ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where yazar like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Yayınevi Adı ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where yayinevi like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();
            }
            if (alinen_deger == "Sayfa Sayısı ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where sayfasayisi like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Türü ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where turu like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Stok Sayısı ile Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where stoksayisi like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Bulunduğu Rafa Göre Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where rafno like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }
            if (alinen_deger == "Açıkalamaya Göre Ara:")
            {
                daset.Tables["Kitap"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from Kitap where aciklama like '%" + txtAra.Text + "%'", baglanti);
                adtr.Fill(daset, "Kitap");
                dataGridView1.DataSource = daset.Tables["Kitap"];


                baglanti.Close();

            }



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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kitap where barkodno like'" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                txtBarkodNo.Text = reader["barkodno"].ToString();
                txtKitapAdı.Text = reader["kitapadi"].ToString();
                txtYazar.Text = reader["yazar"].ToString();
                txtYayinevi.Text = reader["yayinevi"].ToString();
                txtSayfaSayisi.Text = reader["sayfasayisi"].ToString();
                comboTur.Text = reader["turu"].ToString();
                txtStok.Text = reader["stoksayisi"].ToString();
                txtRafNo.Text = reader["rafno"].ToString();
                txtAciklama.Text = reader["aciklama"].ToString();
                
                
            }
            baglanti.Close();
        }

        private void btnİptal_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu sayfayı kapatmak istiyormusunuz", "Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                this.Close();


            }

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


