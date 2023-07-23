using Microsoft.Extensions.DependencyInjection;
using PoleEmploiScrapper.Application.Repositories;
using PoleEmploiScrapper.Infrastructure.Repositories;

namespace PoleEmploiScrapper.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<PoleEmploiScrapperDbContext>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IJobOfferRepository, JobOfferRepository>();

            return services;
        }
    }
}
