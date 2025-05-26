using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.PurchaseInvoices;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Infrastructure.ModuleExtensions;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetPurchaseInvoicesHandler : IQueryHandler<GetPurchaseInvoicesQuery, PaginatedResult<PurchaseInvoiceDto>>
    {
        private readonly DbSet<PurchaseInvoiceReadModel> _purchaseInvoices;
        private readonly IMapper _mapper;
        public GetPurchaseInvoicesHandler(ReadDbContext context, IMapper mapper)
        {
            _purchaseInvoices = context.PurchaseInvoices;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PurchaseInvoiceDto>> Handle(GetPurchaseInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = _purchaseInvoices.Include(t => t.Items).Where(t => !t.IsDeleted).AsNoTracking();

            return await invoices.
                ToPaginatedResultAsync<PurchaseInvoiceReadModel, PurchaseInvoiceDto>(request.PageIndex, request.PageSize, request.SortBy ?? string.Empty, _mapper);
        }
    }
}
