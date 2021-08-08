using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICitiesServices
    {
        List<Cities> findAll();
        Cities fineOne(string name);
        void addCity(Cities city);
        void updateCity(Cities city);
        void deleteCity(int id);
    }
}
