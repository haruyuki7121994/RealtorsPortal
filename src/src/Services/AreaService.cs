using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class AreaService : IAreaService
    {
        private RealtorContext context;
        public AreaService(RealtorContext context)
        {
            this.context = context;
        }
        public void addArea(Area area)
        {
            Area newArea = context.Areas.SingleOrDefault(a => a.Name.Equals(area.Name));
            if (newArea == null)
            {
                context.Areas.Add(area);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deleteArea(int id)
        {
            Area area = context.Areas.SingleOrDefault(a => a.Id.Equals(id));
            if (area != null)
            {
                context.Areas.Remove(area);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Area> findAll()
        {
            return context.Areas.ToList();
        }

        public Area fineOne(string name)
        {
            Area area = context.Areas.SingleOrDefault(a => a.Name.Equals(name));
            if (name != null)
            {
                return area;
            }
            else
            {
                return null;
            }
        }

        public void updateArea(Area area)
        {
            Area editArea = context.Areas.SingleOrDefault(a => a.Id.Equals(area.Id));
            if (editArea != null)
            {
                editArea.Name = area.Name;
                editArea.Is_active = area.Is_active;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
