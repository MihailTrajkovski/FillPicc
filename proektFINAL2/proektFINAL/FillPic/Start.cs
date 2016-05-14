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
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.LightGreen;
            button1.BackColor = System.Drawing.Color.Pink;
            button2.BackColor = System.Drawing.Color.LightBlue;
            button3.BackColor = System.Drawing.Color.LightBlue;
            button1.ForeColor = System.Drawing.Color.DarkBlue;
            button2.ForeColor = System.Drawing.Color.DarkBlue;
            button3.ForeColor = System.Drawing.Color.DarkBlue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Form1();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
            
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Instrukcii form2 = new Instrukcii();
            form2.Closed += (s, args) => this.Close();
            form2.Show();

            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you suuuuuure?", "Exit game?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
