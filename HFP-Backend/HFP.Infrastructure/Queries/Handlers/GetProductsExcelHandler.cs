using AutoMapper;
using ClosedXML.Excel;
using HFP.Application.DTO;
using HFP.Application.Queries.Products;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetProductsExcelHandler : IQueryHandler<GetProductsExcelQuery, MemoryStream>
    {
        private readonly DbSet<ProductReadModel> _products;
        private readonly IMapper _mapper;

        public GetProductsExcelHandler(ReadDbContext context, IMapper mapper)
        {
            _products = context.Products;
            _mapper = mapper;

        }

        public async Task<MemoryStream> Handle(GetProductsExcelQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _products.AsQueryable();
            if (!string.IsNullOrEmpty(request.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{request.Search}%"));
            var products = dbQuery.AsNoTracking();

            var productData = _mapper.Map<IEnumerable<ProductDto>>(products);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("گزارش فروش");
            worksheet.RightToLeft = true;

            worksheet.Cell(1, 1).Value = "نام محصول";
            worksheet.Cell(1, 2).Value = "کد محصول";
            worksheet.Cell(1, 3).Value = "موجودی";
            worksheet.Cell(1, 4).Value = "مبلغ خرید";
            worksheet.Cell(1, 5).Value = "مبلغ فروش";
            worksheet.Cell(1, 6).Value = "سود حاصل از فروش";

            int row = 2;
            foreach (var item in productData)
            {
                worksheet.Cell(row, 1).Value = item.Name;
                worksheet.Cell(row, 2).Value = item.Code;
                worksheet.Cell(row, 3).Value = item.Quantity;
                worksheet.Cell(row, 4).Value = item.PurchasePrice;
                worksheet.Cell(row, 5).Value = item.Price;
                worksheet.Cell(row, 6).Value = item.Profit;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return await Task.FromResult(stream);

        }
    }
}
