using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private RealtorContext _context;
        public ConfigurationService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<Configuration> GetConfigurationByObj(string obj)
        {
            var config = await _context.Configurations.FirstOrDefaultAsync(x => x.Obj.Equals(obj));
            if (config == null) return null;
            return config;
        }

        public async Task<List<Configuration>> GetConfigurations()
        {
            return await _context.Configurations.ToListAsync();
        }

        public async Task<Configuration> UpdateCongiguration(Configuration c)
        {
            var config = await GetConfigurationByObj(c.Obj);
            if (config == null) return null;
            config.Val = c.Val;
            await _context.SaveChangesAsync();
            return config;
        }
    }
}
