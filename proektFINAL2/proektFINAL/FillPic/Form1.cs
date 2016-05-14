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
        bool flag2 = false;
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
        Bitmap source;
        Bitmap background = null;
        int tmp = 0;
        int brojIzminati = 0;
        int broj = 0;
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
            bounds = new Rectangle(10, 10, 780, 580);
            doubleBuffer = new Bitmap(Width, Height);
            graphics = CreateGraphics();
            Random r = new Random();
            broj = r.Next(435000);
            tbMaxPixel.Text = broj.ToString();
            //ball = new Ball(Width / 2, Height / 2, 10, 10, (float)(Math.PI / 4));
            ball = new Ball(r.Next(200, 600), r.Next(200, 600), 10, 10, (float)(Math.PI / 4));
            ball2 = new Ball(r.Next(20, 100), r.Next(20, 100), 10, 10, (float)(Math.PI / 4));
            ball3 = new Ball(10, 10, 10, 10, (float)(Math.PI / 4));
            //ball3.Bounds = bounds;
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
            
             source = Form1.ResizeImage(FillPic.Properties.Resources.background,800,600);
            
            /*int x = 500, y = 500, width = 300, height = 100;
            Bitmap CroppedImage = source.Clone(new System.Drawing.Rectangle(x, y, width, height), source.PixelFormat);
            Bitmap CroppedImage2 = source.Clone(new System.Drawing.Rectangle(100, 100, width, height), source.PixelFormat);
            graphics.DrawImage(CroppedImage, 500, 500);
            graphics.DrawImage(CroppedImage2, 100, 100);*/
            
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
            
        }
        void methodRandom()
        {
            timer.Start();
            g = Graphics.FromImage(doubleBuffer);
            g.Clear(Color.White);
            
            g.DrawRectangle(pen, bounds);
            if (background != null)
            {
                g.DrawImage(background, 0, 0);
            }
            int min1=0;
            int min2=0;
            for (int i = 0; i < parts.Count;i++)
            {
                //label1.Text = "VLAGA LI TUKA???" + i;
                /*if(i==0)
                {
                    if (ball3.Y == bounds.Top || ball3.Y == bounds.Bottom || ball3.X == bounds.Left || ball3.X == bounds.Right)
                    {
                        RecPart rp = new RecPart();
                        rp.X = ball3.X;
                        rp.Y = ball3.Y;
                        parts.Add(rp);
                        g.FillRectangle(brush2, ball3.X, ball3.Y, 5, 5);
                        
                    }
                    if (ball3.Y != bounds.Top || ball3.Y != bounds.Bottom || ball3.X != bounds.Left || ball3.X != bounds.Right)
                    {
                        g.FillRectangle(brush2, 100, 100, 100, 100);
                    }

                }*/
                //g.FillRectangle(brush2, 85, 10, 5, 5);
                if(!(ball3.Y == bounds.Bottom-5)  || !(ball3.Y == bounds.Right-5 ))
                {
                    g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                }
                if((ball3.Y == bounds.Bottom-5) || (ball3.Y == bounds.Right-5))
                {
                    g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                }

                if (i!=0 )
                {
                    
                    if (ball3.Y == bounds.Top || ball3.Y == bounds.Bottom -5 || ball3.X == bounds.Left || ball3.X >= bounds.Right-5)
                    {

                        
                        if (parts[0].X != parts[parts.Count - 1].X && parts[0].Y != parts[parts.Count - 1].Y)
                            {
                                RecPart rp = new RecPart();
                                rp.X = ball3.X;
                                rp.Y = ball3.Y;
                            //g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                            //parts.Add(rp);
                            

                            g.FillRectangle(brush2, rp.X, rp.Y, 5, 5);
                                //g.FillRectangle(brush2, rp.X-5, rp.Y-5, 5, 5);
                                 min1 = Convert.ToInt16(Math.Min(parts[0].X, parts[parts.Count - 1].X));
                                 min2 = Convert.ToInt16(Math.Min(parts[0].Y, parts[parts.Count-1].Y));
                                 flag2 = true;// break; 
                                //break;
                            }
                        if (!flag2)
                        {
                            parts.RemoveAt(0);
                            i = 0;
                        }
                        //break;







                    }
                    
                    
                    
                    
                }
                
                



            }
            if (flag2)
            {
                //label2.Text = min1 + 5 + " MINIMUM " + min2 + 5;
                float minX = float.MaxValue;
                float maxX = float.MinValue;
                float minY = float.MaxValue;
                float maxY = float.MinValue;
                foreach (RecPart part in parts) {
                    if (part.X < minX)
                        minX = part.X;
                    if(part.X > maxX)
                    {
                        maxX = part.X;
                    }
                    if (part.Y < minY)
                        minY = part.Y;
                    if (part.Y > maxY)
                    {
                        maxY = part.Y;
                    }
                }
                Random random = new Random();
                int floatX = 0;
                int floatY = 0;
                doubleBuffer = FloodFill(doubleBuffer, source, new Point(Convert.ToInt16(parts[0].X), Convert.ToInt16(parts[0].Y)), Color.Green, Color.Black, tmp, broj);//ne se koristi black
                int i = 0;
                while (true)
                {
                    i++;
                    floatX = random.Next(Convert.ToInt32(minX), Convert.ToInt32(maxX));
                    floatY = random.Next(Convert.ToInt32(minY), Convert.ToInt32(maxY));

                    if (ColorMatch(doubleBuffer.GetPixel(Convert.ToInt32(floatX), Convert.ToInt32(floatY)), Color.White))
                    {
                        doubleBuffer = FloodFill(doubleBuffer, source, new Point(Convert.ToInt32(floatX), Convert.ToInt32(floatY)), Color.White, Color.Black, tmp, broj);
                        break;
                    }
                    if(i>55)
                    {
                        break;
                    }
                }
                

                //bitmap = new Bitmap(doubleBuffer);

                flag2 = false;
                parts.Clear();
            }


            background = new Bitmap(doubleBuffer);
            //g.DrawImage(background, 0, 0);
            ball.Draw(brush, g);
            ball.Move();
            ball2.Draw(brush, g);
            ball2.Move();
            ball3.Draw(brush2, g);
            CheckCollisons();
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
            
            //label1.Text = ColorMatch(doubleBuffer.GetPixel(Convert.ToInt16(ball3.X) -3, Convert.ToInt16(ball3.Y) - 3), Color.White).ToString();
            

            g.DrawRectangle(pen, bounds);
            
            methodRandom();
            RecPart part = new RecPart();
            part.X = ball3.X;
            part.Y = ball3.Y;
            parts.Add(part);



            

            ball3.Move(e);

            
            
            ball3.Draw(brush2, g);




            CheckCollisons();
            graphics.DrawImageUnscaled(doubleBuffer, 0, 0);
            //check collisions
            
            
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
                        tbMaxPixel.Clear();
                        brojIzminati = 0;
                        tbPixel.Clear();
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
                        tbMaxPixel.Clear();
                        brojIzminati = 0;
                        tbPixel.Clear();
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
                        tbMaxPixel.Clear();
                        brojIzminati = 0;
                        tbPixel.Clear();
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
                    tbMaxPixel.Clear();
                    brojIzminati = 0;
                    tbPixel.Clear();
                    this.Hide();
                    var form2 = new GameOver();
                    form2.Closed += (s, args) => this.Close();
                    form2.Show();
                    
                }
            }
        }
        private static bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
        }
        Bitmap FloodFill(Bitmap bmp, Bitmap source, Point pt, Color targetColor, Color replacementColor, int tmp, int broj)
        {

            //pts = new List<Point>();
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(pt);
            while (q.Count > 0)
            {
                Point n = q.Dequeue();
                if (!ColorMatch(bmp.GetPixel(n.X, n.Y), targetColor))
                    continue;
                Point w = n, e = new Point(n.X + 1, n.Y);
                while ((w.X >= 0) && ColorMatch(bmp.GetPixel(w.X, w.Y), targetColor))
                {
                    //pts.Add(new Point(w.X, w.Y));
                    if (ColorMatch(bmp.GetPixel(w.X, w.Y), Color.White))
                    {
                        tmp++;
                    }
                    //bmp.SetPixel(w.X, w.Y, replacementColor);
                    bmp.SetPixel(w.X, w.Y, source.GetPixel(w.X, w.Y));
                    

                    if ((w.Y > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y - 1), targetColor))
                    {
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    }
                    if ((w.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(w.X, w.Y + 1), targetColor))
                    {
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    }
                    w.X--;
                }
                while ((e.X <= bmp.Width - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y), targetColor))
                {
                    if (ColorMatch(bmp.GetPixel(e.X, e.Y), Color.White))
                    {
                        tmp++;
                    }
                    //bmp.SetPixel(e.X, e.Y, replacementColor);
                    bmp.SetPixel(e.X, e.Y, source.GetPixel(e.X, e.Y));
                    //pts.Add(new Point(e.X, e.Y));
                    
                    if ((e.Y > 0) && ColorMatch(bmp.GetPixel(e.X, e.Y - 1), targetColor))
                    {
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    }
                    if ((e.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y + 1), targetColor))
                    {
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    }
                    e.X++;
                }
            }
            brojIzminati += tmp;
            tbPixel.Text = brojIzminati.ToString();
            if (Convert.ToInt32(tbPixel.Text) >= broj)
            {
                this.Hide();
                brojIzminati = 0;
                tmp = 0;
                tbMaxPixel.Clear();
                tbPixel.Clear();
                var form2 = new Win();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
            tmp = 0;
            return bmp;
            //return points;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tbMaxPixel_TextChanged(object sender, EventArgs e)
        {

        }
    }
}