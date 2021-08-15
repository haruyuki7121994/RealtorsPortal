using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IRegionService
    {
        Task<List<Region>> GetAllRegion();
        Task<Region> GetRegionById(int id);
        Task<Region> CreateEditRegion(Region region);
        Task<bool> DeleteRegion(int id);
    }
}
