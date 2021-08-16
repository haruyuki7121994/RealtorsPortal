using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAreas();
        Task<IEnumerable<Area>> GetAreasByCityId(int Id);
        Task<IEnumerable<Area>> GetAreaByActive(bool active = false);

        Task<Area> GetAreaById(int areaId);
        Task<Area> CreateEditArea(Area area);
        Task<bool> deleteArea(int id);
    }
}
