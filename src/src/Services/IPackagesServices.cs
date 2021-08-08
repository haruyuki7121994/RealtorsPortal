using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPackagesServices
    {
        List<Packages> findAll();
        Packages fineOne(string name);
        void addPackage(Packages packages);
        void updatePackage(Packages packages);
        void deletepackage(int id);
    }
}
