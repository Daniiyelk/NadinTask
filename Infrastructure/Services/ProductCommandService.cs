using Application.Dtos.Product;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductCommandService : IProductCommandService
    {
        private readonly TestingTaskDbContext _context;
        private readonly IProductQueryService _queryService;
        public ProductCommandService(TestingTaskDbContext context,IProductQueryService queryService)
        {
            _context = context;
            _queryService = queryService;
        }

        public async Task<string> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return product.Id;
        }

        public async Task DeleteProduct(string id)
        {
            var product = await _queryService.GetProductByIdAsync(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public async Task UpdateProductAsync(string id, ProductCreationDto productDto)
        {
            var product = await _queryService.GetProductByIdAsync(id);

            product.ManufactorPhone = productDto.ManufactorPhone;
            product.Name = productDto.Name;
            product.ManufactorEmail = productDto.ManufactorEmail;
            product.ProduceDate = productDto.ProduceDate;
            product.IsAvailable = productDto.IsAvailable;
            product.UserId = productDto.UserId;

            _context.Products.Update(product);
            _context.SaveChangesAsync();
        }
    }
}
