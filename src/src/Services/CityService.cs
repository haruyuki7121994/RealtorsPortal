using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CityService : ICityService
    {
        private RealtorContext context;
        public CityService(RealtorContext context)
        {
            this.context = context;
        }
        public void addCity(City city)
        {
            City newCity = context.Cities.SingleOrDefault(a => a.Name.Equals(city.Name));
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
            City city = context.Cities.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<City> findAll()
        {
            return context.Cities.ToList();
        }

        public City fineOne(string name)
        {
            City city = context.Cities.SingleOrDefault(a => a.Name.Equals(name));
            if (city != null)
            {
                return city;
            }
            else
            {
                return null;
            }
        }

        public void updateCity(City city)
        {
            City editCity = context.Cities.SingleOrDefault(a => a.Id.Equals(city.Id));
            if (editCity != null)
            {
                editCity.Name = city.Name;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
