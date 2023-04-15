using Sat.Recruitment.Common.Services;

namespace Sat.Recruitment.Common.Tests.Services;

public class ReadUsersFromFileServiceTests
{
    private readonly string _tempFilePath;

    public ReadUsersFromFileServiceTests()
    {
        _tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Users.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(_tempFilePath));
        File.WriteAllText(_tempFilePath, "Sample content for test");
    }

    [Fact]
    public void ReadUsersFromFile_ReturnsValidStreamReader()
    {
        // Arrange
        var service = new ReadUsersFromFileService();

        // Act
        using (var reader = service.ReadUsersFromFile())
        {
            // Assert
            Assert.NotNull(reader);
            Assert.False(reader.EndOfStream);
        }
    }

    public void Dispose()
    {
        File.Delete(_tempFilePath);
        Directory.Delete(Path.GetDirectoryName(_tempFilePath));
    }
}
