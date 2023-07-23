using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PoleEmploiScrapper.Domain;

namespace PoleEmploiScrapper.Infrastructure
{
    public class PoleEmploiScrapperDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        //public PoleEmploiScrapperDbContext(DbContextOptions<PoleEmploiScrapperDbContext> options) : base(options)
        //{
        //}

        public PoleEmploiScrapperDbContext(DbContextOptions<PoleEmploiScrapperDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("PoleEmploiScrapper");
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
