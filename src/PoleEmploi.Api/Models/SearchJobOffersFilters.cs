namespace PoleEmploi.Api.Models
{
    public class SearchJobOffersFilters
    {
        public int Index { get; set; }

        public int PageSize { get; set; }

        public string[] Municipalities { get; set; }

        public int DistanceFromMunicipalities { get; set; }
    }
}
