using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetGame.Models;
using Op = PetGame.Services.Ops;

namespace PetGame.Tests.PetOps
{
    [TestClass]
    public class when_updating_a_pet_status_hunger
    {
        [TestMethod]
        public void this_pet_is_neutral()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 50,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[2]);
        }

        [TestMethod]
        public void this_pet_is_dead()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 100,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsTrue(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[4]);
        }

        [TestMethod]
        public void this_pet_is_stuffed()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 0,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[0]);
        }

        [TestMethod]
        public void this_pet_is_still_stuffed()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 1,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[0]);
        }

        [TestMethod]
        public void this_pet_is_STILL_stuffed()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 24,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[0]);
        }

        [TestMethod]
        public void this_pet_is_satisfied()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 25,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[1]);
        }

        [TestMethod]
        public void this_pet_is_stuffed_after_1_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-1);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[0]);
            Assert.AreEqual(pet.Hunger, 1);
        }

        [TestMethod]
        public void this_pet_is_stuffed_after_10_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-10);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[0]);
            Assert.AreEqual(pet.Hunger, 10);
        }

        [TestMethod]
        public void this_pet_is_satisfied_after_25_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-25);
            var pet = new Pet
            {
                PetId = 1,
                UserId = 1,
                PetTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 10
            };

            var petType = new PetType
            {
                PetTypeId = 1,
                Name = "PetType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.PetOps.UpdateStatus(pet, petType, now);

            Assert.IsFalse(pet.IsDead);
            Assert.AreEqual(pet.HungerText, Op.PetOps.HungerTexts[1]);
            Assert.AreEqual(pet.Hunger, 25);
        }
    }
}
