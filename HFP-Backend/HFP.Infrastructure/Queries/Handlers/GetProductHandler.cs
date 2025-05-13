using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Products;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Exceptions;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetProductHandler : IQueryHandler<GetProductQuery, ProductDto>
    {
        private readonly DbSet<ProductReadModel> _products;
        private readonly IMapper _mapper;

        public GetProductHandler(ReadDbContext context, IMapper mapper)
        {
            _products = context.Products;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _products.FirstOrDefaultAsync(pr => pr.Id == query.Id);
            if (product is null)
                throw new BusinessException("کالای مورد نظر یافت نشد.");

            return _mapper.Map<ProductDto>(product);
        }
    }
}
