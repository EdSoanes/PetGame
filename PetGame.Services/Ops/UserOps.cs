using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services.Ops
{
    public static class UserOps
    {
        public static ApiResponse<User> CanCreateUser(User user)
        {
            if (user == null)
                return new ApiResponse<User>(null, System.Net.HttpStatusCode.BadRequest, "No user provided");

            if (string.IsNullOrEmpty(user.UserName))
                return new ApiResponse<User>(null, System.Net.HttpStatusCode.BadRequest, "No user name provided");

            if (string.IsNullOrEmpty(user.FullName))
                return new ApiResponse<User>(null, System.Net.HttpStatusCode.BadRequest, "No full name provided");

            return null;
        }
    }
}
