using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetGame.Models;
using Op = PetGame.Services.Ops;

namespace PetGame.Tests.AnimalOps
{
    [TestClass]
    public class when_updating_an_animals_status_hunger
    {
        [TestMethod]
        public void this_animal_is_neutral()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 50,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[2]);
        }

        [TestMethod]
        public void this_animal_is_dead()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 100,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsTrue(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[4]);
        }

        [TestMethod]
        public void this_animal_is_stuffed()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 0,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[0]);
        }

        [TestMethod]
        public void this_animal_is_still_stuffed()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 1,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[0]);
        }

        [TestMethod]
        public void this_animal_is_STILL_stuffed()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 24,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[0]);
        }

        [TestMethod]
        public void this_animal_is_satisfied()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 25,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[1]);
        }

        [TestMethod]
        public void this_animal_is_stuffed_after_1_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-1);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[0]);
            Assert.AreEqual(animal.Hunger, 1);
        }

        [TestMethod]
        public void this_animal_is_stuffed_after_10_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-10);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[0]);
            Assert.AreEqual(animal.Hunger, 10);
        }

        [TestMethod]
        public void this_animal_is_satisfied_after_25_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-25);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 10
            };

            var animalType = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "AnimalType 1",
                HungerIncreasePerMin = 1,   //Hunger will increase by one per minute
                HungerDecreasePerFeed = 10, //Health with decrease by 10 per feed
                MaxHunger = 100,
                MaxHappiness = 20,
                FeedInterval = 5            //Can't be fed more than once per minute
            };

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.HungerText, Op.AnimalOps.HungerTexts[1]);
            Assert.AreEqual(animal.Hunger, 25);
        }
    }
}
