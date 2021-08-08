using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPropertiesServices
    {
        List<Properties> findAll();
        Properties fineOne(int id);
        void addProperties(Properties properties);
        void updateProperties(Properties properties);
        void deleteProperties(int id);
    }
}
