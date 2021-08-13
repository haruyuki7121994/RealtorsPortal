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
        List<Property> FindByCustomerId(int customerId);
        Property fineOne(int id);
        bool addProperty(Property property);
        bool updateProperty(Property property);
        void deleteProperty(int id);
        Property FindOneWithRelation(int id);
    }
}
