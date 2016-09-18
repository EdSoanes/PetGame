using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Op = PetGame.Services.Ops;

namespace PetGame.Tests.PetOps
{
    [TestClass]
    public class when_managing_a_pet
    {
        [TestMethod]
        public void user_has_no_existing_pets()
        {
            var user = new User
            {
                UserName = "Test",
                FullName = "Full Name",
                Pets = Enumerable.Empty<Pet>()
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
            };

            var response = Op.PetOps.CanCreateNew(user, petType, "New Pet Name", new DateTime(2000, 01, 01));

            Assert.IsNull(response);
        }

        [TestMethod]
        public void user_has_existing_pet_of_type()
        {
            var user = new User
            {
                UserId = 1,
                UserName = "Test",
                FullName = "Full Name",
                Pets = new List<Pet>
                {
                    new Pet { PetId = 1, UserId = 1, PetTypeId = 1, Happiness = 10, Hunger = 10 }
                }
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
            };

            var response = Op.PetOps.CanCreateNew(user, petType, "New Pet Name", new DateTime(2000, 01, 01));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "You already have that kind of pet");
        }

        [TestMethod]
        public void can_user_feed_a_pet_that_was_recently_fed()
        {
            var pet = new Pet 
            { 
                PetId = 1, 
                UserId = 1, 
                PetTypeId = 1,
                Hunger = 50,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00),
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            var response = Op.PetOps.CanFeed(pet, petType, new DateTime(2000, 01, 01, 12, 00, 01));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "You can't feed your pet more food yet!");
        }

        [TestMethod]
        public void can_user_feed_a_pet_that_was_not_recently_fed()
        {
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 50,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00),
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            var response = Op.PetOps.CanFeed(pet, petType, new DateTime(2000, 01, 01, 12, 06, 00));

            Assert.IsNull(response);
        }

        public void can_user_feed_a_pet_that_is_full_up()
        {
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 100,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00)
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            var response = Op.PetOps.CanFeed(pet, petType, new DateTime(2000, 01, 01, 12, 06, 00));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "Your pet is full");
        }

        [TestMethod]
        public void user_feeds_a_pet_that_was_not_recently_fed()
        {
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 50,
                LastFeedTime = new DateTime(2000, 01, 01, 12, 00, 00),
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            var response = Op.PetOps.CanFeed(pet, petType, new DateTime(2000, 01, 01, 12, 06, 00));

            Assert.IsNull(response);
        }
    }
}
