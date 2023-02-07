using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Interfaces.Services;
using TruckPlanTools.Models.API;
using TruckPlanTools.Options;

namespace TruckPlanTools.Services
{
    internal class GeolocationService : IGeolocationService
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly ApiOptions _apiOptions;

        public GeolocationService(ApiOptions apiOptions)
        {
            _apiOptions = apiOptions;
        }

        public string GetCountryFromCordinate(string cordinate)
        {
            var url = $"{_apiOptions.BaseUrl}?key={_apiOptions.Apikey}&q={cordinate}&no_annotations=1&pretty=1";

            var response = _httpClient.GetAsync(url).Result;

            var country = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                var responeModel = response.Content.ReadFromJsonAsync<OpenCageRoot>().Result;
                country = responeModel.results[0]?.components?.country ?? string.Empty;
            }
            else
                throw new Exception($"Error while processing request: {response.StatusCode} {response.ReasonPhrase}");

            return country;
        }
    }
}
