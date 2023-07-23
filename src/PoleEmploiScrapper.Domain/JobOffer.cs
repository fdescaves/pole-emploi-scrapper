using System;

namespace PoleEmploiScrapper.Domain
{
    public class JobOffer
    {
        public JobOffer()
        {
        }

        public int Id { get; set; }

        public string PoleEmploiId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LatestModificationDate { get; set; }

        public Location Location { get; set; }

        public string RomeCode { get; set; }

        public Company Company { get; set; }

        public string ContractType { get; set; }

        public Contact Contact { get; set; }
    }
}
