using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiServer;

namespace HttpClientConsole
{
    public interface IWeatherHttpCleint
    {
        Task<string> GetAll();
    }
    public class WeatherHttpCleint:IWeatherHttpCleint
    {
        private HttpClient _client;
        private string urlBase = "https://localhost:44332/WeatherForecast/";
        public WeatherHttpCleint(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetAll()
        {
            return await _client.GetStringAsync($"{urlBase}GetAll");
        }
    }
}