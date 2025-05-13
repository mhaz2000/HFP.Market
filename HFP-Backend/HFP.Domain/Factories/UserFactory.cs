using Microsoft.AspNetCore.Identity;
using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Users;

namespace HFP.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserFactory(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public User Create(Username username, string password)
        {
            var usernameValue = Username.Create(username);

            var user = new User(usernameValue);
            var passwordHash = PasswordHash.Create(password, _passwordHasher);
            user.SetPassword(passwordHash);

            return user;
        }
    }

}
