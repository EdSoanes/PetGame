using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Tests
{
    public class EntityFactory
    {
        public static AnimalType CyclopsType()
        {
            var res = new AnimalType
            {
                AnimalTypeId = 1,
                Name = "Cyclops",
                MaxHunger = 100,
                HungerDecreasePerFeed = 20,
                HungerIncreasePerMin = 10,
                FeedInterval = 1,
                MaxHappiness = 50,
                HappinessIncreasePerPet = 0,
                HappinessDecreasePerMin = 0,
                PettingInterval = 1
            };

            return res;
        }

        public static AnimalType AutomatonType()
        {
            var res = new AnimalType
            {
                AnimalTypeId = 2,
                Name = "Automaton",
                MaxHunger = 100,
                HungerDecreasePerFeed = 0,
                HungerIncreasePerMin = 0,
                FeedInterval = 1,
                MaxHappiness = 50,
                HappinessIncreasePerPet = 10,
                HappinessDecreasePerMin = 1,
                PettingInterval = 1
            };

            return res;
        }

        public static AnimalType WerewolfType()
        {
            var res = new AnimalType
            {
                AnimalTypeId = 3,
                Name = "Werewolf",
                MaxHunger = 100,
                HungerDecreasePerFeed = 20,
                HungerIncreasePerMin = 10,
                FeedInterval = 1,
                MaxHappiness = 50,
                HappinessIncreasePerPet = 10,
                HappinessDecreasePerMin = 1,
                PettingInterval = 1
            };

            return res;
        }
    }
}


