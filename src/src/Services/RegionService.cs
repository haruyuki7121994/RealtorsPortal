using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class RegionService : IRegionService
    {
        private readonly RealtorContext _context;
        public RegionService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<Region> CreateEditRegion(Region region)
        {
            if (region.Id == 0)
            {
                _context.Add(region);
                await _context.SaveChangesAsync();

            }
            else
            {
                var c = await _context.Regions.FirstOrDefaultAsync(c => c.Id == region.Id);
                if (c == null)
                {
                    return null;
                }
                else
                {
                    c.Name = region.Name;
                    c.Is_active = region.Is_active;
                    c.Country_id = region.Country_id;
                    await _context.SaveChangesAsync();

                }

            }
            return region;

        }

      

        public async Task<bool> DeleteRegion(int id)
        {
            var region = await GetRegionById(id);
            if (region == null) return false;
            _context.Remove<Region>(region);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Region>> GetRegions()
        {
            return await _context.Regions.Include(x => x.Country).ToListAsync();
        }

        public async Task<Region> GetRegionById(int id)
        {
               var region = await  _context.Regions.FirstOrDefaultAsync(x => x.Id ==id);
            if (region == null)
            {
                return null;
            }
            return region;
        }
        public async Task<IEnumerable<Region>> GetRegionByActive(bool active = false)
        {
            return await _context.Regions.Where(x =>x.Is_active == active).ToListAsync();
        }

        public async Task<IEnumerable<Region>> GetRegionsByCountryId(int id)
        {
            return await _context.Regions.Where(x=> x.Country_id == id).ToListAsync();
        }


    }
}
