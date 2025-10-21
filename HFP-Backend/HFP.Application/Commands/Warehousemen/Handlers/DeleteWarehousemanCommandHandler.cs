using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Warehousemen.Handlers;

internal class DeleteWarehousemanCommandHandler : ICommandHandler<DeleteWarehousemanCommand>
{
    private readonly IWarehousemanRepository _repository;

    public DeleteWarehousemanCommandHandler(IWarehousemanRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteWarehousemanCommand request, CancellationToken cancellationToken)
    {
        var waresaleman = await _repository.GetAsync(c=> c.Id == request.Id);

        if (waresaleman is null)
            throw new BusinessException("انبار دار یافت نشد.");

        await _repository.DeleteAsync(waresaleman.Id);
    }
}