using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoAeC.Models;
using ProjetoAeC.Services;
using System.Globalization;
using static ProjetoAeC.Models.WeatherDataDTO;

namespace ProjetoAeC.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly BrasilApiService _brasilApiService;
        private readonly IWeatherDataRepository _weatherDataRepository;

        public WeatherController(BrasilApiService brasilApiService, IWeatherDataRepository weatherDataRepository)
        {
            _brasilApiService = brasilApiService;
            _weatherDataRepository = weatherDataRepository;
        }

        /// <summary>
        /// Retorna Pervisão meteorológica para 1 dia na cidade informada.
        /// </summary>
        /// <param name="location">Código da cidade presente nos serviços da CPTEC</param>
        /// <returns>Ok</returns>
        [HttpGet("cidade/{location}")]
        public async Task<IActionResult> GetWeather(int location)
        {
            try
            {
                var weatherData = await _brasilApiService.GetWeatherDataAsync(location);


                // Exibir os dados no console
                Console.WriteLine($"Localização: {weatherData.Cidade}");
                Console.WriteLine($"Estado: {weatherData.Estado}");
                Console.WriteLine($"Atualizado em: {weatherData.Atualizado_em}");


                foreach (var clima in weatherData.Clima)
                {
                    Console.WriteLine($"Data: {clima.Data}");
                    Console.WriteLine($"Condição: {clima.Condicao}");
                    Console.WriteLine($"Descrição da Condição: {clima.Condicao_desc}");
                    Console.WriteLine($"Temperatura Mínima: {clima.Min}");
                    Console.WriteLine($"Temperatura Máxima: {clima.Max}");
                    Console.WriteLine($"Índice UV: {clima.Indice_uv}");
                    Console.WriteLine(); // Linha em branco para separar as previsões
                }

                //return Ok(weatherData);

                var weatherCity = new WeatherCityDTO
                {
                    Cidade = weatherData.Cidade,
                    Estado = weatherData.Estado,
                    Atualizado_em = DateTime.ParseExact(weatherData.Atualizado_em, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Clima = JsonConvert.SerializeObject(weatherData.Clima)
                };

                // Chame o método PostWeatherCity para persistir os dados da cidade
                return PostWeatherCity(weatherCity);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogDTO
                {
                    Timestamp = DateTime.Now,
                    ErrorMessage = ex.Message,
                };

                _weatherDataRepository.AddErrorLog(errorLog);
                return BadRequest($"Erro ao recuperar dados climáticos: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna condições meteorológicas atuais no aeroporto solicitado.
        /// Este endpoint utiliza o código ICAO (4 dígitos) do aeroporto.
        /// </summary>
        /// <param name="codigoAeroporto">Código ICAO (4 dígitos) do aeroporto desejado</param>
        /// <returns>Ok</returns>
        [HttpGet("aeroporto/{codigoAeroporto}")]
        public async Task<IActionResult> GetWeatherByAirport(string codigoAeroporto)
        {
            try
            {
                var weatherDataAirport = await _brasilApiService.GetWeatherByAirportAsync(codigoAeroporto);


                // Exibir os dados do aeroporto no console
                Console.WriteLine($"Código ICAO: {weatherDataAirport.codigo_icao}");
                Console.WriteLine($"Umidade: {weatherDataAirport.Umidade}%");
                Console.WriteLine($"Visibilidade: {weatherDataAirport.Visibilidade}");
                Console.WriteLine($"Pressão Atmosférica: {weatherDataAirport.pressaoAtmosferica} hPa");
                Console.WriteLine($"Vento: {weatherDataAirport.Vento} km/h");
                Console.WriteLine($"Direção do Vento: {weatherDataAirport.direcaoVento} graus");
                Console.WriteLine($"Condição: {weatherDataAirport.Condicao}");
                Console.WriteLine($"Descrição da Condição: {weatherDataAirport.condicao_desc}");
                Console.WriteLine($"Temperatura: {weatherDataAirport.Temp}°C");
                Console.WriteLine($"Atualizado em: {weatherDataAirport.atualizado_em}");

                var weatherAirport = new WeatherAirportDTO
                {
                    Umidade = weatherDataAirport.Umidade,
                    Visibilidade = weatherDataAirport.Visibilidade,
                    CodigoICAO = weatherDataAirport.codigo_icao,
                    PressaoAtmosferica = weatherDataAirport.pressaoAtmosferica,
                    Vento = weatherDataAirport.Vento,
                    DirecaoVento = weatherDataAirport.direcaoVento,
                    Condicao = weatherDataAirport.Condicao,
                    CondicaoDescricao = weatherDataAirport.condicao_desc,
                    Temperatura = weatherDataAirport.Temp,
                    AtualizadoEm = weatherDataAirport.atualizado_em
                };

                // Retorne os dados climáticos do aeroporto como resposta JSON
                return PostWeatherAirport(weatherAirport);


            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogDTO
                {
                    Timestamp = DateTime.Now,
                    ErrorMessage = ex.Message,
                };

                _weatherDataRepository.AddErrorLog(errorLog);
                // Trate erros aqui, por exemplo, registre-os ou retorne uma resposta de erro personalizada
                return BadRequest($"Erro ao recuperar dados climáticos do aeroporto: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza a inserção dos dados meteorológicos da cidade solicitada no GET.
        /// Este endpoint utiliza o código ICAO (4 dígitos) do aeroporto.
        /// </summary>
        [HttpPost("cidade")]
        public IActionResult PostWeatherCity([FromBody] WeatherCityDTO weatherCityDTO)
        {
            try
            {
                _weatherDataRepository.AddWeatherCity(weatherCityDTO);

                return Ok("Dados meteorológicos da cidade adicionados com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro ao adicionar os dados meteorológicos da cidade.");
            }
        }

        /// <summary>
        /// Retorna condições meteorológicas atuais no aeroporto solicitado.
        /// Este endpoint utiliza o código ICAO (4 dígitos) do aeroporto.
        /// </summary>
        [HttpPost("aeroporto")]
        public IActionResult PostWeatherAirport([FromBody] WeatherAirportDTO weatherAirportDTO)
        {
            try
            {
                _weatherDataRepository.AddWeatherAirport(weatherAirportDTO);

                return Ok("Dados meteorológicos do aeroporto adicionados com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao adicionar os dados meteorológicos do aeroporto.: {ex.Message}");
            }
        }
    }
}
