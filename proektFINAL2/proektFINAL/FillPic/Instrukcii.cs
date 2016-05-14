using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillPic
{
    public partial class Instrukcii : Form
    {
        public Instrukcii()
        {
            InitializeComponent();
        }

        private void Instrukcii_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            button1.BackColor = System.Drawing.Color.Pink;
            this.BackColor = System.Drawing.Color.LightBlue;
          

        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
