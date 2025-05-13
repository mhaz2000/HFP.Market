using MediatR;
using HFP.Application.DTO;
using HFP.Application.Services;
using HFP.Shared.Abstractions.Commands;
using HFP.Application.Commands.Captcha;

namespace HFP.Application.Commands.Captcha.Handlers
{
    internal class GetCatpchaHandler : ICommandHandler<GetCatpchaCommand, CaptchaDto>
    {
        private readonly ICaptchaService _captchaService;

        public GetCatpchaHandler(ICaptchaService captchaService)
            => _captchaService = captchaService;

        public Task<CaptchaDto> Handle(GetCatpchaCommand request, CancellationToken cancellationToken)
        {
            var (captchaId, captchaImage) = _captchaService.GenerateCaptcha();

            return Task.FromResult(new CaptchaDto(captchaId, Convert.ToBase64String(captchaImage)));
        }
    }
}
