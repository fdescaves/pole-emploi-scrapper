using PoleEmploi.Api.Models.Responses;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Application.Mappers
{
    public static class JobOfferMapper
    {
        public static JobOffer Map(JobOfferDto jobOffer)
        {
            return new JobOffer
            {
                PoleEmploiId = jobOffer.Id,
                Title = jobOffer.Title,
                Description = jobOffer.Description,
                CreationDate = jobOffer.CreationDate,
                LatestModificationDate = jobOffer.LatestModificationDate,
                Location = jobOffer.Location != null ? new Location
                {
                    Name = jobOffer.Location.Name,
                    Latitude = jobOffer.Location.Latitude,
                    Longitude = jobOffer.Location.Longitude,
                    INSEE = jobOffer.Location.INSEE,
                } : null,
                RomeCode = jobOffer.RomeCode,
                Company = jobOffer.Company != null ? new Company
                {
                    Name = jobOffer.Company.Name,
                    Description = jobOffer.Company.Description,
                    LogoUrl = jobOffer.Company.LogoUrl,
                    WebsiteUrl = jobOffer.Company.WebsiteUrl,
                } : null,
                ContractType = jobOffer.ContractType,
                Contact = jobOffer.Contact != null ? new Contact
                {
                    Name = jobOffer.Contact.Name,
                    TelephoneNumber = jobOffer.Contact.TelephoneNumber,
                    Email = jobOffer.Contact.Email,
                    ApplyUrl = jobOffer.Contact.ApplyUrl
                } : null,
            };
        }
    }
}
