using AutoMapper;
using ClosedXML.Excel;
using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetProfitReportExcelHandler : IQueryHandler<GetProfitReportExcelQuery, MemoryStream>
    {
        private readonly DbSet<TransactionReadModel> _transactions;
        private readonly IMapper _mapper;
        public GetProfitReportExcelHandler(ReadDbContext context, IMapper mapper)
        {
            _transactions = context.Transactions;
            _mapper = mapper;
        }

        public async Task<MemoryStream> Handle(GetProfitReportExcelQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _transactions.Include(t => t.ProductTransactions).ThenInclude(t => t.Product)
                            .Where(c => !c.IsDeleted && c.Type == TransactionType.Invoice).SelectMany(s => s.ProductTransactions).AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Product.Name, $"%{request.Search}%"));

            var report = dbQuery.AsEnumerable()
                .Select(t => new
                {
                    t.BuyTimePirce,
                    t.BuyTimePurchasePirce,
                    t.Quantity,
                    t.Product
                }).GroupBy(t => new { t.Product }).Select(t => new ProfitReportDto()
                {
                    AvailableQuantity = t.Key.Product.Quantity,
                    ProductName = t.Key.Product.Name,
                    SoldQuantity = t.Sum(p => p.Quantity),
                    Profit = t.Sum(p => p.Quantity * (p.BuyTimePirce - p.BuyTimePurchasePirce))
                });

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("گزارش فروش");
            worksheet.RightToLeft = true;

            worksheet.Cell(1, 1).Value = "نام محصول";
            worksheet.Cell(1, 2).Value = "موجودی فعلی";
            worksheet.Cell(1, 3).Value = "مجموع فروش";
            worksheet.Cell(1, 4).Value = "سود حاصل از فروش";

            int row = 2;
            foreach (var item in report)
            {
                worksheet.Cell(row, 1).Value = item.ProductName;
                worksheet.Cell(row, 2).Value = item.AvailableQuantity;
                worksheet.Cell(row, 3).Value = item.SoldQuantity;
                worksheet.Cell(row, 4).Value = item.Profit;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return await Task.FromResult(stream);

        }

    }
    internal class GetTransactionsExcelHandler : IQueryHandler<GetTransactionsExcelQuery, MemoryStream>
    {
        private readonly DbSet<TransactionReadModel> _transactions;
        private readonly IMapper _mapper;
        public GetTransactionsExcelHandler(ReadDbContext context, IMapper mapper)
        {
            _transactions = context.Transactions;
            _mapper = mapper;
        }

        public async Task<MemoryStream> Handle(GetTransactionsExcelQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = await _transactions.Include(t => t.ProductTransactions).ThenInclude(t => t.Product)
                .Where(c => !c.IsDeleted && c.Type == TransactionType.Invoice).AsNoTracking().ToListAsync();

            var data = _mapper.Map<IEnumerable<TransactionDto>>(dbQuery);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("تراکنش ها");
            worksheet.RightToLeft = true;

            worksheet.Cell(1, 1).Value = "خریدار";
            worksheet.Cell(1, 2).Value = "تاریخ";
            worksheet.Cell(1, 3).Value = "مبلغ";

            int row = 2;
            foreach (var transaction in data)
            {
                worksheet.Cell(row, 1).Value = transaction.BuyerId.ToString();
                worksheet.Cell(row, 2).Value = transaction.DateTime;
                worksheet.Cell(row, 3).Value = transaction.Price;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream;

        }
    }
}
