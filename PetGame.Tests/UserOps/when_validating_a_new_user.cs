using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetGame.Models;
using PetGame.Services.Ops;
using System.Net;

namespace PetGame.Tests.UserOps
{
    [TestClass]
    public class when_validating_a_new_user
    {
        [TestMethod]
        public void user_is_missing_username()
        {
            var user = new User
            {
                FullName = "Full Name"
            };

            var response = PetGame.Services.Ops.UserOps.CanCreateUser(user);
            Assert.IsNotNull(response);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(response.Reason, "No user name provided");
        }
    }
}
