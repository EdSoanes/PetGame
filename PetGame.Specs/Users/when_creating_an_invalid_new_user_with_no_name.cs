using Machine.Fakes;
using Machine.Specifications;
using PetGame.Models;
using PetGame.Repositories;
using PetGame.Services.Impl;
using System.Threading.Tasks;

namespace PetGame.Specs.Users
{
    [Subject("creating an invalid user")]
    class when_creating_an_invalid_new_user_with_no_name : WithSubject<GameService>
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

        It should_have_a_response = () => result.ShouldNotBeNull();

        It should_have_status_BadRequest = () => result.StatusCode.ShouldEqual(System.Net.HttpStatusCode.BadRequest);

        It should_have_a_reason = () => result.Reason.ShouldEqual("No full name provided");

        It should_not_return_a_user_entity = () => result.Entity.ShouldBeNull();

        private static User _newUser = new User
        {
            UserName = "testUser"
        };

        private static ApiResponse<User> result;
    }
}