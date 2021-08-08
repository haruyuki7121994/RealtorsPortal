using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPayment_subscriptionServices
    {
        List<Payment_subscription> findAll();
        Payment_subscription fineOne(int id);
        void addPayment_subscription(Payment_subscription Payment_subscription);
        void updatePayment_subscription(Payment_subscription Payment_subscription);
        void deletePayment_subscription(int id);
    }
}
