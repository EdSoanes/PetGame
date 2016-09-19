using PetGame.Models;
using PetGame.Repositories;
using PetGame.Services.Ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetGame.Services.Impl
{
    public class GameService : IGameService
    {
        private readonly ISysTime _sysTime;
        private readonly IUserRepository _userRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IAnimalTypeRepository _animalTypeRepository;

        public GameService(
            ISysTime sysTime,
            IUserRepository userRepository,
            IAnimalRepository animalRepository,
            IAnimalTypeRepository animalTypeRepository)
        {
            _sysTime = sysTime;
            _userRepository = userRepository;
            _animalRepository = animalRepository;
            _animalTypeRepository = animalTypeRepository;
        }

        public async Task<User> GetUserByUserName(string userName, DateTime? now = null)
        {
            var user =  await _userRepository.GetUserByUserName(userName);
            var animals = await _animalRepository.GetByUserName(userName);

            if (user != null && animals != null)
            {
                var animalTypes = GetAnimalTypes();
                now = now ?? _sysTime.Now;

                user.Animals = animals.ToList();
                foreach (var animal in user.Animals)
                    AnimalOps.UpdateStatus(animal, animalTypes.First(x => x.AnimalTypeId == animal.AnimalTypeId), now.Value);
            }

            return user;
        }

        public IEnumerable<AnimalType> GetAnimalTypes()
        {
            return _animalTypeRepository.GetAll();
        }

        public async Task<ApiResponse<User>> CreateUser(User user)
        {
            var response = UserOps.CanCreateUser(user);
            if (response == null)
            {
                var newUser = await _userRepository.GetUserByUserName(user.UserName);
                if (newUser == null)
                    newUser = await _userRepository.Save(user);

                response = new ApiResponse<User>(newUser, System.Net.HttpStatusCode.OK);
            }

            return await Task.FromResult<ApiResponse<User>>(response);
        }

        public async Task<ApiResponse<Animal>> CreateAnimal(string userName, long animalTypeId, string animalName)
        {
            var now = _sysTime.Now;
            var user = await GetUserByUserName(userName, now);
            var animalType = _animalTypeRepository.GetById(animalTypeId);
            
            var response = AnimalOps.CanCreateNew(user, animalType, animalName, _sysTime.Now);
            if (response == null)
            {
                var animal = AnimalOps.New(user, animalType, animalName, _sysTime.Now, _sysTime.Min);
                response = await SaveAnimal(animal, animalType, now);
            }

            return await Task.FromResult<ApiResponse<Animal>>(response);
        }

        public async Task<ApiResponse<Animal>> FeedAnimal(string userName, long animalId)
        {
            var now = _sysTime.Now;

            var user = await GetUserByUserName(userName, now);
            var animals = user.Animals.FirstOrDefault(x => x.AnimalId == animalId);
            var animalType = _animalTypeRepository.GetById(animals.AnimalTypeId);

            AnimalOps.UpdateStatus(animals, animalType, now);
            var response = AnimalOps.CanFeed(animals, animalType, now);
            if (response == null)
            {
                AnimalOps.Feed(animals, animalType, now);
                response = await SaveAnimal(animals, animalType, now);
            }

            return await Task.FromResult<ApiResponse<Animal>>(response);
        }

        public async Task<ApiResponse<Animal>> PetAnimal(string userName, long animalId)
        {
            var now = _sysTime.Now;
            var user = await _userRepository.GetUserByUserName(userName);
            var animal = await _animalRepository.GetByUserNameAndAnimalId(userName, animalId);
            var animalType = _animalTypeRepository.GetById(animal.AnimalTypeId);

            AnimalOps.UpdateStatus(animal, animalType, now);
            var response = AnimalOps.CanPet(animal, animalType, now);
            if (response == null)
            {
                AnimalOps.Pet(animal, animalType, now);
                response = await SaveAnimal(animal, animalType, now);
            }

            return await Task.FromResult<ApiResponse<Animal>>(response);
        }

        private async Task<ApiResponse<Animal>> SaveAnimal(Animal animal, AnimalType animalType, DateTime now)
        {
            animal.LastUpdatedTime = now;
            animal = await _animalRepository.Save(animal);
            AnimalOps.UpdateStatus(animal, animalType, now);

            return new ApiResponse<Animal>(animal);
        }
    }
}
