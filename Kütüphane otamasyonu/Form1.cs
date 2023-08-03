using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BunifuDataGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //show sample data
            bunifuCustomDataGrid1.Rows.Add(
                new object[]
                {
                    "Vincent Willioamson",
                    31,
                    "iOS Developer",
                    "Washington"
                } 
                );
            bunifuCustomDataGrid1.Rows.Add(
              new object[]
              {
                    "Tyler Reyes",
                    22,
                    "UI/UX Designer",
                    "New York"
              }
              );
            bunifuCustomDataGrid1.Rows.Add(
              new object[]
              {
                    "Justin Black",
                    26,
                    "Front End Developer",
                    "Los Angeles"
              }
              );
            bunifuCustomDataGrid1.Rows.Add(
              new object[]
              {
                    "Sean Guzman",
                    25,
                    "Web Designer",
                    "San Francisco"
              }
              );
            bunifuCustomDataGrid1.Rows.Add(
              new object[]
              {
                    "Keith Careter",
                    20,
                    "Graphic Designer",
                    "New York"
              }
              );

        }
    }
}
