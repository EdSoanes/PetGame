﻿using Machine.Fakes;
using Machine.Specifications;
using PetGame.Models;
using PetGame.Repositories;
using PetGame.Services.Impl;
using System.Threading.Tasks;

namespace PetGame.Specs.Users
{
    [Subject("creating an existing user")]
    class when_trying_to_recreate_an_existing_user : WithSubject<GameService>
    {
        Establish context = () =>
        {
            The<IUserRepository>().WhenToldTo(dc => dc.GetUserByUserName("testUser")).Return(Task.FromResult<User>(_existingUser));
        };

        Because of = () => result = Subject.CreateUser(_newUser).Result;

        It should_be_created = () => result.ShouldNotBeNull();

        It should_have_status_OK = () => result.StatusCode.ShouldEqual(System.Net.HttpStatusCode.OK);

        It should_have_a_new_user = () => result.Entity.ShouldNotBeNull();

        It should_have_a_new_userId = () => result.Entity.ShouldBeTheSameAs(_existingUser);

        private static User _existingUser = new User
        {
            UserId = 1,
            UserName = _newUser.UserName,
            FullName = _newUser.FullName
        };

        private static User _newUser = new User
        {
            UserName = "testUser",
            FullName = "Full Name"
        };

        private static ApiResponse<User> result;
    }
}