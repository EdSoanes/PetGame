using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories.Impl
{
    public class PetRepository : Repository, IPetRepository
    {
        public PetRepository()
            : base("PetGameDb")
        {
        }

        public async Task<IEnumerable<Pet>> GetPetsByUserName(string userName)
        {
            var sql = @"SELECT p.* FROM [dbo].[Pet] p
                        INNER JOIN [dbo].[User] u ON u.UserId = p.UserId
                        WHERE u.UserName = @userName";

            return await BaseGetAsync<Pet>(sql, new { userName = userName });
        }

        public async Task<Pet> GetPetByUserNameAndTypeId(string userName, long petTypeId)
        {
            var sql = @"SELECT p.* FROM [dbo].[Pet] p
                        INNER JOIN [dbo].[User] u ON u.UserId = p.UserId
                        WHERE u.UserName = @userName AND p.PetTypeId = @petTypeId";

            var res = await BaseGetAsync<Pet>(sql, new { userName = userName, petTypeId = petTypeId });
            return res.FirstOrDefault();
        }

        public async Task<Pet> Save(Pet pet)
        {
            return await BaseUpdateAsync<Pet>(pet);
        }
    }
}
