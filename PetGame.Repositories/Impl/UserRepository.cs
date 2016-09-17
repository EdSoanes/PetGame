using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var res = await BaseGetAsync<User>("SELECT * FROM [dbo].[User] WHERE UserName = @userName", new { userName = userName });
            return res.FirstOrDefault();
        }

        public async Task<User> Save(User user)
        {
            return await BaseUpdateAsync<User>(user);
        }
    }
}
