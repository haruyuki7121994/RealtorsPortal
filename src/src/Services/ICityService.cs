using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<City>> GetCityByActive(bool active = false);
        Task<City> GetCityById(int id);
        Task<IEnumerable<City>> GetCitiesByRegionId(int id);
        Task<City> CreateEditCity(City city);
        Task<bool> DeleteCity(int id);
    }
}
