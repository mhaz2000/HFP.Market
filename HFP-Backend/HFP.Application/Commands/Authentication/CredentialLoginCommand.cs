using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Authentication
{
    public record CredentialLoginCommand(string Username, string Password, string CaptchaCode, Guid CaptchaId) : ICommand<string>;
}
