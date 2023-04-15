using Sat.Recruitment.Common.Models;
using Sat.Recruitment.Common.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Common.Interfaces
{
    public interface IUsersService
    {
        /// <summary>
        /// Method to add a new user.
        /// </summary>
        /// <param name="users">User data fields.</param>
        /// <returns>Success or not when the user was created.</returns>
        public Task<Result> CreateUsersAsync(Users users);
    }
}
