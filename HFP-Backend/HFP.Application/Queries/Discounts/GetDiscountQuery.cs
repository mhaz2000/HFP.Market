using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Discounts
{
    public record GetDiscountQuery(Guid Id) : IQuery<DiscountDto>;

}
