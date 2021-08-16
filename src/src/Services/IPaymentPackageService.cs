using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPaymentPackageService
    {
        List<PaymentPackage> findAll();
        PaymentPackage fineOne(int id);
        void addPaymentPackage(PaymentPackage payment_package);
        void updatePaymentPackage(PaymentPackage payment_package);
        void deletePaymentPackage(int id);

        List<PaymentPackage> FindPackagesByCustomerId(int customerId);
        PaymentPackage AddTrialPaymentPackageForCustomer(int customerId);
        PaymentPackage CheckPaymentCode(string code);
        bool CheckCanCreateAds(int customerId, bool featuredAds = false);
        bool UpdateUsedAdsForCustomer(int customerId, bool featuredAds = false);
    }
}
