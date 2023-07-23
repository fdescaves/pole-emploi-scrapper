using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PoleEmploi.Api.Models.Responses;
using PoleEmploiScrapper.Application.Mappers;
using PoleEmploiScrapper.Application.Repositories;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Application.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly ILogger<JobOfferService> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public JobOfferService(ILogger<JobOfferService> logger, IJobOfferRepository jobOfferRepository)
        {
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task AddOrUpdate(JobOfferDto jobOfferDto)
        {
            JobOffer jobOffer = JobOfferMapper.Map(jobOfferDto);
            await _jobOfferRepository.AddOrUpdate(jobOffer);
        }
    }
}
