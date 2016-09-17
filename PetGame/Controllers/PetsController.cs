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
    public class PetsController : BaseController
    {
        public PetsController(IGameService gameService)
            : base(gameService)
        {
        }

        [Route("~/pettypes")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetPetTypes()
        {
            return await Execute<IEnumerable<PetType>>(() => GameService.GetPetTypes());
        }

        [Route("{username}/pets")]
        [HttpPut]
        public async Task<HttpResponseMessage> Create(string userName, [FromBody] Pet pet)
        {
            return await Execute<ApiResponse<Pet>>(() => GameService.CreatePet(userName, pet.PetTypeId, pet.Name));
        }

        [Route("{username}/pet/{petId:long}/feed")]
        [HttpPost]
        public async Task<HttpResponseMessage> Feed(string userName, long petId)
        {
            return await Execute<ApiResponse<Pet>>(() => GameService.FeedPet(userName, petId));
        }

        [Route("{username}/pet/{petId:long}/pet")]
        [HttpPost]
        public async Task<HttpResponseMessage> Pet(string userName, long petId)
        {
            return await Execute<ApiResponse<Pet>>(() => GameService.PetPet(userName, petId));
        }
    }
}
