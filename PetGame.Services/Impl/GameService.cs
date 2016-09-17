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

        public async Task<User> GetUserByUserName(string userName)
        {
            var user =  await _userRepository.GetUserByUserName(userName);
            var pets = await _petRepository.GetPetsByUserName(userName);
            var petTypes = await GetPetTypes();
            var now = _sysTime.Now;

            user.Pets = pets.ToList();
            foreach (var pet in user.Pets)
                PetOps.UpdateStatus(pet, petTypes.First(x => x.PetTypeId == pet.PetTypeId), now);

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
            var user = await GetUserByUserName(userName);
            var petType = await _petTypeRepository.GetById(petTypeId);

            var response = PetOps.CanCreateNew(user, petType, petName, _sysTime.Now);
            if (response == null)
            {
                var pet = PetOps.New(user, petType, petName, _sysTime.Now);
                response = new ApiResponse<Pet>(pet);
            }

            return await Task.FromResult<ApiResponse<Pet>>(response);
        }

        public async Task<ApiResponse<Pet>> FeedPet(string userName, long petId)
        {
            var user = await GetUserByUserName(userName);
            var pet = user.Pets.FirstOrDefault(x => x.PetId == petId);
            var petType = await _petTypeRepository.GetById(pet.PetTypeId);

            var response = PetOps.CanFeed(pet, petType, _sysTime.Now);
            if (response == null)
            {
                PetOps.Feed(pet, petType);
                response = new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.OK);
            }

            return await Task.FromResult<ApiResponse<Pet>>(response);
        }

        public async Task<ApiResponse<Pet>> PetPet(string userName, long petId)
        {
            var user = await GetUserByUserName(userName);
            var pet = user.Pets.FirstOrDefault(x => x.PetId == petId);
            var petType = await _petTypeRepository.GetById(pet.PetTypeId);

            var response = PetOps.CanPet(pet, petType, _sysTime.Now);
            if (response == null)
            {
                PetOps.Pet(pet, petType);
                response = new ApiResponse<Pet>(pet, System.Net.HttpStatusCode.OK);
            }

            return await Task.FromResult<ApiResponse<Pet>>(response);
        }
    }
}
