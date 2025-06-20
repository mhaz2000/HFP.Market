﻿using AutoMapper;
using HFP.Application.DTO;
using HFP.Application.Queries.Transactions;
using HFP.Domain.Consts;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using HFP.Shared.Abstractions.Queries;
using HFP.Shared.Helpers;
using HFP.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class GetProfitReportHandler : IQueryHandler<GetProfitReportQuery, PaginatedResult<ProfitReportDto>>
    {
        private readonly DbSet<TransactionReadModel> _transactions;
        public GetProfitReportHandler(ReadDbContext context)
        {
            _transactions = context.Transactions;
        }
        public async Task<PaginatedResult<ProfitReportDto>> Handle(GetProfitReportQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _transactions.Include(t => t.ProductTransactions).ThenInclude(t => t.Product)
                .Where(c => !c.IsDeleted && c.Type == TransactionType.Invoice);

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                var startDate = request.StartDate.ToDate(true);
                var endDate = request.EndDate.ToDate(false);

                dbQuery = dbQuery.Where(t => t.Date.Date <= endDate && t.Date.Date >= startDate);
            }

            var newQuery = dbQuery.SelectMany(s => s.ProductTransactions).AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
                newQuery = newQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Product.Name, $"%{request.Search}%"));


            var report = newQuery.AsEnumerable()
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


            var totalCount = report.Count();
            var items = report
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return await Task.FromResult(new PaginatedResult<ProfitReportDto>(items, totalCount, request.PageSize, request.PageIndex));
        }
    }
}
