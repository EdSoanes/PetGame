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
    public class UsersController : ApiController
    {
        private readonly IGameService _gameService;

        public UsersController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("{userName}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string userName)
        {
            return await Execute<User>(() => _gameService.GetUserByUserName(userName));
        }

        private async Task<HttpResponseMessage> Execute<T>(Func<Task<T>> func)
        {
            var funcResult = await func.Invoke();
            if (funcResult == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return this.Request.CreateResponse<T>(funcResult);
        }
    }
}
