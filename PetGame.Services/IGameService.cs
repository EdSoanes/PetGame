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
        Task<User> GetUserByUserName(string userName, DateTime? now = null);
        IEnumerable<AnimalType> GetAnimalTypes();
        Task<ApiResponse<User>> CreateUser(User user);
        Task<ApiResponse<Animal>> CreateAnimal(string userName, long animalTypeId, string animalName);
        Task<ApiResponse<Animal>> FeedAnimal(string userName, long animalId);
        Task<ApiResponse<Animal>> PetAnimal(string userName, long animalId);
    }
}
