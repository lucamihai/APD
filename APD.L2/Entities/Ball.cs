using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace APD.L2.Entities
{
    public class Ball
    {
        private readonly Control parent;
        private readonly Thread ballThread;

        public Point Position { get; set; }
        public Point Speed { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; }
        
        public int RefreshRateInMilliseconds { get; set; }

        public Ball(Control parent)
        {
            this.parent = parent;

            RefreshRateInMilliseconds = 20;

            ballThread = new Thread(() => Run(Speed.X, Speed.Y));
            ballThread.Start();
        }

        public void TerminateBallThread()
        {
            ballThread.Abort();
        }

        public void Run()
        {
            var gravy = 1;
            var speed = -30;
            var speedY = -30;
            var speedX = 0;

            while (true)
            {
                speedY += gravy;
                Position = new Point
                {
                    X = Position.X + speedX,
                    Y = Position.Y + speedY
                };

                parent.Refresh();

                if (Position.Y > parent.Height - Size)
                {
                    speedY = speed;
                    speed += 3;
                }

                if (speed == 0)
                {
                    break;
                }

                Thread.Sleep(RefreshRateInMilliseconds);
            }
        }

        public void Run(int speedX, int speedY)
        {
            while (true)
            {
                Position = new Point
                {
                    X = Position.X + speedX,
                    Y = Position.Y + speedY
                };

                if (Position.X <= 0 || Position.X >= parent.Width - Size)
                {
                    speedX *= -1;
                }

                if (Position.Y <= 0 || Position.Y >= parent.Height - Size)
                {
                    speedY *= -1;
                }

                parent.Refresh();

                Thread.Sleep(RefreshRateInMilliseconds);
            }
        }
    }
}