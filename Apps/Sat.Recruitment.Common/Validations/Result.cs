namespace Sat.Recruitment.Common.Validations;

public class Result
{
    /// <summary>
    /// Gets or sets the error processed.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string? Errors { get; set; }
}
