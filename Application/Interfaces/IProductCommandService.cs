using Application.Dtos.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductCommandService
    {
        Task<string> AddProductAsync(Product product);

        Task UpdateProductAsync(string id, ProductCreationDto productDto);
        Task DeleteProduct(string id);
    }
}
