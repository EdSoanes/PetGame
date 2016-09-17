using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserName(string userName);
    }
}
