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
        public RealtorContext(DbContextOptions<RealtorContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PaymentPackage> PaymentPackage { get; set; }
        public DbSet<PaymentSubscription> PaymentSubscription { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Region> Regions { get; set; }
        

    }
}
