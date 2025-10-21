using HFP.Application.Services;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Warehousemen.Handlers;
internal class CreateWarehousemanCommandHandler : ICommandHandler<CreateWarehousemanCommand>
{
    private readonly IWarehousemanFactory _factory;
    private readonly IWarehousemanReadService _service;
    private readonly IWarehousemanRepository _repository;

    public CreateWarehousemanCommandHandler(IWarehousemanFactory factory, IWarehousemanReadService service, IWarehousemanRepository repository)
    {
        _repository = repository;
        _factory = factory;
        _service = service;
    }

    public async Task Handle(CreateWarehousemanCommand request, CancellationToken cancellationToken)
    {
        if (await _service.CheckExistByUIdAsync(request.UId))
            throw new BusinessException("شناسه انباردار تکراری است.");

        if (await _service.CheckExistByNameAsync(request.Name))
            throw new BusinessException("نام انباردار تکراری است.");

        var waresaleman = _factory.Create(request.Name, request.UId);
        await _repository.AddAsync(waresaleman);
    }
}
