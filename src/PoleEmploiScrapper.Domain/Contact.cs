namespace PoleEmploiScrapper.Domain
{
    public class Contact
    {
        public Contact()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string TelephoneNumber { get; set; }

        public string Email { get; set; }

        public string ApplyUrl { get; set; }
    }
}
