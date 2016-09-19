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
    public class AnimalsController : BaseController
    {
        public AnimalsController(IGameService gameService)
            : base(gameService)
        {
        }

        [Route("~/animaltypes")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAnimalTypes()
        {
            return await Execute<IEnumerable<AnimalType>>(() => Task.FromResult<IEnumerable<AnimalType>>(GameService.GetAnimalTypes()));
        }

        [Route("{username}/animals")]
        [HttpPut]
        public async Task<HttpResponseMessage> Create(string userName, [FromBody] Animal animal)
        {
            return await Execute<ApiResponse<Animal>>(() => GameService.CreateAnimal(userName, animal.AnimalTypeId, animal.Name));
        }

        [Route("{username}/animals/{animalId:long}/feed")]
        [HttpPost]
        public async Task<HttpResponseMessage> Feed(string userName, long animalId)
        {
            return await Execute<ApiResponse<Animal>>(() => GameService.FeedAnimal(userName, animalId));
        }

        [Route("{username}/animals/{animalId:long}/pet")]
        [HttpPost]
        public async Task<HttpResponseMessage> Pet(string userName, long animalId)
        {
            return await Execute<ApiResponse<Animal>>(() => GameService.PetAnimal(userName, animalId));
        }
    }
}
