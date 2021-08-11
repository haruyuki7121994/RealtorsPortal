using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class RegionService : IRegionservice
    {
        private RealtorContext context;
        public RegionService(RealtorContext context)
        {
            this.context = context;
        }
        public void addRegion(Region regions)
        {
            Region newRegion = context.Regions.SingleOrDefault(a => a.Name.Equals(regions.Name));
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
            Region regions = context.Regions.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Region> findAll()
        {
            return context.Regions.ToList();
        }

        public Region fineOne(string name)
        {
            Region regions = context.Regions.SingleOrDefault(a => a.Name.Equals(name));
            if (name != null)
            {
                return regions;
            }
            else
            {
                return null;
            }
        }

        public void updateRegion(Region regions)
        {
            Region editRegion = context.Regions.SingleOrDefault(a => a.Id.Equals(regions.Id));
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
