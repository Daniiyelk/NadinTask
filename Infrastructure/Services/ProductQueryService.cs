using Application.Interfaces;
using Domain.Entities;
using Infrastructure.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly TestingTaskDbContext _context;

        public ProductQueryService(TestingTaskDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize, string userId)
        {
            int take = pageSize;
            int skip = (pageNumber - 1) * take;

            if (userId == null)
            {
                return await _context.Products.OrderByDescending(p => p.ProduceDate)
                .Skip(skip).Take(take).ToListAsync();
            }

            return await _context.Products.Where(u => u.UserId == userId).OrderByDescending(p => p.ProduceDate)
                .Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> IsProductExistAsync(string id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
    }
}
