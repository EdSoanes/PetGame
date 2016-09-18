using Machine.Fakes;
using Machine.Specifications;
using PetGame.Models;
using PetGame.Repositories;
using PetGame.Services.Impl;
using System.Threading.Tasks;

namespace PetGame.Specs.Users
{
    [Subject("creating a user")]
    class when_creating_a_valid_new_user : WithSubject<GameService>
    {
        Establish context = () =>
        {
            The<IUserRepository>().WhenToldTo(dc => dc.GetUserByUserName("testUser")).Return(Task.FromResult<User>(null));
            The<IUserRepository>().WhenToldTo(dc => dc.Save(_newUser)).Return(Task.FromResult<User>(new User
                {
                    UserId = 1,
                    UserName = _newUser.UserName,
                    FullName = _newUser.FullName
                }));
        };

        Because of = () => result = Subject.CreateUser(_newUser).Result;

        It should_be_created = () => result.ShouldNotBeNull();

        It should_have_status_OK = () => result.StatusCode.ShouldEqual(System.Net.HttpStatusCode.OK);

        It should_have_a_new_user = () => result.Entity.ShouldNotBeNull();

        It should_have_a_new_userId = () => result.Entity.UserId.ShouldNotEqual(0);

        private static User _newUser = new User
        {
            UserName = "testUser",
            FullName = "Full Name"
        };

        private static ApiResponse<User> result;
    }
}