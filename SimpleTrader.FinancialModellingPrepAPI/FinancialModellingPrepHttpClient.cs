using Newtonsoft.Json;
using SimpleTrader.FinancialModellingPrepAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModellingPrepAPI
{
    public class FinancialModellingPrepHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public FinancialModellingPrepHttpClient(HttpClient client, FinancialModellingPrepAPIKey apiKey)
        {
            _httpClient = client;
            _apiKey = apiKey.Key;
            _httpClient.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{uri}?apikey={_apiKey}");
            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(jsonResponse);
            return result;
        }
        public async Task<T> GetFromListAsync<T>(string uri)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{uri}?apikey={_apiKey}");
            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var resultList = JsonConvert.DeserializeObject<List<T>>(jsonResponse);
            return resultList[0];
        }
    }
}
