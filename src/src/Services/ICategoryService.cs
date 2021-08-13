using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICategoryService
    {
        List<Category> findAll(bool isActive = false);
        Category fineOne(string name);
        void addCategory(Category categories);
        void updateCategory(Category categories);
        void deleteCategory(int id);
    }
}
