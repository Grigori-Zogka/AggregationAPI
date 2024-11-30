using APIAggregation.Controllers;
using APIAggregation.Interfaces;
using APIAggregation.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APIAggregation.Tests
{
    public class APIAggregationControllerTests
    {

        private readonly Mock<IBaseService> _mockBaseService;
        private readonly AggregationController _controller;

        public APIAggregationControllerTests()
        {
            _mockBaseService = new Mock<IBaseService>();
            _controller = new AggregationController(_mockBaseService.Object);
        }

        [Fact]
        public async Task GetNewsAndWeather_ReturnsOkResult_WhenDataIsFetched()
        { 
            var city = "Lisbon";
            var spotifyQuery = "folk";
            var newsCategory = "general";

            _mockBaseService
                .Setup(service => service.GetDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new { Message = "Data" });

            var result = await _controller.GetNewsAndWeather(city, spotifyQuery, newsCategory);

            var okResult = Assert.IsType<OkObjectResult>(result);  
            Assert.NotNull(okResult.Value);  
        }

        [Fact]
        public async Task GetNewsAndWeather_ReturnsServerError_WhenServiceThrowsException()
        {
            var city = "Lisbon";
            var spotifyQuery = "folk";
            var newsCategory = "general";

            _mockBaseService
                .Setup(service => service.GetDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Service failure"));

            var result = await _controller.GetNewsAndWeather(city, spotifyQuery, newsCategory);

            
            var objectResult = Assert.IsType<ObjectResult>(result);  
            Assert.Equal(500, objectResult.StatusCode); 
            Assert.Contains("Internal server error", objectResult.Value.ToString());
        }

        [Fact]
        public async Task GetNewsAndWeather_ReturnsBadRequest_WhenInvalidParametersAreProvided()
        {
            var city = "";  
            var spotifyQuery = "folk";
            var newsCategory = "general";

            var result = await _controller.GetNewsAndWeather(city, spotifyQuery, newsCategory);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); 
            Assert.Equal("Invalid parameters.", badRequestResult.Value); 
        }


    }
}