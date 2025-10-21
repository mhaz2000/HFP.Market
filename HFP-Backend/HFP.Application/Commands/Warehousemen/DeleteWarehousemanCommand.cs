using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Warehousemen;

public record DeleteWarehousemanCommand(Guid Id) : ICommand;