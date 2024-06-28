﻿using _2Good2EatStore.Data.Entities;
using _2Good2EatStore.Data.Enums;
using _2Good2EatStore.Data.Interfaces;
using static MudBlazor.CategoryTypes;

namespace _2Good2EatStore.Data.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _dbContext = context;

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

        public IQueryable<Product> GetProducts()
        {
            return _dbContext.Products.AsQueryable();
        }

        public void SaveProduct(Product product)
        {
            var trackedReference = _dbContext.Products.Local.SingleOrDefault(p => p.Id == product.Id);

            if (trackedReference == null)
            { 
                _dbContext.Products.Update(product);
            }
            else if (!Object.ReferenceEquals(trackedReference, product))
            {
                _dbContext.Entry(trackedReference).CurrentValues.SetValues(product);
            }

            _dbContext.SaveChanges();
        }

        public IQueryable<Product> GetProductsByType(ProductTypeEnum type){

            return _dbContext.Products.Where(x => x.ProductType == type);

        }

    }
}
