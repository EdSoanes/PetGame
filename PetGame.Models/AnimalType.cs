using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Models
{
    public class AnimalType
    {
        [Key]
        public long AnimalTypeId { get; set; }
        public string Name { get; set; }
        public int MaxHunger { get; set; }
        public int MaxHappiness { get; set; }
        public int FeedInterval { get; set; }
        public int PettingInterval { get; set; }
        public int HungerIncreasePerMin { get; set; }
        public int HappinessDecreasePerMin { get; set; }
        public int HungerDecreasePerFeed { get; set; }
        public int HappinessIncreasePerPet { get; set; }
    }
}
