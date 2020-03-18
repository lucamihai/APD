using System.Collections.Generic;
using APD.L2.Entities;

namespace APD.L2.Interfaces
{
    public interface IAdsOrderProvider
    {
        AdInformation[] GetAdsToDisplayInOrder(List<AdInformation> ads);
    }
}