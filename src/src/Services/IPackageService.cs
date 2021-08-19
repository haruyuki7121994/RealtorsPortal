using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Services
{
    public interface IPackageService
    {
        List<Package> findAll();
        Package fineOne(string name);
        Package findOne(int id);
        void addPackage(Package packages);
        void updatePackage(Package packages);
        void deletepackage(int id);
    }
}
