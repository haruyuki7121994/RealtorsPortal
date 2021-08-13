using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICustomerService
    {
        Customer checkLogin(string uname, string pass, bool isActive = false, bool isVerified = false);
        List<Customer> findAll();
        Customer fineOne(string uname);
        bool addCustomer(Customer customer);
        bool updateCustomer(Customer customer);
        void deleteCustomer(int id);
    }
}
