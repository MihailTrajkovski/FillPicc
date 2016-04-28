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
    public partial class Form1 : Form
    {
        Timer timer;
        Timer timer2;
        Ball ball;
        Ball ball2;
        Ball ball3;
        Graphics graphics;
        Brush brush;
        Pen pen;
        Rectangle bounds;
        Bitmap doubleBuffer;
        Graphics g;
        static readonly int FRAMES_PER_SECOND = 30;
        public Form1()
        {
            InitializeComponent();
            bounds = new Rectangle(10, 10, this.Bounds.Width - 40, this.Bounds.Height - 60);
            doubleBuffer = new Bitmap(Width, Height);
            graphics = CreateGraphics();
          
            ball = new Ball(Width / 2, Height / 2, 20, 10, (float)(Math.PI / 4));
            ball2 = new Ball(50, 50, 20, 10, (float)(Math.PI / 2));
            ball3 = new Ball(50, 50, 20, 10, (float)(Math.PI / 4));
            ball3.Bounds = bounds;
            ball2.Bounds = bounds;
            ball.Bounds = bounds;
            Show();
            brush = new SolidBrush(Color.Blue);
            pen = new Pen(Color.Red);
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000 / FRAMES_PER_SECOND;
            timer.Start();
            /*timer2 = new Timer();
            timer2.Tick += new EventHandler(timer_Tick);
            timer2.Interval = 1000 / FRAMES_PER_SECOND;
            timer2.Start();*/
        }

        void timer_Tick(object sender, EventArgs e)
        {
            
            methodRandom();
                      
        }
        void methodRandom()
        {
            timer.Start();
            g = Graphics.FromImage(doubleBuffer);
            g.Clear(Color.White);
            g.DrawRectangle(pen, bounds);
            ball.Draw(brush, g);
            ball.Move();
            ball2.Draw(brush, g);
            ball2.Move();
            ball3.Draw(brush, g);
            graphics.DrawImageUnscaled(doubleBuffer, 0, 0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Graphics g = Graphics.FromImage(doubleBuffer);
            //timer.Stop();
            g = Graphics.FromImage(doubleBuffer);
            g.Clear(Color.White);
            g.DrawRectangle(pen, bounds);
            methodRandom();
            //ball.Draw(brush, g);
            //ball.Move();
            //ball2.Draw(brush, g);
            //ball2.Move();
            //g.draw
            g.FillRectangle(brush, ball3.X - 10, ball3.Y - 10, 50, 50);
            ball3.Draw(brush, g);
            ball3.Move(e);
            
            graphics.DrawImageUnscaled(doubleBuffer, 0, 0);
         }
    }
}
