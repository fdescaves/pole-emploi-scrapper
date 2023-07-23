using System.Threading.Tasks;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Application.Repositories
{
    public interface IJobOfferRepository
    {
        /// <summary>
        /// Get a job offer by poleEmploiId
        /// </summary>
        /// <param name="poleEmploiId"></param>
        /// <returns></returns>
        public Task<JobOffer> GetByPoleEmploiId(string poleEmploiId);

        /// <summary>
        /// Update or update job offer
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <returns></returns>
        public Task AddOrUpdate(JobOffer jobOffer);

        /// <summary>
        /// Get statistics on offers
        /// </summary>
        /// <returns></returns>
        public Task<JobOfferStatistics> GetStatistics();
    }
}
