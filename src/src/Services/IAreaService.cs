using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IAreaService
    {
        List<Area> findAll();
        Area fineOne(string name);
        void addArea(Area area);
        void updateArea(Area area);
        void deleteArea(int id);
    }
}
