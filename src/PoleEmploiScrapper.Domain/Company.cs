namespace PoleEmploiScrapper.Domain
{
    public class Company
    {
        public Company()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LogoUrl { get; set; }

        public string WebsiteUrl { get; set; }
    }
}
