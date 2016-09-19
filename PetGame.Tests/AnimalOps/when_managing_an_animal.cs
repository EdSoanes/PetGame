using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Op = PetGame.Services.Ops;

namespace PetGame.Tests.AnimalOps
{
    [TestClass]
    public class when_managing_an_animal
    {
        [TestMethod]
        public void user_has_no_existing_animals()
        {
            var user = new User
            {
                UserName = "Test",
                FullName = "Full Name",
                Animals = Enumerable.Empty<Animal>()
            };

            var animalType = EntityFactory.CyclopsType();

            var response = Op.AnimalOps.CanCreateNew(user, animalType, "New Animal Name", new DateTime(2000, 01, 01));

            Assert.IsNull(response);
        }

        [TestMethod]
        public void user_has_existing_animal_of_type()
        {
            var user = new User
            {
                UserId = 1,
                UserName = "Test",
                FullName = "Full Name",
                Animals = new List<Animal>
                {
                    new Animal { AnimalId = 1, UserId = 1, AnimalTypeId = 1, Happiness = 10, Hunger = 10 }
                }
            };

            var animalType = EntityFactory.CyclopsType();

            var response = Op.AnimalOps.CanCreateNew(user, animalType, "New Animal Name", new DateTime(2000, 01, 01));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "You already have that kind of animal");
        }
    }
}
