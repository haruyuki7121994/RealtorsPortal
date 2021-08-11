using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class CountryService:ICountryService
    {
        private readonly RealtorContext _context;
        public CountryService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<Country> CreateEditCountry(Country country)
        {
            if(country.Id == 0 )
            {
                _context.Add(country);
                _context.SaveChanges();
              
            }
            else
            {
                var c = await _context.Countries.FirstOrDefaultAsync(c => c.Id == country.Id);
                if(c==null)
                {
                    return null;
                }
                else
                {
                    c.Name = country.Name;
                    c.Is_active = country.Is_active;
                    _context.SaveChanges();

                }

            }
            return country;


        }

        public Task<bool> DeleteCountry(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetAllCountry()
        {
            return await _context.Countries.ToListAsync();
        }

        public Task<Country> GetCountryByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
