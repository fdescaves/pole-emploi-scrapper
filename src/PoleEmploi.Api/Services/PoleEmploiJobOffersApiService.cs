using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using PoleEmploi.Api.Models;
using PoleEmploi.Api.Models.Responses;
using PoleEmploiScrapper.Configuration;

namespace PoleEmploi.Api.Services
{
    public class PoleEmploiJobOffersApiService
    {
        private readonly HttpClient _httpClient;
        private readonly PoleEmploiApiOptions _poleEmploiOptions;

        public PoleEmploiJobOffersApiService(HttpClient httpClient, IOptions<PoleEmploiApiOptions> poleEmploiApiOptions)
        {
            _httpClient = httpClient;
            _poleEmploiOptions = poleEmploiApiOptions.Value;
            _httpClient.BaseAddress = new Uri(_poleEmploiOptions.JobOffersApiUrl);
        }

        public async Task<SearchJobOffersResult> SearchJobOffers(string accessToken, SearchJobOffersFilters searchJobOffersFilters)
        {
            string url = "search";

            url = QueryHelpers.AddQueryString(url, new Dictionary<string, string>
            {
                ["range"] = $"{searchJobOffersFilters.Index}-{searchJobOffersFilters.Index + searchJobOffersFilters.PageSize - 1}",
            });

            if (searchJobOffersFilters.Municipalities != null && searchJobOffersFilters.Municipalities.Length > 0)
            {
                url = QueryHelpers.AddQueryString(url, new Dictionary<string, string>
                {
                    ["commune"] = string.Join(",", searchJobOffersFilters.Municipalities),
                    ["distance"] = searchJobOffersFilters.DistanceFromMunicipalities.ToString(),
                });
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Authorization", $"Bearer {accessToken}");

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(request);

            httpResponse.EnsureSuccessStatusCode();

            SearchJobOffersResponse response = await httpResponse.Content.ReadFromJsonAsync<SearchJobOffersResponse>();

            return new SearchJobOffersResult
            {
                JobOffers = response.Results,
                HasMoreResults = httpResponse.StatusCode == HttpStatusCode.PartialContent
            };
        }
    }
}

