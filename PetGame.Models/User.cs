using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetGame.Models
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        [RepoIgnore]
        public IEnumerable<Pet> Pets { get; set; }
    }
}