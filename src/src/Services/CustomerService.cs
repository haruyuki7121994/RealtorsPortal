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

        public bool addCustomer(Customer customer)
        {
            var acc = context.Customers.Where(a => a.Username.Equals(customer.Username) || a.Email.Equals(customer.Email)).ToList();
            if (acc.Count() == 0)
            {
                try
                {
                    var cus = new Customer
                    {
                        Name = customer.Name,
                        Contact = customer.Contact,
                        Address = customer.Address,
                        Email = customer.Email,
                        Image = customer.Image,
                        Username = customer.Username,
                        Password = customer.Password,
                        Is_verified = customer.Is_verified,
                        Is_active = customer.Is_active,
                        Has_contact_form = customer.Has_contact_form,
                        Type = customer.Type,
                    };
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
                catch (Exception e)
                {

                    var msg = e.Message;
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public Customer checkLogin(string uname, string pass, bool isActive = false, bool isVerified = false)
        {
            Customer customers = context.Customers.SingleOrDefault(a => a.Username.Equals(uname));

            //check IsActive
            if (isActive)
            {
                customers.Is_active.Equals(true);
            }

            //check IsVerified
            if (isVerified)
            {
                customers.Is_verified.Equals(true);
            }

            //check password
            if (customers != null)
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
            return context.Customers.OrderByDescending(x => x.Id).ToList();
        }



        public Customer fineOne(string username)
        {
            Customer customers = context.Customers.SingleOrDefault(a => a.Username.Equals(username));

            if (customers != null)
            {
                return customers;
            }
            else
            {
                return null;
            }
        }

        public bool updateCustomer(Customer customer)
        {
            Customer editcustomer = context.Customers.SingleOrDefault(a => a.Id.Equals(customer.Id));
            if (editcustomer != null)
            {
                editcustomer.Name = customer.Name;
                editcustomer.Password = customer.Password;
                editcustomer.Contact = customer.Contact;
                editcustomer.Address = customer.Address;
                editcustomer.Image = customer.Image;
                editcustomer.Is_verified = customer.Is_verified;
                editcustomer.Is_active = customer.Is_active;
                editcustomer.Has_contact_form = customer.Has_contact_form;
                
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateStatus(int customerId, bool isActive, bool isVerified)
        {
            Customer editcustomer = context.Customers.SingleOrDefault(a => a.Id.Equals(customerId));
            if (editcustomer != null)
            {
                editcustomer.Is_active = isActive;
                editcustomer.Is_verified = isVerified;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkPaymentSubscription(Customer customer)
        {
            //check enable
            var paymentSubscriptionConfig = context.Configurations.Where(c => c.Obj.Equals("Payment Subscription")).SingleOrDefault();
            bool enable = paymentSubscriptionConfig.Val.Equals("enabled");
            if (!enable)
            {
                return true;
            }

            //check price
            var priceSubscriptionConfig = context.Configurations.Where(c => c.Obj.Equals("Subscription price")).SingleOrDefault();
            var priceSub = priceSubscriptionConfig != null ? Int32.Parse(priceSubscriptionConfig.Val) : 0;
            if (priceSub <= 0)
            {
                return false;
            }

            //check has payment subscription
            var paymentSubscription = context.PaymentSubscription.Where(ps => ps.Customer_id == customer.Id).SingleOrDefault();
            if (paymentSubscription == null)
            {
                return false;
            }

            return true;
        }
    }
}
