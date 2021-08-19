using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IAreaService
    {
        Task<IEnumerable<Models.Area>> GetAreas();
        Task<IEnumerable<Models.Area>> GetAreasByCityId(int Id);
       

        Task<Models.Area> GetAreaById(int areaId);
        Task<Models.Area> CreateEditArea(Models.Area area);
        Task<bool> deleteArea(int id);
    }
}
