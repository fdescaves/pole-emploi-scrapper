using System.Collections.Generic;

namespace PoleEmploiScrapper.Domain
{
    public class JobOfferStatistics
    {
        public int jobOffersCount { get; set; }

        public int CompaniesCount { get; set; }

        public int JobOffersInFrance { get; set; }

        public Dictionary<string, int> OffersByCompany { get; set; }
    }
}
