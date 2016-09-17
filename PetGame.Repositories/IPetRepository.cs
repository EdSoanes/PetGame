using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetPetsByUserName(string userName);
        Task<Pet> GetPetByUserNameAndTypeId(string userName, long petTypeId);
        Task<Pet> Save(Pet user);
    }
}
