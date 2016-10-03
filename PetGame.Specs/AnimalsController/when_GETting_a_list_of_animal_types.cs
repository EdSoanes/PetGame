using Machine.Fakes;
using Machine.Specifications;
using PetGame.App_Start;
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

namespace PetGame.Specs.AnimalsController
{
    [Subject("creating a user")]
    class when_GETting_a_list_of_animal_types : WithSubject<PetGame.Controllers.AnimalsController>
    {
        Establish context = () =>
        {
            Subject.Request = new HttpRequestMessage();
            Subject.Configuration = new HttpConfiguration();

            Startup.Register(Subject.Configuration);

            The<IGameService>().WhenToldTo(dc => dc.GetAnimalTypes()).Return( new List<AnimalType>
            {
                new AnimalType { AnimalTypeId = 1, Name = "Name 1" },
                new AnimalType { AnimalTypeId = 2, Name = "Name 2" }
            });
        };

        Because of = () => result = Subject.GetAnimalTypes().Result;

        private static HttpResponseMessage result;

        It is_a_valid_response = () => result.IsSuccessStatusCode.ShouldBeTrue();

        It has_a_list_of_2_animal_types = () =>
            {
                IEnumerable<AnimalType> animalTypes;
                result.TryGetContentValue<IEnumerable<AnimalType>>(out animalTypes);

                animalTypes.ShouldNotBeNull();
                animalTypes.Count().ShouldEqual(2);
            };
    }
}