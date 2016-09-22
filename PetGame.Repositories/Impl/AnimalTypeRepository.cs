using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories.Impl
{
    public class AnimalTypeRepository : Repository, IAnimalTypeRepository
    {
        //Naive caching, but no need to keep fetching this from the DB
        private object _lockObject = new object();
        private IEnumerable<AnimalType> _animalTypes;

        public AnimalTypeRepository()
            : base("PetGameDb")
        {
        }

        public AnimalType GetById(long animalTypeId)
        {
            var animalTypes = GetAll();
            return animalTypes.FirstOrDefault(x => x.AnimalTypeId == animalTypeId);
        }

        public IEnumerable<AnimalType> GetAll()
        {
            lock (_lockObject)
            {
                if (_animalTypes == null)
                    _animalTypes = BaseGet<AnimalType>("SELECT * FROM [dbo].[AnimalType]");
            }

            return _animalTypes;
        }
    }
}
