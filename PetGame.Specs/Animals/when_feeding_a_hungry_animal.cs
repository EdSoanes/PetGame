using Machine.Fakes;
using Machine.Specifications;
using PetGame.Controllers;
using PetGame.Models;
using PetGame.Repositories;
using PetGame.Services;
using PetGame.Services.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace PetGame.Specs.Animals
{
    class when_feeding_a_hungry_animal : WithSubject<GameService>
    {
        Establish context = () =>
        {
            //WebApiConfig.Register(Subject.Configuration);
            The<ISysTime>().WhenToldTo(x => x.Today).Return(new System.DateTime(2000, 01, 01));
            The<ISysTime>().WhenToldTo(x => x.Now).Return(new System.DateTime(2000, 01, 01, 12, 01, 01));
            The<ISysTime>().WhenToldTo(x => x.Min).Return(new System.DateTime(1970, 01, 01));

            The<IAnimalTypeRepository>().WhenToldTo(x => x.GetAll()).Return(
                new List<AnimalType>
                {
                    new AnimalType
                    {
                        AnimalTypeId = 1,
                        Name = "Name 1",
                        MaxHunger = 100,
                        HungerDecreasePerFeed = 10,
                        HungerIncreasePerMin = 1,
                        FeedInterval = 1,
                        MaxHappiness = 100,
                        HappinessDecreasePerMin = 1,
                        HappinessIncreasePerPet = 10,
                        PettingInterval = 1
                    }
                });

            The<IAnimalTypeRepository>().WhenToldTo(x => x.GetById(Param.IsAny<long>())).Return(
                new AnimalType 
                { 
                    AnimalTypeId = 1, 
                    Name = "Name 1",
                    MaxHunger = 100,
                    HungerDecreasePerFeed = 10,
                    HungerIncreasePerMin = 1,
                    FeedInterval = 1,
                    MaxHappiness = 100,
                    HappinessDecreasePerMin = 1,
                    HappinessIncreasePerPet = 10,
                    PettingInterval = 1
                });

            The<IUserRepository>().WhenToldTo(x => x.GetUserByUserName(Param.IsAny<string>())).Return(
                Task.FromResult<User>(
                    new User
                    {
                        UserId = 1,
                        UserName = "UserName",
                        FullName = "FullName"
                    }));

            The<IAnimalRepository>().WhenToldTo(dc => dc.GetByUserName(Param.IsAny<string>())).Return(
                Task.FromResult<IEnumerable<Animal>>(
                    new List<Animal> 
                    {
                        animal
                    }));

            The<IAnimalRepository>().WhenToldTo(dc => dc.Save(Param.IsAny<Animal>())).Return(Task.FromResult<Animal>(animal));

        };

        private static Animal animal = new Animal
        {
            AnimalId = 1,
            UserId = 1,
            AnimalTypeId = 1,
            Hunger = 75,
            Happiness = 25,
            LastFeedTime = new System.DateTime(1970, 01, 01),
            LastPetTime = new System.DateTime(1970, 01, 01),
            LastUpdatedTime = new System.DateTime(2000, 01, 01, 12, 01, 01)
        };

        private static ApiResponse<Animal> result;

        Because of = () => result = Subject.FeedAnimal("UserName", 1).Result;

        It is_a_valid_response = () => result.StatusCode.ShouldEqual(HttpStatusCode.OK);

        It should_return_an_animal = () =>  result.Entity.ShouldNotBeNull();

        It should_be_less_hungry_by_10 = () => result.Entity.Hunger.ShouldEqual(65);

        It should_be_satisfied = () => result.Entity.HungerText.ShouldEqual("Neutral");
    }
}