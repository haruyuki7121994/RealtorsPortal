using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IConfigurationsServices
    {
        List<Configurations> findAll();
        
        void updateCongiguration(Configurations configurations);
        
    }
}
