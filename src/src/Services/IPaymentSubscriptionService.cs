using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPaymentSubscriptionService
    {
        List<PaymentSubscription> findAll();
        PaymentSubscription fineOne(int id);
        bool addPaymentSubscription(PaymentSubscription payment_subscription);
        void updatePaymentSubscription(PaymentSubscription payment_subscription);
        void deletePaymentSubscription(int id);
    }
}
