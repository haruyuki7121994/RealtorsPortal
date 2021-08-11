using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetAllCountry();
        Task<Country> GetCountryById(int id);
        Task<Country> CreateEditCountry(Country country);
        Task<bool> DeleteCountry(int id);
    }
}
