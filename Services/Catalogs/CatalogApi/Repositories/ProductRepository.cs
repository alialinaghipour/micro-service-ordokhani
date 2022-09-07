using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogApi.Data;
using CatalogApi.Entities;
using MongoDB.Driver;

namespace CatalogApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Product>.Filter.Eq(_ => _.Id, id);

            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products
                                    .Find(_ => _.Id == id)
                                    .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(_ => _.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                                    .Find(_ => true)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(string Category)
        {
            var filter = Builders<Product>.Filter.Eq(_ => _.Category, Category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context.Products
                                    .ReplaceOneAsync(
                                        filter: _ => _.Id == product.Id,
                                        replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}