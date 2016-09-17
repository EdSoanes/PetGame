using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGame.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}