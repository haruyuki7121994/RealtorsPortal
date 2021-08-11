using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICustomerService
    {
        Customer checkLogin(string uname, string pass);
        List<Customer> findAll();
        Customer fineOne(string uname);
        void addCustomer(Customer customers);
        void updateCustomer(Customer customers);
        void deleteCustomer(int id);
    }
}
