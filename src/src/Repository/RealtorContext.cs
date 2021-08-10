using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;
using Microsoft.EntityFrameworkCore;

namespace src.Repository
{
    public class RealtorContext:DbContext
    {
        public RealtorContext(DbContextOptions options) : base(options) { }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Configurations> Configurations { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<PaymentPackage> PaymentPackage { get; set; }
        public DbSet<PaymentSubscription> PaymentSubscription { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<Regions> Regions { get; set; }
        

    }
}
