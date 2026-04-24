using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories;

public class BookingRepository(BookingDbContext context) : IBookingRepository
{
    public async Task AddBookingAsync(int customerId, int liftId, DateOnly date)
    {
        Booking booking = new Booking()
        {
            CustomerId = customerId,
            LiftId = liftId,
            BookedDate = date
        };

        await context.AddAsync(booking);
        await context.SaveChangesAsync();
    }

    public async Task DeleteBookingAsync(Booking booking)
    {

        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();
    }

    public async Task<List<Booking>> GetLiftsAsync(int id)
    {
        var customer = new Customer()
        {
            Id = id
        };

        return await context.Bookings
            .Where(b => b.CustomerId == customer.Id)
            .Include(b => b.Lift)
            .ToListAsync();
    }
}
