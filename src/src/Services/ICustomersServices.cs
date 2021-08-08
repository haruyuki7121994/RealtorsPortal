using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface ICustomersServices
    {
        Customers checkLogin(string uname, string pass);
        List<Customers> findAll();
        Customers fineOne(string uname);
        void addCustomer(Customers customers);
        void updateCustomer(Customers customers);
        void deleteCustomer(int id);
    }
}
