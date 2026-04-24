using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Services.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> AddCustomerAsync(Customer customer);
    Task<List<Customer>> GetCustomersAsync();
    //Task AddLiftAsync(Lift lift, int id);
}
