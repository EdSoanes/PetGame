using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PetGame.Repositories.Impl
{
    public class AnimalRepository : Repository, IAnimalRepository
    {
        public AnimalRepository()
            : base("PetGameDb")
        {
        }

        public async Task<IEnumerable<Animal>> GetByUserName(string userName)
        {
            var sql = @"SELECT p.* FROM [dbo].[Animal] p
                        INNER JOIN [dbo].[User] u ON u.UserId = p.UserId
                        WHERE u.UserName = @userName";

            //return await BaseGetAsync<Animal>(sql, new { userName = userName });

            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();
                var res = await conn.QueryAsync<Animal>(sql, new { userName });

                return res;
            }
        }

        public async Task<Animal> GetByUserNameAndTypeId(string userName, long animalTypeId)
        {
            var sql = @"SELECT p.* FROM [dbo].[Animal] p
                        INNER JOIN [dbo].[User] u ON u.UserId = p.UserId
                        WHERE u.UserName = @userName AND p.AnimalTypeId = @animalTypeId";

            //var res = await BaseGetAsync<Animal>(sql, new { userName = userName, animalTypeId = animalTypeId });
            //return res.FirstOrDefault();

            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();
                var res = await conn.QueryFirstOrDefaultAsync<Animal>(sql, new { userName });

                return res;
            }
        }

        public async Task<Animal> GetByUserNameAndAnimalId(string userName, long animalId)
        {
            var sql = @"SELECT p.* FROM [dbo].[Animal] p
                        INNER JOIN [dbo].[User] u ON u.UserId = p.UserId
                        WHERE u.UserName = @userName AND p.AnimalId = @animalId";

            var res = await BaseGetAsync<Animal>(sql, new { userName = userName, animalId = animalId });
            return res.FirstOrDefault();
        }

        public async Task<Animal> Save(Animal animal)
        {
            return await BaseUpdateAsync<Animal>(animal);
        }
    }
}
