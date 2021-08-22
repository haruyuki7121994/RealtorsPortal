using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetProperties();
        Task<IEnumerable<Property>> GetPropertiesByCustomerId(int Id);
        Task<Property> GetPropertyById(int id);
        Task<Property> CreateEditProperty(Property property);
       
        Task<bool> DeleteProperty(int id);

        List<Property> FindAllWithRelation();
        Property FindOneWithRelation(int id);
        List<Property> FindPropertiesByCustomerId(int id);

        bool updateProperty(Property property);
        bool addProperty(Property property);

        Task<IEnumerable<Property>> GetPropertiesByAreaId(int id);
        Task<IEnumerable<Property>> GetPropertiesByCategoryId(int id);
    }
}
