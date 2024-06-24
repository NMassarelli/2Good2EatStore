﻿using _2Good2EatStore.Data.Interfaces;
using static MudBlazor.Icons;

namespace _2Good2EatStore.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0) _dbContext.Products.Add(product);
            else _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}