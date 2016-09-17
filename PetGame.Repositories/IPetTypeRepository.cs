using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories
{
    public interface IPetTypeRepository
    {
        Task<PetType> GetById(long petTypeId);
        Task<IEnumerable<PetType>> GetAll();
    }
}
