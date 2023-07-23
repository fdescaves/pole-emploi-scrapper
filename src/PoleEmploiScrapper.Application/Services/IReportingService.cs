using System.Threading.Tasks;
using PoleEmploiScrapper.Application.Models;

namespace PoleEmploiScrapper.Application.Services
{
    public interface IReportingService
    {
        /// <summary>
        /// Get statistics on offers
        /// </summary>
        /// <returns></returns>
        public Task<JobOfferStatisticsDto> GetStatistics();
    }
}
