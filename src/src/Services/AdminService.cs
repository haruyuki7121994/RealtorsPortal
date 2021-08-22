﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Helper;
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
                admin.Password = Md5Hash.getMd5Hash(admin.Password);
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
            Admin admin = context.Admins.SingleOrDefault(a => a.Username.Equals(uname) && a.Is_active.Equals(true) && a.Is_verified.Equals(true));
            if (admin == null) return null;
            if(Md5Hash.verifyMd5Hash(pass,admin.Password))
            {
                admin.Password = null;
                return admin;
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
            return context.Admins.OrderByDescending(a => a.Id).ToList();
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
                editAdmin.Password = Md5Hash.getMd5Hash(admin.Password);
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
