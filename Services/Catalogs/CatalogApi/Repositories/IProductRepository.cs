using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogApi.Entities;

namespace CatalogApi.Repositories
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategoryId(string Category);
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}