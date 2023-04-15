using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Common.Interfaces;
using Sat.Recruitment.Common.Models;
using Sat.Recruitment.Common.Validations;
using System;
using Xunit;

namespace Sat.Recruitment.Test;

[CollectionDefinition("Tests", DisableParallelization = true)]
public class UsersControllerTests
{
    private readonly Mock<IUsersService> _usersService;
    private readonly Mock<ILogger<UsersController>> _controllerLogger;

    public UsersControllerTests()
    {
        _controllerLogger = new Mock<ILogger<UsersController>>();
        _usersService = new Mock<IUsersService>();
    }

    [Fact]
    public void UsersControllers_CreateUserAsync_SuccessResult()
    {
        var newUser = UsersUtilitiesTests.NewUserMock();

        var resultMessage = new Result { IsSuccess = true, Errors = "User Created" };

        _usersService.Setup(x => x.CreateUsersAsync(It.IsAny<Users>())).ReturnsAsync(resultMessage);

        var userController = new UsersController(
            _controllerLogger.Object,
            _usersService.Object);

        var result = userController.CreateUserAsync(newUser).Result;

        _usersService.Verify(x => x.CreateUsersAsync(It.IsAny<Users>()),
            Times.Once);

        Assert.True(result.IsSuccess);
        Assert.Equal("User Created", result.Errors);

        _controllerLogger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "[CreateUserAsync] : Received message request from application."),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    /// <summary>
    /// Success validation of all the user fields are required.
    /// </summary>
    [Fact]
    public void UsersControllers_CreateUserAsync_WrongDataSuccessResult()
    {
        var newUser = UsersUtilitiesTests.NewWrongUserMock();

        var resultMessage = new Result { IsSuccess = false, Errors = "The email is required" };

        _usersService.Setup(x => x.CreateUsersAsync(newUser)).ReturnsAsync(resultMessage);

        var userController = new UsersController(
            _controllerLogger.Object,
            _usersService.Object);

        var result = userController.CreateUserAsync(newUser).Result;

        _usersService.Verify(x => x.CreateUsersAsync(It.IsAny<Users>()),
            Times.Never);

        Assert.False(result.IsSuccess);
        Assert.Equal("The name is required\r\nThe email is required\r\nThe address is required\r\nThe phone is required\r\n", result.Errors);

        _controllerLogger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "[CreateUserAsync] : Received message request from application."),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
