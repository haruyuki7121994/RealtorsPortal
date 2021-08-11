using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICityService
    {
        List<City> findAll();
        City fineOne(string name);
        void addCity(City city);
        void updateCity(City city);
        void deleteCity(int id);
    }
}
