using System;
using System.IO;
using System.Reflection;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PoleEmploi.Api;
using PoleEmploiScrapper.Application;
using PoleEmploiScrapper.Configuration;
using PoleEmploiScrapper.Infrastructure;

namespace PoleEmploiScrapper.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.ScheduleBackgroundJobs();
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            ConfigurationManager configuration = builder.Configuration;

            services.AddControllers();

            builder.Services.AddControllers();

            builder.Services
                .AddScheduler()
                .AddMemoryCache()
                .AddLogging()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Pole Emploi Scrapper Api",
                        Description = "Api for scrapping pole emploi job offers and get basic statistics"
                    });

                    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                })
                .Configure<PoleEmploiApiOptions>(configuration.GetSection(nameof(PoleEmploiApiOptions)))
                .AddPoleEmploiHttpClients()
                .AddPersistence()
                .AddApplicationServices()
                .AddRepositories();
        }
    }
}
