using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Shelves.Handlers;
public class UpdateShelvesCommandHandler : ICommandHandler<UpdateShelvesCommand>
{
    private readonly IShelfRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IShelfFactory _shelfFactory;
    public UpdateShelvesCommandHandler(IShelfRepository repository, IProductRepository productRepository, IShelfFactory shelfFactory)
    {
        _repository = repository;
        _productRepository = productRepository;
        _shelfFactory = shelfFactory;
    }
    public async Task Handle(UpdateShelvesCommand request, CancellationToken cancellationToken)
    {
        var shelves = await _repository.GetAllShelfAsync();
        if (shelves.Any())
            _repository.ClearShelves();

        var newShelves = new List<Shelf>();

        foreach (var shelve in request.Shelves)
        {
            var productList = await _productRepository.GetByIds(shelve.ProductIds);
            var newShelf = _shelfFactory.Create(shelve.Order);
            newShelf.AddProducts(productList);

            newShelves.Add(newShelf);
        }

        await _repository.AddBatchAsync(newShelves);
    }
}
