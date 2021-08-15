using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class PropertyService : IPropertyService
    {
        private RealtorContext context;
        public PropertyService(RealtorContext context)
        {
            this.context = context;
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
            context.Properties.Add(newProp);
            context.SaveChanges();
            return true;
        }

        public void deleteProperty(int id)
        {
            Property Properties = context.Properties.SingleOrDefault(a => a.Id.Equals(id));
            if (Properties != null)
            {
                context.Properties.Remove(Properties);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Property> findAll()
        {
            return context.Properties.ToList();
        }

        public Property fineOne(int id)
        {
            Property Properties = context.Properties.SingleOrDefault(a => a.Id.Equals(id));
            if (Properties != null)
            {
                return Properties;
            }
            else
            {
                return null;
            }
        }

        public bool updateProperty(Property property)
        {
            Property editProperties = context.Properties.SingleOrDefault(a => a.Id.Equals(property.Id));
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
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Property> FindByCustomerId(int customerId)
        {
            return context.Properties.Where(p => p.Customer_id.Equals(customerId)).ToList();
        }

        public Property FindOneWithRelation(int id)
        {
            var query = from p in context.Properties
                        join a in context.Areas on p.Area_id equals a.Id
                        join c in context.Cities on a.City_id equals c.Id
                        join re in context.Regions on c.Region_id equals re.Id
                        join cou in context.Countries on re.Country_id equals cou.Id
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

        public List<Property> FindAllWithRelation()
        {
            var query = from p in context.Properties
                        join a in context.Areas on p.Area_id equals a.Id
                        join c in context.Cities on a.City_id equals c.Id
                        join re in context.Regions on c.Region_id equals re.Id
                        join cou in context.Countries on re.Country_id equals cou.Id
                        join cate in context.Categories on p.Category_id equals cate.Id
                        join cus in context.Customers on p.Customer_id equals cus.Id
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
                        };
            return query.ToList();
        }
    }
}
