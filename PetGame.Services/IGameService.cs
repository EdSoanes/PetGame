using PetGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services
{
    public interface IGameService
    {
        Task<User> GetUserByUserName(string userName);
        Task<User> CreateUser(User user);
    }
}
