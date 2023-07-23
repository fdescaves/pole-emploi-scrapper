using System;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using PoleEmploiScrapper.Application.Jobs;

namespace PoleEmploiScrapper.Api
{
    public static class WebApplicationExtensions
    {
        public static void ScheduleBackgroundJobs(this WebApplication app)
        {
            app.Services.UseScheduler(scheduler =>
            {
                scheduler.Schedule<ScrapPoleEmploJobOffersJob>()
                .EveryThirtySeconds()
                .Zoned(TimeZoneInfo.Local)
                .RunOnceAtStart()
                .PreventOverlapping(nameof(ScrapPoleEmploJobOffersJob));
            })
            .OnError((exception) =>
            {
                app.Logger.LogCritical(exception, "Critical error occured");
                app.StopAsync();
            });
        }
    }
}
