using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IRegionservice
    {
        List<Region> findAll();
        Region fineOne(string name);
        void addRegion(Region regions);
        void updateRegion(Region regions);
        void deleteRegion(int id);
    }
}
