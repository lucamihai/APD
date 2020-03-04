using System.Collections.Generic;
using System.Linq;
using APD.L2.Entities;
using APD.L2.Interfaces;

namespace APD.L2.AdsOrderProviders
{
    public class AdsOrderProviderV1 : IAdsOrderProvider
    {
        public AdInformation[] GetAdsToDisplayInOrder(List<AdInformation> ads)
        {
            var sumPriorities = ads.Sum(x => x.Priority);
            var adsToDisplayInOrder = new AdInformation[sumPriorities];
            var adsOrderedByPriority = ads.OrderBy(x => x.Priority).ToList();

            for (int index = 0; index < adsToDisplayInOrder.Length; index++)
            {
                adsToDisplayInOrder[index] = null;
            }

            // Ads are ordered ascending by Priority
            for (int index = adsOrderedByPriority.Count - 1; index >= 0; index--)
            {
                var currentPriority = adsOrderedByPriority[index].Priority;
                var currentProbability = (double)sumPriorities / currentPriority;

                for (int i = 0; i < currentPriority; i++)
                {
                    var adIndex = Round(i * currentProbability);

                    while (adsToDisplayInOrder[adIndex] != null)
                    {
                        adIndex++;
                    }

                    adsToDisplayInOrder[adIndex] = adsOrderedByPriority[index];
                }
            }

            return adsToDisplayInOrder;
        }

        private int Round(double number)
        {
            var floatingValue = number - (int)number;

            if (floatingValue < 0.5)
            {
                return (int)number;
            }
            else
            {
                return (int)(number + 1);
            }
        }
    }
}