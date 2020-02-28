using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using APD.L1.Entities;

namespace APD.L1
{
    public class AdManager
    {
        private readonly Control adContainer;
        private readonly Thread adDisplayThread;
        private readonly Random rng;

        private AdInformation currentAd;

        public List<AdInformation> AdInformationList { get; private set; }

        public AdManager(Control adContainer)
        {
            this.adContainer = adContainer;
            adContainer.BackgroundImageLayout = ImageLayout.Stretch;

            adDisplayThread = new Thread(DisplayAds);
            rng = new Random();

            AdInformationList = new List<AdInformation>();
        }

        public void StartDisplayingAds()
        {
            AdInformationList = AdInformationList
                .OrderBy(x => x.Priority)
                .ToList();

            adContainer.Click += AdContainer_Click;

            adDisplayThread.Start();
        }

        private void AdContainer_Click(object sender, EventArgs e)
        {
            if (currentAd == null)
            {
                return;
            }

            if (!currentAd.RedirectTo.Trim().StartsWith("http://") && !currentAd.RedirectTo.Trim().StartsWith("https://"))
            {
                return;
            }

            System.Diagnostics.Process.Start(currentAd.RedirectTo.Trim());
        }

        public void StopDisplayingAds()
        {
            adDisplayThread.Abort();
        }

        private void DisplayAds()
        {
            while (true)
            {
                foreach (var adInformation in AdInformationList)
                {
                    var shouldDisplayCurrentAd = rng.Next(0, 2) == 1;

                    if (shouldDisplayCurrentAd)
                    {
                        currentAd = adInformation;
                        DisplayCurrentAd(5000);
                        break;
                    }
                }
            }
        }

        private void DisplayCurrentAd(int displayPeriodInMilliseconds)
        {
            adContainer.BackgroundImage = Image.FromFile(currentAd.ImageFilePath);

            Thread.Sleep(displayPeriodInMilliseconds);
        }
    }
}