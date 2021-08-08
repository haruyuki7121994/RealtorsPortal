using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CountriesServices:ICountriesServices
    {
        private RealtorContext context;
        public CountriesServices(RealtorContext context)
        {
            this.context = context;
        }

        public void addCountry(Countries country)
        {
            Countries newCountry = context.Countries.SingleOrDefault(a => a.Name.Equals(country.Name));
            if (newCountry == null)
            {
                context.Countries.Add(country);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteCountry(int id)
        {
            Countries country = context.Countries.SingleOrDefault(a => a.Id.Equals(id));
            if (country != null)
            {
                context.Countries.Remove(country);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Countries> findAll()
        {
            return context.Countries.ToList();
        }

        public Countries fineOne(string name)
        {
            Countries country = context.Countries.SingleOrDefault(a => a.Name.Equals(name));

            if (country != null)
            {
                return country;
            }
            else
            {
                return null;
            }
        }

        public void updateCountry(Countries country)
        {
            Countries editCountry = context.Countries.SingleOrDefault(a => a.Id.Equals(country.Id));
            if (editCountry != null)
            {
                editCountry.Name = country.Name;
                editCountry.Is_active = country.Is_active;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
