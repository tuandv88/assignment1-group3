using BusinessObject;

namespace WebAPI.Repositories {
    public interface IProductRepository {
        bool SaveProduct(Product p);
        Product GetProductById(int id);
        bool DeleteProductById(int id);
        void UpdateProduct(Product p);
        List<Product> GetProducts();
    }
}
