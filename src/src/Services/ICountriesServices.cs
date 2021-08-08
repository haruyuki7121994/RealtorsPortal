using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICountriesServices
    {
        List<Countries> findAll();
        Countries fineOne(string name);
        void addCountry(Countries country);
        void updateCountry(Countries country);
        void deleteCountry(int id);
    }
}
