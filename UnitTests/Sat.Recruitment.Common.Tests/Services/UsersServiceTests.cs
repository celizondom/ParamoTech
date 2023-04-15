using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Common.Interfaces;
using Sat.Recruitment.Common.Services;
using Sat.Recruitment.Common.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Common.Tests.Services;

public class UsersServiceTests
{
    private readonly Mock<ILogger<UsersService>> _logger;
    private readonly Mock<IReadUsersFromFileService> _readUsersFromFileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersServiceTests"/> class.
    /// </summary>
    public UsersServiceTests()
    {
        _logger = new Mock<ILogger<UsersService>>();
        _readUsersFromFileService = new Mock<IReadUsersFromFileService>();
    }

    /// <summary>
    /// Success when the user was created successfully.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UsersServices_CreateUsersAsync_UserCreated_SuccessAsync()
    {
        string[] stringArray = { "Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124" };
        string stringJoined = string.Join(",", stringArray);

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(stringJoined));
        var reader = new StreamReader(memoryStream);
        _readUsersFromFileService.Setup(x => x.ReadUsersFromFile()).Returns(reader);

        var userModel = UsersUtilitiesTests.NewUserMock();

        var serviceMock = new UsersService(
            _logger.Object,
            _readUsersFromFileService.Object);

        var resultService = await serviceMock.CreateUsersAsync(userModel);

        Assert.True(resultService.IsSuccess);
        Assert.Equal("User created", resultService.Errors);

        _logger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "[CreateUsersAsync] : User created successfully"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    /// <summary>
    /// Success error when the user is duplicated.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UsersServices_CreateUsersAsync_UserCreatedIsDuplicated_SuccessAsync()
    {
        string[] stringArray = { "Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124" };
        string stringJoined = string.Join(",", stringArray);

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(stringJoined));
        var reader = new StreamReader(memoryStream);
        _readUsersFromFileService.Setup(x => x.ReadUsersFromFile()).Returns(reader);

        var userModel = UsersUtilitiesTests.NewWrongUserMock();

        var serviceMock = new UsersService(
            _logger.Object,
            _readUsersFromFileService.Object);

        var resultService = await serviceMock.CreateUsersAsync(userModel);

        Assert.False(resultService.IsSuccess);
        Assert.Equal("The user is duplicated", resultService.Errors);

        _logger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "[CreateUsersAsync] : The user is duplicated"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    /// <summary>
    /// Success when the User Type is SuperUser and it created successfully.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UsersServices_CreateUsersAsync_UserCreatedSuperUser_SuccessAsync()
    {
        string[] stringArray = { "Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "SuperUser", "124" };
        string stringJoined = string.Join(",", stringArray);

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(stringJoined));
        var reader = new StreamReader(memoryStream);
        _readUsersFromFileService.Setup(x => x.ReadUsersFromFile()).Returns(reader);

        var userModel = UsersUtilitiesTests.NewUserMock();
        userModel.UserType = "SuperUser";

        var serviceMock = new UsersService(
            _logger.Object,
            _readUsersFromFileService.Object);

        var resultService = await serviceMock.CreateUsersAsync(userModel);

        Assert.True(resultService.IsSuccess);
        Assert.Equal("User created", resultService.Errors);

        _logger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "[CreateUsersAsync] : User created successfully"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    /// <summary>
    /// Success when the User Type is PremiumUser and it created successfully.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UsersServices_CreateUsersAsync_UserCreatedPremiumUser_SuccessAsync()
    {
        string[] stringArray = { "Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Premium", "563.5" };
        string stringJoined = string.Join(",", stringArray);

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(stringJoined));
        var reader = new StreamReader(memoryStream);
        _readUsersFromFileService.Setup(x => x.ReadUsersFromFile()).Returns(reader);

        var userModel = UsersUtilitiesTests.NewUserMock();
        userModel.UserType = "Premium";

        var serviceMock = new UsersService(
            _logger.Object,
            _readUsersFromFileService.Object);

        var resultService = await serviceMock.CreateUsersAsync(userModel);

        Assert.True(resultService.IsSuccess);
        Assert.Equal("User created", resultService.Errors);

        _logger.Verify(
            logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "[CreateUsersAsync] : User created successfully"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
