using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Patika.Entity.Models;
using SP.Entity;
using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SP.Data;

public class SPDbContext : DbContext
{
    public SPDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<Building> Building { get; set; }
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


        modelBuilder.Entity<Payment>().Property(p => p.InvoiceAmount).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<MonthlyInvoice>().Property(mi => mi.InvoiceAmount).HasColumnType("decimal(18,2)");


        modelBuilder.Entity<Payment>()
            .HasOne(p => p.MonthlyInvoice)
            .WithMany(mi => mi.Payments)
            .HasForeignKey(p => p.MonthlyInvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Balance)
            .HasColumnType("decimal(18, 2)"); // Example: 18 digits with 2 decimal places

        // Configure the 'Balance' property of the 'User' entity
        modelBuilder.Entity<User>()
            .Property(u => u.Balance)
            .HasColumnType("decimal(18, 2)");
        //modelBuilder.ApplyConfiguration(new AdminConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

