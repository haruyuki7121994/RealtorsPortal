using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class AreaService : IAreaService
    {
        private RealtorContext _context;
        public AreaService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<Area> CreateEditArea(Area area)
        {
            if (area.Id == 0)
            {
                _context.Add(area);
            }
            else
            {
                var areaRepo = await GetAreaById(area.Id);
                if (area == null) return null;
                areaRepo.Name = area.Name;
                areaRepo.Properties = area.Properties;
                areaRepo.City_id = area.City_id;
            }
            await _context.SaveChangesAsync();
            return area;
        }

        public async Task<bool> deleteArea(int id)
        {
            var area = await GetAreaById(id);
            if (area == null) return false;
            _context.Remove<Area>(area);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Area> GetAreaById(int areaId)
        {
            var area = await _context.Areas.FirstOrDefaultAsync(x => x.Id == areaId);
            if (area == null) return null;
            return area;
        }

        public async Task<IEnumerable<Area>> GetAreas()
        {
            return await _context.Areas.Include(x => x.city).ToListAsync();
        }
        public async Task<IEnumerable<Area>> GetAreaByActive(bool active = false)
        {
            return await _context.Areas.ToListAsync();
        }
        public async Task<IEnumerable<Area>> GetAreasByCityId(int Id)
        {
            return await _context.Areas.Where(x => x.City_id == Id).ToListAsync();
        }
    }
}
