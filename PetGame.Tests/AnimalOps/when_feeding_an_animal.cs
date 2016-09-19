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
    public class when_feeding_an_animal
    {

        [TestMethod]
        public void can_user_feed_an_animal_that_was_recently_fed()
        {
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 50,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00),
                Happiness = 10
            };

            var animalType = EntityFactory.CyclopsType();

            var response = Op.AnimalOps.CanFeed(animal, animalType, new DateTime(2000, 01, 01, 12, 00, 01));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "You can't feed your animal more food yet!");
        }

        [TestMethod]
        public void can_user_feed_an_animal_that_was_not_recently_fed()
        {
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 50,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00),
                Happiness = 10
            };

            var animalType = EntityFactory.CyclopsType();

            var response = Op.AnimalOps.CanFeed(animal, animalType, new DateTime(2000, 01, 01, 12, 06, 00));

            Assert.IsNull(response);
        }

        public void can_user_feed_an_animal_that_is_full_up()
        {
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 100,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00)
            };

            var animalType = EntityFactory.CyclopsType();

            var response = Op.AnimalOps.CanFeed(animal, animalType, new DateTime(2000, 01, 01, 12, 06, 00));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "Your animal is full");
        }

        [TestMethod]
        public void user_feeds_an_animal_that_was_not_recently_fed()
        {
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 50,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00),
                Happiness = 10
            };

            var animalType = EntityFactory.CyclopsType();

            var response = Op.AnimalOps.CanFeed(animal, animalType, new DateTime(2000, 01, 01, 12, 06, 00));

            Assert.IsNull(response);
        }
    }
}
