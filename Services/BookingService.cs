using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    public async Task AddBookingAsync(int customerId, int liftId, DateOnly date)
    {
        await bookingRepository.AddBookingAsync(customerId, liftId, date);
    }

    public async Task DeleteBookingAsync(Booking booking)
    {
        await bookingRepository.DeleteBookingAsync(booking);
    }

    public async Task<List<Booking>> GetLiftsAsync(int id)
    {
        return await bookingRepository.GetLiftsAsync(id);
    }
}
