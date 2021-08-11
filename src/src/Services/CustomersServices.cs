using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{

    public class CustomersServices:ICustomersServices
    {
        private RealtorContext context;
        public CustomersServices(RealtorContext context)
        {
            this.context = context;
        }

        public void addCustomer(Customers customers)
        {
            Customers acc = context.Customers.SingleOrDefault(a => a.Username.Equals(customers.Username));
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

        public Customers checkLogin(string uname, string pass)
        {
            Customers customers = context.Customers.SingleOrDefault(a => a.Username.Equals(uname));
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
            Customers customers = context.Customers.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Customers> findAll()
        {
            return context.Customers.ToList();
        }

        public Customers fineOne(int id)
        {
            Customers customers = context.Customers.SingleOrDefault(a => a.Id.Equals(id));
            if (customers != null)
            {
                return customers;
            }
            else
            {
                return null;
            }
        }

        public void updateCustomer(Customers customers)
        {
            Customers editcustomers = context.Customers.SingleOrDefault(a => a.Id.Equals(customers.Id));
            if (editcustomers != null)
            {
                editcustomers.Username = customers.Username;
                editcustomers.Password = customers.Password;
                editcustomers.Contact = customers.Contact;
                editcustomers.Address = customers.Address;
                editcustomers.Email = customers.Email;
                editcustomers.Image = customers.Image;
                editcustomers.Is_verified = customers.Is_verified;
                editcustomers.Is_active = customers.Is_active;
                
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
