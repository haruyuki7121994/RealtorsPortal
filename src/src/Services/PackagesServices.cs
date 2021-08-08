using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class PackagesServices : IPackagesServices
    {
        private RealtorContext context;
        public PackagesServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addPackage(Packages packages)
        {
            Packages newPackage = context.Packages.SingleOrDefault(a => a.Name.Equals(packages.Name));
            if (newPackage == null)
            {
                context.Packages.Add(packages);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deletepackage(int id)
        {
            Packages packages = context.Packages.SingleOrDefault(a => a.Id.Equals(id));
            if (packages != null)
            {
                context.Packages.Remove(packages);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Packages> findAll()
        {
            return context.Packages.ToList();
        }

        public Packages fineOne(string name)
        {
            Packages packages = context.Packages.SingleOrDefault(a => a.Name.Equals(name));
            if (packages != null)
            {
                return packages;
            }
            else
            {
                return null;
            }
        }

        public void updatePackage(Packages packages)
        {
            Packages editpackages = context.Packages.SingleOrDefault(a => a.Id.Equals(packages.Id));
            if (editpackages != null)
            {
                editpackages.Name = packages.Name;
                editpackages.Price = packages.Price;
                editpackages.Limit_ads = packages.Limit_ads; 
                editpackages.Limit_featured_ads = packages.Limit_featured_ads;
                editpackages.Is_active = packages.Is_active;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
