using System.Drawing;
using System.Threading;

namespace APD.L1
{
    public class Ball
    {
        private readonly MainForm parent;
        private readonly Thread ballThread;

        public int Px { get; private set; }
        public int Py { get; private set; }
        public int Size { get; private set; }
        public Color Color { get; private set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int RefreshRateInMilliseconds { get; set; }

        public Ball(MainForm parent, int px, int py, int size, Color color)
        {
            this.parent = parent;
            Px = px;
            Py = py;
            Size = size;
            Color = color;

            RefreshRateInMilliseconds = 20;

            ballThread = new Thread(new ThreadStart(() => Run(SpeedX, SpeedY)));
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

                if (Py > parent.LabelBallSize.Height - Size)
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

                if (Px <= 0 || Px >= parent.LabelBallSize.Width - Size)
                {
                    speedX *= -1;
                }

                if (Py <= 0 || Py >= parent.LabelBallSize.Height - Size)
                {
                    speedY *= -1;
                }

                parent.Refresh();

                Thread.Sleep(RefreshRateInMilliseconds);
            }
        }
    }

}