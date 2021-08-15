using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class PropertyService : IPropertyService
    {
        private RealtorContext _context;
        public PropertyService(RealtorContext context)
        {
            this._context = context;
        }

        public async Task<Property> CreateEditProperty(Property property)
        {
            if(property.Id == 0 )
            {
                _context.Add<Property>(property);
            }
            else
            {
                var propertyRepo = await _context.Properties.FirstOrDefaultAsync(x => x.Id == property.Id);
                if (property == null) return null;
                propertyRepo.Title = property.Title;
                propertyRepo.Price = property.Price;
                propertyRepo.Method = property.Method;
                propertyRepo.Features = property.Features;
                propertyRepo.Is_featured = property.Is_featured;
                propertyRepo.Introduction = property.Introduction;
                propertyRepo.Description = property.Description;
                propertyRepo.Deposit = property.Deposit;
                propertyRepo.Customer_id = property.Customer_id;
                propertyRepo.Category_id = property.Category_id;
                propertyRepo.Area_id = property.Area_id;
                propertyRepo.Ended_at = property.Ended_at;


            }
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<bool> DeleteProperty(int id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (property == null) return false; ;
            _context.Remove<Property>(property);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
           return await _context.Properties.Include(x=>x.Area).Include(c=>c.Customer).Include(d=>d.Category).Select(p => new Property {
                                                Id = p.Id,
                                                Title = p.Title,
                                                Introduction = p.Introduction,
                                                Features = p.Features,
                                                Description = p.Description,
                                                Ended_at = p.Ended_at,
                                                Area = p.Area,
                                                Deposit = p.Deposit,
                                                Category = p.Category,
                                                Customer = p.Customer,
                                                Is_featured = p.Is_featured,
                                                Method = p.Method
                                                
                                            } )
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByCustomerId(int Id)
        {
            return await _context.Properties.Where(x => x.Customer_id == Id).ToListAsync();
        }

        public async Task<Property> GetPropertyById(int id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (property == null) return null;
            return property;
        }

        
    }
}
