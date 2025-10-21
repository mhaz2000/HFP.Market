using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Interactive
{
    public record SendDoorCodeCommand(int doorCode) : ICommand;
}
