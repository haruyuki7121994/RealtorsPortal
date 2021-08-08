using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPayment_packageServices
    {
        List<Payment_package> findAll();
        Payment_package fineOne(int id);
        void addPayment_package(Payment_package Payment_package);
        void updatePayment_package(Payment_package Payment_package);
        void deletePayment_package(int id);
    }
}
