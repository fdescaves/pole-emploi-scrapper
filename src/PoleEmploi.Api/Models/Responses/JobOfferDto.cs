using System;
using System.Text.Json.Serialization;

namespace PoleEmploi.Api.Models.Responses
{
    public class JobOfferDto
    {
        public JobOfferDto()
        {
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("intitule")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("dateCreation")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("dateActualisation")]
        public DateTime LatestModificationDate { get; set; }

        [JsonPropertyName("lieuTravail")]
        public LocationDto Location { get; set; }

        [JsonPropertyName("romeCode")]
        public string RomeCode { get; set; }

        [JsonPropertyName("entreprise")]
        public CompanyDto Company { get; set; }

        [JsonPropertyName("typeContrat")]
        public string ContractType { get; set; }

        [JsonPropertyName("contact")]
        public ContactDto Contact { get; set; }
    }
}
