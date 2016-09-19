using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetGame.Models;
using Op = PetGame.Services.Ops;

namespace PetGame.Tests.AnimalOps
{
    [TestClass]
    public class when_updating_an_animals_status_happiness
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
                Happiness = 25
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[2], animal.HappinessText);
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
                Hunger = 50,
                LastFeedTime = now,
                LastPetTime = now,
                LastUpdatedTime = now,
                Happiness = 0
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsTrue(animal.IsDead);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[0], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_ecstatic()
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
                Happiness = 40
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[4], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_still_ecstatic()
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
                Happiness = 49
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[4], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_STILL_ecstatic()
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
                Happiness = 48
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[4], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_happy()
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
                Happiness = 37
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[3], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_ecstatic_after_1_min()
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
                Happiness = 50
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(49, animal.Happiness);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[4], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_ecstatic_after_10_min()
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
                Happiness = 50
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(40, animal.Happiness);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[4], animal.HappinessText);
        }

        [TestMethod]
        public void this_animal_is_happy_after_20_min()
        {
            var now = new DateTime(2000, 01, 01, 12, 00, 00);
            var then = now.AddMinutes(-20);
            var animal = new Animal
            {
                AnimalId = 1,
                UserId = 1,
                AnimalTypeId = 1,
                Hunger = 0,
                LastFeedTime = then,
                LastPetTime = then,
                LastUpdatedTime = then,
                Happiness = 50
            };

            var animalType = EntityFactory.AutomatonType();

            Op.AnimalOps.UpdateStatus(animal, animalType, now);

            Assert.IsFalse(animal.IsDead);
            Assert.AreEqual(animal.Happiness, 30);
            Assert.AreEqual(Op.AnimalOps.HappinessTexts[3], animal.HappinessText);
        }
    }
}
