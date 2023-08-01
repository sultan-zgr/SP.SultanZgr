using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Patika.Entity.Models;
using SP.Entity;
using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data;

public class SPDbContext : IdentityDbContext<AppUser>
{
    public SPDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<User> Users { get;set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<MonthlyInvoice> MonthlyInvoices { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Bank> Banks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)   //AutoFac library de kullanılabilir.
    {
        //  DbContext'in Assembly
        var assembly = Assembly.GetExecutingAssembly();

        // IEntityTypeConfiguration<T>
        var configurationTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        // config modelbuilder ekle
        foreach (var configurationType in configurationTypes)
        {
            dynamic configuration = Activator.CreateInstance(configurationType);
            modelBuilder.ApplyConfiguration(configuration);
        }

        var dbSetTypes = assembly.GetTypes()
       .Where(t => t.IsClass && !t.IsAbstract &&
                   t.IsGenericType && t.GetGenericTypeDefinition() == typeof(DbSet<>))
       .ToList();




        //modelBuilder.ApplyConfiguration(new AdminConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

