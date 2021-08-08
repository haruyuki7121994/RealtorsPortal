using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class RegionsServices : IRegionsServices
    {
        private RealtorContext context;
        public RegionsServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addRegion(Regions regions)
        {
            Regions newRegion = context.Regions.SingleOrDefault(a => a.Name.Equals(regions.Name));
            if (newRegion == null)
            {
                context.Regions.Add(regions);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteRegion(int id)
        {
            Regions regions = context.Regions.SingleOrDefault(a => a.Id.Equals(id));
            if (regions != null)
            {
                context.Regions.Remove(regions);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Regions> findAll()
        {
            return context.Regions.ToList();
        }

        public Regions fineOne(string name)
        {
            Regions regions = context.Regions.SingleOrDefault(a => a.Name.Equals(name));
            if (name != null)
            {
                return regions;
            }
            else
            {
                return null;
            }
        }

        public void updateRegion(Regions regions)
        {
            Regions editRegion = context.Regions.SingleOrDefault(a => a.Id.Equals(regions.Id));
            if (editRegion != null)
            {
                editRegion.Name = regions.Name;
                editRegion.Is_active = regions.Is_active;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
