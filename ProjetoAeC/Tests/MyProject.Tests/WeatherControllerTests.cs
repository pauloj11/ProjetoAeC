using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoAeC.Controllers;
using ProjetoAeC.Models;
using ProjetoAeC.Services;
using System.Threading.Tasks;

namespace ProjetoAeC.Tests.MyProject.Tests
{
    public class WeatherControllerTests
    {
        [Fact]
        public void GetWeather_ShouldReturnOkResult()
        {
            var mockBrasilApiService = new Mock<BrasilApiService>();
            var mockWeatherDataRepository = new Mock<IWeatherDataRepository>();
            // Arrange
            var controller = new WeatherController(mockBrasilApiService.Object, mockWeatherDataRepository.Object);

            // Act
            var result = controller.GetWeather(4754);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetWeatherCity_ShouldReturnBadRequestResult()
        {
            // Arrange
            var mockWeatherDataRepository = new Mock<IWeatherDataRepository>();
            var brasilApiServiceMock = new Mock<BrasilApiService>();
            brasilApiServiceMock.Setup(service => service.GetWeatherDataAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Erro simulado"));

            var controller = new WeatherController(brasilApiServiceMock.Object, mockWeatherDataRepository.Object);

            // Act
            var result = await controller.GetWeather(1);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro ao recuperar dados climáticos: Erro simulado", badRequestResult.Value);
        }


        [Fact]
        public async Task GetWeatherAirport_ShouldReturnOkResult()
        {
            // Arrange
            var mockWeatherDataRepository = new Mock<IWeatherDataRepository>();
            var brasilApiServiceMock = new Mock<BrasilApiService>();
            brasilApiServiceMock.Setup(service => service.GetWeatherByAirportAsync(It.IsAny<string>()))
                .ReturnsAsync(new WeatherAirport());

            var controller = new WeatherController(brasilApiServiceMock.Object, mockWeatherDataRepository.Object);

            // Act
            var result = await controller.GetWeatherByAirport("SBAR");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetWeatherAirport_ShouldReturnNotFoundResult()
        {
            // Arrange
            var mockWeatherDataRepository = new Mock<IWeatherDataRepository>();
            var brasilApiServiceMock = new Mock<BrasilApiService>();
            brasilApiServiceMock.Setup(service => service.GetWeatherByAirportAsync(It.IsAny<string>()))
                .ReturnsAsync((WeatherAirport)null);

            var controller = new WeatherController(brasilApiServiceMock.Object, mockWeatherDataRepository.Object);

            // Act
            var result = await controller.GetWeatherByAirport("XYZ789");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Dados climáticos não encontrados para o aeroporto: XYZ789", notFoundResult.Value);
        }
    }
}
