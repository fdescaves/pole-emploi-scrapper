using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoleEmploiScrapper.Application.Repositories;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Infrastructure.Repositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly ILogger<JobOfferRepository> _logger;
        private readonly PoleEmploiScrapperDbContext _dbContext;

        public JobOfferRepository(ILogger<JobOfferRepository> logger, PoleEmploiScrapperDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<JobOffer> GetByPoleEmploiId(string poleEmploiId)
        {
            JobOffer result = await _dbContext.JobOffers
                .Include(x => x.Company)
                .Include(x => x.Location)
                .Include(x => x.Contact)
                .Where(x => x.PoleEmploiId == poleEmploiId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task AddOrUpdate(JobOffer jobOffer)
        {
            _logger.LogInformation("Add or update job offer with id {id}", jobOffer.Id);

            if (jobOffer is null)
            {
                throw new ArgumentNullException(nameof(jobOffer));
            }

            JobOffer existingJobOffer = await GetByPoleEmploiId(jobOffer.PoleEmploiId);
            if (existingJobOffer is null)
            {
                _logger.LogInformation("Job offer with id {id} will be added", jobOffer.Id);

                _dbContext.JobOffers.Add(jobOffer);
            }
            else
            {
                _logger.LogInformation("Job offer with id {id} will be updated", jobOffer.Id);
                jobOffer.Id = existingJobOffer.Id;
                jobOffer.Company.Id = existingJobOffer.Company.Id;
                jobOffer.Location.Id = existingJobOffer.Location.Id;
                jobOffer.Contact.Id = existingJobOffer.Contact.Id;

                _dbContext.Entry(jobOffer).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<JobOfferStatistics> GetStatistics()
        {
            int jobOffersCount = await _dbContext.JobOffers.CountAsync();

            Dictionary<string, int> offersByCompany = await _dbContext.JobOffers
                    .Where(x => !string.IsNullOrEmpty(x.Company.Name))
                    .GroupBy(x => x.Company.Name)
                    .ToDictionaryAsync(x => x.Key, x => x.Count());

            int companiesCount = await _dbContext.Companies
                .GroupBy(x => x.Name)
                .CountAsync();

            int jobOffersInFrance = await _dbContext.JobOffers
                .Where(x => !string.IsNullOrEmpty(x.Location.INSEE))
                .CountAsync();

            var statistics = new JobOfferStatistics
            {
                jobOffersCount = jobOffersCount,
                CompaniesCount = companiesCount,
                JobOffersInFrance = jobOffersInFrance,
                OffersByCompany = offersByCompany
            };

            return statistics;
        }
    }
}
