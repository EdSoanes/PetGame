using PetGame.Models;
using PetGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace PetGame.Controllers
{
    [RoutePrefix("user")]
    public class AnimalsController : BaseController
    {
        public AnimalsController(IGameService gameService)
            : base(gameService)
        {
        }

        [Route("~/animaltypesold")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAnimalTypes()
        {
            return await Execute<IEnumerable<AnimalType>>(() => Task.FromResult<IEnumerable<AnimalType>>(GameService.GetAnimalTypes()));
        }

        [Route("~/animaltypes")]
        [HttpGet]
        [Authorize]
        public async Task<HttpResponseMessage> GetAnimalTypesAsync()
        {
            return await Execute<IEnumerable<AnimalType>>(() => Task.FromResult<IEnumerable<AnimalType>>(GameService.GetAnimalTypes()));
        }

        [Route("animals")]
        [HttpPut]
        [Authorize]
        public async Task<HttpResponseMessage> Create([FromBody] Animal animal)
        {
            var userName = GetUserName();
            return await Execute<ApiResponse<Animal>>(() => GameService.CreateAnimal(userName, animal.AnimalTypeId, animal.Name));
        }

        [Route("animals/{animalId:long}/feed")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Feed(long animalId)
        {
            var userName = GetUserName();
            return await Execute<ApiResponse<Animal>>(() => GameService.FeedAnimal(userName, animalId));
        }

        [Route("animals/{animalId:long}/pet")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Pet(long animalId)
        {
            var userName = GetUserName();
            return await Execute<ApiResponse<Animal>>(() => GameService.PetAnimal(userName, animalId));
        }
    }
}
