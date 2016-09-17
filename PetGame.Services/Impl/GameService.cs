using PetGame.Models;
using PetGame.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services.Impl
{
    public class GameService : IGameService
    {
        private readonly IUserRepository _userRepository;

        public GameService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _userRepository.GetUserByUserName(userName);
        }

        public async Task<User> CreateUser(User user)
        {
            var existingUser = await _userRepository.GetUserByUserName(user.UserName);
            if (existingUser != null)
                return null;

            return user;
        }
    }
}
