using BusinessObject;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;

namespace WebAPI.Service {
    public class ProductService : IProductRepository {
        private readonly TDbContext _context;
        public ProductService(TDbContext context) {
            _context = context;
        }
        public bool DeleteProductById(int id) {
            var p = _context.Products.SingleOrDefault(
                        c => c.ProductId == id);
            if(p !=null) {
                _context.Products.Remove(p);
                _context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public Product GetProductById(int id) {
            Product p = _context.Products
                .Include(p => p.Category)
                .SingleOrDefault(x => x.ProductId == id);
            return p;
        }

        public List<Product> GetProducts() {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public bool SaveProduct(Product p) {
            try {
                _context.Products.Add(p);
                _context.SaveChanges();
                return true;
            }catch(Exception ex) {
                return false;
            }
        }

        public void UpdateProduct(Product p) {
            _context.Entry<Product>(p).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public List<Product> GetProductByNameAndUnitPrice(string? name, decimal? startUnitPrice, decimal? endUnitPrice) {
            List<Product> products;
            if (name != null) {
                products = _context.Products.Include(p => p.Category).Where(p => p.ProductName.Contains(name)).ToList();
            } else {
                products = _context.Products.Include(p => p.Category).ToList();
            }
            if(startUnitPrice!=null && endUnitPrice !=null 
                && startUnitPrice < endUnitPrice) {
                products = products.Where(p => p.UnitPrice < endUnitPrice && p.UnitPrice> startUnitPrice).ToList();
            }
            return products;
        }
    }
}
