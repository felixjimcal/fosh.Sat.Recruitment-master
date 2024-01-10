using Moq;
using Sat.Recruitment.Models.DTOs;
using Sat.Recruitment.Models.Entities;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Repositories.Repositories.Interfaces;
using Sat.Recruitment.Services.Mappings;
using Sat.Recruitment.Services.Services.Impl;
using Sat.Recruitment.Services.Services.Interfaces;
using Sat.Recruitment.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test.Services
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserDto testDto;
        private readonly ResultModel resultModel;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);

            testDto = DtoMockCreator.CreateUserDto("John", "123 Main St", "john@example.com", "+1234567890", 101, 1);
            resultModel = new ResultModel() { IsSuccess = true, Errors = "User Created" };
        }

        [Fact]
        public async Task CreateUser_CallsRepository_WithCorrectUser()
        {
            // Arrange
            var userDto = testDto;
            var userModel = userDto.ToModel();

            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserModel>())).ReturnsAsync(resultModel);

            // Act
            await _userService.CreateUser(userDto);

            // Assert
            _userRepositoryMock.Verify(repo => repo.CreateUser(It.Is<UserModel>(u => u.Name == userModel.Name)), Times.Once);
        }

        [Fact]
        public async Task CreateUser_ReturnsResultModel_WhenRepositorySucceeds()
        {
            // Arrange
            var userDto = testDto;
            var userModel = userDto.ToModel();

            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserModel>())).ReturnsAsync(resultModel);

            // Act
            var result = await _userService.CreateUser(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(resultModel, result);
        }

        [Fact]
        public async Task CreateUser_ThrowsException_WhenRepositoryThrowsException()
        {
            // Arrange
            var userDto = testDto;
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserModel>())).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.CreateUser(userDto));
        }
    }
}