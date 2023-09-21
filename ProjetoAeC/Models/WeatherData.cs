using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ProjetoAeC.Models
{
    public class WeatherCity
    {
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Atualizado_em { get; set; }
        public List<Clima> Clima { get; set; }
    }

    public class Clima
    {
        public string Data { get; set; }
        public string Condicao { get; set; }
        public string Condicao_desc { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Indice_uv { get; set; }
    }

    public class WeatherAirport
    {
        public int Umidade { get; set; }
        public string Visibilidade { get; set; }
        public string codigo_icao { get; set; } 
        public int pressaoAtmosferica { get; set; } 
        public int Vento { get; set; }
        public int direcaoVento { get; set; } 
        public string Condicao { get; set; }
        public string condicao_desc { get; set; } 
        public int Temp { get; set; }
        public DateTime atualizado_em { get; set; } 
    }
}
