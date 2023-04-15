using Sat.Recruitment.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.Test
{
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
        /// User test 1 with whole data is wrong.
        /// </summary>
        /// <returns></returns>
        public static Users NewWrongUserMock()
        {
            return new Users
            {
                Name = string.Empty,
                Email = string.Empty,
                Address = string.Empty,
                Phone = string.Empty,
                UserType = string.Empty,
                Money = 565.3m,
            };
        }
    }
}
