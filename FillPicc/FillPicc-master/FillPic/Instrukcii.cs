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
            this.BackColor = System.Drawing.Color.LightBlue;
            button1.BackColor = System.Drawing.Color.Pink;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
