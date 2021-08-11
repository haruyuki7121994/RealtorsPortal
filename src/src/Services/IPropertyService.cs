using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPropertyService
    {
        List<Property> findAll();
        Property fineOne(int id);
        void addProperties(Property properties);
        void updateProperties(Property properties);
        void deleteProperties(int id);
    }
}
