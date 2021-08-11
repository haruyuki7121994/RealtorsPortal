using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IAdminService
    {
        Admin checkLogin(string uname, string pass);
        List<Admin> findAll();
        Admin fineOne(string uname);
        void addAdmin(Admin admin);
        void updateAdmin(Admin admin);
        void deleteAdmin(int id);

    }
}
