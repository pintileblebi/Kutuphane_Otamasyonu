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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kütüphane_otamasyonu
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        bool isThere;
        SqlConnection connection = new SqlConnection("Data Source=pinti\\SQLEXPRESS;Initial Catalog=giris;Integrated Security=TRUE");
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtKulAd.Text;
            string pass = txtŞifre.Text;

            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM giris",connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (username == reader["username"].ToString().TrimEnd() && pass == reader["pass"].ToString().TrimEnd())
                {
                    isThere = true;
                    break;

                }
                else
                {
                    isThere = false;
                }

            }
            if (isThere)
            {
              
                form1 anasayfa = new  form1();
                anasayfa.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("giris yapılamadı", "program");
                this.Hide();
            }

        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
 }

