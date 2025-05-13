using Microsoft.AspNetCore.Identity;
using HFP.Application.Services;
using HFP.Domain.Entities;
using HFP.Domain.Repositories;
using HFP.Domain.ValueObjects.Users;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Users.Handlers
{
    internal class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ChangePasswordHandler(IUserReadService readService, IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
        {
            _readService = readService;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var (UserId, Password, OldPassword) = command;
            if (!await _readService.ValidateUserCredentialByUserIdAsync(UserId, OldPassword))
                throw new BusinessException("نام کاربری یا رمز عبور اشتباه می‌باشد.");

            var user = await _userRepository.GetAsync(u => u.Id == UserId);
            user!.SetPassword(PasswordHash.Create(Password, _passwordHasher));

            await _userRepository.UpdateAsync(user);
        }
    }
}
