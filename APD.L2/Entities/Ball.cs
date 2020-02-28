using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace APD.L2.Entities
{
    public class Ball
    {
        private readonly Control parent;
        private readonly Thread ballThread;

        public int Px { get; set; }
        public int Py { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int RefreshRateInMilliseconds { get; set; }

        public Ball(Control parent)
        {
            this.parent = parent;

            RefreshRateInMilliseconds = 20;

            ballThread = new Thread(() => Run(SpeedX, SpeedY));
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
            var speedy = -30;
            var speedx = 0;

            while (true)
            {
                speedy += gravy;
                Py += speedy;
                Px += speedx;

                parent.Refresh();

                if (Py > parent.Height - Size)
                {
                    speedy = speed;
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
                Px += speedX;
                Py += speedY;

                if (Px <= 0 || Px >= parent.Width - Size)
                {
                    speedX *= -1;
                }

                if (Py <= 0 || Py >= parent.Height - Size)
                {
                    speedY *= -1;
                }

                parent.Refresh();

                Thread.Sleep(RefreshRateInMilliseconds);
            }
        }
    }

}