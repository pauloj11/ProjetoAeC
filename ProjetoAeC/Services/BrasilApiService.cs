using Newtonsoft.Json;
using ProjetoAeC.Models;

namespace ProjetoAeC.Services
{
    public class BrasilApiService
    {
        private readonly HttpClient _httpClient;

        public BrasilApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Configuracao da base URL da Brasil API
            _httpClient.BaseAddress = new Uri("https://brasilapi.com.br/api/cptec/v1/");
        }

        public async Task<WeatherCity> GetWeatherDataAsync(int location)
        {
            try
            {
                var response = await _httpClient.GetAsync($"clima/previsao/{location}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherCity>(content);
                return weatherData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter dados climáticos da cidade: {ex.Message}");
            }
        }

        public async Task<WeatherAirport> GetWeatherByAirportAsync(string icaoCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"clima/aeroporto/{icaoCode}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var weatherAirport = JsonConvert.DeserializeObject<WeatherAirport>(content);
                return weatherAirport;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter dados climáticos do aeroporto: {ex.Message}");
            }
        }
    }
}