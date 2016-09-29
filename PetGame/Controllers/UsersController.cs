using PetGame.Models;
using PetGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PetGame.Controllers
{
    [RoutePrefix("user")]
    public class UsersController : BaseController
    {
        public UsersController(IGameService gameService)
            : base(gameService)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]

        public async Task<HttpResponseMessage> Get()
        {
            var userName = GetUserName();
            return await Execute<User>(() => GameService.GetUserByUserName(userName));
        }

        [Route("")]
        [HttpPut]
        [Authorize]

        public async Task<HttpResponseMessage> Put([FromBody] User user)
        {
            user.UserName = GetUserName();
            return await Execute<ApiResponse<User>>(() => GameService.CreateUser(user));
        }
    }
}
