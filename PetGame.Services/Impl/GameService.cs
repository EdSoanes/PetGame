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
        private readonly IPetRepository _petRepository;
        private readonly IPetTypeRepository _petTypeRepository;

        public GameService(
            ISysTime sysTime,
            IUserRepository userRepository,
            IPetRepository petRepository,
            IPetTypeRepository petTypeRepository)
        {
            _sysTime = sysTime;
            _userRepository = userRepository;
            _petRepository = petRepository;
            _petTypeRepository = petTypeRepository;
        }

        public async Task<User> GetUserByUserName(string userName, DateTime? now = null)
        {
            var user =  await _userRepository.GetUserByUserName(userName);
            var pets = await _petRepository.GetPetsByUserName(userName);
            var petTypes = await GetPetTypes();
            now = now ?? _sysTime.Now;

            user.Pets = pets.ToList();
            foreach (var pet in user.Pets)
                PetOps.UpdateStatus(pet, petTypes.First(x => x.PetTypeId == pet.PetTypeId), now.Value);

            return user;
        }

        public async Task<IEnumerable<PetType>> GetPetTypes()
        {
            return await _petTypeRepository.GetAll();
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

        public async Task<ApiResponse<Pet>> CreatePet(string userName, long petTypeId, string petName)
        {
            var now = _sysTime.Now;
            var user = await GetUserByUserName(userName, now);
            var petType = await _petTypeRepository.GetById(petTypeId);
            
            var response = PetOps.CanCreateNew(user, petType, petName, _sysTime.Now);
            if (response == null)
            {
                var pet = PetOps.New(user, petType, petName, _sysTime.Now, _sysTime.Min);
                response = await SavePet(pet, petType, now);
            }

            return await Task.FromResult<ApiResponse<Pet>>(response);
        }

        public async Task<ApiResponse<Pet>> FeedPet(string userName, long petId)
        {
            var now = _sysTime.Now;

            var user = await GetUserByUserName(userName, now);
            var pet = user.Pets.FirstOrDefault(x => x.PetId == petId);
            var petType = await _petTypeRepository.GetById(pet.PetTypeId);

            PetOps.UpdateStatus(pet, petType, now);
            var response = PetOps.CanFeed(pet, petType, now);
            if (response == null)
            {
                PetOps.Feed(pet, petType, now);
                response = await SavePet(pet, petType, now);
            }

            return await Task.FromResult<ApiResponse<Pet>>(response);
        }

        public async Task<ApiResponse<Pet>> PetPet(string userName, long petId)
        {
            var now = _sysTime.Now;
            var user = await _userRepository.GetUserByUserName(userName);
            var pet = await _petRepository.GetPetByUserNameAndPetId(userName, petId);
            var petType = await _petTypeRepository.GetById(pet.PetTypeId);

            PetOps.UpdateStatus(pet, petType, now);
            var response = PetOps.CanPet(pet, petType, now);
            if (response == null)
            {
                PetOps.Pet(pet, petType, now);
                response = await SavePet(pet, petType, now);
            }

            return await Task.FromResult<ApiResponse<Pet>>(response);
        }

        private async Task<ApiResponse<Pet>> SavePet(Pet pet, PetType petType, DateTime now)
        {
            pet.LastUpdatedTime = now;
            pet = await _petRepository.Save(pet);
            PetOps.UpdateStatus(pet, petType, now);

            return new ApiResponse<Pet>(pet);
        }
    }
}
