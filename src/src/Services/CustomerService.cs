using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{

    public class CustomerService:ICustomerService
    {
        private RealtorContext context;
        public CustomerService(RealtorContext context)
        {
            this.context = context;
        }

        public void addCustomer(Customer customers)
        {
            Customer acc = context.Customers.SingleOrDefault(a => a.Username.Equals(customers.Username));
            if (acc == null)
            {
                context.Customers.Add(customers);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public Customer checkLogin(string uname, string pass)
        {
            Customer customers = context.Customers.SingleOrDefault(a => a.Username.Equals(uname));
            if (customers == null)
            {
                if (customers.Password.Equals(pass))
                {
                    return customers;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void deleteCustomer(int id)
        {
            Customer customers = context.Customers.SingleOrDefault(a => a.Id.Equals(id));
            if (customers != null)
            {
                context.Customers.Remove(customers);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Customer> findAll()
        {
            return context.Customers.ToList();
        }

        public Customer fineOne(string uname)
        {
            Customer customers = context.Customers.SingleOrDefault(a => a.Username.Equals(uname));
            if (customers != null)
            {
                return customers;
            }
            else
            {
                return null;
            }
        }

        public void updateCustomer(Customer customers)
        {
            Customer editcustomers = context.Customers.SingleOrDefault(a => a.Id.Equals(customers.Id));
            if (editcustomers != null)
            {
                editcustomers.Username = customers.Username;
                editcustomers.Password = customers.Password;
                editcustomers.Contact = customers.Contact;
                editcustomers.Address = customers.Address;
                editcustomers.Email = customers.Email;
                editcustomers.Image = customers.Image;
                editcustomers.Type = customers.Type;
                
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
