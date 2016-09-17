using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetGame.Models
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class RepoIgnoreAttribute : System.Attribute
    {
        public RepoIgnoreAttribute()
        {
        }
    }
}
