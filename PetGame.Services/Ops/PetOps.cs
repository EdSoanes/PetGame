using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services.Ops
{
    public static class AnimalOps
    {
        public static string[] HungerTexts = 
        {
            "Stuffed",
            "Satisfied",
            "Neutral",
            "Hungry",
            "Dead"
        };

        public static string[] HappinessTexts = 
        {
            "Broken heart",
            "Miserable",
            "Neutral",
            "Happy",
            "Ecstatic"
        };

        public static ApiResponse<Animal> CanCreateNew(User user, AnimalType animalType, string name, DateTime now)
        {
            var msg = string.Empty;

            if (string.IsNullOrEmpty(name))
                return new ApiResponse<Animal>(null, System.Net.HttpStatusCode.BadRequest, "Give your animal a name!");

            if (animalType == null)
                return new ApiResponse<Animal>(null, System.Net.HttpStatusCode.BadRequest, "You need to choose which animal type you want");

            var pet = user.Animals.FirstOrDefault(x => x.AnimalTypeId == animalType.AnimalTypeId && !x.IsDead);
            if (pet != null)
                return new ApiResponse<Animal>(null, System.Net.HttpStatusCode.BadRequest, "You already have that kind of animal");

            return null;
        }

        public static Animal New(User user, AnimalType animalType, string name, DateTime now, DateTime min)
        {
            var res = new Animal
            {
                UserId = user.UserId,
                AnimalTypeId = animalType.AnimalTypeId,
                Name = name,
                Hunger = HungerNeutral(animalType.MaxHunger),
                LastFeedTime = min,
                Happiness = HappinessNeutral(animalType.MaxHappiness),
                LastPetTime = min,
                LastUpdatedTime = now
            };

            return res;
        }

        public static ApiResponse<Animal> CanFeed(Animal animal, AnimalType animalType, DateTime now)
        {
            if (animal.IsDead)
                return new ApiResponse<Animal>(animal, System.Net.HttpStatusCode.BadRequest, "Your animal is dead!");

            if (animal.LastFeedTime.AddMinutes(animalType.FeedInterval) > now)
                return new ApiResponse<Animal>(animal, System.Net.HttpStatusCode.BadRequest, "You can't feed your animal more food yet!");

            if (animal.Hunger == 0)
                return new ApiResponse<Animal>(animal, System.Net.HttpStatusCode.BadRequest, "Your animal is full");

            return null;
        }

        public static void Feed(Animal animal, AnimalType animalType, DateTime now)
        {
            animal.Hunger = Math.Max(animal.Hunger - animalType.HungerDecreasePerFeed, 0);
            animal.LastFeedTime = now;
        }

        public static ApiResponse<Animal> CanPet(Animal pet, AnimalType petType, DateTime now)
        {
            if (pet.IsDead)
                return new ApiResponse<Animal>(pet, System.Net.HttpStatusCode.BadRequest, "Your animal is dead!");

            if (pet.LastPetTime.AddMinutes(petType.PettingInterval) > now)
                return new ApiResponse<Animal>(pet, System.Net.HttpStatusCode.BadRequest, "Your animal has been petted enough!");

            return null;
        }

        public static void Pet(Animal animal, AnimalType animalType, DateTime now)
        {
            animal.Happiness = Math.Max(animal.Happiness + animalType.HappinessIncreasePerPet, animalType.MaxHappiness);
            animal.LastPetTime = now;
        }

        public static void UpdateStatus(Animal animal, AnimalType animalType, DateTime now)
        {
            var periodSinceLastUpdate = (now - animal.LastUpdatedTime).TotalMinutes;
            animal.Hunger = Math.Min(animalType.MaxHunger, animal.Hunger + Convert.ToInt32(Math.Floor(animalType.HungerIncreasePerMin * periodSinceLastUpdate)));
            animal.Happiness -= Math.Max(Convert.ToInt32(Math.Floor(animalType.HappinessDecreasePerMin * periodSinceLastUpdate)), 0);

            animal.IsDead = animal.Hunger >= animalType.MaxHunger || animal.Happiness == 0;

            var hungerIdx = animal.IsDead 
                ? HungerTexts.Length - 1
                : Convert.ToInt32(Math.Floor((double)animal.Hunger / (animalType.MaxHunger / (HungerTexts.Length - 1))));
            animal.HungerText = HungerTexts[hungerIdx];

            var happinessIdx = animal.IsDead 
                ? 0 
                : Math.Max(0, Convert.ToInt32(Math.Ceiling((double)animal.Happiness / (animalType.MaxHappiness / HappinessTexts.Length))) - 1);
            animal.HappinessText = HappinessTexts[happinessIdx];
        }

        private static int HungerNeutral(int maxHunger)
        {
            var idx = HungerTexts.ToList().IndexOf("Neutral");
            return Convert.ToInt32(Math.Ceiling(maxHunger / (HungerTexts.Length - 1) * (idx - 0.5)));
        }


        private static int HappinessNeutral(int maxHappiness)
        {
            var idx = HappinessTexts.ToList().IndexOf("Neutral");
            return Convert.ToInt32(Math.Ceiling(maxHappiness / (HungerTexts.Length - 1) * (idx + 0.5)));
        }
    }
}
