using System.Threading.Tasks;
using PoleEmploi.Api.Models.Responses;

namespace PoleEmploiScrapper.Application.Services
{
    public interface IJobOfferService
    {
        /// <summary>
        /// Add or update a job offer
        /// </summary>
        /// <param name="jobOfferDto"></param>
        /// <returns></returns>
        public Task AddOrUpdate(JobOfferDto jobOfferDto);
    }
}
