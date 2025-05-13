using HFP.Application.Services;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Products.Handlers
{
    internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductFactory _factory;
        private readonly IProductRepository _repository;
        private readonly IProductReadService _readService;

        public UpdateProductCommandHandler(IProductReadService readService, IProductRepository repository, IProductFactory factory)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(pr => pr.Id == request.Id);
            if(product is null)
                throw new BusinessException("کالا‌ی مورد نظر وجود ندارد.");

            if (await _readService.CheckExistByNameAsync(request.Name, product.Id))
                throw new BusinessException("عنوان کالا تکراری است.");

            _factory.Update(request.Name, request.Quantity, request.Price, request.ImageId, product);

            await _repository.UpdateAsync(product);
        }
    }
}
