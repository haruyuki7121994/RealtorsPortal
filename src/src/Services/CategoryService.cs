using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CategoryService : ICategoryService
    {
        private RealtorContext context;
        public CategoryService(RealtorContext context)
        {
            this.context = context;
        }
        public void addCategory(Category categories)
        {
            Category newCate = context.Categories.SingleOrDefault(a => a.Name.Equals(categories.Name));
            if (newCate == null)
            {
                context.Categories.Add(categories);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteCategory(int id)
        {
            Category categories = context.Categories.SingleOrDefault(a => a.Id.Equals(id));
            if (categories != null)
            {
                context.Categories.Remove(categories);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Category> findAll(bool isActive = false)
        {
            var list = context.Categories.ToList();
            if (isActive)
            {
                list.Where(c => c.Is_active.Equals(true));
            }
            return list;
        }

        public Category fineOne(string name)
        {
            Category categories = context.Categories.SingleOrDefault(a => a.Name.Equals(name));
            if (categories != null)
            {
                return categories;
            }
            else
            {
                return null;
            }
        }

        public void updateCategory(Category categories)
        {
            Category editCate = context.Categories.SingleOrDefault(a => a.Id.Equals(categories.Id));
            if (editCate != null)
            {
                editCate.Name = categories.Name;
                editCate.Is_active = categories.Is_active;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
