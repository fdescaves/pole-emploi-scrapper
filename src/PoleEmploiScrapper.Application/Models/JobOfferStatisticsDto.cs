using System.Collections.Generic;

namespace PoleEmploiScrapper.Application.Models
{
    public class JobOfferStatisticsDto
    {
        public int TotalCount { get; set; }

        public int CompaniesCount { get; set; }

        public int JobOffersInFrance { get; set; }

        public Dictionary<string, int> OffersByCompany { get; set; }
    }
}
