using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Users
{
    public record ChangePasswordCommand(Guid UserId, string Password, string OldPassword) : ICommand;
}
