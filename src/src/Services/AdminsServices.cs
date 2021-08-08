using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using src.Repository;

namespace src.Services
{
    public class AdminsServices : IAdminsServices
    {
        private RealtorContext context;
        public AdminsServices(RealtorContext context)
        {
            this.context = context;
        }

        public void addAdmin(Admins admin)
        {
            Admins acc = context.Admins.SingleOrDefault(a => a.Username.Equals(admin.Username));
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

        public Admins checkLogin(string uname, string pass)
        {
            Admins admin = context.Admins.SingleOrDefault(a => a.Username.Equals(uname));
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
            Admins admin = context.Admins.SingleOrDefault(a => a.Id.Equals(id));
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

        public List<Admins> findAll()
        {
            return context.Admins.ToList();
        }

        public Admins fineOne(string uname)
        {
            Admins admin = context.Admins.SingleOrDefault(a => a.Username.Equals(uname));
            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }

        public void updateAdmin(Admins admin)
        {
            Admins editAdmin = context.Admins.SingleOrDefault(a => a.Id.Equals(admin.Id));
            if (editAdmin != null)
            {
                editAdmin.Username = admin.Username;
                context.SaveChanges();
            }
            else
            {
                //do nothing
            }
        }
    }
}
