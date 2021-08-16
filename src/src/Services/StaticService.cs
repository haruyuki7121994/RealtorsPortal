using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class StaticService : IStaticService
    {
        private RealtorContext context;
        public StaticService(RealtorContext context)
        {
            this.context = context;
        }
        public List<Property> FindAllHistory()
        {
            return (from p in context.Properties
                    join cate in context.Categories on p.Id equals cate.Id
                    join cus in context.Customers on p.Id equals cus.Id
                    join pay in context.PaymentSubscription on p.Id equals pay.Id
                    select new Property
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Category_id = p.Id,
                        Customer_id = p.Id,
                        Category = cate,
                        Customer = cus
                    }).ToList();
        }
    }
}
