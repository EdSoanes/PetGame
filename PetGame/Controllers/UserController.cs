using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PetGame.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        [Route("{userName}")]
        public User Get(string userName)
        {
            return new User { UserName = userName };
        }
    }
}
