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
    #region DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }
    public DbSet<Building> Building { get; set; }
    public DbSet<MonthlyInvoice> MonthlyInvoices { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Bank> Banks { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {
        //  DbContext Assembly
        var assembly = Assembly.GetExecutingAssembly();

        var configurationTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

 
        foreach (var configurationType in configurationTypes)
        {
            dynamic configuration = Activator.CreateInstance(configurationType);
            modelBuilder.ApplyConfiguration(configuration);
        }

        var dbSetTypes = assembly.GetTypes()
       .Where(t => t.IsClass && !t.IsAbstract &&
                   t.IsGenericType && t.GetGenericTypeDefinition() == typeof(DbSet<>))
       .ToList();


        #region Payment Entity Configuration
        modelBuilder.Entity<Payment>().Property(p => p.InvoiceAmount).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.MonthlyInvoice)
            .WithMany(mi => mi.Payments)
            .HasForeignKey(p => p.MonthlyInvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Balance)
            .HasColumnType("decimal(18, 2)");
        #endregion

        // Region for MonthlyInvoice entity configuration
        #region MonthlyInvoice Entity Configuration
        modelBuilder.Entity<MonthlyInvoice>().Property(mi => mi.InvoiceAmount).HasColumnType("decimal(18,2)");
  
        #endregion

        // Region for User entity configuration
        #region User Entity Configuration
        modelBuilder.Entity<User>().Property(u => u.Balance).HasColumnType("decimal(18, 2)");
        #endregion

        // Region for Messages entity configuration
        #region Messages Entity Configuration
        modelBuilder.Entity<Messages>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Messages>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region Payment
        modelBuilder.Entity<Payment>()
    .Property(p => p.NewBalance)
    .HasColumnType("decimal(18, 2)");
        modelBuilder.Entity<Payment>()
    .Property(p => p.NewBalance)
    .HasPrecision(18, 2); 

        #endregion


        base.OnModelCreating(modelBuilder);
    }
}
