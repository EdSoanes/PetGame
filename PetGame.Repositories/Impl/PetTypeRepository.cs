using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories.Impl
{
    public class PetTypeRepository : Repository, IPetTypeRepository
    {
        public PetTypeRepository()
            : base("PetGameDb")
        {
        }

        public async Task<PetType> GetById(long petTypeId)
        {
            var petTypes = await GetAll();
            return petTypes.FirstOrDefault(x => x.PetTypeId == petTypeId);
        }

        public async Task<IEnumerable<PetType>> GetAll()
        {
            //Would probably cache pet types
            return await BaseGetAsync<PetType>("SELECT * FROM [dbo].[PetType]");
        }
    }
}
