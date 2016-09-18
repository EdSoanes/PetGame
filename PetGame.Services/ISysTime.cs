using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services
{
    public interface ISysTime
    {
        DateTime Now { get; }
        DateTime Today { get; }
        DateTime Min { get; }
    }
}
