using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using APD.L2.AdsOrderProviders;
using APD.L2.Entities;

namespace APD.L2
{
    public partial class MainForm : Form
    {
        private List<Ball> balls;
        private AdManager adManager;
        public Size LabelBallSize { get; set; }

        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.BackColor = Color.White;

            InitializeBalls();
            InitializeAdManager();

            adManager.StartDisplayingAds();
        }

        private void InitializeBalls()
        {
            balls = new List<Ball>
            {
                new Ball(labelBalls)
                {
                    Position = new Point(41, 50),
                    Speed = new Point(5, 10),
                    Size = 25,
                    Color = Color.Blue,
                },
                new Ball(labelBalls)
                {
                    Position = new Point(11, 100),
                    Size = 40,
                    Color = Color.Yellow,
                    Speed = new Point(6, 9)
                },
                new Ball(labelBalls)
                {
                    Position = new Point(200, 13),
                    Size = 50,
                    Color = Color.Red,
                    Speed = new Point(7, 2)
                },
            };
        }

        private void InitializeAdManager()
        {
            adManager = new AdManager(labelAds, new AdsOrderProviderV1());

            var csvLines = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}\\AdInformation.csv");

            for (int index = 1; index < csvLines.Length; index++)
            {
                var csvLine = csvLines[index];
                var splitValues = csvLine.Split(',');

                adManager.AdInformationList.Add(new AdInformation
                {
                    ImageFilePath = splitValues[0],
                    RedirectTo = splitValues[1],
                    Priority = Convert.ToInt32(splitValues[2])
                });
            }
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            LabelBallSize = labelBalls.Size;
            this.Name = "Balls";
        }

        private void MyForm_Paint(object sender, PaintEventArgs e)
        {
            for (int index = 0; index < balls.Count; index++)
            {
                var currentBall = balls[index];

                using (var brush = new SolidBrush(currentBall.Color))
                {
                    e.Graphics.FillEllipse(brush, currentBall.Position.X, currentBall.Position.Y, currentBall.Size, currentBall.Size);
                }
            }
        }

        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int index = 0; index < balls.Count; index++)
            {
                balls[index].TerminateBallThread();
            }

            adManager.StopDisplayingAds();
        }
    }
}
