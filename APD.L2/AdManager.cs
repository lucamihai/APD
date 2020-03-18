using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using APD.L2.Entities;
using APD.L2.Interfaces;

namespace APD.L2
{
    public class AdManager
    {
        private readonly Control adContainer;
        private readonly Thread adDisplayThread;
        private readonly IAdsOrderProvider adsOrderProvider;

        private AdInformation currentAd;

        public List<AdInformation> AdInformationList { get; private set; }

        public AdManager(Control adContainer, IAdsOrderProvider adsOrderProvider)
        {
            this.adContainer = adContainer;
            adContainer.BackgroundImageLayout = ImageLayout.Stretch;

            adDisplayThread = new Thread(DisplayAds);
            this.adsOrderProvider = adsOrderProvider;

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
            var adsToDisplayInOrder = adsOrderProvider.GetAdsToDisplayInOrder(AdInformationList);

            while (true)
            {
                foreach (var adInformation in adsToDisplayInOrder)
                {
                    currentAd = adInformation;
                    DisplayCurrentAd(5000);
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