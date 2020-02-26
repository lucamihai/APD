using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace APD.L1
{
    public partial class MainForm : Form
    {
        private readonly List<Ball> balls;
        public Size LabelBallSize { get; set; }


        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.BackColor = Color.White;

            balls = new List<Ball>
            {
                new Ball(this, 0, 50, 50, Color.Blue) {SpeedX = 5, SpeedY = 10},
                new Ball(this, 55, 50, 50, Color.Yellow) {SpeedX = 6, SpeedY = 9},
                new Ball(this, 110, 50, 50, Color.Red) {SpeedX = 7, SpeedY = 2},
            };
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            LabelBallSize = labelBalls.Size;
            this.Name = "Balls";
        }

        private void MyForm_Paint(object sender, PaintEventArgs e)
        {
            //using (var brush = new SolidBrush(Color.White))
            //{
            //    e.Graphics.FillRectangle(brush, 0, 0, Size.Width, Size.Height);
            //}

            for (int index = 0; index < balls.Count; index++)
            {
                var currentBall = balls[index];

                using (var brush = new SolidBrush(currentBall.Color))
                {
                    e.Graphics.FillEllipse(brush, currentBall.Px, currentBall.Py, currentBall.Size, currentBall.Size);
                }
            }
        }

        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int index = 0; index < balls.Count; index++)
            {
                balls[index].TerminateBallThread();
            }
        }
    }
}
