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
        public static ApiResponse<Pet> CanCreateNew(User user, PetType petType, string name, DateTime now)
        {
            var msg = string.Empty;
            if (petType == null)
                return new ApiResponse<Pet>(null, System.Net.HttpStatusCode.BadRequest, "You need to choose which pet type you want");

            var pet = user.Pets.FirstOrDefault(x => x.PetTypeId == petType.PetTypeId);
            if (pet != null)
                return new ApiResponse<Pet>(null, System.Net.HttpStatusCode.BadRequest, "You already have that kind of pet");

            return null;
        }

        public static Pet New(User user, PetType petType, string name, DateTime now)
        {
            var res = new Pet
            {
                UserId = user.UserId,
                PetTypeId = petType.PetTypeId,
                Name = name,
                Health = petType.StartingHealth,
                LastFeedTime = now,
                Happiness = petType.StartingHappiness,
                LastPetTime = now
            };

            return res;
        }

        public static ApiResponse<Pet> CanFeed(Pet pet, PetType petType, DateTime now)
        {
            if (pet.LastFeedTime.AddMinutes(petType.FeedInterval) > now)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet isn't hungry!");

            if (pet.Health == 0)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet is dead from starvation");

            return null;
        }

        public static void Feed(Pet pet, PetType petType)
        {
            pet.Health += petType.HealthIncreasePerFeed;
        }

        public static ApiResponse<Pet> CanPet(Pet pet, PetType petType, DateTime now)
        {
            if (pet.LastPetTime.AddMinutes(petType.PettingInterval) > now)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet has been petted enough!");

            if (pet.Happiness == 0)
                return new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.BadRequest, "Your pet is dead from a broken heart");

            return null;
        }

        public static void Pet(Pet pet, PetType petType)
        {
            pet.Happiness += petType.HappinessIncreasePerPet;
        }

        public static void UpdateStatus(Pet pet, PetType petType, DateTime now)
        {
            var periodSinceLastFeed = (now - pet.LastFeedTime).TotalMinutes;
            pet.Health -= Math.Max(Convert.ToInt32(Math.Floor(petType.HealthDecreasePerMin * periodSinceLastFeed)), 0);

            var periodSinceLastPet = (now - pet.LastPetTime).TotalMinutes;
            pet.Happiness -= Math.Max(Convert.ToInt32(Math.Floor(petType.HappinessDecreasePerMin * periodSinceLastPet)), 0);
        }
    }
}
