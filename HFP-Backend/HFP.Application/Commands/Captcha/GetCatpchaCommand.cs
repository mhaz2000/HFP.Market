using HFP.Application.DTO;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Captcha
{
    public record GetCatpchaCommand : ICommand<CaptchaDto>;

}
