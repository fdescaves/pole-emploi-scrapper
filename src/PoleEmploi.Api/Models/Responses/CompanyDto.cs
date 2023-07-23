using System.Text.Json.Serialization;

namespace PoleEmploi.Api.Models.Responses
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        [JsonPropertyName("nom")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("logo")]
        public string LogoUrl { get; set; }

        [JsonPropertyName("url")]
        public string WebsiteUrl { get; set; }
    }
}
