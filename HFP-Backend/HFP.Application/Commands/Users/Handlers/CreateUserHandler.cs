using HFP.Application.Services;
using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Users.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserReadService _readService;
        private readonly IUserFactory _factory;
        private readonly IUserRepository _repository;
        private readonly ICaptchaService _captchaService;
        private readonly IRoleReadService _roleReadService;

        public CreateUserHandler(IUserFactory factory, IUserReadService readService, IUserRepository repository,
            ICaptchaService captchaService, IRoleReadService roleReadService)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _captchaService = captchaService;
            _roleReadService = roleReadService; 
        }

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var (Username, PhoneNumber, CaptchaId, CaptchaCode) = command;

            if (!_captchaService.ValidateCaptcha(CaptchaId.ToString(), CaptchaCode))
                throw new BusinessException("کپچا صحیح نمی‌باشد.");

            if (await _readService.ExistsByUserNameAsync(Username))
                throw new BusinessException("کاربری قبلا با این نام ثبت شده است.");


            var roleId = await _roleReadService.GetRoleIdByNameAsync("User");

            var user = _factory.Create(Username, PhoneNumber);

            user.AddRole(new Role(roleId, "User"));

            await _repository.AddAsync(user);
        }
    }
}
