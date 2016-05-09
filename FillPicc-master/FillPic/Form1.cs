using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FillPic
{
    public partial class Form1 : Form
    {
        bool flag = false;
        Timer timer;
        List<RecPart> parts = new List<RecPart>();
        List<Ball> balls = new List<Ball>();
        Ball ball;
        Ball ball2;
        Ball ball3;
        Graphics graphics;
        Brush brush;
        Brush brush2;
        Pen pen;
        Rectangle bounds;
        Bitmap doubleBuffer;
        Graphics g;
        static readonly int FRAMES_PER_SECOND = 30;
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
        public Form1()
        {

            InitializeComponent();
            //BackgroundImage = FillPic.Properties.Resources.background;
            bounds = new Rectangle(10, 10, this.Bounds.Width - 40, this.Bounds.Height - 60);
            doubleBuffer = new Bitmap(Width, Height);
            graphics = CreateGraphics();
          
            ball = new Ball(Width / 2, Height / 2, 20, 10, (float)(Math.PI / 4));
            ball2 = new Ball(50, 50, 20, 10, (float)(Math.PI / 2));
            ball3 = new Ball(10, 10, 20, 10, (float)(Math.PI / 4));
            ball3.Bounds = bounds;
            ball2.Bounds = bounds;
            ball.Bounds = bounds;
            Show();
            brush = new SolidBrush(Color.Red);
            brush2 = new SolidBrush(Color.Green);
            pen = new Pen(Color.Red);
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1000 / FRAMES_PER_SECOND;
            timer.Start();
            //g = Graphics.FromImage(doubleBuffer);
            
            Bitmap source = Form1.ResizeImage(FillPic.Properties.Resources.background,800,600);
            
            int x = 500, y = 500, width = 300, height = 100;
            Bitmap CroppedImage = source.Clone(new System.Drawing.Rectangle(x, y, width, height), source.PixelFormat);
            Bitmap CroppedImage2 = source.Clone(new System.Drawing.Rectangle(100, 100, width, height), source.PixelFormat);
            graphics.DrawImage(CroppedImage, 500, 500);
            graphics.DrawImage(CroppedImage2, 100, 100);
            
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            
            methodRandom();
            CheckCollisons();
        }
        void methodRandom()
        {
            timer.Start();
            g = Graphics.FromImage(doubleBuffer);
            g.Clear(Color.White);
            foreach(RecPart part in parts)
            {
                g.FillRectangle(brush2, part.X, part.Y, 3, 3);
            }
            g.DrawRectangle(pen, bounds);
            ball.Draw(brush, g);
            ball.Move();
            ball2.Draw(brush, g);
            ball2.Move();
            ball3.Draw(brush2, g);
            graphics.DrawImageUnscaled(doubleBuffer, 0, 0);
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Graphics g = Graphics.FromImage(doubleBuffer);
            //timer.Stop();
            //Bitmap bip = new Bitmap(FillPic.Properties.Resources.background);
            
            g = Graphics.FromImage(doubleBuffer);
            //g.Clear(Color.White);
            RecPart part = new RecPart();
            part.X = ball3.X;
            part.Y = ball3.Y;
            parts.Add(part);
            g.DrawRectangle(pen, bounds);
            methodRandom();
            ball3.Move(e);
            ball3.Draw(brush2, g);
            graphics.DrawImageUnscaled(doubleBuffer, 0, 0);
            //check collisions
            CheckCollisons();
            
         }
        public void CheckCollisons()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                double distance = (parts[i].X - ball.X) * (parts[i].X - ball.X) + (parts[i].Y - ball.Y) * (parts[i].Y - ball.Y);
                if(distance <= (2 * ball.Radius) * (2 * ball.Radius)){
                    if (flag == false)
                    {
                        flag = true;
                        this.Hide();
                        var form2 = new GameOver();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();

                    }
                }
                double distance2 = (parts[i].X - ball2.X) * (parts[i].X - ball2.X) + (parts[i].Y - ball2.Y) * (parts[i].Y - ball2.Y);
                if (distance2 <= (2 * ball2.Radius) * (2 * ball2.Radius))
                {
                    if (flag == false)
                    {
                        flag = true;
                        this.Hide();
                        var form2 = new GameOver();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();

                    }
                }

            }
                if (ball3.IsColiding(ball))
                {
                    if (flag == false)
                    {
                        flag = true;
                        this.Hide();
                        var form2 = new GameOver();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();

                    }
                }
            if (ball3.IsColiding(ball2))
            {
                if (flag == false)
                {
                    flag = true;
                    this.Hide();
                    var form2 = new GameOver();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                    
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
        }
        }
    }