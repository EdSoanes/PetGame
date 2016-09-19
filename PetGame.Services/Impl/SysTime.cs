using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services.Impl
{
    public class SysTime : ISysTime
    {
        public DateTime Now 
        { 
            get { return DateTime.Now; } 
        }

        public DateTime Today
        {
            get { return DateTime.Today; } 
        }

        public DateTime Min
        {
            get { return new DateTime(1970, 01, 01); }
        }
    }
}
