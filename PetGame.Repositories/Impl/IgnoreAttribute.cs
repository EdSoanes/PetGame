using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetGame.Repositories.Impl
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IgnoreAttribute : System.Attribute
    {
        public IgnoreAttribute()
        {
        }
    }
}
