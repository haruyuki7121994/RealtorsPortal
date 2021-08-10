using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPaymentPackageServices
    {
        List<PaymentPackage> findAll();
        PaymentPackage fineOne(int id);
        void addPaymentPackage(PaymentPackage payment_package);
        void updatePaymentPackage(PaymentPackage payment_package);
        void deletePaymentPackage(int id);
    }
}
