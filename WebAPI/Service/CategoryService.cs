using BusinessObject;
using WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Service {
    public class CategoryService : ICategoryRepository {
        private readonly TDbContext _context;
        public CategoryService(TDbContext context) {
            _context = context;
        }
        public bool DeleteCategoryById(int id) {
            var p = _context.Products.SingleOrDefault(
                        c => c.ProductId == id);
            if (p != null) {
                _context.Products.Remove(p);
                _context.SaveChanges();
                return true;
            } else {
                return false;
            }
        }

        public List<Category> GetCategories() {
            return _context.Categories.ToList();
        }

        public void SaveCategory(Category c) {
            _context.Categories.Add(c);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category c) {
            _context.Entry<Category>(c).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Category GetCategoryById(int id) {
            Category c = _context.Categories.SingleOrDefault(x => x.CategoryId == id);
            return c;
        }
    }
}
