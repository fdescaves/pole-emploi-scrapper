using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PoleEmploi.Api.Models.Responses
{
    public class SearchJobOffersResponse
    {
        [JsonPropertyName("resultats")]
        public IEnumerable<JobOfferDto> Results { get; set; }
    }
}
