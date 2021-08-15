using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetRegions();
        Task<IEnumerable<Region>> GetRegionsByCountryId(int id);
        Task<IEnumerable<Region>> GetRegionByActive(bool active = false);
        Task<Region> GetRegionById(int id);
        Task<Region> CreateEditRegion(Region region);
        Task<bool> DeleteRegion(int id);
    }
}
