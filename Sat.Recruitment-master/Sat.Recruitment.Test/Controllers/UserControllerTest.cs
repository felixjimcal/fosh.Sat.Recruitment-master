using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Models.Entities;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Services.Services.Interfaces;
using Sat.Recruitment.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<HealthCheckService> _healthCheckMock;
        private readonly UserDto testDto;

        public UserControllerTest()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _userServiceMock = new Mock<IUserService>();
            _healthCheckMock = new Mock<HealthCheckService>();

            _controller = new UserController(_loggerMock.Object, _healthCheckMock.Object, _userServiceMock.Object);

            testDto = DtoMockCreator.CreateUserDto("John", "123 Main St", "john@example.com", "+1234567890", 101, 1);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedResult_WhenUserIsValid()
        {
            // Arrange
            var userDto = testDto;
            _userServiceMock.Setup(s => s.CreateUser(It.IsAny<UserDto>())).ReturnsAsync(new ResultModel());

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Model state is invalid");
            var userDto = testDto;

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_ReturnsProblemDetails_WhenExceptionIsThrown()
        {
            // Arrange
            var userDto = testDto;
            _userServiceMock.Setup(s => s.CreateUser(It.IsAny<UserDto>())).ThrowsAsync(new System.Exception("Error de prueba"));

            // Act
            var result = await _controller.CreateUser(userDto);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }
    }
}