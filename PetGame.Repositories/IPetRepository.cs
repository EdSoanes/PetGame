using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetByUserName(string userName);
        Task<Animal> GetByUserNameAndTypeId(string userName, long animalTypeId);
        Task<Animal> GetByUserNameAndAnimalId(string userName, long animalId);
        Task<Animal> Save(Animal user);
    }
}
