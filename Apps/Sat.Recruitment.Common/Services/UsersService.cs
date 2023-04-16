using Microsoft.Extensions.Logging;
using Sat.Recruitment.Common.Interfaces;
using Sat.Recruitment.Common.Models;
using Sat.Recruitment.Common.Validations;

namespace Sat.Recruitment.Common.Services;

public class UsersService : IUsersService
{
    private readonly ILogger<UsersService> _logger;
    private readonly IReadUsersFromFileService _readUsersFromFileService;

    private readonly List<Users> _users = new List<Users>();

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersService"/> class.
    /// </summary>
    /// <param name="logger">Logger instant.</param>
    /// <param name="readUsersFromFileService">Read Users From File Service instant.</param>
    public UsersService(
         ILogger<UsersService> logger,
        IReadUsersFromFileService readUsersFromFileService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _readUsersFromFileService = readUsersFromFileService ?? throw new ArgumentNullException(nameof(readUsersFromFileService));
    }

    /// <summary>
    /// Method to add a new user.
    /// </summary>
    /// <param name="users">User data fields.</param>
    /// <returns>The result message of the process.</returns>
    public async Task<Result> CreateUsersAsync(Users users)
    {
        _logger.LogInformation("[CreateUsersAsync] : Started add new user");
        var money = users.Money.ToString();

        UpdateUserMoneyByType(users, money);

        var reader = _readUsersFromFileService.ReadUsersFromFile();

        //Normalize email
        var userEmailSplited = users.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

        var atIndex = userEmailSplited[0].IndexOf("+", StringComparison.Ordinal);

        userEmailSplited[0] = atIndex < 0 ? userEmailSplited[0].Replace("+", "") : userEmailSplited[0].Replace("+", "").Remove(atIndex);

        users.Email = string.Join("@", new string[] { userEmailSplited[0], userEmailSplited[1] });

        var lineRead = string.Empty;
        while ((lineRead = await reader.ReadLineAsync()) != null)
        {
            var splitLine = lineRead.Split(',');

            var user = new Users
            {
                Name = splitLine[0],
                Email = splitLine[1],
                Phone = splitLine[2],
                Address = splitLine[3],
                UserType = splitLine[4],
                Money = decimal.Parse(splitLine[5]),
            };
            _users.Add(user);
        }

        reader.Close();

        var isDuplicated = _users.Any(user =>
                            user.Email == users.Email ||
                            user.Phone == users.Phone ||
                            (user.Name == users.Name && user.Address == users.Address));

        if (!isDuplicated)
        {
            _logger.LogInformation("[CreateUsersAsync] : User created successfully");
            return ErrorMessageManagerService.BuildErrorMessage(true, "User created");
        }
        else
        {
            _logger.LogInformation("[CreateUsersAsync] : The user is duplicated");
            return ErrorMessageManagerService.BuildErrorMessage(false, "The user is duplicated");
        }
    }

    /// <summary>
    /// Multiply the amount (money) by the correspondent percentage.
    /// </summary>
    /// <param name="amount">Amount decimal.</param>
    /// <param name="percentage">Percentage to multiply.</param>
    /// <returns>The calculate result.</returns>
    private decimal CalculateBonus(decimal amount, decimal percentage)
    {
        return amount * percentage;
    }

    /// <summary>
    /// Update the user money.
    /// </summary>
    /// <param name="usersModel">User data object.</param>
    /// <param name="money">Amount of the user.</param>
    private void UpdateUserMoneyByType(Users usersModel, string money)
    {
        decimal parsedMoney = decimal.Parse(money);

        switch (usersModel.UserType)
        {
            case "Normal":
                if (parsedMoney > 100)
                {
                    //If new user is normal and has more than USD100
                    usersModel.Money += CalculateBonus(parsedMoney, 0.12m);
                }
                else if (parsedMoney > 10)
                {
                    usersModel.Money += CalculateBonus(parsedMoney, 0.08m);
                }
                break;

            case "SuperUser":
                if (parsedMoney > 100)
                {
                    usersModel.Money += CalculateBonus(parsedMoney, 0.20m);
                }
                break;

            case "Premium":
                if (parsedMoney > 100)
                {
                    usersModel.Money += CalculateBonus(parsedMoney, 2.00m);
                }
                break;

            default:
                break;
        }
    }
}
