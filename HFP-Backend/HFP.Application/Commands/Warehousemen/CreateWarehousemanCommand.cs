using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Warehousemen;

public record CreateWarehousemanCommand(string Name, string UId) : ICommand;
