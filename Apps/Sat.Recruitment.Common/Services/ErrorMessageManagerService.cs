using Sat.Recruitment.Common.Validations;

namespace Sat.Recruitment.Common.Services;

public class ErrorMessageManagerService
{
    /// <summary>
    /// Build a specific error message.
    /// </summary>
    /// <param name="code">Error code store in a FedNowOutgoingConstants.</param>
    /// <param name="args">Values to replace in the error message.</param>
    /// <returns>Return the error code object.</returns>
    public static Result BuildErrorMessage(bool isSuccess, string errors)
    {
        return new Result()
        {
            IsSuccess = isSuccess,
            Errors = errors,
        };
    }
}
