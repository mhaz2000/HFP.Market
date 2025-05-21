using HFP.Application.Services;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;
using System.Security.Claims;

namespace HFP.Application.Commands.Authentication.Handlers
{
    public class CredentialLoginHandler : ICommandHandler<CredentialLoginCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserReadService _readService;
        private readonly IRoleReadService _roleReadService;
        private readonly ICaptchaService _captchaService;

        public CredentialLoginHandler(ITokenService tokenService, IUserReadService readService, ICaptchaService captchaService, IRoleReadService roleReadService)
        {
            _readService = readService;
            _tokenService = tokenService;
            _captchaService = captchaService;
            _roleReadService = roleReadService;
        }

        public async Task<string> Handle(CredentialLoginCommand command, CancellationToken cancellationToken)
        {
            //var (Username, Password, CaptchaCode, CaptchaId) = command;
            var (Username, Password) = command;

            //if (!_captchaService.ValidateCaptcha(CaptchaId.ToString(), CaptchaCode))
            //    throw new BusinessException("کپچا صحیح نمی‌باشد.");

            var userId = await _readService.ValidateUserCredentialByUsernameAsync(Username, Password);
            if (userId is null)
                throw new BusinessException("نام کاربری یا رمز عبور اشتباه می‌باشد.");

            var userRoleName = await _roleReadService.GetUserRoleNameAsync(userId.Value);
            if (string.IsNullOrEmpty(userRoleName))
                throw new BusinessException("نقش کاربر یافت نشد.");

            return _tokenService.GenerateToken(userId.Value.ToString(), new List<Claim> { new("role", userRoleName) });
        }
    }
}
