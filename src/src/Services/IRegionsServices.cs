using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IRegionsServices
    {
        List<Regions> findAll();
        Regions fineOne(string name);
        void addRegion(Regions regions);
        void updateRegion(Regions regions);
        void deleteRegion(int id);
    }
}
