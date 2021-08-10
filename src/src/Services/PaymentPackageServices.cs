using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    
    public class PaymentPackageServices:IPaymentPackageServices
    {
        private RealtorContext context;
        public PaymentPackageServices(RealtorContext context)
        {
            this.context = context;
        }

        public void addPaymentPackage(PaymentPackage payment_package)
        {
            PaymentPackage newPayment_package = context.PaymentPackage.SingleOrDefault(a => a.Id.Equals(payment_package.Id));
            if (newPayment_package == null)
            {
                context.PaymentPackage.Add(payment_package);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deletePaymentPackage(int id)
        {
            PaymentPackage Payment_package = context.PaymentPackage.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_package != null)
            {
                context.PaymentPackage.Remove(Payment_package);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<PaymentPackage> findAll()
        {
            return context.PaymentPackage.ToList();
        }

        public PaymentPackage fineOne(int id)
        {
            PaymentPackage Payment_package = context.PaymentPackage.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_package != null)
            {
                return Payment_package;
            }
            else
            {
                return null;
            }
        }

        public void updatePaymentPackage(PaymentPackage payment_package)
        {
            PaymentPackage editPayment_package = context.PaymentPackage.SingleOrDefault(a => a.Id.Equals(payment_package.Id));
            if (editPayment_package != null)
            {
                editPayment_package.Transaction_id = payment_package.Transaction_id;
                editPayment_package.Payment_price = payment_package.Payment_price;
                editPayment_package.Status = payment_package.Status;
                editPayment_package.Customer_id = payment_package.Customer_id;
                editPayment_package.Package_id = payment_package.Package_id;

                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
