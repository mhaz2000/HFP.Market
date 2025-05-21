using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Discounts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Exceptions;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetDiscountHandler : IQueryHandler<GetDiscountQuery, DiscountDto>
    {
        private readonly DbSet<DiscountReadModel> _discounts;
        private readonly IMapper _mapper;

        public GetDiscountHandler(ReadDbContext context, IMapper mapper)
        {
            _discounts = context.Discounts;
            _mapper = mapper;
        }

        public async Task<DiscountDto> Handle(GetDiscountQuery query, CancellationToken cancellationToken)
        {
            var discount = await _discounts.FirstOrDefaultAsync(pr => pr.Id == query.Id);
            if (discount is null)
                throw new BusinessException("تخفیف مورد نظر یافت نشد.");

            return _mapper.Map<DiscountDto>(discount);
        }
    }
}
