using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class PropertiesServices : IPropertiesServices
    {
        private RealtorContext context;
        public PropertiesServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addProperties(Properties properties)
        {
            Properties newProperties = context.Properties.SingleOrDefault(a => a.Id.Equals(properties.Id));
            if (newProperties == null)
            {
                context.Properties.Add(properties);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteProperties(int id)
        {
            Properties Properties = context.Properties.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Properties> findAll()
        {
            return context.Properties.ToList();
        }

        public Properties fineOne(int id)
        {
            Properties Properties = context.Properties.SingleOrDefault(a => a.Id.Equals(id));
            if (Properties != null)
            {
                return Properties;
            }
            else
            {
                return null;
            }
        }

        public void updateProperties(Properties properties)
        {
            Properties editProperties = context.Properties.SingleOrDefault(a => a.Id.Equals(properties.Id));
            if (editProperties != null)
            {
                editProperties.Title = properties.Title;
                editProperties.Introduction = properties.Introduction;
                editProperties.Features = properties.Features;
                editProperties.Method = properties.Method;
                editProperties.Price = properties.Price;
                editProperties.Deposit = properties.Deposit;
                editProperties.Is_featured = properties.Is_featured;
                editProperties.Area_id = properties.Area_id;
                editProperties.Category_id = properties.Category_id;
                editProperties.Customer_id = properties.Customer_id;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
