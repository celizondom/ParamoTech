using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Common.Interfaces;
using Sat.Recruitment.Common.Models;
using Sat.Recruitment.Common.Services;
using Sat.Recruitment.Common.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;

        private readonly List<Users> _users = new List<Users>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="usersService"></param>
        public UsersController(
        ILogger<UsersController> logger,
        IUsersService usersService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUserAsync(Users userModel)
        {
            _logger.LogInformation("[CreateUserAsync] : Received message request from application.");
            var errors = ValidateErrors(userModel);

            if (errors != null && errors != "")
            {
                return ErrorMessageManagerService.BuildErrorMessage(false, errors);
            }

            var userResult = await _usersService.CreateUsersAsync(userModel);
            return userResult;
        }

        /// <summary>
        /// Validate the users data fields the possible when the user is new.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <param name="email">User email.</param>
        /// <param name="address">User address.</param>
        /// <param name="phone">User phone.</param>
        /// <returns>The possible error from validation.</returns>
        private string ValidateErrors(Users userModel)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrEmpty(userModel.Name))
            {
                // Validate if Name is null
                errors.AppendLine("The name is required");
            }
            if (string.IsNullOrEmpty(userModel.Email))
            {
                // Validate if Email is null
                errors.AppendLine("The email is required");
            }
            if (string.IsNullOrEmpty(userModel.Address))
            {
                // Validate if Address is null
                errors.AppendLine("The address is required");
            }
            if (string.IsNullOrEmpty(userModel.Phone))
            {
                // Validate if Phone is null
                errors.AppendLine("The phone is required");
            }

            return errors.ToString();
        }
    }
}
