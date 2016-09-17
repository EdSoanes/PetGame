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
        Task<IEnumerable<PetType>> GetPetTypes();
        Task<ApiResponse<User>> CreateUser(User user);
        Task<ApiResponse<Pet>> CreatePet(string userName, long petTypeId, string petName);
        Task<ApiResponse<Pet>> FeedPet(string userName, long petId);
        Task<ApiResponse<Pet>> PetPet(string userName, long petId);
    }
}
