using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class ConfigurationsServices : IConfigurationsServices
    {
        private RealtorContext context;
        public ConfigurationsServices(RealtorContext context)
        {
            this.context = context;
        }
        public List<Configurations> findAll()
        {
            return context.Configurations.ToList();
        }

        public void updateCongiguration(Configurations configurations)
        {
            Configurations editConfig = context.Configurations.SingleOrDefault(a => a.Obj.Equals(configurations.Obj));
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
