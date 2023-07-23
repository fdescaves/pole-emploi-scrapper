using System.Text.Json.Serialization;

namespace PoleEmploi.Api.Models.Responses
{
    public class ContactDto
    {
        public ContactDto()
        {
        }

        [JsonPropertyName("nom")]
        public string Name { get; set; }

        [JsonPropertyName("telephone")]
        public string TelephoneNumber { get; set; }

        [JsonPropertyName("courriel")]
        public string Email { get; set; }

        [JsonPropertyName("urlPostulation")]
        public string ApplyUrl { get; set; }
    }
}
