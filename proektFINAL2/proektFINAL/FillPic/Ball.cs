using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillPic
{
    class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float Radius { get; set; }

        public float Velocity { get; set; }
        public float Angle { get; set; }


        public Rectangle Bounds;

        private float velocityX;
        private float velocityY;
        private Graphics g;
        private Brush brush;


        public Ball(float x, float y, float radius, float velocity, float angle)
        {
            X = x;
            Y = y;
            Radius = radius;
            Velocity = velocity;
            Angle = angle;

            velocityX = (float)Math.Cos(Angle) * Velocity;
            velocityY = (float)Math.Sin(Angle) * Velocity;
        }
        public void Move(KeyEventArgs e)
        {
            float nextY = Y;
            float nextX = X;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Y -= 5;
                    if (Y < 10) {
                        Y = nextY;
                        break;
                    }
                    else{
                        break;
                    }
                        
                case Keys.Down:
                    Y += 5;
                    if (Y > 585)
                    {
                        Y = nextY;
                        break;
                    }
                    else if (Y == 585)
                    {
                        Y = 585;
                        break;
                    }
                    else
                    {
                        //g.FillRectangle(brush, X-10, Y-10, 50, 50);
                        break;
                    }
                case Keys.Left:
                    X -= 5;
                    if (X < 10)
                    {
                        X = nextX;
                        break;
                    }
                    else
                    {
                        break;
                    }
                case Keys.Right:
                    X += 5;
                    if (X > 785)
                    {
                        X = nextX;
                        break;
                    }
                    else if(X==785)
                    {
                        X = 785;
                        break;
                    }
                    else
                    {
                        //g.FillRectangle(brush, X-10, Y-10, 50, 50);
                        break;
                    }
                    /*case Keys.Up:
                        nextY = Y - velocityY;
                        nextX = X + 0;
                        break;
                    case Keys.Down:
                        nextY = Y + velocityY;
                        nextX = X + 0;
                        break;
                    case Keys.Left:
                        nextX = X - velocityX;
                        nextY = Y + 0;
                        break;
                    case Keys.Right:
                        nextX = X + velocityX;
                        nextY = Y + 0;
                        break;
                        */
            }
            
            //X = nextX;
            //Y = nextY;
        }
        public void Move()
        {
            float nextX = X + velocityX;
            float nextY = Y + velocityY;
            if (nextX - Radius <= Bounds.Left || (nextX + Radius >= Bounds.Right))
            {
                velocityX = -velocityX;
            }
            if (nextY - Radius <= Bounds.Top || (nextY + Radius >= Bounds.Bottom))
            {
                velocityY = -velocityY;
            }
            X += velocityX;
            Y += velocityY;
        }
        public void Draw(Brush brush, Graphics g)
        {
            this.g = g;
            this.brush = brush;
            g.FillEllipse(brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }
        public bool IsColiding(Ball ball)
        {
            double d = (X - ball.X) * (X - ball.X) + (Y - ball.Y) * (Y - ball.Y);
            return d <= (2 * Radius) * (2 * Radius);
        }
    }
}
