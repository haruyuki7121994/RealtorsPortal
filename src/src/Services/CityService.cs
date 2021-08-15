using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CityService : ICityService
    {
        private readonly RealtorContext _context;
        public CityService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<City> CreateEditCity(City city)
        {
           if(city.Id == 0)
            {
                _context.Add(city);
            }else
            {
                var c = await GetCityById(city.Id);
                if (c == null) return null;
                c.Name = city.Name;
                c.Region_id = city.Region_id;
                c.Is_active = city.Is_active;
                
            }
          await  _context.SaveChangesAsync();
            return city;
        }

        public async Task<bool> DeleteCity(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city == null) return false;
            _context.Remove<City>(city);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<City>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city == null) return null;
            return city;
        }
    }
}
