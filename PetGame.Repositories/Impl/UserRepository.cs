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
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository()
            : base("PetGameDb")
        {
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();

                var sql = @"SELECT * FROM [dbo].[User] WHERE UserName = @userName
                            SELECT a.* FROM [dbo].[Animal] a
                            INNER JOIN [dbo].[User] u ON u.[UserId] = a.[UserId]
                            WHERE u.[UserName] = @userName";

                using (var multi = await conn.QueryMultipleAsync(sql, new { userName }))
                {
                    var user = multi.ReadFirstOrDefault<User>();
                    if (user != null)
                        user.Animals = multi.Read<Animal>();

                    return user;
                } 

                //using (var multi = await conn.QueryMultipleAsync("GetUserByUserName", new { userName }, commandType: CommandType.StoredProcedure))
                //{
                //    var user = multi.ReadSingleOrDefault<User>();
                //    user.Animals = multi.Read<Animal>().Where(x => x.UserId == user.UserId);

                //    return user;
                //}
            }
        }

        public async Task<User> Save(User user)
        {
            return await BaseUpdateAsync<User>(user);
        }
    }
}
