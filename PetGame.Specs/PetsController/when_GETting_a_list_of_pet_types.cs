using Machine.Fakes;
using Machine.Specifications;
using PetGame.Controllers;
using PetGame.Models;
using PetGame.Repositories;
using PetGame.Services;
using PetGame.Services.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace PetGame.Specs.PetsController
{
    [Subject("creating a user")]
    class when_GETting_a_list_of_pet_types : WithSubject<PetGame.Controllers.PetsController>
    {
        Establish context = () =>
        {
            Subject.Request = new HttpRequestMessage();
            Subject.Configuration = new HttpConfiguration();

            WebApiConfig.Register(Subject.Configuration);

            The<IGameService>().WhenToldTo(dc => dc.GetPetTypes()).Return(Task.FromResult<IEnumerable<PetType>>( new List<PetType>
                {
                    new PetType { PetTypeId = 1, Name = "Name 1" },
                    new PetType { PetTypeId = 2, Name = "Name 2" }
                }));

        };

        Because of = () => result = Subject.GetPetTypes().Result;

        private static HttpResponseMessage result;

        It is_a_valid_response = () => result.IsSuccessStatusCode.ShouldBeTrue();

        It has_a_list_of_2_pet_types = () =>
            {
                IEnumerable<PetType> petTypes;
                result.TryGetContentValue<IEnumerable<PetType>>(out petTypes);

                petTypes.ShouldNotBeNull();
                petTypes.Count().ShouldEqual(2);
            };
    }
}