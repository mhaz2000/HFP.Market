using HFP.Application.Services;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Consumers.Handlers;
internal class CreateConsumerCommandHandler : ICommandHandler<CreateConsumerCommand>
{
    private readonly IConsumerFactory _factory;
    private readonly IConsumerReadService _service;
    private readonly IConsumerRepository _repository;

    public CreateConsumerCommandHandler(IConsumerFactory factory, IConsumerReadService service, IConsumerRepository repository)
    {
        _repository = repository;
        _factory = factory;
        _service = service;
    }

    public async Task Handle(CreateConsumerCommand request, CancellationToken cancellationToken)
    {
        if (await _service.CheckExistByUIdAsync(request.UId))
            throw new BusinessException("شناسه انباردار تکراری است.");

        if (await _service.CheckExistByNameAsync(request.Name))
            throw new BusinessException("نام انباردار تکراری است.");

        var consumer = _factory.Create(request.Name, request.UId, request.IsWarehouseman);
        await _repository.AddAsync(consumer);
    }
}
