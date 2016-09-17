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
    [RoutePrefix("users")]
    public class UsersController : BaseController
    {
        public UsersController(IGameService gameService)
            : base(gameService)
        {
        }

        [Route("{userName}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string userName)
        {
            return await Execute<User>(() => GameService.GetUserByUserName(userName));
        }

        [Route("")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromBody] User user)
        {
            return await Execute<ApiResponse<User>>(() => GameService.CreateUser(user));
        }
    }
}
