using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task<Customer?> AddCustomerAsync(Customer customer)
    {
        return await customerRepository.AddCustomerAsync(customer);
    }

    //public async Task AddLiftAsync(Lift lift, int id)
    //{
    //    await customerRepository.AddLiftAsync(lift, id);
    //}

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await customerRepository.GetCustomersAsync();
    }
}
