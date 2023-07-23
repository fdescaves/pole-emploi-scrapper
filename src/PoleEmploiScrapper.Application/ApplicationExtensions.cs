using Microsoft.Extensions.DependencyInjection;
using PoleEmploiScrapper.Application.Jobs;
using PoleEmploiScrapper.Application.Services;

namespace PoleEmploiScrapper.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IJobOfferService, JobOfferService>();
            services.AddTransient<IReportingService, ReportingService>();
            services.AddTransient<ScrapPoleEmploJobOffersJob>();

            return services;
        }
    }
}
