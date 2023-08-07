using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Kütüphane_otamasyonu
{
    public partial class uyelistelemefrm : Form
    {
        public uyelistelemefrm()
        {
            InitializeComponent();
        }
        private void uyelistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from uye", baglanti);
            adtr.Fill(daset, "uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
        }

        private void uyelistelemefrm_Load(object sender, EventArgs e)
        {
            uyelistele();

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from uye where tc like'" + txtTc.Text + "'", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {

                txtAdSoyad.Text = reader["adsoyad"].ToString();
                txtYas.Text = reader["yas"].ToString();
                comboCinsiyet.Text = reader["cinsiyet"].ToString();
                txtTelefon.Text = reader["telefon"].ToString();
                txtAdres.Text = reader["adres"].ToString();
                txtEmail.Text = reader["email"].ToString();
                txtOkunanSayi.Text = reader["okukitapsayisi"].ToString();
            }
            baglanti.Close();
        }
        DataSet daset = new DataSet();

        private void txtTcAra_TextChanged(object sender, EventArgs e)
        {
            string alinen_deger = comboBox1.Text;
            if (alinen_deger == "T.C ile Arama")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where tc like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "İsim ile arama")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where adsoyad like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "Yas")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where yas like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "Cinsiyet")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where cinsiyet like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "Adres")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where adres like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "Telefon")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where telefon like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "E-mail")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where email like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


                baglanti.Close();

            }
            if (alinen_deger == "Okunan Kitap ")
            {
                daset.Tables["uye"].Clear();
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select *from uye where okukitapsayisi like '%" + txtTcAra.Text + "%'", baglanti);
                adtr.Fill(daset, "uye");
                dataGridView1.DataSource = daset.Tables["uye"];


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

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı silmek istiyormusunuz?", "sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {


                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from uye where tc=@tc", baglanti);
                komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("silme işlemi gerçekleşti");
                daset.Tables["uye"].Clear();
                uyelistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }

                }
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı güncellemek istediğinize emin misiniz?", "Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update uye set adsoyad=@adsoyad,yas=@yas,cinsiyet=@cinsiyet,telefon=@telefon,adres=@adres,email=@email,okukitapsayisi=@okukitapsayisi where tc =@tc", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@yas", txtYas.Text);
                komut.Parameters.AddWithValue("@cinsiyet", comboCinsiyet.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@adres", txtAdres.Text);
                komut.Parameters.AddWithValue("@email", txtEmail.Text);
                komut.Parameters.AddWithValue("@okukitapsayisi", int.Parse(txtOkunanSayi.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi gerçekleşti");

                daset.Tables["Uye"].Clear();
                uyelistele();
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

        private void txtAdres_TextChanged(object sender, EventArgs e)
        {

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
    

