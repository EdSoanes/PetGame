using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories
{
    public interface IAnimalTypeRepository
    {
        AnimalType GetById(long animalTypeId);
        IEnumerable<AnimalType> GetAll();
    }
}
