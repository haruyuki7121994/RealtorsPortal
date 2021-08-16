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
                                                Method = p.Method,
                                                Thumbnail_url = p.Thumbnail_url
                                            } )
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByCustomerId(int Id)
        {
            return await _context.Properties.Where(x => x.Customer_id == Id).ToListAsync();
        }

        public List<Property> FindPropertiesByCustomerId(int Id)
        {
            return  _context.Properties.Where(x => x.Customer_id == Id).ToList();
        }

        public async Task<Property> GetPropertyById(int id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);
            if (property == null) return null;
            return property;
        }

        public List<Property> FindAllWithRelation()
        {
            return (from p in _context.Properties
                        join a in _context.Areas on p.Area_id equals a.Id
                        join c in _context.Cities on a.City_id equals c.Id
                        join re in _context.Regions on c.Region_id equals re.Id
                        join cou in _context.Countries on re.Country_id equals cou.Id
                        join cate in _context.Categories on p.Category_id equals cate.Id
                        join cus in _context.Customers on p.Customer_id equals cus.Id
                        select new Property
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Introduction = p.Introduction,
                            Description = p.Description,
                            Features = p.Features,
                            Method = p.Method,
                            Price = p.Price,
                            Deposit = p.Deposit,
                            Thumbnail_url = p.Thumbnail_url,
                            Is_featured = p.Is_featured,
                            Is_active = p.Is_active,
                            Created_at = p.Created_at,
                            Ended_at = p.Ended_at,
                            Area_id = p.Area_id,
                            Category_id = p.Category_id,
                            Customer_id = p.Customer_id,
                            Region = re,
                            City = c,
                            Country = cou,
                            Area = a,
                            Category = cate,
                            Customer = cus
                        }).ToList();
        }

        public Property FindOneWithRelation(int id)
        {
            var query = from p in _context.Properties
                        join a in _context.Areas on p.Area_id equals a.Id
                        join c in _context.Cities on a.City_id equals c.Id
                        join re in _context.Regions on c.Region_id equals re.Id
                        join cou in _context.Countries on re.Country_id equals cou.Id
                        where p.Id == id
                        select new Property
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Introduction = p.Introduction,
                            Description = p.Description,
                            Features = p.Features,
                            Method = p.Method,
                            Price = p.Price,
                            Deposit = p.Deposit,
                            Thumbnail_url = p.Thumbnail_url,
                            Is_featured = p.Is_featured,
                            Is_active = p.Is_active,
                            Created_at = p.Created_at,
                            Ended_at = p.Ended_at,
                            Area_id = p.Area_id,
                            Category_id = p.Category_id,
                            Customer_id = p.Customer_id,
                            Region = re,
                            City = c,
                            Country = cou,
                            Area = a,
                        };
            return query.FirstOrDefault();
        }

        public bool updateProperty(Property property)
        {
            Property editProperties = _context.Properties.SingleOrDefault(a => a.Id.Equals(property.Id));
            if (editProperties != null)
            {
                editProperties.Title = property.Title;
                editProperties.Introduction = property.Introduction;
                editProperties.Description = property.Description;
                editProperties.Features = property.Features;
                editProperties.Method = property.Method;
                editProperties.Price = property.Price;
                editProperties.Deposit = property.Deposit;
                editProperties.Thumbnail_url = property.Thumbnail_url;
                editProperties.Is_featured = property.Is_featured;
                editProperties.Is_active = property.Is_active;
                editProperties.Area_id = property.Area_id;
                editProperties.Category_id = property.Category_id;
                editProperties.Customer_id = property.Customer_id;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool addProperty(Property property)
        {
            var newProp = new Property
            {
                Title = property.Title,
                Introduction = property.Introduction,
                Description = property.Description,
                Method = property.Method,
                Price = property.Price,
                Deposit = property.Deposit,
                Thumbnail_url = property.Thumbnail_url,
                Is_featured = property.Is_featured,
                Is_active = property.Is_active,
                Created_at = DateTime.Now,
                Ended_at = DateTime.Now,
                Area_id = property.Area_id,
                Category_id = property.Category_id,
                Customer_id = property.Customer_id,
            };
            _context.Properties.Add(newProp);
            _context.SaveChanges();
            return true;
        }
    }

    
}
