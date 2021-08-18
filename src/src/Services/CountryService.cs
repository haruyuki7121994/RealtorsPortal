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
                await _context.SaveChangesAsync();
              
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
                  
                    await _context.SaveChangesAsync();

                }

            }
            return country;


        }

        public async Task<bool> DeleteCountry(int id)
        {

            var country = await GetCountryById(id);
            if(country == null)
            {
                return false;
            }
            _context.Remove<Country>(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryById(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if(country == null)
            {
                return null;
            }
            return country;
        }
     
    }
}
