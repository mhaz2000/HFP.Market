using HFP.Application.Services;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Products.Handlers
{
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductFactory _factory;
        private readonly IProductRepository _repository;
        private readonly IProductReadService _readService;

        public CreateProductCommandHandler(IProductReadService readService, IProductRepository repository, IProductFactory factory)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.CheckExistByNameAsync(request.Name))
                throw new BusinessException("عنوان کالا تکراری است.");

            var product = _factory.Create(request.Name, request.Quantity, request.Price, request.ImageId);

            await _repository.AddAsync(product);
        }
    }
}
