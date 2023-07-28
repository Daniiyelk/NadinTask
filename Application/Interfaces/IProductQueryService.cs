using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductQueryService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int pageNumber, int pageSize, string userId);

        Task<Product> GetProductByIdAsync(string id);
        Task<bool> IsProductExistAsync(string id);
    }
}
