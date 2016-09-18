using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Models
{
    public class Pet
    {
        [Key]
        public long PetId { get; set; }
        public long UserId { get; set; }
        public long PetTypeId { get; set; }
        public string Name { get; set; }
        public int Hunger { get; set; }
        public DateTime LastFeedTime { get; set; }
        public int Happiness { get; set; }
        public DateTime LastPetTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        [RepoIgnore]
        public string HungerText { get; set; }

        [RepoIgnore]
        public string HappinessText { get; set; }

        [RepoIgnore]
        public bool IsDead { get; set; }
    }
}
