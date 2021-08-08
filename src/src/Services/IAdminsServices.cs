using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IAdminsServices
    {
        Admins checkLogin(string uname, string pass);
        List<Admins> findAll();
        Admins fineOne(string uname);
        void addAdmin(Admins admin);
        void updateAdmin(Admins admin);
        void deleteAdmin(int id);

    }
}
