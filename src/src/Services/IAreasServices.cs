using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IAreasServices
    {
        List<Areas> findAll();
        Areas fineOne(string name);
        void addArea(Areas area);
        void updateArea(Areas area);
        void deleteArea(int id);
    }
}
