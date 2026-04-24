using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using Microsoft.IdentityModel.Tokens;

namespace EFCore.Repositories;

public class CustomerRepository(BookingDbContext context) : ICustomerRepository
{
    public async Task<Customer?> AddCustomerAsync(Customer customer)
    {
        var existingCustomer = await context.Customers
        .FirstOrDefaultAsync(c => c.Name.ToLower() == customer.Name.ToLower());

        if (existingCustomer == null)
        {
            await context.AddAsync(customer);
            await context.SaveChangesAsync();
            return customer;
        }
        else
        {
            return existingCustomer;
        }
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await context.Customers
            .Include(c => c.Bookings)
            .ToListAsync();
    }
}
