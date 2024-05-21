using BusinessObject;

namespace WebAPI.Repositories {
    public interface ICategoryRepository {
        void SaveCategory(Category c);
        Category GetCategoryById(int id);
        bool DeleteCategoryById(int id);
        void UpdateCategory(Category c);
        List<Category> GetCategories();
    }
}
