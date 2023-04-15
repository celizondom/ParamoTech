using Sat.Recruitment.Common.Interfaces;

namespace Sat.Recruitment.Common.Services;

/// <summary>
/// Class to handled the reading users from files.
/// </summary>
public class ReadUsersFromFileService: IReadUsersFromFileService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadUsersFromFileService"/> class.
    /// </summary>
    public ReadUsersFromFileService()
    { }

    /// <summary>
    /// Gets the users from txt flat file.
    /// </summary>
    /// <returns>The list of users from file.</returns>
    public StreamReader ReadUsersFromFile()
    {
        var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

        FileStream fileStream = new FileStream(path, FileMode.Open);

        StreamReader reader = new StreamReader(fileStream);
        return reader;
    }
}
