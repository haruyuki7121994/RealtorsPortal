using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(bool checkActive = false);
        Task<IEnumerable<Category>> GetCategoriesByActive(bool active = false);
        Task<Category> GetCategoryById(int id);
 
        Task<Category> CreateEditCategory(Category category);
        Task<bool> DeleteCategory(int id);
    }
}
