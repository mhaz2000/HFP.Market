using HFP.Application.Services;
using HFP.Infrastructure.EF.Contexts;
using HFP.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace HFP.Infrastructure.EF.Services
{
    internal sealed class ProductReadService : IProductReadService
    {
        private readonly DbSet<ProductReadModel> _products;

        public ProductReadService(ReadDbContext context)
        {
            _products = context.Products;
        }
        public async Task<bool> CheckExistByNameAsync(string productName, Guid? id = null)
        {
            if (id is null)
                return await _products.AnyAsync(pr => pr.Name == productName);
            else
                return await _products.AnyAsync(pr => pr.Name == productName && pr.Id != id);
        }
    }
}
