using PoleEmploiScrapper.Application.Models;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Application.Mappers
{
    public static class JobOfferStatisticsMapper
    {
        public static JobOfferStatisticsDto Map(JobOfferStatistics statistics)
        {
            return new JobOfferStatisticsDto
            {
                TotalCount = statistics.jobOffersCount,
                CompaniesCount = statistics.CompaniesCount,
                JobOffersInFrance = statistics.JobOffersInFrance,
                OffersByCompany = statistics.OffersByCompany
            };
        }
    }
}
