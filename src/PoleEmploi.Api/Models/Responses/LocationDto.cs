using System.Text.Json.Serialization;

namespace PoleEmploi.Api.Models.Responses
{
    public class LocationDto
    {
        public LocationDto()
        {
        }

        [JsonPropertyName("libelle")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }

        [JsonPropertyName("commune")]
        public string INSEE { get; set; }
    }
}
