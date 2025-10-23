using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Consumers;

public record DeleteConsumerCommand(Guid Id) : ICommand;