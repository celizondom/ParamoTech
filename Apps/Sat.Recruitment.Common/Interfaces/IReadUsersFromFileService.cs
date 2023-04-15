namespace Sat.Recruitment.Common.Interfaces;

/// <summary>
/// Interface for reading the users from files.
/// </summary>
public interface IReadUsersFromFileService
{
    /// <summary>
    /// Gets the users from txt flat file.
    /// </summary>
    /// <returns>The list of users from file.</returns>
    public StreamReader ReadUsersFromFile();
}
