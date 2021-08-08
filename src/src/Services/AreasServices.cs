using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class AreasServices : IAreasServices
    {
        private RealtorContext context;
        public AreasServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addArea(Areas area)
        {
            Areas newArea = context.Areas.SingleOrDefault(a => a.Name.Equals(area.Name));
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
            Areas area = context.Areas.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Areas> findAll()
        {
            return context.Areas.ToList();
        }

        public Areas fineOne(string name)
        {
            Areas area = context.Areas.SingleOrDefault(a => a.Name.Equals(name));
            if (name != null)
            {
                return area;
            }
            else
            {
                return null;
            }
        }

        public void updateArea(Areas area)
        {
            Areas editArea = context.Areas.SingleOrDefault(a => a.Id.Equals(area.Id));
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
