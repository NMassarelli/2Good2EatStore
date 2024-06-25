using _2Good2EatStore.Data.Entities;

namespace _2Good2EatStore.Data.Interfaces
{
    public interface IProductService 
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void SaveProduct(Product product);
        void DeleteProduct(int id);
    }
}
