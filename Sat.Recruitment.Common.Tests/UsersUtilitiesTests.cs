using Sat.Recruitment.Common.Models;

namespace Sat.Recruitment.Common.Tests;

/// <summary>
/// Common objects to use in unit test.
/// </summary>
public class UsersUtilitiesTests
{
    /// <summary>
    /// User test 1 with whole data correct.
    /// </summary>
    /// <returns></returns>
    public static Users NewUserMock()
    {
        return new Users
        {
            Name = "Test user 1",
            Email = "emial@correct.com",
            Address = "Argentina",
            Phone = "+596-2626885",
            UserType = "Normal",
            Money = 565.3m,
        };
    }

    /// <summary>
    /// User test 2 with whole data is wrong.
    /// </summary>
    /// <returns></returns>
    public static Users NewWrongUserMock()
    {
        return new Users
        {
            Name = "Test user 2",
            Email = "mike@gmail.com",
            Address = "Argentina",
            Phone = "+596-2626885",
            UserType = "Normal",
            Money = 55.3m,
        };
    }
}
