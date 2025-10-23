using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Consumers;

public record CreateConsumerCommand(string Name, string UId, bool IsWarehouseman) : ICommand;
