using Microsoft.Extensions.DependencyInjection;
using PoleEmploi.Api.Services;

namespace PoleEmploi.Api
{
    public static class PoleEmploiApiExtensions
    {
        public static IServiceCollection AddPoleEmploiHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<PoleEmploiCompanyApiService>();
            services.AddHttpClient<PoleEmploiJobOffersApiService>();

            return services;
        }
    }
}
