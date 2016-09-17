using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Models
{
    public class PetType
    {
        [Key]
        public long PetTypeId { get; set; }
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int MaxHappiness { get; set; }
        public int StartingHealth { get; set; }
        public int StartingHappiness { get; set; }
        public int FeedInterval { get; set; }
        public int PettingInterval { get; set; }
        public int HealthDecreasePerMin { get; set; }
        public int HappinessDecreasePerMin { get; set; }
        public int HealthIncreasePerFeed { get; set; }
        public int HappinessIncreasePerPet { get; set; }
    }
}
