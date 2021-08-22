using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CategoryService : ICategoryService
    {
        private RealtorContext _context;
        public CategoryService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<Category> CreateEditCategory(Category category)
        {
            if (category.Id == 0)
            {
                _context.Add(category);
            }
            else
            {
                var c = await GetCategoryById(category.Id);
                if (c == null) return null;
                c.Name = category.Name;
              
                c.Is_active = category.Is_active;

            }
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            if (category == null) return false;
            _context.Remove<Category>(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByActive(bool active)
        {
            return await _context.Categories.Where(x=>x.Is_active == active).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.OrderByDescending(x => x.Id).ToListAsync();
        }
        
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return null;
            return category;
        }
     
    }
}
