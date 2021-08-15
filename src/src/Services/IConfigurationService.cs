using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    public interface IConfigurationService
    {
        Task<List<Configuration>> GetConfigurations();
        
        Task<Configuration> UpdateCongiguration(Configuration c);

        Task<Configuration> GetConfigurationByObj(string obj);
        
    }
}
