using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services.Ops
{
    public static class PetOps
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

        public static ApiResponse<Pet> CanCreateNew(User user, PetType petType, string name, DateTime now)
        {
            var msg = string.Empty;
            if (petType == null)
                return new ApiResponse<Pet>(null, System.Net.HttpStatusCode.BadRequest, "You need to choose which pet type you want");

            var pet = user.Pets.FirstOrDefault(x => x.PetTypeId == petType.PetTypeId && !x.IsDead);
            if (pet != null)
                return new ApiResponse<Pet>(null, System.Net.HttpStatusCode.BadRequest, "You already have that kind of pet");

            return null;
        }

        public static Pet New(User user, PetType petType, string name, DateTime now, DateTime min)
        {
            var res = new Pet
            {
                UserId = user.UserId,
                PetTypeId = petType.PetTypeId,
                Name = name,
                Hunger = HungerNeutral(petType.MaxHunger),
                LastFeedTime = min,
                Happiness = HappinessNeutral(petType.MaxHappiness),
                LastPetTime = min,
                LastUpdatedTime = now
            };

            return res;
        }

        public static ApiResponse<Pet> CanFeed(Pet pet, PetType petType, DateTime now)
        {
            if (pet.IsDead)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet is dead!");

            if (pet.LastFeedTime.AddMinutes(petType.FeedInterval) > now)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "You can't feed your pet more food yet!");

            if (pet.Hunger == 0)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet is full");

            return null;
        }

        public static void Feed(Pet pet, PetType petType, DateTime now)
        {
            pet.Hunger = Math.Max(pet.Hunger - petType.HungerDecreasePerFeed, 0);
            pet.LastFeedTime = now;
        }

        public static ApiResponse<Pet> CanPet(Pet pet, PetType petType, DateTime now)
        {
            if (pet.IsDead)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet is dead!");

            if (pet.LastPetTime.AddMinutes(petType.PettingInterval) > now)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet has been petted enough!");

            return null;
        }

        public static void Pet(Pet pet, PetType petType, DateTime now)
        {
            pet.Happiness = Math.Max(pet.Happiness + petType.HappinessIncreasePerPet, petType.MaxHappiness);
            pet.LastPetTime = now;
        }

        public static void UpdateStatus(Pet pet, PetType petType, DateTime now)
        {
            var periodSinceLastUpdate = (now - pet.LastUpdatedTime).TotalMinutes;
            pet.Hunger = Math.Min(petType.MaxHunger, pet.Hunger + Convert.ToInt32(Math.Floor(petType.HungerIncreasePerMin * periodSinceLastUpdate)));
            pet.Happiness -= Math.Max(Convert.ToInt32(Math.Floor(petType.HappinessDecreasePerMin * periodSinceLastUpdate)), 0);

            pet.IsDead = pet.Hunger >= petType.MaxHunger || pet.Happiness == 0;

            var hungerIdx = pet.IsDead 
                ? HungerTexts.Length - 1
                : Convert.ToInt32(Math.Floor((double)pet.Hunger / (petType.MaxHunger / (HungerTexts.Length - 1))));
            pet.HungerText = HungerTexts[hungerIdx];

            var happinessIdx = pet.IsDead 
                ? 0 
                : Math.Max(0, Convert.ToInt32(Math.Ceiling((double)pet.Happiness / (petType.MaxHappiness / HappinessTexts.Length))) - 1);
            pet.HappinessText = HappinessTexts[happinessIdx];
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
