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
        int counter = 0;
        bool flag2 = false;
        List<RecPart> parts = new List<RecPart>();
        List<Ball> balls = new List<Ball>();
        Ball ball;
        Ball ball2;
        Ball ball3;
        String tempString = "";
        Graphics graphics;
        Brush brush;
        Brush brush2;
        Pen pen;
        Rectangle bounds;
        Bitmap doubleBuffer;
        Graphics g;
        Bitmap source;
        Bitmap bitmap;
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
            bounds = new Rectangle(10, 10, this.Bounds.Width-40, this.Bounds.Height - 60);
            doubleBuffer = new Bitmap(Width, Height);
            graphics = CreateGraphics();
          
            ball = new Ball(Width / 2, Height / 2, 10, 10, (float)(Math.PI / 4));
            ball2 = new Ball(50, 50, 10, 10, (float)(Math.PI / 2));
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
                g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                if (i!=0 )
                {
                    
                    if (ball3.Y == bounds.Top || ball3.Y == bounds.Bottom || ball3.X == bounds.Left || ball3.X == bounds.Right)
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
                    
                    
                    
                    //label1.Text += parts[i].X + parts[i].Y.ToString();
                }
                
                //
                /*if (i <5)
                {
                    label1.Text = "VLAGA LI TUKA???" + i;
                    if (ball3.Y == bounds.Top || ball3.Y  == bounds.Bottom || ball3.X - 3 == bounds.Left || ball3.X - 3 == bounds.Right)
                    {
                        g.FillRectangle(brush2, ball3.X, ball3.Y, 5, 5);
                    }
                }*/
                /*if (ball3.Y == bounds.Top || ball3.Y == bounds.Bottom || ball3.X == bounds.Left || ball3.X == bounds.Right)
                {
                    if (i > 1)
                    {
                        parts.Clear();
                        break;
                    }
                }*/
                /* if (parts[i].Y == bounds.Top || parts[i].Y == bounds.Bottom || parts[i].X == bounds.Left || parts[i].X == bounds.Right)
                 {
                     label1.Text = "VLAGA LI TUKA???0";
                     if (i < 1 )
                     {
                         g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                     }
                     if (i != parts.Count - 1)
                     {
                         parts.Clear();
                         break;
                     }
                     if (i == parts.Count - 1)
                     {
                         g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                     }
                 }
                 if (parts[i].Y > bounds.Top && parts[i].Y < bounds.Bottom && parts[i].X > bounds.Left && parts[i].Y < bounds.Right)
                 {

                     if (ColorMatch(doubleBuffer.GetPixel(Convert.ToInt16(parts[i].X), Convert.ToInt16(parts[i].Y)), Color.White))
                     {
                         g.FillRectangle(brush2, parts[i].X, parts[i].Y, 5, 5);
                         //label1.Text = ball3.X.ToString() + doubleBuffer.GetPixel(Convert.ToInt16(ball3.X), Convert.ToInt16(ball3.Y));

                     }

                 }*/



            }
            if (flag2)
            {//problemot e ja brishe poslednata nacrtana linija i zatoa ne raboti kako sho treba no nz sho da praam
             /*while(true)
             {
                 int first = Convert.ToInt16(parts[parts.Count - 1].X) ;
                 int second = Convert.ToInt16(parts[parts.Count - 1].Y);
                 //first += 5;
                 second += 5;
                 if (!ColorMatch(doubleBuffer.GetPixel(first,second),Color.Green))
                 {
                     g.FillRectangle(brush2, first, second, 5, 5);
                 }
                 else if(ColorMatch(doubleBuffer.GetPixel(first, second), Color.Green))
                 {
                     break;
                 }
             } posho ostava prazni rectangli se obidov vaka da go resham ama ni vaka nejkje pu picka mu mater) */
                label2.Text = min1 + 5 + " MINIMUM " + min2 + 5;
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
                bitmap = FloodFill(doubleBuffer, source, new Point(Convert.ToInt16(parts[1].X), Convert.ToInt16(parts[1].Y)), Color.Green, Color.Black);//ne se koristi black
                while (true)
                {
                    floatX = random.Next(Convert.ToInt32(minX), Convert.ToInt32(maxX));
                    floatY = random.Next(Convert.ToInt32(minY), Convert.ToInt32(maxY));

                    if (ColorMatch(doubleBuffer.GetPixel(Convert.ToInt32(floatX), Convert.ToInt32(floatY)), Color.White))
                    {
                        bitmap = FloodFill(doubleBuffer, source, new Point(Convert.ToInt32(floatX), Convert.ToInt32(floatY)), Color.White, Color.Black);
                        break;
                    }
                }
                

                bitmap = new Bitmap(doubleBuffer);

                flag2 = false;
                parts.Clear();
            }
            if (bitmap != null)
            {
                g.DrawImage(bitmap, 0, 0);
                String label = "";
                foreach (RecPart r in parts)
                {
                    label += r.X + ", " + r.Y + " ";
                }
                label1.Text = label;
                //label2.Text = "";
                //label2.Text = tempString;
                //bitmap = null;
            }
            
            
            
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



            /*if (ColorMatch(doubleBuffer.GetPixel(Convert.ToInt16(ball3.X + ball3.Radius + 1), Convert.ToInt16(ball3.Y + 1)), Color.Red))
            {
                if (parts[0].X != ball3.X && parts[0].Y != ball3.Y)
                {
                    bitmap = FloodFill(doubleBuffer, source, new Point(100, 100), Color.White, Color.Black);
                    bitmap = new Bitmap(doubleBuffer);
                    g.DrawImage(bitmap, 0, 0);
                }

                /*RecPart part = new RecPart();
                part.X = ball3.X;
                part.Y = ball3.Y;
                parts.Add(part);
            }*/

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
        private static bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
        }
        Bitmap FloodFill(Bitmap bmp, Bitmap source, Point pt, Color targetColor, Color replacementColor)
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

                    //bmp.SetPixel(w.X, w.Y, replacementColor);
                    bmp.SetPixel(w.X, w.Y, source.GetPixel(w.X, w.Y));


                    if ((w.Y > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y - 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    if ((w.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(w.X, w.Y + 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    w.X--;
                }
                while ((e.X <= bmp.Width - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y), targetColor))
                {
                    //bmp.SetPixel(e.X, e.Y, replacementColor);
                    bmp.SetPixel(e.X, e.Y, source.GetPixel(e.X, e.Y));
                    //pts.Add(new Point(e.X, e.Y));
                    if ((e.Y > 0) && ColorMatch(bmp.GetPixel(e.X, e.Y - 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    if ((e.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y + 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    e.X++;
                }
            }
            return bmp;
            //return points;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}