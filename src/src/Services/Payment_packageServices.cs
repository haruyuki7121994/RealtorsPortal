using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    
    public class Payment_packageServices:IPayment_packageServices
    {
        private RealtorContext context;
        public Payment_packageServices(RealtorContext context)
        {
            this.context = context;
        }

        public void addPayment_package(Payment_package Payment_package)
        {
            Payment_package newPayment_package = context.Payment_Packages.SingleOrDefault(a => a.Id.Equals(Payment_package.Id));
            if (newPayment_package == null)
            {
                context.Payment_Packages.Add(Payment_package);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deletePayment_package(int id)
        {
            Payment_package Payment_package = context.Payment_Packages.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_package != null)
            {
                context.Payment_Packages.Remove(Payment_package);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Payment_package> findAll()
        {
            return context.Payment_Packages.ToList();
        }

        public Payment_package fineOne(int id)
        {
            Payment_package Payment_package = context.Payment_Packages.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_package != null)
            {
                return Payment_package;
            }
            else
            {
                return null;
            }
        }

        public void updatePayment_package(Payment_package Payment_package)
        {
            Payment_package editPayment_package = context.Payment_Packages.SingleOrDefault(a => a.Id.Equals(Payment_package.Id));
            if (editPayment_package != null)
            {
                editPayment_package.Status = Payment_package.Status;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
