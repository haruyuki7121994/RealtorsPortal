using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class Payment_subscriptionServices : IPayment_subscriptionServices
    {
        private RealtorContext context;
        public Payment_subscriptionServices(RealtorContext context)
        {
            this.context = context;
        }
        public void addPayment_subscription(Payment_subscription Payment_subscription)
        {
            Payment_subscription newPayment_subscription = context.Payment_Subscriptions.SingleOrDefault(a => a.Id.Equals(Payment_subscription.Id));
            if (newPayment_subscription == null)
            {
                context.Payment_Subscriptions.Add(Payment_subscription);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deletePayment_subscription(int id)
        {
            Payment_subscription Payment_subscription = context.Payment_Subscriptions.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_subscription != null)
            {
                context.Payment_Subscriptions.Remove(Payment_subscription);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Payment_subscription> findAll()
        {
            return context.Payment_Subscriptions.ToList();
        }

        public Payment_subscription fineOne(int id)
        {
            Payment_subscription Payment_subscription = context.Payment_Subscriptions.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_subscription != null)
            {
                return Payment_subscription;
            }
            else
            {
                return null;
            }
        }

        public void updatePayment_subscription(Payment_subscription Payment_subscription)
        {
            Payment_subscription editPayment_subscription = context.Payment_Subscriptions.SingleOrDefault(a => a.Id.Equals(Payment_subscription.Id));
            if (editPayment_subscription != null)
            {
                editPayment_subscription.Status = Payment_subscription.Status;
                editPayment_subscription.Type = Payment_subscription.Type;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
