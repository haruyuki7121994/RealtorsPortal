using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CitiesServices : ICitiesServices
    {
        private RealtorContext context;
        public CitiesServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addCity(Cities city)
        {
            Cities newCity = context.Cities.SingleOrDefault(a => a.Name.Equals(city.Name));
            if (newCity == null)
            {
                context.Cities.Add(city);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteCity(int id)
        {
            Cities city = context.Cities.SingleOrDefault(a => a.Id.Equals(id));
            if (city != null)
            {
                context.Cities.Remove(city);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Cities> findAll()
        {
            return context.Cities.ToList();
        }

        public Cities fineOne(string name)
        {
            Cities city = context.Cities.SingleOrDefault(a => a.Name.Equals(name));
            if (city != null)
            {
                return city;
            }
            else
            {
                return null;
            }
        }

        public void updateCity(Cities city)
        {
            Cities editCity = context.Cities.SingleOrDefault(a => a.Id.Equals(city.Id));
            if (editCity != null)
            {
                editCity.Name = city.Name;
                editCity.Is_active = city.Is_active;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
