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
            var city = "London";
            var spotifyQuery = "rock";
            var newsCategory = "general";

           
            _mockBaseService
                .Setup(service => service.GetDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new { Message = "Raw data" });

            var result = await _controller.GetNewsAndWeather(city, spotifyQuery, newsCategory);

            var okResult = Assert.IsType<OkObjectResult>(result);  
            Assert.NotNull(okResult.Value);  
        }

     
    }
}