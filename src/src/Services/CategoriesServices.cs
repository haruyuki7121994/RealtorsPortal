using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private RealtorContext context;
        public CategoriesServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addCategory(Categories categories)
        {
            Categories newCate = context.Categories.SingleOrDefault(a => a.Name.Equals(categories.Name));
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
            Categories categories = context.Categories.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Categories> findAll()
        {
            return context.Categories.ToList();
        }

        public Categories fineOne(string name)
        {
            Categories categories = context.Categories.SingleOrDefault(a => a.Name.Equals(name));
            if (categories != null)
            {
                return categories;
            }
            else
            {
                return null;
            }
        }

        public void updateCategory(Categories categories)
        {
            Categories editCate = context.Categories.SingleOrDefault(a => a.Id.Equals(categories.Id));
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
