using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICategoriesServices
    {
        List<Categories> findAll();
        Categories fineOne(string name);
        void addCategory(Categories categories);
        void updateCategory(Categories categories);
        void deleteCategory(int id);
    }
}
