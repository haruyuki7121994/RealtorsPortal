using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class AdminService : IAdminService
    {
        private RealtorContext context;
        public AdminService(RealtorContext context)
        {
            this.context = context;
        }

        public void addAdmin(Admin admin)
        {
            Admin acc = context.Admins.SingleOrDefault(a => a.Username.Equals(admin.Username));
            if (acc == null)
            {
                context.Admins.Add(admin);
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }

        public Admin checkLogin(string uname, string pass)
        {
            Admin admin = context.Admins.SingleOrDefault(a => a.Username.Equals(uname));
            if (admin == null)
            {
                if (admin.Password.Equals(pass))
                {
                    return admin;
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

        public void deleteAdmin(int id)
        {
            Admin admin = context.Admins.SingleOrDefault(a => a.Id.Equals(id));
            if (admin != null)
            {
                context.Admins.Remove(admin);
                context.SaveChanges();
            }
            else
            {
                //do nothing

            }
        }

        public List<Admin> findAll()
        {
            return context.Admins.ToList();
        }


        public Admin fineOne(int id)
        {
            Admin admin = context.Admins.SingleOrDefault(a => a.Id.Equals(id));
            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }

        public void updateAdmin(Admin admin)
        {
            Admin editAdmin = context.Admins.SingleOrDefault(a => a.Id.Equals(admin.Id));
            if (editAdmin != null)
            {
                
                editAdmin.Email = admin.Email;
                editAdmin.Username = admin.Username;
                editAdmin.Password = admin.Password;
                editAdmin.Is_active = admin.Is_active;
                editAdmin.Is_verified = admin.Is_verified;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
