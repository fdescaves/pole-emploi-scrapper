using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PoleEmploi.Api.Models;
using PoleEmploi.Api.Models.Responses;
using PoleEmploi.Api.Services;
using PoleEmploiScrapper.Application.Services;

namespace PoleEmploiScrapper.Application.Jobs
{
    public class ScrapPoleEmploJobOffersJob : IInvocable
    {
        private const string ACCESS_TOKEN_CACHE_KEY = "POLE_EMPLOI_ACCESS_TOKEN";
        private const string JOB_OFFER_CURRENT_INDEX_CACHE_KEY = "JOB_OFFER_CURRENT_INDEX";

        private readonly ILogger<ScrapPoleEmploJobOffersJob> _logger;
        private readonly Guid _jobId;
        private readonly IMemoryCache _memoryCache;
        private readonly IJobOfferService _jobOfferService;
        private readonly PoleEmploiCompanyApiService _poleEmploiCompanyApiService;
        private readonly PoleEmploiJobOffersApiService _poleEmploiJobOffersApiService;

        public ScrapPoleEmploJobOffersJob(ILogger<ScrapPoleEmploJobOffersJob> logger,
            IMemoryCache memoryCache,
            IJobOfferService jobOfferService,
            PoleEmploiCompanyApiService poleEmploiCompanyApiService,
            PoleEmploiJobOffersApiService poleEmploiJobOffersApiService)
        {
            _logger = logger;
            _jobId = Guid.NewGuid();
            _memoryCache = memoryCache;
            _jobOfferService = jobOfferService;
            _poleEmploiCompanyApiService = poleEmploiCompanyApiService;
            _poleEmploiJobOffersApiService = poleEmploiJobOffersApiService;
        }

        public async Task Invoke()
        {
            try
            {
                _logger.LogInformation("{jobName} started - {jobId}", nameof(ScrapPoleEmploJobOffersJob), _jobId);

                IEnumerable<JobOfferDto> jobOffers = await GetJobOffers();

                foreach (JobOfferDto jobOfferDto in jobOffers)
                {
                    await _jobOfferService.AddOrUpdate(jobOfferDto);
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, e.Message);
            }
            finally
            {
                _logger.LogInformation("{jobName} ended - {jobId}", nameof(ScrapPoleEmploJobOffersJob), _jobId);
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetJobOffers()
        {
            _logger.LogInformation("Get job offers from pole emploi API");

            string accessToken = await GetAccessToken();
            int pageSize = 150;
            int index = GetCurrentIndex();

            SearchJobOffersResult result = await _poleEmploiJobOffersApiService.SearchJobOffers(accessToken, new SearchJobOffersFilters
            {
                Index = index,
                PageSize = 150,
                Municipalities = new string[] { "35238", "33063" },
                DistanceFromMunicipalities = 0
            });

            int newIndex = result.HasMoreResults ? index + pageSize : 0;

            UpdateCurrentIndex(newIndex);

            return result.JobOffers;
        }

        private async Task<string> GetAccessToken()
        {
            if (!_memoryCache.TryGetValue(ACCESS_TOKEN_CACHE_KEY, out PoleEmploiAccessToken accessToken))
            {
                _logger.LogInformation("Access token is expired, getting a new one from pole emploi API");

                accessToken = await _poleEmploiCompanyApiService.GetAccessToken();
                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(accessToken.ExpiresIn - 60));
                _memoryCache.Set(ACCESS_TOKEN_CACHE_KEY, accessToken, cacheEntryOptions);
            }

            return accessToken.AccessToken;
        }

        private int GetCurrentIndex()
        {
            if (!_memoryCache.TryGetValue(JOB_OFFER_CURRENT_INDEX_CACHE_KEY, out int currentIndex))
            {
                _memoryCache.Set(JOB_OFFER_CURRENT_INDEX_CACHE_KEY, 0);
            }

            return currentIndex;
        }

        private void UpdateCurrentIndex(int newIndex)
        {
            _memoryCache.Set(JOB_OFFER_CURRENT_INDEX_CACHE_KEY, newIndex);
        }
    }
}
