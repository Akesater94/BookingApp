using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace EFCore;

public class BookingDbContext : DbContext
{
    DbSet<Customer> Customers { get; set; } = null!;
    DbSet<Lift> Lifts { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BookingDB; Trusted_Connection = True; MultipleActiveResultSets = true; ");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
    }
}
