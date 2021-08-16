using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    
    public class PaymentPackageService:IPaymentPackageService
    {
        private RealtorContext context;
        public PaymentPackageService(RealtorContext context)
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

        public PaymentPackage GetDetails(int id)
        {
            PaymentPackage paymentPackage = context.PaymentPackage.Find(id);
            return paymentPackage;
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

        public List<PaymentPackage> FindPackagesByCustomerId(int customerId)
        {
            var query = from pp in context.PaymentPackage
                        join p in context.Packages on pp.Package_id equals p.Id
                        where pp.Customer_id == customerId
                        select new PaymentPackage {
                            Id = pp.Id,
                            Limit_ads = pp.Limit_ads,
                            Limit_featured_ads = pp.Limit_featured_ads,
                            Used_ads = pp.Used_ads,
                            Used_featured_ads = pp.Used_featured_ads,
                            PackageName = p.Name,
                            Status =  pp.Status
                        };
            return query.ToList();
                   
        }

        public PaymentPackage AddTrialPaymentPackageForCustomer(int customerId)
        {
            var trialPackage = context.Packages.SingleOrDefault(p => p.Name == "Trial" && p.Is_active.Equals(true));
            if (trialPackage != null)
            {
                DateTime current = DateTime.Now;
                string timestamp = current.ToString("yyyyMMddHHmmssffff");
                var payment = new PaymentPackage
                {
                    Customer_id = customerId,
                    Package_id = trialPackage.Id,
                    Transaction_id = $"Trial{timestamp}",
                    Limit_ads = trialPackage.Limit_ads,
                    Limit_featured_ads = trialPackage.Limit_featured_ads,
                    Status = PaymentPackage.APPROVED_STATUS,
                    Payment_price = trialPackage.Price,
                    Created_at = current,
                    Updated_at = current
                };
                context.PaymentPackage.Add(payment);
                context.SaveChanges();
                return payment;
            }
            else
            {
                return null;
            }
        }

        public PaymentPackage CheckPaymentCode(string code)
        {
            string[] words = code.Split('-');
            string transaction = "Trial" + words[0];
            int id = int.Parse(words[1]);
            return context.PaymentPackage.SingleOrDefault(pp => pp.Id.Equals(id) && pp.Transaction_id.Equals(transaction));
        }

        public bool CheckCanCreateAds(int customerId, bool featuredAds = false)
        {
            int count = 0;
            int limit = 0;
            var payments = context.PaymentPackage.Where(pp => 
                            pp.Customer_id.Equals(customerId) && 
                            pp.Status.Equals(PaymentPackage.APPROVED_STATUS)
                        ).ToList();

            foreach (var item in payments)
            {
                if (featuredAds)
                {
                    count += item.Used_featured_ads;
                    limit += item.Limit_featured_ads;
                }
                else
                {
                    count += item.Used_ads;
                    limit += item.Limit_ads;
                }
            }

            return count < limit;
        }

        public bool UpdateUsedAdsForCustomer(int customerId, bool featuredAds = false)
        {
            var payment = context.PaymentPackage.Where(pp =>
                            pp.Customer_id.Equals(customerId) &&
                            pp.Status.Equals(PaymentPackage.APPROVED_STATUS))
                        .OrderByDescending(pp => pp.Updated_at)
                        .FirstOrDefault();
            if (payment != null)
            {
                if (featuredAds)
                {
                    payment.Used_featured_ads += 1;
                }
                else
                {
                    payment.Used_ads += 1;
                }
                int compare = (payment.Limit_featured_ads + payment.Limit_ads) - (payment.Used_featured_ads + payment.Used_ads);
                if (compare == 0)
                {
                    payment.Status = PaymentPackage.EXPIRED_STATUS;
                }
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
