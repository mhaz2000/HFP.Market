using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.PurchaseInvoices;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Exceptions;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetPurchaseInvoiceHandler : IQueryHandler<GetPurchaseInvoiceQuery, EditPurchaseInvoiceDto>
    {

        private readonly DbSet<PurchaseInvoiceReadModel> _purchaseInvoices;
        private readonly IMapper _mapper;

        public GetPurchaseInvoiceHandler(ReadDbContext context, IMapper mapper)
        {
            _purchaseInvoices = context.PurchaseInvoices;
            _mapper = mapper;
        }

        public async Task<EditPurchaseInvoiceDto> Handle(GetPurchaseInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _purchaseInvoices.Include(t=> t.Items).FirstOrDefaultAsync(pi => pi.Id == request.Id);
            if (invoice is null)
                throw new BusinessException("فاکتور خرید یافت نشد.");

            return _mapper.Map<EditPurchaseInvoiceDto>(invoice);
        }
    }
}
