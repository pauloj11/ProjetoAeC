using Microsoft.EntityFrameworkCore;
using ProjetoAeC.Models;
using System;
using System.Linq;
using static ProjetoAeC.Models.WeatherDataDTO;

public interface IWeatherDataRepository
{
    void AddWeatherCity(WeatherCityDTO weatherCityDTO);
    void AddWeatherAirport(WeatherAirportDTO weatherAirportDTO);
    void AddErrorLog(ErrorLogDTO errorLogDTO);
}

public class WeatherDataRepository : IWeatherDataRepository
{
    private readonly WeatherDbContext _context;

    public WeatherDataRepository(WeatherDbContext context)
    {
        _context = context;
    }

    public void AddWeatherCity(WeatherCityDTO weatherCityDTO)
    {

        _context.WeatherCity.Add(weatherCityDTO);
        _context.SaveChanges();
    }

    public void AddWeatherAirport(WeatherAirportDTO weatherAirportDTO)
    {

        _context.WeatherAirport.Add(weatherAirportDTO);
        _context.SaveChanges();
    }

    public void AddErrorLog(ErrorLogDTO errorLogDTO)
    {
        _context.ErrorLog.Add(errorLogDTO);
        _context.SaveChanges();
    }
}
