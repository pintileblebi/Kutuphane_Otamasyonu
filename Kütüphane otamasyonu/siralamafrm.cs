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
    public partial class siralamafrm : Form
    {
        public siralamafrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = pinti\\SQLEXPRESS; Initial Catalog = KütüphaneOtamsayonu; Integrated Security = True");
        DataSet daset = new DataSet();
            
        private void siralamafrm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Uye order by okukitapsayisi desc", baglanti);
            adtr.Fill(daset, "uye");
            dataGridView1.DataSource = daset.Tables["uye"];
            baglanti.Close();
            label3.Text = "";
            label4.Text = "";
            label3.Text = daset.Tables["Uye"].Rows[0]["adsoyad"].ToString()+":";
            label3.Text += daset.Tables["Uye"].Rows[0]["okukitapsayisi"].ToString();
            label4.Text = daset.Tables["Uye"].Rows[dataGridView1.Rows.Count- 2]["adsoyad"].ToString()+":";
            label4.Text += daset.Tables["Uye"].Rows[dataGridView1.Rows.Count - 2]["okukitapsayisi"].ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
