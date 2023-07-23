using System.Collections.Generic;
using PoleEmploi.Api.Models.Responses;

namespace PoleEmploi.Api.Models
{
    public class SearchJobOffersResult
    {
        public IEnumerable<JobOfferDto> JobOffers { get; set; }

        public bool HasMoreResults { get; set; }
    }
}
