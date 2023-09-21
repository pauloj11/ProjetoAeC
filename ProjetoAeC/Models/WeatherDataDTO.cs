using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAeC.Models
{
    public class WeatherDataDTO
    {
        public class WeatherDbContext : DbContext
        {
            public DbSet<WeatherCityDTO> WeatherCity { get; set; }
            public DbSet<WeatherAirportDTO> WeatherAirport { get; set; }
            public DbSet<ErrorLogDTO> ErrorLog { get; set; }

            public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configuração das entidades

                modelBuilder.Entity<Clima>().HasNoKey(); // Indica que Clima é uma entidade sem chave primária
                //modelBuilder.Entity<WeatherAirport>().HasNoKey(); // Indica que WeatherAirport é uma entidade sem chave primária

                base.OnModelCreating(modelBuilder);
            }

        }

        public class WeatherCityDTO
        {
            [Key] // Esta atribuição indica que a propriedade será a chave primária
            public int Id { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public DateTime Atualizado_em { get; set; }
            public string Clima { get; set; }
        }

        public class WeatherAirportDTO
        {
            [Key] // Esta atribuição indica que a propriedade será a chave primária
            public int Id { get; set; }
            public int Umidade { get; set; }
            public string Visibilidade { get; set; }
            public string CodigoICAO { get; set; }
            public int PressaoAtmosferica { get; set; }
            public int Vento { get; set; }
            public int DirecaoVento { get; set; }
            public string Condicao { get; set; }
            public string CondicaoDescricao { get; set; }
            public int Temperatura { get; set; }
            public DateTime AtualizadoEm { get; set; }
        }

        public class ErrorLogDTO
        {
            [Key] // Esta atribuição indica que a propriedade será a chave primária
            public int Id { get; set; }
            public DateTime Timestamp { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
