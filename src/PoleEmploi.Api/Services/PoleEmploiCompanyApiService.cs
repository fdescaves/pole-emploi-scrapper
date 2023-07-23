using System;
using System.Collections.Generic;
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
    public class PoleEmploiCompanyApiService
    {
        private readonly HttpClient _httpClient;
        private readonly PoleEmploiApiOptions _poleEmploiOptions;
        private readonly string _requiredScopes = "api_offresdemploiv2 o2dsoffre";

        public PoleEmploiCompanyApiService(HttpClient httpClient, IOptions<PoleEmploiApiOptions> poleEmploiApiOptions)
        {
            _httpClient = httpClient;
            _poleEmploiOptions = poleEmploiApiOptions.Value;
            _httpClient.BaseAddress = new Uri(_poleEmploiOptions.CompanyApiUrl);
        }

        public async Task<PoleEmploiAccessToken> GetAccessToken()
        {
            string url = QueryHelpers.AddQueryString("connexion/oauth2/access_token", "realm", "/partenaire");

            IList<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>
            {
                { new ("grant_type", "client_credentials") },
                { new ("client_id", _poleEmploiOptions.ClientId) },
                { new ("client_secret", _poleEmploiOptions.ClientSecret) },
                { new ("scope", _requiredScopes) },
            };

            HttpResponseMessage httpResponse = await _httpClient.PostAsync(url, new FormUrlEncodedContent(formData));

            httpResponse.EnsureSuccessStatusCode();

            GetAccessTokenResponse response = await httpResponse.Content.ReadFromJsonAsync<GetAccessTokenResponse>();

            return new PoleEmploiAccessToken
            {
                AccessToken = response.AccessToken,
                ExpiresIn = response.ExpiresIn,
            };
        }
    }
}
