using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private RealtorContext context;
        public ConfigurationService(RealtorContext context)
        {
            this.context = context;
        }
        public List<Configuration> findAll()
        {
            return context.Configurations.ToList();
        }

        public void updateCongiguration(Configuration configurations)
        {
            Configuration editConfig = context.Configurations.SingleOrDefault(a => a.Obj.Equals(configurations.Obj));
            if (editConfig != null)
            {
                editConfig.Val = configurations.Val;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
