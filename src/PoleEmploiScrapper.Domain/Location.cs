namespace PoleEmploiScrapper.Domain
{
    public class Location
    {
        public Location()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string INSEE { get; set; }
    }
}
