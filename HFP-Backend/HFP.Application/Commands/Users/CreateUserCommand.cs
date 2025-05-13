using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Users
{
    public record CreateUserCommand(string Username, string Password, Guid CaptchaId, string CaptchaCode) : ICommand;
}
