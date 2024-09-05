using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Pawz.Application.Helpers
{
    public static class PetImageFetcher
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _catApiKey;
        static PetImageFetcher()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _catApiKey = configuration.GetValue<string>("ApiSettings:CatApiKey");

            if (string.IsNullOrEmpty(_catApiKey))
            {
                throw new InvalidOperationException("Cat API key is not set in appsettings.json.");
            }
        }

        public static async Task<string> FetchRandomDogImageUrlAsync()
        {
            var apiUrl = "https://dog.ceo/api/breeds/image/random";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var jsonDoc = JsonDocument.Parse(response);
            var imageUrl = jsonDoc.RootElement.GetProperty("message").GetString();
            return imageUrl;
        }

        public static async Task<string> FetchRandomCatImageUrlAsync()
        {
            var apiUrl = $"https://api.thecatapi.com/v1/images/search?api_key={_catApiKey}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var jsonDoc = JsonDocument.Parse(response);
            var imageUrl = jsonDoc.RootElement[0].GetProperty("url").GetString();
            return imageUrl;
        }
    }
}
