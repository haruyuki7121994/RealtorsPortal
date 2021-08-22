using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class PaymentSubscriptionService : IPaymentSubscriptionService
    {
        private RealtorContext context;
        public PaymentSubscriptionService(RealtorContext context)
        {
            this.context = context;
        }
        public void addPaymentSubscription(PaymentSubscription payment_subscription)
        {
            PaymentSubscription newPayment_subscription = context.PaymentSubscription.SingleOrDefault(a => a.Id.Equals(payment_subscription.Id));
            if (newPayment_subscription == null)
            {
                context.PaymentSubscription.Add(payment_subscription);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public void deletePaymentSubscription(int id)
        {
            PaymentSubscription Payment_subscription = context.PaymentSubscription.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_subscription != null)
            {
                context.PaymentSubscription.Remove(Payment_subscription);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<PaymentSubscription> findAll()
        {
            return context.PaymentSubscription.OrderByDescending(x => x.Create_at).ToList();
        }

        public PaymentSubscription fineOne(int id)
        {
            PaymentSubscription Payment_subscription = context.PaymentSubscription.SingleOrDefault(a => a.Id.Equals(id));
            if (Payment_subscription != null)
            {
                return Payment_subscription;
            }
            else
            {
                return null;
            }
        }

        public void updatePaymentSubscription(PaymentSubscription payment_subscription)
        {
            PaymentSubscription editPayment_subscription = context.PaymentSubscription.SingleOrDefault(a => a.Id.Equals(payment_subscription.Id));
            if (editPayment_subscription != null)
            {
                editPayment_subscription.Status = payment_subscription.Status;
                editPayment_subscription.Type = payment_subscription.Type;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
