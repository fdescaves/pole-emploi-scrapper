using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PoleEmploiScrapper.Application.Mappers;
using PoleEmploiScrapper.Application.Models;
using PoleEmploiScrapper.Application.Repositories;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Application.Services
{
    public class ReportingService : IReportingService
    {
        private readonly ILogger<ReportingService> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public ReportingService(ILogger<ReportingService> logger, IJobOfferRepository jobOfferRepository)
        {
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<JobOfferStatisticsDto> GetStatistics()
        {
            JobOfferStatistics statistics = await _jobOfferRepository.GetStatistics();

            return JobOfferStatisticsMapper.Map(statistics);
        }
    }
}
